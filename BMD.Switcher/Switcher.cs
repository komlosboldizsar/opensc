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
            foreach (IBMDSwitcherInput apiPort in ApiSwitcher.GetPorts())
            {
                apiPort.GetPortType(out _BMDSwitcherPortType portType);
                switch (portType)
                {
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeExternal:
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeBlack:
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeColorBars:
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeColorGenerator:
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeMediaPlayerFill:
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeMediaPlayerCut:
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeSuperSource:
                        Source source = new Source(this, apiPort);
                        sources.Add(source.ID, source);
                        break;
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeAuxOutput:
                        AuxOutput auxOutput = new AuxOutput(this, apiPort as IBMDSwitcherInputAux, 0);
                        break;
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeKeyCutOutput:
                        break;
                    case _BMDSwitcherPortType.bmdSwitcherPortTypeMixEffectBlockOutput:
                        break;
                }
                
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

        #region MixEffect blocks
        private List<MixEffectBlock> mixEffectBlocks = new List<MixEffectBlock>();

        public MixEffectBlock GetMixEffectBlock(int index)
        {
            if (index >= mixEffectBlocks.Count)
                throw new NotExistingMixEffectBlockException();
            return mixEffectBlocks[index];
        }
        #endregion

        #region Sources (external inputs, media players, etc.)
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

        #region Aux outputs
        private List<AuxOutput> auxOutputs = new List<AuxOutput>();

        public AuxOutput GetAuxOutput(int index)
        {
            if (index >= auxOutputs.Count)
                throw new NotExistingAuxOutputException();
            return auxOutputs[index];
        }
        #endregion

    }

}
