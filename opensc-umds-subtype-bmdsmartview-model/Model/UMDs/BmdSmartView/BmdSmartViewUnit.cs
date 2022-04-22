using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.BmdSmartView
{

    public class BmdSmartViewUnit : ModelBase
    {

        #region Instantiation, restoration
        public BmdSmartViewUnit()
        {
            startSocketStatusCheckTask();
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            updateEndpoint();
            shouldBeConnected = autoConnect;
        }

        public override void Removed()
        {
            base.Removed();
            stopSocketStatusCheckTask();
            disposeExistingSocket();
            IpAddressChanged = null;
            PortChanged = null;
            AutoConnectChanged = null;
            AutoReconnectChanged = null;
            ConnectedChanged = null;
        }
        #endregion

        #region OwnerDatabase
        public override IDatabaseBase OwnerDatabase => BmdSmartViewUnitDatabase.Instance;
        #endregion

        #region Before & after update hooks
        protected override void beforeUpdate()
        {
            base.beforeUpdate();
            ipEndpointBeforeUpdate();
        }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            ipEndpointAfterUpdate();
        }
        #endregion

        #region Property: IpAddress
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewUnit, string> IpAddressChanged;

        [PersistAs("ipaddress")]
        private string ipAddress;

        public string IpAddress
        {
            get => ipAddress;
            set => this.setProperty(ref ipAddress, value, IpAddressChanged, null, (ov, nv) => updateEndpoint());
        }
        #endregion

        #region Property: Port
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewUnit, int> PortChanged;

        [PersistAs("port")]
        private int port = 9992;

        public int Port
        {
            get => port;
            set => this.setProperty(ref port, value, PortChanged, null, (ov, nv) => updateEndpoint(), ValidatePort);
        }

        public void ValidatePort(int port)
        {
            if ((port < 0) || (port > 65535))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: AutoConnect
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewUnit, bool> AutoConnectChanged;

        [PersistAs("auto_connect")]
        private bool autoConnect = true;

        public bool AutoConnect
        {
            get => autoConnect;
            set => this.setProperty(ref autoConnect, value, AutoConnectChanged, null, (ov, nv) => {
                if (nv)
                    shouldBeConnected = true;
            });
        }
        #endregion

        #region Property: AutoReconnect
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewUnit, bool> AutoReconnectChanged;

        [PersistAs("auto_reconnect")]
        private bool autoReconnect = true;

        public bool AutoReconnect
        {
            get => autoReconnect;
            set => this.setProperty(ref autoReconnect, value, AutoReconnectChanged);
        }
        #endregion

        #region Property: AutoReconnectInterval
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewUnit, int> AutoReconnectIntervalChanged;

        [PersistAs("auto_reconnect_interval")]
        private int autoReconnectInterval = 5;

        public int AutoReconnectInterval
        {
            get => autoReconnectInterval;
            set => this.setProperty(ref autoReconnectInterval, value, AutoReconnectIntervalChanged);
        }
        #endregion

        #region Property: Connected
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewUnit, bool> ConnectedChanged;

        private bool connected = false;

        public bool Connected
        {
            get => connected;
            private set => this.setProperty(ref connected, value, ConnectedChanged);
        }
        #endregion

        #region Socket - connection basics
        private Socket tcpSocket;

        public async Task Connect()
        {
            if (ipEndpoint == null)
            {
                Connected = false;
                return;
            }
            shouldBeConnected = true;
            disposeExistingSocket();
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await tcpSocket.ConnectAsync(ipEndpoint);
            Connected = tcpSocket.Connected;
        }

        public void Disconnect()
        {
            shouldBeConnected = false;
            if (tcpSocket != null)
            {
                try
                {
                    tcpSocket.DisconnectAsync(new());
                }
                catch { }
            }
            disposeExistingSocket();
        }

        public async void Reconnect()
        {
            Disconnect();
            await Connect();
        }

        private void disposeExistingSocket()
        {
            if (tcpSocket == null)
                return;
            try
            {
                tcpSocket.Dispose();
            }
            catch { }
            tcpSocket = null;
            Connected = false;
        }
        #endregion

        #region Socket - status checking, auto connection and reconnection
        bool shouldBeConnected = false;
        int timeSinceLastConnectAttempt = -1;

        private async Task socketStatusCheckTaskMethod()
        {
            while (true)
            {
                try
                {
                    if (Connected)
                    {
                        checkSocketStatus();
                    }
                    else if (shouldBeConnected)
                    {
                        if ((timeSinceLastConnectAttempt < 0) || (timeSinceLastConnectAttempt >= autoReconnectInterval))
                        {
                            await Connect();
                            timeSinceLastConnectAttempt = 0;
                        }
                        else
                        {
                            timeSinceLastConnectAttempt++;
                        }
                    }
                    await Task.Delay(1000);
                }
                catch (OperationCanceledException)
                { }
            }
        }

        private CancellationTokenSource socketStatusCheckTasCancellationTokenSource;
        private Task socketStatusCheckTask;

        private void startSocketStatusCheckTask()
        {
            socketStatusCheckTasCancellationTokenSource = new();
            socketStatusCheckTask = Task.Run(socketStatusCheckTaskMethod);
        }

        private async void stopSocketStatusCheckTask()
        {
            if (socketStatusCheckTask == null)
                return;
            try
            {
                socketStatusCheckTasCancellationTokenSource.Cancel();
                await socketStatusCheckTask;
                socketStatusCheckTask.Dispose();
                socketStatusCheckTask = null;
                socketStatusCheckTasCancellationTokenSource.Dispose();
                socketStatusCheckTasCancellationTokenSource = null;
            }
            catch (ObjectDisposedException)
            { }
        }

        private void notifySocketDisconnected()
        {
            timeSinceLastConnectAttempt = 0;
            disposeExistingSocket();
        }

        private bool checkSocketStatus()
        {
            try
            {
                return tcpSocket.Poll(1, SelectMode.SelectWrite);
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
        #endregion

        #region Socket - sending
        private void sendTcpPacket(string packet)
        {
            if (tcpSocket == null)
                return;
            byte[] data = Encoding.ASCII.GetBytes(packet);
            try
            {
                tcpSocket.Send(data, 0, data.Length, SocketFlags.None);
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.NotConnected)
                    notifySocketDisconnected();
            }
        }

        internal void SendDisplayCommands(char monitorLetter, Dictionary<string, string> commands)
        {
            StringBuilder commandBuilder = new();
            commandBuilder.AppendLine($"MONITOR {monitorLetter}:");
            foreach (KeyValuePair<string, string> command in commands)
                commandBuilder.AppendLine($"{command.Key}: {command.Value}");
            commandBuilder.AppendLine();
            sendTcpPacket(commandBuilder.ToString());
        }
        #endregion

        #region Socket - endpoint
        private IPEndPoint ipEndpoint;

        private void updateEndpoint()
        {
            bool wasConnected = Connected;
            if (!IPAddress.TryParse(ipAddress, out IPAddress typedIpAddress))
            {
                ipEndpoint = null;
                return;
            }
            ipEndpoint = new IPEndPoint(typedIpAddress, port);
            if (wasConnected)
                Reconnect();
        }

        string ipAddressBeforeUpdate;
        int portBeforeUpdate;

        private void ipEndpointBeforeUpdate()
        {
            ipAddressBeforeUpdate = ipAddress;
            portBeforeUpdate = port;
        }

        private void ipEndpointAfterUpdate()
        {
            if ((ipAddressBeforeUpdate != ipAddress) || (portBeforeUpdate != port))
                updateEndpoint();
        }
        #endregion

    }

}
