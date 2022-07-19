using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Tsl50
{

    public partial class Tsl50Screen : ModelBase
    {

        #region Instantiation, restoration
        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            updateEndpoint();
        }

        public override void Removed()
        {
            base.Removed();
            try
            {
                socket.Dispose();
            }
            catch { }
            socket = null;
            IpAddressChanged = null;
            PortChanged = null;
            IndexChanged = null;
        }
        #endregion

        #region OwnerDatabase
        public override IDatabaseBase OwnerDatabase => Tsl50ScreenDatabase.Instance;
        #endregion

        #region Property: IpAddress
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateEndpoint))]
        [PersistAs("ipaddress")]
        private string ipAddress;
        #endregion

        #region Property: Port
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateEndpoint))]
        [AutoProperty.Validator(nameof(ValidatePort))]
        [PersistAs("port")]
        private int port = 1024;

        public void ValidatePort(int port)
        {
            if ((port < 0) || (port > 65534))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: Index
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateIndex))]
        [PersistAs("index")]
        private int index = 1;

        public void ValidateIndex(int index)
        {
            if ((index < 0) || (index > 65534))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Sending data to screen
        internal void SendDisplayData(byte[] displayData) => sendUdpPacket(getBytesForPacket(displayData));

        private byte[] getBytesForPacket(byte[] displayData)
        {
            int totalByteCount = 6 + displayData.Length;
            byte[] totalBytes = new byte[totalByteCount]; // LITTLE ENDIAN!
            totalBytes[0] = (byte)(totalByteCount & 0xFF); // PBC LSB
            totalBytes[1] = (byte)((totalByteCount >> 8) & 0xFF); // PBC MSB
            totalBytes[2] = 0; // VER
            totalBytes[3] = 0; // FLAGS (ASCII, DMSG)
            totalBytes[4] = (byte)(index & 0xFF); // SCREEN INDEX LSB
            totalBytes[5] = (byte)((index >> 8) & 0xFF); // SCREEN INDEX MSB
            displayData.CopyTo(totalBytes, 6);
            return totalBytes;
        }
        #endregion

        #region Socket, IP endpoint, UDP datagram sending
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        private void sendUdpPacket(byte[] data)
        {
            if (ipEndpoint == null)
                return;
            socket?.SendTo(data, ipEndpoint);
        }

        private IPEndPoint ipEndpoint;

        private void updateEndpoint()
        {
            if (!IPAddress.TryParse(ipAddress, out IPAddress typedIpAddress))
            {
                ipEndpoint = null;
                return;
            }
            ipEndpoint = new IPEndPoint(typedIpAddress, port);
        }
        #endregion

    }

}
