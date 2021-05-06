using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class BidirectionalPassthroughSignalTally : IBidirectionalSignalTally, ISignalTallySender
    {

        public BidirectionalPassthroughSignalTally(ISignalSource parentSignalSource)
        {
            ParentSignalSource = parentSignalSource;
            tallyState = new PassthroughSignalTallyState(ParentSignalSource);
            tallyReceiver = new PassthroughSignalTallyReceiver();
            tallyState.StateChanged += tallyStateChanged;
        }

        public ISignalSource ParentSignalSource { get; private set; }

        #region Composite elements
        private PassthroughSignalTallyState tallyState;
        private PassthroughSignalTallyReceiver tallyReceiver;
        #endregion

        private ISignalTallyStateOrReceiver previousElement;

        public ISignalTallyStateOrReceiver PreviousElement
        {
            get => previousElement;
            set
            {
                previousElement = value;
                tallyState.PreviousElement = value as ISignalTallyState;
                tallyReceiver.PreviousElement = value as ISignalTallyReceiver;
            }
        }

        public bool State => tallyState.State;

        public bool GetState(List<object> recursionChain = null)
            => tallyState.GetState(recursionChain);

        public event StateChangedHandler StateChanged;

        private void tallyStateChanged(ISignalSource signalSource, ISignalTallyState tally, bool newState)
            => StateChanged?.Invoke(signalSource, tally, newState);

        public void Give(List<ISignalTallySender> recursionChain)
            => tallyReceiver.Give(recursionChain);

        public void Revoke(List<ISignalTallySender> recursionChain)
            => tallyReceiver.Revoke(recursionChain);

        public string Label
        {
            get => tallyReceiver.Label;
            set { tallyReceiver.Label = value; }
        }

    }

}
