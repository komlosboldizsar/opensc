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

        [PersistAs("port_name")]
        protected string comPortName;

        private SerialPort serialPort;

        private List<Packet> packetFifo = new List<Packet>();
        private ManualResetEvent packetFifoNotEmpty = new ManualResetEvent(false);

        private Thread packetSchedulerThread;

        public UmdSerialPort(string comPortName)
        {
            this.comPortName = comPortName;
            Init();
            createAndStartPacketSchedulerThread();
        }

        public UmdSerialPort()
        {
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
            try
            {
                serialPort = new SerialPort(comPortName, COMPORT_BAUDRATE, COMPORT_PARITY, COMPORT_DATABITS, COMPORT_STOPBITS);
                serialPort.Open();
            }
            catch { }
        }

        public override void DeInit()
        {
            try
            {
                serialPort?.Close();
                serialPort = null;
            }
            catch { }
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
