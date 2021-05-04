using BMD.Switcher;
using BMD.Switcher.Exceptions;
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

            //disposeInputMonitors();
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
            catch (CouldNotConnectException ex)
            {
                string errorMessage = string.Format("Couldn't connect to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}. Exception message: [{2}]",
                    ID,
                    IpAddress,
                    ex.Message);
                LogDispatcher.E(LOG_TAG, errorMessage);
            }
            catch(AlreadyConnectedException ex)
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
            catch (NotConnectedException ex)
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
                    //disposeInputMonitors();
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

        #region Sources
        private void getInputMonitors()
        {
            foreach(Source source in switcher.GetSources().Values)
            {
                source.IsProgramTalliedChanged += sourceIsProgramTalliedChangedHandler;
                source.IsPreviewTalliedChanged += sourceIsPreviewTalliedChangedHandler;
            }
        }

        private void sourceIsProgramTalliedChangedHandler(BMDSwitcherAPI.IBMDSwitcherInput apiSource, Source source, bool isTallied)
        {
            Inputs.FindAll(input => (input.Index == source.ID)).ForEach(input => { input.RedTally = isTallied; });
        }

        private void sourceIsPreviewTalliedChangedHandler(BMDSwitcherAPI.IBMDSwitcherInput apiSource, Source source, bool isTallied)
        {
            Inputs.FindAll(input => (input.Index == source.ID)).ForEach(input => { input.GreenTally = isTallied; });
        }
        #endregion

        #region Mix/effect block monitors
        private const int MONITORED_MIXEFFECT_BLOCK_INDEX = 0;

        MixEffectBlock mixEffectBlock;

        private void getMixEffectBlockMonitor()
        {
            mixEffectBlock = switcher.GetMixEffectBlock(MONITORED_MIXEFFECT_BLOCK_INDEX);
            mixEffectBlock.ProgramInputChanged += mixEffectBlockMonitorProgramInputChangedHandler;
            mixEffectBlock.PreviewInputChanged += mixEffectBlockMonitorPreviewInputChangedHandler;
        }

        private void disposeMixEffectBlockMonitor()
        {
            mixEffectBlock?.Dispose();
            mixEffectBlock = null;
        }

        private void mixEffectBlockMonitorProgramInputChangedHandler(BMDSwitcherAPI.IBMDSwitcherMixEffectBlock apiMixEffectBlock, MixEffectBlock mixEffectBlock, long sourceId)
        {
            OnProgramInput = Inputs.First(input => input.Index == sourceId);
        }

        private void mixEffectBlockMonitorPreviewInputChangedHandler(BMDSwitcherAPI.IBMDSwitcherMixEffectBlock apiMixEffectBlock, MixEffectBlock mixEffectBlock, long sourceId)
        {
            OnPreviewInput = Inputs.First(input => input.Index == sourceId);
        }
        #endregion

        #region P/P input sources
        public void SetProgramSource(int meBlockIndex, int inputId)
            => switcher.GetMixEffectBlock(meBlockIndex).RequestSetProgramSource(inputId);

        public void SetPreviewSource(int meBlockIndex, int inputId)
            => switcher.GetMixEffectBlock(meBlockIndex).RequestSetPreviewSource(inputId);
        #endregion

        #region Transitions
        public void AutoTransition(int meBlockIndex)
            => switcher.GetMixEffectBlock(meBlockIndex).PerformAutoTransition();

        public void CutTransition(int meBlockIndex)
            => switcher.GetMixEffectBlock(meBlockIndex).PerformAutoTransition();
        #endregion

    }

}