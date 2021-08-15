using BMD.Switcher;
using BMD.Switcher.Exceptions;
using OpenSC.Logger;
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
            switcher = null;

            IpAddressChanged = null;
            AutoReconnectChanged = null;
            ConnectionStateChanged = null;

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
                switcher?.Disconnect();
            }
            catch (NotConnectedException ex)
            {
                string errorMessage = string.Format("Tried to disconnect from a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}, but wasn't connected. Exception message: [{2}]",
                    ID, IpAddress, ex.Message);
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
            => ConnectionState = newState;

        #region Property: IpAddress
        public event PropertyChangedTwoValuesDelegate<BmdMixer, string> IpAddressChanged;

        [PersistAs("ip_address")]
        private string ipAddress;

        public string IpAddress
        {
            get => ipAddress;
            set
            {
                ValidateIpAddress(ipAddress);
                setProperty(this, ref ipAddress, value, IpAddressChanged, null, (ov, nv) => switcher.IpAddress = nv);
            }
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
                setProperty(this, ref connectionState, value, ConnectionStateChanged, null, afterChangeDelegate);             
            }
        }
        #endregion

        #region Property: AutoReconnect
        public event PropertyChangedTwoValuesDelegate<BmdMixer, bool> AutoReconnectChanged;

        [PersistAs("auto_reconnect")]
        private bool autoReconnect;

        public bool AutoReconnect
        {
            get => autoReconnect;
            set => setProperty(this, ref autoReconnect, value, AutoReconnectChanged);
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

            string logMessage = string.Format("Trying auto reconnect to a BlackMagic Design mixer/switcher (ID: {0}) with IP {1}...", ID, IpAddress);
            LogDispatcher.I(LOG_TAG, logMessage);

            if (autoReconnect && !connectionState)
                Connect();
            while (autoReconnect && !connectionState)
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
            foreach(Source source in switcher.GetSources().Values)
            {
                source.IsProgramTalliedChanged += sourceIsProgramTalliedChangedHandler;
                source.IsPreviewTalliedChanged += sourceIsPreviewTalliedChangedHandler;
                Inputs.FindAll(input => (input.Index == source.ID)).ForEach(input => { input.RedTally = source.IsProgramTallied; });
                Inputs.FindAll(input => (input.Index == source.ID)).ForEach(input => { input.GreenTally = source.IsPreviewTallied; });
            }
        }

        private void sourceIsProgramTalliedChangedHandler(BMDSwitcherAPI.IBMDSwitcherInput apiSource, Source source, bool isTallied)
            => Inputs.FindAll(input => (input.Index == source.ID)).ForEach(input => { input.RedTally = isTallied; });

        private void sourceIsPreviewTalliedChangedHandler(BMDSwitcherAPI.IBMDSwitcherInput apiSource, Source source, bool isTallied)
            => Inputs.FindAll(input => (input.Index == source.ID)).ForEach(input => { input.GreenTally = isTallied; });
        #endregion

        #region Mix/effect block monitors
        private const int MONITORED_MIXEFFECT_BLOCK_INDEX = 0;

        MixEffectBlock mixEffectBlock;

        private void getMixEffectBlockMonitor()
        {
            mixEffectBlock = switcher.GetMixEffectBlock(MONITORED_MIXEFFECT_BLOCK_INDEX);
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