using System.Net.Sockets;

namespace BMD.Videohub
{

    public static class SocketHelpers
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
        }

    }

}
