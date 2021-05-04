using BMD.Switcher.Exceptions;
using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BMD.Switcher
{

    public class Switcher : IBMDSwitcherCallback
    {

        public Switcher(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public IBMDSwitcher ApiSwitcher { get; set; }

        #region Property: IpAddress
        private string ipAddress;

        public string IpAddress
        {
            get => ipAddress;
            set
            {
                if (value == ipAddress)
                    return;
                bool wasConnected = Connected;
                if (wasConnected)
                    Disconnect();
                ipAddress = value;
                if (wasConnected)
                    Connect();
            }
        }
        #endregion

        #region Property: Connected
        private bool connected;

        public bool Connected
        {
            get => connected;
            private set
            {
                if (value == connected)
                    return;
                connected = value;
                ConnectionStateChanged?.Invoke(this, value);
            }
        }

        public delegate void ConnectionStateChangedDelegate(Switcher switcher, bool newState);
        public event ConnectionStateChangedDelegate ConnectionStateChanged;
        #endregion

        #region Connect, disconnect
        public void Connect()
        {

            if (Connected)
                throw new AlreadyConnectedException();

            IBMDSwitcherDiscovery switcherDiscovery = new CBMDSwitcherDiscovery();
            if(switcherDiscovery == null)
                throw new CouldNotConnectException("Could not create Switcher Discovery Instance. ATEM Switcher Software may not be installed.");

            _BMDSwitcherConnectToFailure failReason = 0;
            try
            {
                switcherDiscovery.ConnectTo(ipAddress, out IBMDSwitcher connectedSwitcher, out failReason);
                switcherConnectedHandler(connectedSwitcher);
            }
            catch (COMException ex)
            {
                switch (failReason)
                {
                    case _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureNoResponse:
                        throw new CouldNotConnectException("No response from switcher.", ex);
                    case _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureIncompatibleFirmware:
                        throw new CouldNotConnectException("Incompatible firmware.", ex);
                    default:
                        throw new CouldNotConnectException("Unknown reason of connection failure.", ex);
                }
            }

        }

        public void Disconnect()
        {
            if (!Connected)
                throw new NotConnectedException();
            switcherDisconnectedHandler();
        }

        private void switcherConnectedHandler(IBMDSwitcher connectedSwitcher)
        {

            ApiSwitcher = connectedSwitcher;
            ApiSwitcher.AddCallback(this);
            Connected = true;

            mixEffectBlocks.Clear();
            int mixEffectBlockIndex = 0;
            foreach (IBMDSwitcherMixEffectBlock apiMixEffectBlock in ApiSwitcher.GetAllMixEffectBlocks())
            {
                mixEffectBlocks.Add(new MixEffectBlock(this, apiMixEffectBlock, mixEffectBlockIndex));
                mixEffectBlockIndex++;
            }

            sources.Clear();
            foreach (IBMDSwitcherInput apiSource in ApiSwitcher.GetSources())
            {
                Source source = new Source(this, apiSource);
                sources.Add(source.ID, source);
            }

        }

        private void switcherDisconnectedHandler()
        {
            Connected = false;
            ApiSwitcher = null;
            mixEffectBlocks.Clear();
            sources.Clear();
        }

        void IBMDSwitcherCallback.Notify(_BMDSwitcherEventType eventType, _BMDSwitcherVideoMode coreVideoMode)
        {
            switch (eventType)
            {
                case _BMDSwitcherEventType.bmdSwitcherEventTypeDisconnected:
                    switcherDisconnectedHandler();
                    break;
            }
        }
        #endregion

        #region MixEffect Blocks
        private List<MixEffectBlock> mixEffectBlocks = new List<MixEffectBlock>();

        public MixEffectBlock GetMixEffectBlock(int index)
        {
            if (index >= mixEffectBlocks.Count)
                throw new NotExistingMixEffectBlockException();
            return mixEffectBlocks[index];
        }
        #endregion

        #region MixEffect Blocks
        private Dictionary<long, Source> sources = new Dictionary<long, Source>();

        public Source GetSource(long id)
        {
            if (!sources.TryGetValue(id, out Source source))
                throw new NotExistingSourceException();
            return source;
        }

        public Dictionary<long, Source> GetSources()
            => sources;
        #endregion

    }

}
