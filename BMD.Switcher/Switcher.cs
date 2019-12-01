using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BMD.Switcher
{

    public class Switcher
    {

        public Switcher(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

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
                switcher = connectedSwitcher;
                Connected = true;
                switcherMonitor = new SwitcherMonitor(switcher);
                switcherMonitor.SwitcherDisconnected += switcherDisconnectedHandler;
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
            Connected = false;
            switcherMonitor?.Dispose();
            switcherMonitor = null;
            switcher = null;
        }

        private void switcherDisconnectedHandler(IBMDSwitcher switcher, SwitcherMonitor monitor)
        {
            Connected = false;
            switcherMonitor?.Dispose();
            switcherMonitor = null;
            switcher = null;
        }
        #endregion

        #region Transitions
        public void TransitionAuto(int meBlockIndex)
        {
            IBMDSwitcherMixEffectBlock meBlock = switcher.GetMixEffectBlock(meBlockIndex);
            if (meBlock == null)
                throw new NotExistingMEException(string.Format("Switcher has no M/E block with index #{0}!", meBlockIndex));
            meBlock.PerformAutoTransition();
        }

        public void TransitionCut(int meBlockIndex)
        {
            IBMDSwitcherMixEffectBlock meBlock = switcher.GetMixEffectBlock(meBlockIndex);
            if (meBlock == null)
                throw new NotExistingMEException(string.Format("Switcher has no M/E block with index #{0}!", meBlockIndex));
            meBlock.PerformCut();
        }
        #endregion

        #region Set program, preset/preview
        public void SetProgramSource(int meBlockIndex, int inputId)
        {
            IBMDSwitcherMixEffectBlock meBlock = switcher.GetMixEffectBlock(meBlockIndex);
            if (meBlock == null)
                throw new NotExistingMEException(string.Format("Switcher has no M/E block with index #{0}!", meBlockIndex));
            IBMDSwitcherInput input = switcher.GetInput(inputId);
            if (input == null)
                throw new NotExistingInputException(string.Format("Switcher has no input with ID #{0}!", inputId));
            meBlock.SetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput, inputId);
        }

        public void SetPreviewSource(int meBlockIndex, int inputId)
        {
            IBMDSwitcherMixEffectBlock meBlock = switcher.GetMixEffectBlock(meBlockIndex);
            if (meBlock == null)
                throw new NotExistingMEException(string.Format("Switcher has no M/E block with index #{0}!", meBlockIndex));
            IBMDSwitcherInput input = switcher.GetInput(inputId);
            if (input == null)
                throw new NotExistingInputException(string.Format("Switcher has no input with ID #{0}!", inputId));
            meBlock.SetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput, inputId);
        }
        #endregion

        private IBMDSwitcher switcher;
        private SwitcherMonitor switcherMonitor;

        public InputMonitor GetInputMonitor(long inputId)
        {
            if (!Connected)
                throw new NotConnectedException();
            return new InputMonitor(switcher.GetInput(inputId));
        }

        public List<InputMonitor> GetInputMonitors()
        {
            if (!Connected)
                throw new NotConnectedException();
            List<InputMonitor> inputMonitors = new List<InputMonitor>();
            switcher.GetInputs().ForEach(input => inputMonitors.Add(new InputMonitor(input)));
            return inputMonitors;
        }

        public MixEffectBlockMonitor GetMixEffectBlockMonitor(int index)
        {
            if (!Connected)
                throw new NotConnectedException();
            return new MixEffectBlockMonitor(switcher.GetMixEffectBlock(index));
        }

        #region Exceptions
        public class AlreadyConnectedException : Exception
        {

            public AlreadyConnectedException()
            { }

            public AlreadyConnectedException(string message) : base(message)
            { }

            public AlreadyConnectedException(string message, Exception innerException) : base(message, innerException)
            { }

        }

        public class NotConnectedException : Exception
        {

            public NotConnectedException()
            { }

            public NotConnectedException(string message) : base(message)
            { }

            public NotConnectedException(string message, Exception innerException) : base(message, innerException)
            { }

        }

        public class CouldNotConnectException : Exception
        {

            public CouldNotConnectException()
            { }

            public CouldNotConnectException(string message) : base(message)
            { }

            public CouldNotConnectException(string message, Exception innerException) : base(message, innerException)
            { }

        }

        public class NotExistingMEException : Exception
        {

            public NotExistingMEException()
            { }

            public NotExistingMEException(string message) : base(message)
            { }

            public NotExistingMEException(string message, Exception innerException) : base(message, innerException)
            { }

        }

        public class NotExistingInputException : Exception
        {

            public NotExistingInputException()
            { }

            public NotExistingInputException(string message) : base(message)
            { }

            public NotExistingInputException(string message, Exception innerException) : base(message, innerException)
            { }

        }
        #endregion

    }

}
