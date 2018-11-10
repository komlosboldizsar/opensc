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

        public override void Restored()
        {
            createAndStartPacketSchedulerThread();
            Init();
        }

        public override void Removed()
        {

            base.Removed();

            DeInit();
            packetSchedulerThread.Abort();
            packetSchedulerThread = null;

            IdChanged = null;
            NameChanged = null;
            ComPortNameChanged = null;
            InitializedChanged = null;
            BaudRateChanged = null;
            ParityChanged = null;
            DataBitsChanged = null;
            StopBitsChanged = null;
            ReceivedDataBytes = null;
            ReceivedDataAsciiString = null;

        }

        #region Property: ID
        public delegate void IdChangedDelegate(SerialPort port, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        private int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                int oldValue = id;
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ID));
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!SerialPortDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(SerialPort port, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: ComPortName
        public delegate void ComPortNameChangedDelegate(SerialPort port, string oldComPortName, string newComPortName);
        public event ComPortNameChangedDelegate ComPortNameChanged;

        [PersistAs("port_name")]
        protected string comPortName;

        public string ComPortName
        {
            get { return comPortName; }
            set
            {
                if (value == comPortName)
                    return;
                if ((serialPort != null) && (serialPort.IsOpen))
                    DeInit();
                string oldComPortName = comPortName;
                comPortName = value;
                ComPortNameChanged?.Invoke(this, oldComPortName, value);
                RaisePropertyChanged(nameof(ComPortName));
                Init();
            }
        }
        #endregion

        #region Property: Initialized
        public delegate void InitializedChangedDelegate(SerialPort port, bool oldState, bool newState);
        public event InitializedChangedDelegate InitializedChanged;
        
        protected bool initialized;

        public bool Initialized
        {
            get { return initialized; }
            set
            {
                if (value == initialized)
                    return;
                bool oldState = initialized;
                initialized = value;
                InitializedChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(Initialized));
            }
        }
        #endregion

        // >>>> ComPort properties

        private const int DEFAULT_BAUDRATE = 9600;
        private const Parity DEFAULT_PARITY = Parity.None;
        private const int DEFAULT_DATABITS = 8;
        private const StopBits DEFAULT_STOPBITS = StopBits.One;

        #region Property: BaudRate
        public delegate void BaudRateChangedDelegate(SerialPort port, int oldBaudRate, int newBaudRate);
        public event BaudRateChangedDelegate BaudRateChanged;

        [PersistAs("baudrate")]
        private int baudRate = DEFAULT_BAUDRATE;

        public int BaudRate
        {
            get => baudRate;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();
                if (value == baudRate)
                    return;
                int oldBaudRate = baudRate;
                baudRate = value;
                BaudRateChanged?.Invoke(this, oldBaudRate, value);
                RaisePropertyChanged(nameof(BaudRate));
                if (Initialized && !Updating)
                {
                    DeInit();
                    Init();
                }
            }
        }
        #endregion

        #region Property: Parity
        public delegate void ParityChangedDelegate(SerialPort port, Parity oldParity, Parity newParity);
        public event ParityChangedDelegate ParityChanged;

        [PersistAs("parity")]
        private Parity parity = DEFAULT_PARITY;

        public Parity Parity
        {
            get => parity;
            set
            {
                if (value == parity)
                    return;
                Parity oldParity = parity;
                parity = value;
                ParityChanged?.Invoke(this, oldParity, value);
                RaisePropertyChanged(nameof(Parity));
                if (Initialized && !Updating)
                {
                    DeInit();
                    Init();
                }
            }
        }
        #endregion

        #region Property: DataBits
        public delegate void DataBitsChangedDelegate(SerialPort port, int oldDataBits, int newDataBits);
        public event DataBitsChangedDelegate DataBitsChanged;

        [PersistAs("databits")]
        private int dataBits = DEFAULT_DATABITS;

        public int DataBits
        {
            get => dataBits;
            set
            {
                if ((value < 5) || (value > 8))
                    throw new ArgumentOutOfRangeException();
                if (value == dataBits)
                    return;
                int oldDataBits = dataBits;
                dataBits = value;
                DataBitsChanged?.Invoke(this, oldDataBits, value);
                RaisePropertyChanged(nameof(DataBits));
                if (Initialized && !Updating)
                {
                    DeInit();
                    Init();
                }
            }
        }
        #endregion

        #region Property: StopBits
        public delegate void StopBitsChangedDelegate(SerialPort port, StopBits oldStopBits, StopBits newStopBits);
        public event StopBitsChangedDelegate StopBitsChanged;

        [PersistAs("stopbits")]
        private StopBits stopBits = DEFAULT_STOPBITS;

        public StopBits StopBits
        {
            get => stopBits;
            set
            {
                if (value == stopBits)
                    return;
                StopBits oldStopBits = stopBits;
                stopBits = value;
                StopBitsChanged?.Invoke(this, oldStopBits, value);
                RaisePropertyChanged(nameof(StopBits));
                if (Initialized && !Updating)
                {
                    DeInit();
                    Init();
                }
            }
        }
        #endregion

        // <<<< ComPort properties

        #region Scheduler and sender thread
        private List<Packet> packetFifo = new List<Packet>();
        private ManualResetEvent packetFifoNotEmpty = new ManualResetEvent(false);
        private Thread packetSchedulerThread;

        private void createAndStartPacketSchedulerThread()
        {
            packetSchedulerThread = new Thread(packetSchedulerThreadMethod)
            {
                IsBackground = true
            };
            packetSchedulerThread.Start();
        }

        private void packetSchedulerThreadMethod()
        {
            while (true)
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
            
        }
        #endregion

    }

}
