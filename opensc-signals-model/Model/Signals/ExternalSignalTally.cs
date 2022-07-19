using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class ExternalSignalTally : ObjectBase, IBidirectionalSignalTally
    {

        public ExternalSignalTally(ExternalSignal parentSignalSource, SignalTallyColor color)
        {
            ParentSignalSource = parentSignalSource;
            Color = color;
        }

        public ISignalSource ParentSignalSource { get; private init; }
        public SignalTallyColor Color { get; private init; }

        #region ISignalTallyState implementation

        public event StateChangedHandler StateChanged;

        private bool state = false;

        public bool State
        {
            get => state;
            private set
            {
                if (value == state)
                    return;
                state = value;
                StateChanged?.Invoke(ParentSignalSource, this, value, RecursionChainHelpers.CreateRecursionChain(this));
                RaisePropertyChanged(nameof(State));
            }
        }

        public bool GetState(List<object> recursionChain = null) => state;

        private void updateState() => State = (activeSources.Count > 0);
        #endregion

        #region ISignalTallyReceiver implementation
        private List<List<ISignalTallySender>> activeSources = new List<List<ISignalTallySender>>();

        public void Give(List<ISignalTallySender> recursionChain)
        {
            if (activeSources.Contains(recursionChain))
                return;
            activeSources.Add(recursionChain);
            updateState();
            Got?.Invoke(this, recursionChain);
        }

        public void Revoke(List<ISignalTallySender> recursionChain)
        {
            activeSources.RemoveAll(rc => rc.SequenceEqual(recursionChain));
            updateState();
            Revoked?.Invoke(this, recursionChain);
        }

        public event SignalTallyReceiverGotTally Got;
        public event SignalTallyReceiverRevokedTally Revoked;

        public List<List<ISignalTallySender>> CurrentRecursionChains => activeSources;
        #endregion

    }

}
