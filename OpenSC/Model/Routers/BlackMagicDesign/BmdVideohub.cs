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

        public BmdVideohub()
        {
            initVideohub();
        }

        public override void Restored()
        {
            base.Restored();
            initVideohub();
            startAutoReconnectThread();
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
                    State = RouterState.Ok;
                    StateString = "connected";
                }
                else
                {
                    State = RouterState.Warning;
                    StateString = "disconnected";
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
        }

        private void crosspointChangedHandler(int output, int? input)
        {
            if ((output < Outputs.Count) && (input != null) && (input < Inputs.Count))
                Outputs[output].Crosspoint = Inputs[(int)input];
        }

        protected override bool setCrosspoint(RouterOutput output, RouterInput input)
        {
            try
            {
                videohub.SetCrosspoint(output.Index, input.Index);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            { }
            return false;
        }

        protected override void updateAllCrosspoints()
        {
            //
        }

    }

}
