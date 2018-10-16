using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.TSL31
{
    class TSL31Port : UmdPort
    {

        [PersistAs("port_name")]
        string comPortName;

        SerialPort serialPort;

        List<Packet> packetFifo = new List<Packet>();
        ManualResetEvent packetFifoNotEmpty = new ManualResetEvent(false);

        Thread packetSchedulerThread;

        public TSL31Port(string comPortName)
        {
            this.comPortName = comPortName;
            Init();
            packetSchedulerThread = new Thread(packetSchedulerThreadMethod);
            packetSchedulerThread.Start();
        }

        private const int COMPORT_BAUDRATE = 38400;
        private const Parity COMPORT_PARITY = Parity.Even;
        private const int COMPORT_DATABITS = 8;
        private const StopBits COMPORT_STOPBITS = StopBits.One;
        
        public override void Init()
        {
            serialPort = new SerialPort(comPortName, COMPORT_BAUDRATE, COMPORT_PARITY, COMPORT_DATABITS, COMPORT_STOPBITS);
            serialPort.Open();
        }

        public override void DeInit()
        {
            serialPort?.Close();
            serialPort = null;
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
                            byte[] b = getByteStreamForPacket(p);
                            serialPort.Write(b, 0, 18);
                        }
                    }
                    packetFifoNotEmpty.Reset();
                }
            }
        }

        private byte[] getByteStreamForPacket(Packet p)
        {

            byte[] data = new byte[18];

            data[0] = (byte)(p.Address + 0x80);

            data[1] = 0x00;

            bool[] tallies = p.Data.Tallies;
            if (tallies.Length >= 1 && tallies[0])
                data[1] &= 0x01;
            if (tallies.Length >= 2 && tallies[1])
                data[1] &= 0x02;

            string text = p.Data.Text;
            int len = text.Length;
            if (len > 16)
                len = 16;
            for (int i = 0; i < len; i++)
                data[i + 2] = (byte)(((text[i] >= 0x20) && (text[i] <= 0x7E)) ? text[i] : '?');
            for (int i = len + 2; i < 18; i++)
                data[i] = (byte)' ';

            return data;

        }

        private bool packetIsValid(Packet p)
        {
            if (p.Data.ValidUntil < DateTime.Now)
                return false;
            if (p.Address < 0 || p.Address > 126)
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
