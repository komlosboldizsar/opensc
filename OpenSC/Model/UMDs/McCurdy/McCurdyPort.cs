using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.McCurdy
{
    class McCurdyPort : UmdSerialPort
    {
        public McCurdyPort():
            base()
        { }

        public McCurdyPort(string comPortName):
            base(comPortName)
        { }

        protected override byte[] getBytesForPacket(Packet packet)
        {
            string content = string.IsNullOrEmpty(packet.Data.Text) ? " " : packet.Data.Text;

            int startPercentSign = -1;
            for(int i = 0; i < content.Length; i++)
            {
                if ((startPercentSign == -1) && (content[i] == '%'))
                    startPercentSign = i;
                if((startPercentSign != -1) && (content[i] != '%'))
                {
                    content = content.Insert(startPercentSign, "%");
                    startPercentSign = -1;
                }
            }
            if(startPercentSign != -1)
                content = content.Insert(startPercentSign, "%");

            if (content.Last() == '%')
                content += " ";

            string umdMessage = string.Format("%{0}D{1}%Z", packet.Address, content);
            Debug.WriteLine(umdMessage);
            // https://stackoverflow.com/a/16072742
            return Encoding.ASCII.GetBytes(umdMessage);
        }

        protected override bool packetIsValid(Packet packet)
        {
            if (packet.Data.ValidUntil < DateTime.Now)
                return false;
            if (packet.Address <= 0 || packet.Address > 255)
                return false;
            return true;
        }

    }
}
