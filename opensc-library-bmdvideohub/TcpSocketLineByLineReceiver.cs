using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BMD.Videohub
{

    internal class TcpSocketLineByLineReceiver : IDisposable
    {

        private Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

        private string ipAddress;
        private int port;

        public TcpSocketLineByLineReceiver(string ipAddress, int port)
        {
            if ((port < 1) || (port >= 65535))
                throw new ArgumentOutOfRangeException();
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public void Dispose()
        {
            try
            {
                if(socket.Connected)
                    socket.Disconnect(false);
                ConnectedStateChanged?.Invoke(socket.Connected);
                socket.Dispose();
            }
            catch (ObjectDisposedException) { }
        }

        #region Connection state
        private bool connected;

        public bool Connected
        {
            get => connected;
            set
            {
                if (value == connected)
                    return;
                connected = value;
                ConnectedStateChanged?.Invoke(value);
                if (value)
                    connectedHandler();
                else
                    disconnectedHandler();
            }
        }

        public delegate void ConnectedStateChangedDelegate(bool state);
        public event ConnectedStateChangedDelegate ConnectedStateChanged;

        private void connectedHandler()
        {
            startDisconnectDetectorThread();
        }

        private void disconnectedHandler()
        {
            stopDisconnectDetectorThread();
        }
        #endregion

        #region Connecting, disconnecting
        public void Connect()
        {

            if (Connected)
                throw new AlreadyConnectedException();

            if (string.IsNullOrWhiteSpace(ipAddress))
                return;

            try
            {
                socket.ReceiveTimeout = -1;
                socket.BeginConnect(ipAddress, port, connectedCallback, null);
            }
            catch (SocketException)
            { }
        }

        private void connectedCallback(IAsyncResult ar)
        {
            if (socket.Connected)
            {
                Connected = true;
                socketReceive();
            }
        }

        public void Disconnect()
        {
            if (Connected && (socket.Connected))
                socket.Disconnect(true);
            stopDisconnectDetectorThread();
            Connected = false;
        }

        public class AlreadyConnectedException : Exception
        {

            public AlreadyConnectedException()
            { }

            public AlreadyConnectedException(string message) : base(message)
            { }

            public AlreadyConnectedException(string message, Exception innerException) : base(message, innerException)
            { }

        }
        #endregion

        #region Receiving
        private void socketReceive()
        {
            if (!(Connected && socket.Connected))
                return;
            try
            {
                socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, receiveCallback, null);
            }
            catch (SocketException)
            {
                Connected = false;
            }
        }

        private const int BUFFER_SIZE = 1024;
        private byte[] buffer = new byte[BUFFER_SIZE];
        StringBuilder bufferBuilder = new StringBuilder();

        private void receiveCallback(IAsyncResult ar)
        {

            try
            {
                int charsRead = socket.EndReceive(ar);
                string receivedText = Encoding.ASCII.GetString(buffer, 0, charsRead);
                bufferBuilder.Append(receivedText);
                string totalText = bufferBuilder.ToString();
                totalText = totalText.Replace("\r", "");
                bufferBuilder.Clear();

                string[] parts = totalText.Split('\n');
                for (int i = 0; i < parts.Length; i++)
                {
                    if (i == parts.Length - 1)
                    {
                        if (parts[i] != string.Empty)
                            bufferBuilder.Append(parts[i]);
                    }
                    else
                    {
                        processReceivedLine(parts[i]);
                    }

                }

                socketReceive();
            }
            catch (SocketException)
            {
                Connected = false;
            }
            catch (ObjectDisposedException)
            {
                Connected = false;
            }

        }

        public delegate void LineReceivedDelegate(string line);
        public event LineReceivedDelegate LineReceived;

        private void processReceivedLine(string line)
        {
            LineReceived?.Invoke(line);
        }
        #endregion

        #region Sending
        public void SendLine(string line)
        {
            string toSend = line + "\r\n";
            byte[] bytesToSend = Encoding.ASCII.GetBytes(toSend);
            try
            {
                socket.BeginSend(bytesToSend, 0, bytesToSend.Length, SocketFlags.None, sendCallback, null);
                Console.Write(toSend);
            }
            catch (SocketException)
            {
                Connected = false;
            }
        }

        private void sendCallback(IAsyncResult ar)
        { }
        #endregion

        #region Disconnect detection
        private Thread disconnectDetectorThread;

        private void startDisconnectDetectorThread()
        {
            exitDisconnectDetectorThreadMethod = false;
            disconnectDetectorThread = new Thread(disconnectDetectorThreadMethod)
            {
                IsBackground = true
            };
            disconnectDetectorThread.Start();
        }

        private void stopDisconnectDetectorThread()
        {
            if (disconnectDetectorThread != null)
            {
                exitDisconnectDetectorThreadMethod = true;
                disconnectDetectorThread = null;
            }
        }

        private bool exitDisconnectDetectorThreadMethod = false;

        private void disconnectDetectorThreadMethod()
        {
            while (!exitDisconnectDetectorThreadMethod)
            {
                Thread.Sleep(1000);
                if (!socket.IsConnected())
                {
                    Connected = false;
                    break;
                }
            }
        }
        #endregion

    }

}
