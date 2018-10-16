using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.McCurdy
{
    class McCurdyPort : UmdPort
    {

        [PersistAs("port_name")]
        string comPortName;

        SerialPort serialPort;

        List<Packet> packetFifo = new List<Packet>();
        ManualResetEvent packetFifoNotEmpty = new ManualResetEvent(false);

        Thread packetSchedulerThread;

        public McCurdyPort(string comPortName)
        {
            this.comPortName = comPortName;
            Init();
            packetSchedulerThread = new Thread(packetSchedulerThreadMethod);
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
            lock (packetFifo) {
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
            while(true)
            {
                packetFifoNotEmpty.WaitOne();
                lock(packetFifo)
                {
                    while(packetFifo.Count > 0)
                    {
                        Packet p = packetFifo[0];
                        packetFifo.RemoveAt(0);
                        if(packetIsValid(p))
                        {
                            string str = getStringForPacket(p);
                            serialPort.Write(str);
                        }
                    }
                    packetFifoNotEmpty.Reset();
                }
            }
        }

        private string getStringForPacket(Packet p)
        {
            string content = string.IsNullOrEmpty(p.Data.Text) ? " " : p.Data.Text;
            return string.Format("%{0}D{1}%Z", p.Address, content);

        }

        private bool packetIsValid(Packet p)
        {
            if (p.Data.ValidUntil < DateTime.Now)
                return false;
            if (p.Address <= 0 || p.Address > 255)
                return false;
            return true;
        }

        private struct Packet
        {
            public int Address;
            public Datagram Data;
        }

    }
}
