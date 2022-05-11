using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.GpioInterfaces.BlackMagicDesign
{

    [TypeLabel("BMD Tally Box")]
    [TypeCode("bmdtallybox")]
    public class BmdTallyBox : GpioInterface
    {

        private new const string LOG_TAG = "GpioInterface/BMD";

        public BmdTallyBox()
        {
            Connected = false;
            initTcpServer();
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            reinitTcpServer();
        }

        public override void Removed()
        {
            base.Removed();
            deinitTcpServer();
            IpAddressChanged = null;
            ConnectionStateChanged = null;
        }

        #region Property: IpAddress
        public event PropertyChangedTwoValuesDelegate<BmdTallyBox, string> IpAddressChanged;

        [PersistAs("ip_address")]
        private string ipAddress;

        public string IpAddress
        {
            get => ipAddress;
            set => this.setProperty(ref ipAddress, value, IpAddressChanged, null, (_, _) => reinitTcpServer(), validator: ValidateIpAddress);
        }

        public void ValidateIpAddress(string ipAddress)
        {
            // ... throw new ArgumentException();
        }
        #endregion

        #region Property: Connected
        public event PropertyChangedTwoValuesDelegate<BmdTallyBox, bool> ConnectionStateChanged;

        private bool connected;

        public bool Connected
        {
            get => connected;
            protected set
            {
                AfterChangePropertyDelegate<bool> afterChangeDelegate = (ov, nv) =>
                {
                    if (nv)
                    {
                        State = GpioInterfaceState.Ok;
                        StateString = "connected";
                        LogDispatcher.I(LOG_TAG, $"BlackMagic Design Tally Box (ID: {ID}) connected to application from {IpAddress}.");
                    }
                    else
                    {
                        State = GpioInterfaceState.Warning;
                        StateString = "disconnected";
                        LogDispatcher.I(LOG_TAG, $"BlackMagic Design Tally Box (ID: {ID}) disconnected from application from {IpAddress}.");
                    }
                };
                this.setProperty(ref connected, value, ConnectionStateChanged, null, afterChangeDelegate);
            }
        }
        #endregion

        #region Input and output instantiation
        public override GpioInterfaceInput CreateInput(string name, int index) => new BmdTallyBoxInput(name, this, index);

        private static readonly Dictionary<Type, string> INPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(BmdTallyBoxInput), "bmd" }
        };

        protected override Dictionary<Type, string> InputTypesDictionaryGetter() => INPUT_TYPES;

        public override GpioInterfaceOutput CreateOutput(string name, int index) => new BmdTallyBoxOutput(name, this, index);

        private static readonly Dictionary<Type, string> OUTPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(BmdTallyBoxOutput), "bmd" }
        };

        protected override Dictionary<Type, string> OutputTypesDictionaryGetter() => OUTPUT_TYPES;
        #endregion

        #region Socket, connection
        private TcpServer tcpServer;
        private TcpServer.Connection tcpServerConnection;

        private void initTcpServer()
        {
            tcpServerConnection = null;
            tcpServer = new();
            tcpServer.NewConnection += tcpNewConnectionHandler;
            tcpServer.ConnectionClosed += tcpConnectionClosedHandler;
            tcpServer.ConnectionReceivedLine += tcpLineReceiverHandler;
            tcpServer.StartListening();
        }

        private void deinitTcpServer()
        {
            tcpServer.Close();
            tcpServer = null;
            tcpServerConnection = null;
        }

        private void reinitTcpServer()
        {
            deinitTcpServer();
            initTcpServer();
        }

        private void tcpNewConnectionHandler(TcpServer.Connection connection)
        {
            EndPoint removeEndPoint = connection.connectionSocket.RemoteEndPoint;
            if (removeEndPoint.AddressFamily != AddressFamily.InterNetwork)
            {
                connection.Close();
                return;
            }
            if (((IPEndPoint)removeEndPoint).Address.ToString() != IpAddress)
            {
                connection.Close();
                return;
            }
            tcpServerConnection = connection;
            sendProtocolPreamble();
            sendDeviceInformation();
            queryAllInputStates();
            sendAllOutputStates();
            Connected = true;
        }

        private void tcpConnectionClosedHandler(TcpServer.Connection connection)
        {
            if (connection != tcpServerConnection)
                return;
            tcpServerConnection = null;
            Connected = false;
        }

        private class TcpServer
        {

            public delegate void ConnectionEventDelegate(Connection connection);
            public event ConnectionEventDelegate NewConnection;
            public event ConnectionEventDelegate ConnectionClosed;
            public delegate void ConnectionReceivedLineEventDelegate(Connection connection, string line);
            public event ConnectionReceivedLineEventDelegate ConnectionReceivedLine;

            private List<Connection> connections = new();
            private bool acceptingConnections = false;

            private CancellationTokenSource listeningCancellationTokenSource;
            private CancellationToken listeningCancellationToken;

            public void StartListening()
            {
                listeningCancellationTokenSource = new();
                listeningCancellationToken = listeningCancellationTokenSource.Token;
                Task.Run(StartListeningAsync, listeningCancellationToken);
            }

            public async Task StartListeningAsync()
            {
                IPAddress localIpAddress = IPAddress.Any;
                IPEndPoint localEndPoint = new IPEndPoint(localIpAddress, BMD_VIDEOHUB_PROTOCOL_TCP_PORT);
                Socket socket = new Socket(localIpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Bind(localEndPoint);
                    socket.Listen(10);
                    acceptingConnections = true;
                    using (listeningCancellationToken.Register(() => socket.Close()))
                    {
                        while (acceptingConnections)
                        {
                            try
                            {
                                Socket connectionSocket = await socket.AcceptAsync();
                                Connection connection = new Connection(this, connectionSocket);
                                if (connectionSocket.Connected)
                                {
                                    setKeepAlive(connectionSocket);
                                    connections.Add(connection);
                                    NewConnection?.Invoke(connection);
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            }

            private void setKeepAlive(Socket socket)
            {
                int size = Marshal.SizeOf((uint)0);
                byte[] keepAlive = new byte[size * 3];
                Buffer.BlockCopy(BitConverter.GetBytes((uint)1), 0, keepAlive, 0, size);
                Buffer.BlockCopy(BitConverter.GetBytes((uint)500), 0, keepAlive, size, size);
                Buffer.BlockCopy(BitConverter.GetBytes((uint)500), 0, keepAlive, size * 2, size);
                socket.IOControl(IOControlCode.KeepAliveValues, keepAlive, null);
            }

            public void Close()
            {
                listeningCancellationTokenSource.Cancel();
                acceptingConnections = false;
                List<Connection> connectionsToClose = new(connections);
                connectionsToClose.ForEach(c => c.Close());
                NewConnection = null;
                ConnectionClosed = null;
                ConnectionReceivedLine = null;
            }

            public void SendAll(string data) => connections.ForEach(c => c.Send(data));

            private void notifyConnectionReceivedLine(Connection connection, string line) =>
                ConnectionReceivedLine?.Invoke(connection, line);

            private void notifyConnectionClosed(Connection connection)
            {
                connections.Remove(connection);
                ConnectionClosed?.Invoke(connection);
            }

            public class Connection
            {

                private const int BUFFER_SIZE = 1024;
                private byte[] byteBuffer = new byte[BUFFER_SIZE];
                private string stringBuffer = "";
                public TcpServer server = null;
                public Socket connectionSocket = null;

                public Connection(TcpServer server, Socket connectionSocket)
                {
                    this.server = server;
                    this.connectionSocket = connectionSocket;
                    startCheckState();
                    read();
                }

                private void read()
                {
                    try
                    {
                        connectionSocket.BeginReceive(byteBuffer, 0, BUFFER_SIZE, 0, readCallback, null);
                    }
                    catch (SocketException se)
                    {
                        if (se.SocketErrorCode == SocketError.SocketError)
                            closed();
                    }
                }

                private void readCallback(IAsyncResult ar)
                {
                    try
                    {
                        int bytesRead = connectionSocket.EndReceive(ar);
                        if (bytesRead > 0)
                        {
                            string data = Encoding.ASCII.GetString(byteBuffer, 0, bytesRead);
                            stringBuffer += data;
                            stringBuffer = stringBuffer.Replace("\r\n", "\n").Replace("\r", "\n");
                            bool keepLast = !stringBuffer.EndsWith("\n");
                            string[] lines = stringBuffer.Split("\n");
                            int notifyAboutLines = lines.Length - 1;
                            stringBuffer = "";
                            if (keepLast)
                                stringBuffer = lines[lines.Length - 1];
                            for (int i = 0; i < notifyAboutLines; i++)
                                server.notifyConnectionReceivedLine(this, lines[i]);
                            read();
                        }
                        if ((bytesRead == 0) && connectionSocket.Poll(1, SelectMode.SelectRead))
                            closed();
                    }
                    catch { }
                }

                public void Send(string data)
                {
                    byte[] byteData = Encoding.ASCII.GetBytes(data);
                    try
                    {
                        connectionSocket.BeginSend(byteData, 0, byteData.Length, 0, sendCallback, connectionSocket);
                    }
                    catch (SocketException se)
                    {
                        if (se.SocketErrorCode == SocketError.SocketError)
                            closed();
                    }
                }

                private void sendCallback(IAsyncResult ar)
                {
                    try
                    {
                        int bytesSent = connectionSocket.EndSend(ar);
                    }
                    catch (SocketException se)
                    {
                        if (se.SocketErrorCode == SocketError.SocketError)
                            closed();
                    }
                }

                public void Close() => closed();

                private void closed()
                {
                    try
                    {
                        stopCheckState();
                        connectionSocket.Shutdown(SocketShutdown.Both);
                        connectionSocket.Close();
                        connectionSocket.Dispose();
                    }
                    catch { }
                    finally
                    {
                        server.notifyConnectionClosed(this);
                    }
                }

                private CancellationTokenSource checkStateTaskCancellationTokenSource;
                private CancellationToken checkStateTaskCancellationToken;
                private Task checkStateTask;

                private void startCheckState()
                {
                    if (checkStateTask != null)
                        return;
                    checkStateTaskCancellationTokenSource = new();
                    checkStateTaskCancellationToken = checkStateTaskCancellationTokenSource.Token;
                    checkStateTask = Task.Run(checkStateTaskMethod);
                }

                private void stopCheckState()
                {
                    try
                    {
                        if (checkStateTaskCancellationTokenSource != null)
                        {
                            checkStateTaskCancellationTokenSource.Cancel();
                            checkStateTaskCancellationTokenSource.Dispose();
                            checkStateTaskCancellationTokenSource = null;
                        }
                        checkStateTask = null;
                    }
                    catch { }
                }

                private async Task checkStateTaskMethod()
                {
                    while (true)
                    {
                        if (checkStateTaskCancellationToken.IsCancellationRequested == true)
                            return;
                        if (!checkState())
                            closed();
                        await Task.Delay(500, checkStateTaskCancellationToken);
                    }
                }

                private bool checkState()
                {
                    try
                    {
                        return !(connectionSocket.Poll(1, SelectMode.SelectRead) && connectionSocket.Available == 0);
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

        private const int BMD_VIDEOHUB_PROTOCOL_TCP_PORT = 9990;
        #endregion

        #region Protocol
        private const string PROTOCOL_ACK = "ACK";
        private const string PROTOCOL_NAK = "NAK";

        private void sendBlock(IEnumerable<string> strings)
        {
            if (tcpServerConnection == null)
                return;
            StringBuilder sb = new();
            foreach(string s in strings)
                sb.Append(s + "\n");
            sb.Append("\n");
            tcpServerConnection.Send(sb.ToString());
        }

        private const string PROTOCOL_BLOCK_PING = "PING:";

        private const string PROTOCOL_BLOCK_PROTOCOL_PREAMBLE = "PROTOCOL PREAMBLE:";
        private const string PROTOCOL_PP_VERSION = "Version: 2.3";
        private static readonly string[] PROTOCOL_PP = new string[]
        {
            PROTOCOL_BLOCK_PROTOCOL_PREAMBLE,
            PROTOCOL_PP_VERSION
        };

        private void sendProtocolPreamble() => sendBlock(PROTOCOL_PP);

        private const string PROTOCOL_BLOCK_DEVICE_INFORMATION = "VIDEOHUB DEVICE:";
        private const string PROTOCOL_DI_DEVICE_PRESENT = "Device present: true";
        private const string PROTOCOL_DI_MODEL_NAME = "Model name: Blackmagic Smart Videohub";
        private const string PROTOCOL_DI_VIDEO_INPUTS = "Video inputs: 16";
        private const string PROTOCOL_DI_VIDEO_PROCESSING_UNITS = "Video processing units: 0";
        private const string PROTOCOL_DI_VIDEO_OUTPUTS = "Video outputs: 16";
        private const string PROTOCOL_DI_VIDEO_MONITORING_OUTPUTS = "Video monitoring outputs: 0";
        private const string PROTOCOL_DI_SERIAL_PORTS = "Serial ports: 0";
        private static readonly string[] PROTOCOL_DI = new string[]
        {
            PROTOCOL_BLOCK_DEVICE_INFORMATION,
            PROTOCOL_DI_DEVICE_PRESENT,
            PROTOCOL_DI_MODEL_NAME,
            PROTOCOL_DI_VIDEO_INPUTS,
            PROTOCOL_DI_VIDEO_PROCESSING_UNITS,
            PROTOCOL_DI_VIDEO_OUTPUTS,
            PROTOCOL_DI_VIDEO_MONITORING_OUTPUTS,
            PROTOCOL_DI_SERIAL_PORTS
        };

        private void sendDeviceInformation() => sendBlock(PROTOCOL_DI);

        private const string PROTOCOL_BLOCK_VIDEO_OUTPUT_ROUTING = "VIDEO OUTPUT ROUTING:";

        internal void SendOutputState(int index, bool state)
            => sendBlock(new string[] {
                PROTOCOL_BLOCK_VIDEO_OUTPUT_ROUTING,
                outputStateToProtocol(index, state)
            });

        protected override void sendAllOutputStates()
        {
            List<string> lines = new();
            lines.Add(PROTOCOL_BLOCK_VIDEO_OUTPUT_ROUTING);
            foreach (BmdTallyBoxOutput output in Outputs)
                lines.Add(outputStateToProtocol(output));
            sendBlock(lines);
        }

        private const int PROTOCOL_INPUT_INDEX_OFF = 0;
        private const int PROTOCOL_INPUT_INDEX_ON = 7;
        private static readonly string PROTOCOL_INPUT_INDEX_OFF_STR = PROTOCOL_INPUT_INDEX_OFF.ToString();
        private static readonly string PROTOCOL_INPUT_INDEX_ON_STR = PROTOCOL_INPUT_INDEX_ON.ToString();

        string outputStateToProtocol(int index, bool state)
            => string.Format("{0} {1}", index, (state ? PROTOCOL_INPUT_INDEX_ON_STR : PROTOCOL_INPUT_INDEX_OFF_STR));

        string outputStateToProtocol(BmdTallyBoxOutput output)
            => string.Format("{0} {1}", output.Index, ((output.Driver?.CurrentState ?? false) ? PROTOCOL_INPUT_INDEX_ON_STR : PROTOCOL_INPUT_INDEX_OFF_STR));

        private void tcpLineReceiverHandler(TcpServer.Connection connection, string line)
        {
            if (connection != tcpServerConnection)
                return;
            if (line == string.Empty)
            {
                blockEnd();
                return;
            }
            switch (currentBlockType)
            {
                case BlockType.Ping:
                    handlePing(line);
                    break;
                case BlockType.VideohubOutputRouting:
                    handleLineVideohubOutputRouting(line);
                    break;
                case BlockType.OtherOrUnknown:
                    handleLineOtherOrUnknown(line);
                    break;
            }
        }

        private void handlePing(string line)
        { }

        private void handleLineVideohubOutputRouting(string line)
        {
            string[] linePieces = line.Split(' ');
            if (linePieces.Length != 2)
            {
                protocolError();
                return;
            }
            if (!int.TryParse(linePieces[0], out int outputIndex))
            {
                protocolError();
                return;
            }
            GetInput(outputIndex)?.NotifyStateChanged(linePieces[1] == PROTOCOL_INPUT_INDEX_ON_STR);
        }

        private void handleLineOtherOrUnknown(string line)
        {
            if (line == PROTOCOL_BLOCK_PING)
            {
                currentBlockType = BlockType.Ping;
                return;
            }
            if (line == PROTOCOL_BLOCK_VIDEO_OUTPUT_ROUTING)
            {
                currentBlockType = BlockType.VideohubOutputRouting;
                return;
            }
        }

        private bool haveProtocolError = false;

        private void protocolError() => haveProtocolError = true;

        private void blockEnd()
        {
            string ackText = (haveProtocolError ? PROTOCOL_NAK : PROTOCOL_ACK) + "\r\n\r\n";
            tcpServerConnection?.Send(ackText);
            currentBlockType = BlockType.OtherOrUnknown;
            haveProtocolError = false;
        }

        private enum BlockType
        {
            Ping,
            VideohubOutputRouting,
            OtherOrUnknown
        }

        private BlockType currentBlockType = BlockType.OtherOrUnknown;
        #endregion

    }

}
