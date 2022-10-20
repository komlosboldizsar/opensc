using System;
using System.Net.Sockets;

namespace OpenSC.Library.BmdVideohub
{

    internal static class SocketHelpers
    {

        // https://stackoverflow.com/a/722265
        public static bool IsConnected(this Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }

    }

}
