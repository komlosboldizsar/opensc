using OpenSC.Logger;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BlackMagicDesign
{

    public delegate void BmdVideohubIpAddressChangingDelegate(BmdVideohub router, string oldIpAddress, string newIpAddress);
    public delegate void BmdVideohubIpAddressChangedDelegate(BmdVideohub router, string oldIpAddress, string newIpAddress);

    public delegate void BmdVideohubConnectionStateChangingDelegate(BmdVideohub router, bool oldState, bool newState);
    public delegate void BmdVideohubConnectionStateChangedDelegate(BmdVideohub router, bool oldState, bool newState);

    public delegate void BmdVideohubAutoReconnectChangingDelegate(BmdVideohub router, bool oldSetting, bool newSetting);
    public delegate void BmdVideohubAutoReconnectChangedDelegate(BmdVideohub router, bool oldSetting, bool newSetting);

    [TypeLabel("BMD Videohub")]
    [TypeCode("bmd")]
    public class BmdVideohub : Router
    {

        private new const string LOG_TAG = "Router/BMD";

        public BmdVideohub()
        {
            initVideohub();
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            initVideohub();
            startAutoReconnectThread();
        }

        public override void Removed()
        {

            base.Removed();

            Disconnect();
            videohub.CrosspointChanged -= crosspointChangedHandler;
            videohub.ConnectionStateChanged -= connectionStateChangedHandler;
            videohub = null;

            IpAddressChanged = null;
            IpAddressChanging = null;
            ConnectionStateChanged = null;
            ConnectionStateChanging = null;
            AutoReconnectChanged = null;
            AutoReconnectChanging = null;

            autoReconnectThread?.Abort();
            autoReconnectThread = null;

        }

        public void Connect()
        {
            videohub.Connect();
        }

        public void Disconnect()
        {
            videohub.Disconnect();
        }

        public event BmdVideohubIpAddressChangingDelegate IpAddressChanging;
        public event BmdVideohubIpAddressChangedDelegate IpAddressChanged;

        [PersistAs("ip_address")]
        private string ipAddress;

        public string IpAddress
        {
            get { return ipAddress; }
            set
            {
                ValidateIpAddress(ipAddress);
                if (value == ipAddress)
                    return;
                string oldIpAddress = ipAddress;
                IpAddressChanging?.Invoke(this, oldIpAddress, value);
                ipAddress = value;
                videohub.IpAddress = value;
                IpAddressChanged?.Invoke(this, oldIpAddress, value);
                RaisePropertyChanged(nameof(IpAddress));
            }
        }

        public void ValidateIpAddress(string ipAddress)
        {
            // ... throw new ArgumentException();
        }

        public event BmdVideohubConnectionStateChangingDelegate ConnectionStateChanging;
        public event BmdVideohubConnectionStateChangedDelegate ConnectionStateChanged;

        private bool connected;

        public bool Connected
        {
            get { return connected; }
            set
            {

                if (value == connected)
                    return;
                bool oldState = connected;
                ConnectionStateChanging?.Invoke(this, oldState, value);
                connected = value;
                ConnectionStateChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(Connected));

                if (value)
                {

                    // State
                    State = RouterState.Ok;
                    StateString = "connected";

                    // Lo
                    string logMessage = string.Format("Connected to a BlackMagic Design router/videohub (ID: {0}) with IP {1}.",
                        ID,
                        IpAddress);
                    LogDispatcher.I(LOG_TAG, logMessage);

                }
                else
                {

                    // State
                    State = RouterState.Warning;
                    StateString = "disconnected";

                    // Log
                    string logMessage = string.Format("Disconnected from a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}.",
                        ID,
                        IpAddress);
                    LogDispatcher.I(LOG_TAG, logMessage);

                }

            }
        }

        #region Auto reconnect
        public event BmdVideohubAutoReconnectChangingDelegate AutoReconnectChanging;
        public event BmdVideohubAutoReconnectChangedDelegate AutoReconnectChanged;

        [PersistAs("auto_reconnect")]
        private bool autoReconnect;

        public bool AutoReconnect
        {
            get { return autoReconnect; }
            set
            {
                if (value == autoReconnect)
                    return;
                bool oldValue = autoReconnect;
                AutoReconnectChanging?.Invoke(this, oldValue, value);
                autoReconnect = value;
                AutoReconnectChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(AutoReconnect));
            }
        }

        private const int RECONNECT_TRY_INTERVAL = 10000;

        private Thread autoReconnectThread = null;

        private void startAutoReconnectThread()
        {
            autoReconnectThread = new Thread(autoReconnectThreadMethod)
            {
                IsBackground = true
            };
            autoReconnectThread.Start();
        }

        private void autoReconnectThreadMethod()
        {

            string logMessage = string.Format("Trying auto reconnect to a BlackMagic Design router/videohub (ID: {0}) with IP {1}...",
                ID,
                IpAddress);
            LogDispatcher.I(LOG_TAG, logMessage);

            if (autoReconnect && !connected)
                Connect();
            while(autoReconnect && !connected)
            {
                Thread.Sleep(RECONNECT_TRY_INTERVAL);
                if (autoReconnect && !connected)
                    Connect();
            }

        }
        #endregion

        private BMD.Videohub.BlackMagicVideohub videohub = null;

        private void initVideohub()
        {
            videohub = new BMD.Videohub.BlackMagicVideohub(ipAddress);
            videohub.ConnectionStateChanged += connectionStateChangedHandler;
            videohub.CrosspointChanged += crosspointChangedHandler;
        }

        private void connectionStateChangedHandler(bool state)
        {
            Connected = state;
            if (Connected)
                queryAllCrosspoints();
        }

        private void crosspointChangedHandler(int output, int? input)
        {
            if (input == null)
                return;
            try
            {
                notifyCrosspointChanged(output, (int)input);
            }
            catch { }
        }

        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
        {
            try
            {
                videohub.SetCrosspoint(output.Index, input.Index);
            }
            catch (ArgumentOutOfRangeException)
            { }
        }

        protected override void queryAllCrosspoints()
        {
            videohub.QueryAllCrosspoints();
        }

        protected override void requestLockOperationImpl(RouterOutput output, RouterOutputLockOperationType operationType)
        { }

        #region Input and output instantiation
        public override RouterInput CreateInput(string name, int index) => new RouterInput(name, this, index);
        public override RouterOutput CreateOutput(string name, int index) => new RouterOutput(name, this, index);
        #endregion

    }

}
