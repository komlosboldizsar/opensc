using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Tsl50
{

    public class Tsl50Screen : ModelBase
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
        public event PropertyChangedTwoValuesDelegate<Tsl50Screen, string> IpAddressChanged;

        [PersistAs("ipaddress")]
        private string ipAddress;

        public string IpAddress
        {
            get => ipAddress;
            set => this.setProperty(ref ipAddress, value, IpAddressChanged, null, (ov, nv) => updateEndpoint());
        }
        #endregion

        #region Property: Port
        public event PropertyChangedTwoValuesDelegate<Tsl50Screen, int> PortChanged;

        [PersistAs("port")]
        private int port = 1024;

        public int Port
        {
            get => port;
            set => this.setProperty(ref port, value, PortChanged, null, (ov, nv) => updateEndpoint(), ValidatePort);
        }

        public void ValidatePort(int port)
        {
            if ((port < 0) || (port > 65534))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: Index
        public event PropertyChangedTwoValuesDelegate<Tsl50Screen, int> IndexChanged;

        [PersistAs("index")]
        private int index = 1;

        public int Index
        {
            get => index;
            set => this.setProperty(ref index, value, IndexChanged, validator: ValidateIndex);
        }

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
            byte[] totalBytes = new byte[totalByteCount];
            totalBytes[0] = (byte)((totalByteCount >> 8) & 0xFF); // PBC
            totalBytes[1] = (byte)(totalByteCount & 0xFF); // PBC
            totalBytes[2] = 0; // VER
            totalBytes[3] = 0; // FLAGS (ASCII, DMSG)
            totalBytes[4] = (byte)((index >> 8) & 0xFF);
            totalBytes[5] = (byte)(index & 0xFF);
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
