using OpenSC.Logger;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.SerialPorts
{

    public class SerialPort : ModelBase
    {

        private const string LOG_TAG = "SerialPort";

        public SerialPort()
        {
            createAndStartPacketSchedulerThread();
        }

        public override void RestoredOwnFields()
        {
            createAndStartPacketSchedulerThread();
            Init();
        }

        public override void Removed()
        {
            base.Removed();
            DeInit();
            packetSchedulerThreadWorking = false;
            packetSchedulerThread = null;
            ComPortNameChanged = null;
            InitializedChanged = null;
            BaudRateChanged = null;
            ParityChanged = null;
            DataBitsChanged = null;
            StopBitsChanged = null;
            ReceivedDataBytes = null;
            ReceivedDataAsciiString = null;
        }

        #region ID validation
        protected override void validateIdForDatabase(int id)
        {
            if (!SerialPortDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = SerialPortDatabase.Instance;
        #endregion

        #region Property: ComPortName
        public event PropertyChangedTwoValuesDelegate<SerialPort, string> ComPortNameChanged;

        [PersistAs("port_name")]
        protected string comPortName;

        public string ComPortName
        {
            get => comPortName;
            set
            {
                BeforeChangePropertyDelegate<string> beforeChangeDelegate = (ov, nv) => {
                    if (serialPort?.IsOpen == true)
                        DeInit();
                };
                if (!setProperty(this, ref comPortName, value, ComPortNameChanged, beforeChangeDelegate))
                    return;
                Init();
            }
        }
        #endregion

        #region Property: Initialized
        public event PropertyChangedTwoValuesDelegate<SerialPort, bool> InitializedChanged;
        
        protected bool initialized;

        public bool Initialized
        {
            get => initialized;
            set => setProperty(this, ref initialized, value, InitializedChanged);
        }
        #endregion

        // >>>> ComPort properties

        private const int DEFAULT_BAUDRATE = 9600;
        private const Parity DEFAULT_PARITY = Parity.None;
        private const int DEFAULT_DATABITS = 8;
        private const StopBits DEFAULT_STOPBITS = StopBits.One;

        private void afterPortPropertyChanged()
        {
            if (Initialized && !Updating)
            {
                DeInit();
                Init();
            }
        }

        #region Property: BaudRate
        public event PropertyChangedTwoValuesDelegate<SerialPort, int> BaudRateChanged;

        [PersistAs("baudrate")]
        private int baudRate = DEFAULT_BAUDRATE;

        public int BaudRate
        {
            get => baudRate;
            set
            {
                if (!setProperty(this, ref baudRate, value, BaudRateChanged, validator: ValidateBaudRate))
                    return;
                afterPortPropertyChanged();
            }
        }
        
        public void ValidateBaudRate(int baudRate)
        {
            if (baudRate <= 0)
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: Parity
        public event PropertyChangedTwoValuesDelegate<SerialPort, Parity> ParityChanged;

        [PersistAs("parity")]
        private Parity parity = DEFAULT_PARITY;

        public Parity Parity
        {
            get => parity;
            set
            {
                if (!setProperty(this, ref parity, value, ParityChanged))
                    return;
                afterPortPropertyChanged();
            }
        }
        #endregion

        #region Property: DataBits
        public event PropertyChangedTwoValuesDelegate<SerialPort, int> DataBitsChanged;

        [PersistAs("databits")]
        private int dataBits = DEFAULT_DATABITS;

        public int DataBits
        {
            get => dataBits;
            set
            {
                if (!setProperty(this, ref dataBits, value, DataBitsChanged, validator: ValidateDataBits))
                    return;
                afterPortPropertyChanged();
            }
        }

        public void ValidateDataBits(int dataBits)
        {
            if ((dataBits < 5) || (dataBits > 8))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: StopBits
        public event PropertyChangedTwoValuesDelegate<SerialPort, StopBits> StopBitsChanged;

        [PersistAs("stopbits")]
        private StopBits stopBits = DEFAULT_STOPBITS;

        public StopBits StopBits
        {
            get => stopBits;
            set
            {
                if (!setProperty(this, ref stopBits, value, StopBitsChanged))
                    return;
                afterPortPropertyChanged();
            }
        }
        #endregion

        // <<<< ComPort properties

        #region Scheduler and sender thread
        private List<Packet> packetFifo = new List<Packet>();
        private ManualResetEvent packetFifoNotEmpty = new ManualResetEvent(false);
        private Thread packetSchedulerThread;
        private bool packetSchedulerThreadWorking = false;

        private void createAndStartPacketSchedulerThread()
        {
            packetSchedulerThread = new Thread(packetSchedulerThreadMethod)
            {
                IsBackground = true
            };
            packetSchedulerThreadWorking = true;
            packetSchedulerThread.Start();
        }

        private void packetSchedulerThreadMethod()
        {
            while (packetSchedulerThreadWorking)
            {
                packetFifoNotEmpty.WaitOne();
                lock (packetFifo)
                {
                    while (packetFifo.Count > 0)
                    {
                        Packet p = packetFifo[0];
                        packetFifo.RemoveAt(0);
                        if (packetIsValid(p))
                        {
                            serialPort.Write(p.Data, 0, p.Data.Length);
                        }
                        else
                        {
                            string errorMessage = string.Format("Dropped an invalid packet on port (ID: {0}).", ID);
                            LogDispatcher.W(LOG_TAG, errorMessage);
                        }
                    }
                    packetFifoNotEmpty.Reset();
                }
            }
        }
        #endregion

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
                Initialized = true;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Couldn't initialize port (ID: {0}) with settings [baudrate: {1}, parity: {2}, databits: {3}, stopbits: {4}]. Exception message: [{5}].",
                    ID,
                    baudRate,
                    parity,
                    dataBits,
                    stopBits,
                    ex.Message);
                LogDispatcher.E(LOG_TAG, errorMessage);
            }

        }

        public void DeInit()
        {
            try
            {
                if (serialPort != null) {
                    serialPort.DataReceived -= dataReceivedHandler;
                    serialPort?.Close();
                    serialPort?.Dispose(); 
                }
                serialPort = null;
                Initialized = false;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Couldn't deinitialize port (ID: {0}). Exception message: [{1}].",
                        ID,
                        ex.Message);
                LogDispatcher.E(LOG_TAG, errorMessage);
            }
        }
        #endregion

        protected override void afterUpdate()
        {
            base.afterUpdate();
            if (Initialized)
            {
                DeInit();
                Init();
            }
        }

        #region Data sending
        public void SendData(byte[] data, DateTime validUntil)
        {
            SendData(new Packet() {
                Data = data,
                ValidUntil = validUntil
            });
        }

        public void SendData(Packet packet)
        {
            if ((serialPort == null) || !serialPort.IsOpen)
                return;
            lock (packetFifo)
            {
                packetFifo.Add(packet);
                packetFifoNotEmpty.Set();
            }
        }

        protected bool packetIsValid(Packet packet)
        {
            return (packet.ValidUntil >= DateTime.Now);
        }

        public class Packet
        {
            public DateTime ValidUntil;
            public byte[] Data;
        }
        #endregion

        #region Data receiving
        public delegate void ReceivedDataBytesDelegate(SerialPort port, byte[] data);
        public event ReceivedDataBytesDelegate ReceivedDataBytes;

        public delegate void ReceivedDataAsciiStringDelegate(SerialPort port, string asciiString);
        public event ReceivedDataAsciiStringDelegate ReceivedDataAsciiString;

        public delegate void ReceivedDataAsciiLineDelegate(SerialPort port, string asciiLine);
        public event ReceivedDataAsciiLineDelegate ReceivedDataAsciiLine;

        private string asciiLineBuffer = "";

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
