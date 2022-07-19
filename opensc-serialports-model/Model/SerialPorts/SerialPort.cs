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
            packetQueue = new(sendPacket, invalidPacket);
        }

        public override void RestoredOwnFields()
        {
            Init();
        }

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
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = SerialPortDatabase.Instance;
        #endregion

        #region Property: ComPortName
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_comPortName_beforeChange))]
        [AutoProperty.AfterChange(nameof(Init))]
        [PersistAs("port_name")]
        protected string comPortName;

        private void _comPortName_beforeChange(string oldValue, string newValue, BeforeChangePropertyArgs args)
        {
            if (serialPort?.IsOpen == true)
                DeInit();
        }
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

        private void afterPortPropertyChanged()
        {
            if (Initialized && !Updating)
            {
                DeInit();
                Init();
            }
        }

        #region Property: BaudRate
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(afterPortPropertyChanged))]
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
        [AutoProperty.AfterChange(nameof(afterPortPropertyChanged))]
        [PersistAs("parity")]
        private Parity parity = DEFAULT_PARITY;
        #endregion

        #region Property: DataBits
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(afterPortPropertyChanged))]
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
        [AutoProperty.AfterChange(nameof(afterPortPropertyChanged))]
        [PersistAs("stopbits")]
        private StopBits stopBits = DEFAULT_STOPBITS;
        #endregion

        // <<<< ComPort properties

        private System.IO.Ports.SerialPort serialPort;

        #region Init and DeInit
        public void Init()
        {
            if (string.IsNullOrEmpty(comPortName))
                return;
            try
            {
                serialPort = new System.IO.Ports.SerialPort(comPortName, baudRate, parity, dataBits, stopBits);
                serialPort.Open();
                serialPort.DataReceived += dataReceivedHandler;
                packetQueue.Start();
                Initialized = true;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Couldn't initialize port (ID: {0}) with settings [baudrate: {1}, parity: {2}, databits: {3}, stopbits: {4}]. Exception message: [{5}].",
                    ID, baudRate, parity, dataBits, stopBits, ex.Message);
                LogDispatcher.E(LOG_TAG, errorMessage);
            }
        }

        public void DeInit()
        {
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
            }
            catch (Exception ex)
            {
                LogDispatcher.E(LOG_TAG, $"Couldn't deinitialize port (ID: {ID}). Exception message: [{ex.Message}].");
            }
        }

        public void ReInit()
        {
            DeInit();
            Init();
        }
        #endregion

        #region Data sending
        private readonly TaskQueue<Packet> packetQueue;
        private void sendPacket(Packet packet) => serialPort.Write(packet.Data, 0, packet.Data.Length);
        private void invalidPacket(Packet packet) => LogDispatcher.W(LOG_TAG, $"Dropped an invalid packet on port [{this}]");

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
        #endregion

    }

}
