using OpenSC.Library.TaskSchedulerQueue;
using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.SerialPorts
{

    public partial class SerialPort : ModelBase
    {

        #region Constants
        private const string LOG_TAG = "SerialPort";
        #endregion

        #region Persistence, instantiation
        public SerialPort()
        {
            createPacketQueue();
        }

        public override void RestoredOwnFields() => Init();

        public override void Removed()
        {
            base.Removed();
            DeInit();
            ComPortNameChanged = null;
            InitializedChanged = null;
            BaudRateChanged = null;
            ParityChanged = null;
            DataBitsChanged = null;
            StopBitsChanged = null;
            ReceivedDataBytes = null;
            ReceivedDataAsciiString = null;
        }

        protected override void afterUpdate() => ReInit();
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = SerialPortDatabase.Instance;
        #endregion

        #region Property: ComPortName
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(ReInit))]
        [PersistAs("port_name")]
        protected string comPortName;
        #endregion

        #region Property: Initialized
        [AutoProperty]
        protected bool initialized;
        #endregion

        // >>>> ComPort properties

        #region ComPort contants
        private const int DEFAULT_BAUDRATE = 9600;
        private const Parity DEFAULT_PARITY = Parity.None;
        private const int DEFAULT_DATABITS = 8;
        private const StopBits DEFAULT_STOPBITS = StopBits.One;
        #endregion

        #region Property: BaudRate
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(ReInit))]
        [AutoProperty.Validator(nameof(ValidateBaudRate))]
        [PersistAs("baudrate")]
        private int baudRate = DEFAULT_BAUDRATE;

        public void ValidateBaudRate(int baudRate)
        {
            if (baudRate <= 0)
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: Parity
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(ReInit))]
        [PersistAs("parity")]
        private Parity parity = DEFAULT_PARITY;
        #endregion

        #region Property: DataBits
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(ReInit))]
        [AutoProperty.Validator(nameof(ValidateDataBits))]
        [PersistAs("databits")]
        private int dataBits = DEFAULT_DATABITS;

        public void ValidateDataBits(int dataBits)
        {
            if ((dataBits < 5) || (dataBits > 8))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: StopBits
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(ReInit))]
        [PersistAs("stopbits")]
        private StopBits stopBits = DEFAULT_STOPBITS;
        #endregion

        // <<<< ComPort properties

        private System.IO.Ports.SerialPort serialPort;

        #region Init and DeInit
        public void Init()
        {
            LogDispatcher.I(LOG_TAG, $"Initializing port [{this}]...");
            if (Initialized)
            {
                LogDispatcher.W(LOG_TAG, $"Couldn't initialize port ({this}). It was already initialized.");
                return;
            }
            if (string.IsNullOrEmpty(comPortName))
                return;
            try
            {
                serialPort = new System.IO.Ports.SerialPort(comPortName, baudRate, parity, dataBits, stopBits);
                serialPort.Open();
                serialPort.DataReceived += dataReceivedHandler;
                packetQueue.Start();
                Initialized = true;
                LogDispatcher.I(LOG_TAG, $"Initializing port [{this}] successful.");
            }
            catch (Exception ex)
            {
                LogDispatcher.W(LOG_TAG, $"Couldn't initialize port [{this}] with settings [name: {comPortName}, baudrate: {baudRate}, parity: {parity}, databits: {dataBits}, stopbits: {stopBits}]. Exception message: [{ex.Message}].");
            }
        }

        public void DeInit()
        {
            LogDispatcher.I(LOG_TAG, $"Deinitializing port [{this}]...");
            if (!Initialized)
            {
                LogDispatcher.W(LOG_TAG, $"Couldn't deinitialize port [{this}]. It was not initialized.");
                return;
            }
            try
            {
                packetQueue.Stop();
                if (serialPort != null)
                {
                    serialPort.DataReceived -= dataReceivedHandler;
                    serialPort?.Close();
                    serialPort?.Dispose(); 
                }
                serialPort = null;
                Initialized = false;
                LogDispatcher.I(LOG_TAG, $"Deinitializing port [{this}] successful.");
            }
            catch (Exception ex)
            {
                LogDispatcher.E(LOG_TAG, $"Couldn't deinitialize port (ID: {ID}). Exception message: [{ex.Message}].");
            }
        }

        public void ReInit()
        {
            if (Initialized && !Updating)
            {
                DeInit();
                Init();
            }
        }
        #endregion

        #region Data sending
        private /*readonly*/ TaskQueue<Packet> packetQueue;
        private void createPacketQueue() => packetQueue = new(sendPacket, invalidPacket);

        private void sendPacket(Packet packet)
        {
            serialPort.Write(packet.Data, 0, packet.Data.Length);
            SentPacket?.Invoke(this, packet);
        }

        private void invalidPacket(Packet packet)
        {
            LogDispatcher.W(LOG_TAG, $"Dropped an invalid packet on port [{this}].");
            DroppedInvalidPacket?.Invoke(this, packet);
        }

        public delegate void PacketEventDelegate(SerialPort port, Packet packet);
        public event PacketEventDelegate SentPacket;
        public event PacketEventDelegate DroppedInvalidPacket;

        public void SendData(byte[] data, DateTime validUntil) => packetQueue?.Enqueue(new(data, validUntil));

        public class Packet : ImmediatelyReadyQueuedTask
        {
            public byte[] Data;
            public DateTime ValidUntil;
            public Packet(byte[] data, DateTime validUntil)
            {
                Data = data;
                ValidUntil = validUntil;
            }
            protected override bool IsValid => (ValidUntil >= DateTime.Now);
        }
        #endregion

        #region Data receiving
        public delegate void ReceivedDataBytesDelegate(SerialPort port, byte[] data);
        public event ReceivedDataBytesDelegate ReceivedDataBytes;

        public delegate void ReceivedDataAsciiStringDelegate(SerialPort port, string asciiString);
        public event ReceivedDataAsciiStringDelegate ReceivedDataAsciiString;

        public delegate void ReceivedDataAsciiLineDelegate(SerialPort port, string asciiLine);
        public event ReceivedDataAsciiLineDelegate ReceivedDataAsciiLine;

        private string asciiLineBuffer = string.Empty;

        private void dataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null)
                return;
            int bytesToRead = serialPort.BytesToRead;
            byte[] receivedBytes = new byte[bytesToRead];
            for (int i = 0; i < bytesToRead; i++)
                receivedBytes[i] = (byte)serialPort.ReadByte();
            bytesReceivedHandler(receivedBytes);
        }

        private void bytesReceivedHandler(byte[] receivedBytes)
        {

            ReceivedDataBytes?.Invoke(this, receivedBytes);

            string receivedAsciiString = Encoding.ASCII.GetString(receivedBytes);
            ReceivedDataAsciiString?.Invoke(this, receivedAsciiString);

            asciiLineBuffer += receivedAsciiString;
            char lastAsciiChar = asciiLineBuffer[asciiLineBuffer.Length - 1];
            bool noHalfLine = ((lastAsciiChar == '\r') || (lastAsciiChar == '\n'));
            string[] asciiLinesSplit = asciiLineBuffer.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> asciiLines = asciiLinesSplit.ToList();
            string lastHalfLine = "";
            if (!noHalfLine)
            {
                lastHalfLine = asciiLines[asciiLines.Count - 1];
                asciiLines.RemoveAt(asciiLines.Count - 1);
            }
            foreach (string asciiLine in asciiLines)
                ReceivedDataAsciiLine?.Invoke(this, asciiLine);
            asciiLineBuffer = lastHalfLine;

        }

        public void SimulateReceiveBytes(byte[] bytes) => bytesReceivedHandler(bytes);
        #endregion

    }

}
