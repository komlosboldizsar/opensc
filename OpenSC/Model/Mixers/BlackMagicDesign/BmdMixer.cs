using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign
{

    public delegate void BmdMixerIpAddressChangingDelegate(BmdMixer mixer, string oldIpAddress, string newIpAddress);
    public delegate void BmdMixerIpAddressChangedDelegate(BmdMixer mixer, string oldIpAddress, string newIpAddress);

    public delegate void BmdMixerConnectionStateChangingDelegate(BmdMixer mixer, bool oldState, bool newState);
    public delegate void BmdMixerConnectionStateChangedDelegate(BmdMixer mixer, bool oldState, bool newState);

    public delegate void BmdMixerAutoReconnectChangingDelegate(BmdMixer mixer, bool oldSetting, bool newSetting);
    public delegate void BmdMixerAutoReconnectChangedDelegate(BmdMixer mixer, bool oldSetting, bool newSetting);

    [TypeLabel("BMD Switched")]
    [TypeCode("bmd")]
    public class BmdMixer : Mixer
    {

        public BmdMixer()
        {
            initSwitcher();
        }

        public override void Restored()
        {
            base.Restored();
            initSwitcher();
            startAutoReconnectThread();
        }

        public void Connect()
        {
            // TODO: USE BMD API
        }

        public void Disconnect()
        {
            // TODO: USE BMD API
        }

        public event BmdMixerIpAddressChangingDelegate IpAddressChanging;
        public event BmdMixerIpAddressChangedDelegate IpAddressChanged;
        public event ParameterlessChangeNotifierDelegate IpAddressChangingPCN;
        public event ParameterlessChangeNotifierDelegate IpAddressChangedPCN;

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
                IpAddressChangingPCN?.Invoke();
                ipAddress = value;
                // TODO: USE BMD API
                IpAddressChanged?.Invoke(this, oldIpAddress, value);
                IpAddressChangedPCN?.Invoke();
            }
        }

        public void ValidateIpAddress(string ipAddress)
        {
            // ... throw new ArgumentException();
        }

        public event BmdMixerConnectionStateChangingDelegate ConnectionStateChanging;
        public event BmdMixerConnectionStateChangedDelegate ConnectionStateChanged;
        public event ParameterlessChangeNotifierDelegate ConnectionStateChangingPCN;
        public event ParameterlessChangeNotifierDelegate ConnectionStateChangedPCN;

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
                ConnectionStateChangingPCN?.Invoke();
                connected = value;
                ConnectionStateChanged?.Invoke(this, oldState, value);
                ConnectionStateChangedPCN?.Invoke();
            }
        }

        #region Auto reconnect
        public event BmdMixerAutoReconnectChangingDelegate AutoReconnectChanging;
        public event BmdMixerAutoReconnectChangedDelegate AutoReconnectChanged;
        public event ParameterlessChangeNotifierDelegate AutoReconnectChangingPCN;
        public event ParameterlessChangeNotifierDelegate AutoReconnectChangedPCN;

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
                AutoReconnectChangingPCN?.Invoke();
                autoReconnect = value;
                AutoReconnectChanged?.Invoke(this, oldValue, value);
                AutoReconnectChangedPCN?.Invoke();
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
            while (autoReconnect && !connected)
            {
                Thread.Sleep(RECONNECT_TRY_INTERVAL);
                if (autoReconnect && !connected)
                    Connect();
            }
        }
        #endregion

        private void initSwitcher()
        {
            // TODO: USE BMD API
        }

    }

}