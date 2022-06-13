using BMD.Switcher;
using BMD.Switcher.Exceptions;
using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign
{

    [TypeLabel("BMD Switcher")]
    [TypeCode("bmd")]
    public class BmdMixer : Mixer
    {

        private const string LOG_TAG = "Mixer/BMD";

        public BmdMixer()
        {
            initSwitcher();
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            initSwitcher();
            Connect();
        }

        public override void Removed()
        {

            base.Removed();

            Disconnect();
            deinitSwitcher();
            ApiSwitcher = null;

            IpAddressChanged = null;
            AutoReconnectChanged = null;
            ConnectionStateChanged = null;

            //disposeInputMonitors();
            disposeMixEffectBlockMonitor();

            autoReconnectThreadWorking = false;
            autoReconnectThread = null;

        }

        public void Connect()
        {
            try
            {
                ApiSwitcher?.Connect();
            }
            catch (CouldNotConnectException ex)
            {
                string errorMessage = string.Format("Couldn't connect to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}. Exception message: [{2}]",
                    ID, IpAddress, ex.Message);
                LogDispatcher.E(LOG_TAG, errorMessage);
            }
            catch(AlreadyConnectedException ex)
            {
                string errorMessage = string.Format("Tried to connect to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}, but was already connected. Exception message: [{2}]",
                    ID, IpAddress, ex.Message);
                LogDispatcher.W(LOG_TAG, errorMessage);
            }
        }

        public void Disconnect()
        {
            try
            {
                ApiSwitcher?.Disconnect();
            }
            catch (NotConnectedException ex)
            {
                string errorMessage = string.Format("Tried to disconnect from a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}, but wasn't connected. Exception message: [{2}]",
                    ID, IpAddress, ex.Message);
                LogDispatcher.W(LOG_TAG, errorMessage);
            }
        }

        public Switcher ApiSwitcher { get; private set; }

        private void initSwitcher()
        {
            ApiSwitcher = new Switcher(ipAddress);
            ApiSwitcher.ConnectionStateChanged += switcherConnectionStateChanged;
            State = MixerState.Warning;
            StateString = "disconnected";
        }

        private void deinitSwitcher()
        {
            ApiSwitcher.ConnectionStateChanged -= switcherConnectionStateChanged;
        }

        private void switcherConnectionStateChanged(Switcher switcher, bool newState)
            => ConnectionState = newState;

        #region Property: IpAddress
        public event PropertyChangedTwoValuesDelegate<BmdMixer, string> IpAddressChanged;

        private string ipAddress;

        [PersistAs("ip_address")]
        public string IpAddress
        {
            get => ipAddress;
            set => this.setProperty(ref ipAddress, value, IpAddressChanged, null, (ov, nv) => ApiSwitcher.IpAddress = nv, ValidateIpAddress);
        }

        public void ValidateIpAddress(string ipAddress)
        {
            // ... throw new ArgumentException();
        }
        #endregion

        #region Property: ConnectionState
        public event PropertyChangedTwoValuesDelegate<BmdMixer, bool> ConnectionStateChanged;

        private bool connectionState;

        public bool ConnectionState
        {
            get => connectionState;
            set
            {
                AfterChangePropertyDelegate<bool> afterChangeDelegate = (ov, nv) =>
                {
                    if (nv)
                    {
                        getMixEffectBlockMonitor();
                        getInputMonitors();
                        State = MixerState.Ok;
                        StateString = "connected";
                        string logMessage = string.Format("Connected to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}.", ID, IpAddress);
                        LogDispatcher.I(LOG_TAG, logMessage);
                    }
                    else
                    {
                        disposeMixEffectBlockMonitor();
                        //disposeInputMonitors();
                        deinitSwitcher();
                        State = MixerState.Warning;
                        StateString = "disconnected";
                        string logMessage = string.Format("Disconnected from a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}.", ID, IpAddress);
                        LogDispatcher.I(LOG_TAG, logMessage);

                    }
                };
                this.setProperty(ref connectionState, value, ConnectionStateChanged, null, afterChangeDelegate);             
            }
        }
        #endregion

        #region Property: AutoReconnect
        public event PropertyChangedTwoValuesDelegate<BmdMixer, bool> AutoReconnectChanged;

        private bool autoReconnect;

        [PersistAs("auto_reconnect")]
        public bool AutoReconnect
        {
            get => autoReconnect;
            set => this.setProperty(ref autoReconnect, value, AutoReconnectChanged);
        }
        #endregion

        #region Auto reconnect
        private const int RECONNECT_TRY_INTERVAL = 10000;

        private Thread autoReconnectThread = null;
        private bool autoReconnectThreadWorking = false;

        private void startAutoReconnectThread()
        {
            autoReconnectThread = new Thread(autoReconnectThreadMethod)
            {
                IsBackground = true
            };
            autoReconnectThreadWorking = true;
            autoReconnectThread.Start();
        }

        private void autoReconnectThreadMethod()
        {

            string logMessage = string.Format("Trying auto reconnect to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}...", ID, IpAddress);
            LogDispatcher.I(LOG_TAG, logMessage);

            if (autoReconnect && !connectionState)
                Connect();
            while (autoReconnectThreadWorking && autoReconnect && !connectionState)
            {
                Thread.Sleep(RECONNECT_TRY_INTERVAL);
                if (autoReconnect && !connectionState)
                    Connect();
            }

        }
        #endregion

        #region Sources
        private void getInputMonitors()
        {
            foreach(Source source in ApiSwitcher.GetSources().Values)
            {
                source.IsProgramTalliedChanged += sourceIsProgramTalliedChangedHandler;
                source.IsPreviewTalliedChanged += sourceIsPreviewTalliedChangedHandler;
                Inputs.Findall(input => (input.Index == source.ID)).Foreach(input => { input.RedTally = source.IsProgramTallied; });
                Inputs.Findall(input => (input.Index == source.ID)).Foreach(input => { input.GreenTally = source.IsPreviewTallied; });
            }
        }

        private void sourceIsProgramTalliedChangedHandler(BMDSwitcherAPI.IBMDSwitcherInput apiSource, Source source, bool isTallied)
            => Inputs.Findall(input => (input.Index == source.ID)).Foreach(input => { input.RedTally = isTallied; });

        private void sourceIsPreviewTalliedChangedHandler(BMDSwitcherAPI.IBMDSwitcherInput apiSource, Source source, bool isTallied)
            => Inputs.Findall(input => (input.Index == source.ID)).Foreach(input => { input.GreenTally = isTallied; });
        #endregion

        #region Mix/effect block monitors
        private const int MONITORED_MIXEFFECT_BLOCK_INDEX = 0;

        MixEffectBlock mixEffectBlock;

        private void getMixEffectBlockMonitor()
        {
            mixEffectBlock = ApiSwitcher.GetMixEffectBlock(MONITORED_MIXEFFECT_BLOCK_INDEX);
            mixEffectBlock.ProgramInputChanged += mixEffectBlockMonitorProgramInputChangedHandler;
            mixEffectBlock.PreviewInputChanged += mixEffectBlockMonitorPreviewInputChangedHandler;
            OnProgramInput = Inputs.FirstOrDefault(input => input.Index == mixEffectBlock.ProgramSourceId);
            OnPreviewInput = Inputs.FirstOrDefault(input => input.Index == mixEffectBlock.PreviewSourceId);
        }

        private void disposeMixEffectBlockMonitor()
        {
            mixEffectBlock?.Dispose();
            mixEffectBlock = null;
        }

        private void mixEffectBlockMonitorProgramInputChangedHandler(BMDSwitcherAPI.IBMDSwitcherMixEffectBlock apiMixEffectBlock, MixEffectBlock mixEffectBlock, long sourceId)
            => OnProgramInput = Inputs.FirstOrDefault(input => input.Index == sourceId);

        private void mixEffectBlockMonitorPreviewInputChangedHandler(BMDSwitcherAPI.IBMDSwitcherMixEffectBlock apiMixEffectBlock, MixEffectBlock mixEffectBlock, long sourceId)
            => OnPreviewInput = Inputs.FirstOrDefault(input => input.Index == sourceId);
        #endregion

        #region P/P input sources
        public void SetProgramSource(int meBlockIndex, int inputId)
            => ApiSwitcher.GetMixEffectBlock(meBlockIndex).RequestSetProgramSource(inputId);

        public void SetPreviewSource(int meBlockIndex, int inputId)
            => ApiSwitcher.GetMixEffectBlock(meBlockIndex).RequestSetPreviewSource(inputId);
        #endregion

        #region Transitions
        public void AutoTransition(int meBlockIndex)
            => ApiSwitcher.GetMixEffectBlock(meBlockIndex).PerformAutoTransition();

        public void CutTransition(int meBlockIndex)
            => ApiSwitcher.GetMixEffectBlock(meBlockIndex).PerformAutoTransition();

        public void FadeToBlack(int meBlockIndex)
            => ApiSwitcher.GetMixEffectBlock(meBlockIndex).PerformFadeToBlack();
        #endregion

    }

}