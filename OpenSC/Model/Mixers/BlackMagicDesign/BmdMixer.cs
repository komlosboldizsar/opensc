using BMD.Switcher;
using OpenSC.Logger;
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

    [TypeLabel("BMD Switcher")]
    [TypeCode("bmd")]
    public class BmdMixer : Mixer
    {

        private const string LOG_TAG = "Mixer/BMD";

        public BmdMixer()
        {
            initSwitcher();
        }

        public override void Restored()
        {
            base.Restored();
            initSwitcher();
            Connect();
        }

        public override void Removed()
        {

            base.Removed();

            Disconnect();
            deinitSwitcher();
            switcher = null;

            IpAddressChanged = null;
            IpAddressChanging = null;
            AutoReconnectChanged = null;
            AutoReconnectChanging = null;
            ConnectionStateChanged = null;
            ConnectionStateChanging = null;

            disposeInputMonitors();
            disposeMixEffectBlockMonitor();

            autoReconnectThread?.Abort();
            autoReconnectThread = null;

        }

        public void Connect()
        {
            try
            {
                switcher?.Connect();
            }
            catch (Switcher.CouldNotConnectException ex)
            {
                string errorMessage = string.Format("Couldn't connect to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}. Exception message: [{2}]",
                    ID,
                    IpAddress,
                    ex.Message);
                LogDispatcher.E(LOG_TAG, errorMessage);
            }
            catch(Switcher.AlreadyConnectedException ex)
            {
                string errorMessage = string.Format("Tried to connect to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}, but was already connected. Exception message: [{2}]",
                    ID,
                    IpAddress,
                    ex.Message);
                LogDispatcher.W(LOG_TAG, errorMessage);
            }
        }

        public void Disconnect()
        {
            try
            {
                switcher?.Disconnect();
            }
            catch (Switcher.NotConnectedException ex)
            {
                string errorMessage = string.Format("Tried to disconnect from a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}, but wasn't connected. Exception message: [{2}]",
                    ID,
                    IpAddress,
                    ex.Message);
                LogDispatcher.W(LOG_TAG, errorMessage);
            }
        }

        private Switcher switcher;

        private void initSwitcher()
        {
            switcher = new Switcher(ipAddress);
            switcher.ConnectionStateChanged += switcherConnectionStateChanged;
            State = MixerState.Warning;
            StateString = "disconnected";
        }

        private void deinitSwitcher()
        {
            switcher.ConnectionStateChanged -= switcherConnectionStateChanged;
        }

        private void switcherConnectionStateChanged(Switcher switcher, bool newState)
        {
            Connected = newState;
        }

        #region Property: IpAddress
        public event BmdMixerIpAddressChangingDelegate IpAddressChanging;
        public event BmdMixerIpAddressChangedDelegate IpAddressChanged;

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
                switcher.IpAddress = value;

                IpAddressChanged?.Invoke(this, oldIpAddress, value);
                RaisePropertyChanged(nameof(IpAddress));

            }
        }

        public void ValidateIpAddress(string ipAddress)
        {
            // ... throw new ArgumentException();
        }
        #endregion

        #region Property: ConnectionState
        public event BmdMixerConnectionStateChangingDelegate ConnectionStateChanging;
        public event BmdMixerConnectionStateChangedDelegate ConnectionStateChanged;

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

                    getMixEffectBlockMonitor();
                    getInputMonitors();

                    // State
                    State = MixerState.Ok;
                    StateString = "connected";

                    // Log
                    string logMessage = string.Format("Connected to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}.",
                        ID,
                        IpAddress);
                    LogDispatcher.I(LOG_TAG, logMessage);

                }
                else
                {

                    disposeMixEffectBlockMonitor();
                    disposeInputMonitors();
                    deinitSwitcher();

                    // State
                    State = MixerState.Warning;
                    StateString = "disconnected";

                    // Log
                    string logMessage = string.Format("Disconnected from a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}.",
                        ID,
                        IpAddress);
                    LogDispatcher.I(LOG_TAG, logMessage);

                }

            }
        }
        #endregion

        #region Property: AutoReconnect
        public event BmdMixerAutoReconnectChangingDelegate AutoReconnectChanging;
        public event BmdMixerAutoReconnectChangedDelegate AutoReconnectChanged;

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
        #endregion

        #region Auto reconnect
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

            string logMessage = string.Format("Trying auto reconnect to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}...",
                ID,
                IpAddress);
            LogDispatcher.I(LOG_TAG, logMessage);

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

        #region Input monitors
        private List<InputMonitor> inputMonitors = new List<InputMonitor>();
        
        private void getInputMonitors()
        {
            inputMonitors = switcher.GetInputMonitors();
            foreach(InputMonitor inputMonitor in inputMonitors)
            {
                inputMonitor.IsProgramTalliedChanged += inputMonitorIsProgramTalliedChangedHandler;
                inputMonitor.IsPreviewTalliedChanged += inputMonitorIsPreviewTalliedChangedHandler;
            }
        }

        private void disposeInputMonitors()
        {
            inputMonitors.ForEach(im => im?.Dispose());
            inputMonitors.Clear();
        }

        private void inputMonitorIsProgramTalliedChangedHandler(BMDSwitcherAPI.IBMDSwitcherInput switcherInput, InputMonitor monitor, bool isTallied)
        {
            Inputs.FindAll(input => (input.Index == switcherInput.GetId())).ForEach(input => { input.RedTally = isTallied; });
        }

        private void inputMonitorIsPreviewTalliedChangedHandler(BMDSwitcherAPI.IBMDSwitcherInput switcherInput, InputMonitor monitor, bool isTallied)
        {
            Inputs.FindAll(input => (input.Index == switcherInput.GetId())).ForEach(input => { input.GreenTally = isTallied; });
        }
        #endregion

        #region Mix/effect block monitors
        private const int MONITORED_MIXEFFECT_BLOCK_INDEX = 0;

        MixEffectBlockMonitor mixEffectBlockMonitor;

        private void getMixEffectBlockMonitor()
        {
            mixEffectBlockMonitor = switcher.GetMixEffectBlockMonitor(MONITORED_MIXEFFECT_BLOCK_INDEX);
            mixEffectBlockMonitor.ProgramInputChanged += mixEffectBlockMonitorProgramInputChangedHandler;
            mixEffectBlockMonitor.PreviewInputChanged += mixEffectBlockMonitorPreviewInputChangedHandler;
        }

        private void disposeMixEffectBlockMonitor()
        {
            mixEffectBlockMonitor?.Dispose();
            mixEffectBlockMonitor = null;
        }

        private void mixEffectBlockMonitorProgramInputChangedHandler(BMDSwitcherAPI.IBMDSwitcherMixEffectBlock meBlock, MixEffectBlockMonitor monitor, long inputIndex)
        {
            OnProgramInput = Inputs.First(input => input.Index == inputIndex);
        }

        private void mixEffectBlockMonitorPreviewInputChangedHandler(BMDSwitcherAPI.IBMDSwitcherMixEffectBlock meBlock, MixEffectBlockMonitor monitor, long inputIndex)
        {
            OnPreviewInput = Inputs.First(input => input.Index == inputIndex);
        }
        #endregion

    }

}