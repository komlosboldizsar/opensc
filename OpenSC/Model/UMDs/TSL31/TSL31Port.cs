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
    [TypeLabel("TSL 3.1 port")]
    [TypeCode("tsl31")]
    class TSL31Port : UmdSerialPort
    {

        public TSL31Port() :
             base()
        { }

        public TSL31Port(string comPortName) :
            base(comPortName)
        { }

        protected override byte[] getBytesForPacket(Packet packet)
        {

            byte[] data = new byte[18];

            data[0] = (byte)(packet.Address + 0x80);

            data[1] = 0x00;

            bool[] tallies = packet.Data.Tallies;
            if (tallies.Length >= 1 && tallies[0])
                data[1] &= 0x01;
            if (tallies.Length >= 2 && tallies[1])
                data[1] &= 0x02;

            string text = packet.Data.Text;
            int len = text.Length;
            if (len > 16)
                len = 16;
            for (int i = 0; i < len; i++)
                data[i + 2] = (byte)(((text[i] >= 0x20) && (text[i] <= 0x7E)) ? text[i] : '?');
            for (int i = len + 2; i < 18; i++)
                data[i] = (byte)' ';

            return data;

        }

        protected override bool packetIsValid(Packet packet)
        {
            if (packet.Data.ValidUntil < DateTime.Now)
                return false;
            if (packet.Address < 0 || packet.Address > 126)
                return false;
            return true;
        }

    }
}
