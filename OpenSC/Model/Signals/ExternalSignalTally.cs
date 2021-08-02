using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class ExternalSignalTally : IBidirectionalSignalTally
    {

        public ExternalSignalTally(ExternalSignal parentSignalSource)
        {
            ParentSignalSource = parentSignalSource;
        }

        #region ISignalTallyState implementation
        public ISignalSource ParentSignalSource { get; private set; }

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
            }
        }

        public bool GetState(List<object> recursionChain = null) => state;

        private void updateState()
        {
            State = (activeSources.Count > 0);
        }
        #endregion

        #region ISignalTallyReceiver implementation
        private List<List<ISignalTallySender>> activeSources = new List<List<ISignalTallySender>>();

        public void Give(List<ISignalTallySender> recursionChain)
        {
            if (activeSources.Contains(recursionChain))
                return;
            activeSources.Add(recursionChain);
            updateState();
        }

        public void Revoke(List<ISignalTallySender> recursionChain)
        {
            activeSources.RemoveAll(rc => rc.SequenceEqual(recursionChain));
            updateState();
        }
        #endregion

    }

}
