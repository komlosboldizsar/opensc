using OpenSC.Logger;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    abstract class UmdSerialPort: UmdPort
    {

        private const string LOG_TAG = "Umd/Port/SerialPort";

        [PersistAs("port_name")]
        protected string comPortName;

        public string ComPortName
        {
            get { return comPortName; }
            set
            {
                if ((serialPort != null) && (serialPort.IsOpen))
                    DeInit();
                comPortName = value;
                Init();
            }
        }

        private SerialPort serialPort;

        private List<Packet> packetFifo = new List<Packet>();
        private ManualResetEvent packetFifoNotEmpty = new ManualResetEvent(false);

        private Thread packetSchedulerThread;

        public UmdSerialPort()
        {
            createAndStartPacketSchedulerThread();
        }

        public UmdSerialPort(string comPortName)
        {
            this.comPortName = comPortName;
            Init();
            createAndStartPacketSchedulerThread();
        }

        private void createAndStartPacketSchedulerThread()
        {
            packetSchedulerThread = new Thread(packetSchedulerThreadMethod) {
                IsBackground = true
            };
            packetSchedulerThread.Start();
        }

        private const int COMPORT_BAUDRATE = 9600;
        private const Parity COMPORT_PARITY = Parity.None;
        private const int COMPORT_DATABITS = 8;
        private const StopBits COMPORT_STOPBITS = StopBits.One;

        public override void Init()
        {

            if (string.IsNullOrEmpty(comPortName))
                return;

            try
            {
                serialPort = new SerialPort(comPortName, COMPORT_BAUDRATE, COMPORT_PARITY, COMPORT_DATABITS, COMPORT_STOPBITS);
                serialPort.Open();
                Initialized = true;
            }
            catch(Exception ex)
            {
                string errorMessage = string.Format("Couldn't initialize port (ID: {0}) with settings [baudrate: {1}, parity: {2}, databits: {3}, stopbits: {4}]. Exception message: [{5}].",
                    ID,
                    COMPORT_BAUDRATE,
                    COMPORT_PARITY,
                    COMPORT_DATABITS,
                    COMPORT_STOPBITS,
                    ex.Message);
                LogDispatcher.E(LOG_TAG, errorMessage);
            }

        }

        public override void DeInit()
        {
            try
            {
                serialPort?.Close();
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

        public override void Restored()
        {
            Init();
        }

        public void SendData(int port, Datagram datagram)
        {
            if ((serialPort == null) || !serialPort.IsOpen)
                return;
            lock (packetFifo)
            {
                Packet p = new Packet()
                {
                    Data = datagram,
                    Address = port
                };
                packetFifo.Add(p);
                packetFifoNotEmpty.Set();
            }
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
                            byte[] d = getBytesForPacket(p);
                            serialPort.Write(d, 0, d.Length);
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

        protected abstract bool packetIsValid(Packet packet);

        protected abstract byte[] getBytesForPacket(Packet packet);

        protected class Packet
        {
            public int Address;
            public Datagram Data;
        }

    }
}
