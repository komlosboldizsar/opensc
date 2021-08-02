using OpenSC.Model.General;
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

        #region Composite elements
        private PassthroughSignalTallyState tallyState;
        private PassthroughSignalTallyReceiver tallyReceiver;
        #endregion

        #region Property: PreviousElement
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
        #endregion

        #region ISignalTallyState interface
        public ISignalSource ParentSignalSource { get; private set; }
        public bool State => tallyState.State;
        public bool GetState(List<object> recursionChain = null) => tallyState.GetState(recursionChain);
        public event StateChangedHandler StateChanged;

        private void tallyStateChanged(ISignalSource signalSource, ISignalTallyState tally, bool newState, List<object> recursionChain)
        {
            StateChanged?.Invoke(signalSource, tally, newState, recursionChain);
            PropertyChanged?.Invoke(nameof(State));
        }

        public event PropertyChangedDelegate PropertyChanged; // INotifyPropertyChanged implementation
        #endregion

        #region ISignalTallyReceiver interface
        public void Give(List<ISignalTallySender> recursionChain) => tallyReceiver.Give(recursionChain);
        public void Revoke(List<ISignalTallySender> recursionChain) => tallyReceiver.Revoke(recursionChain);
        #endregion

        #region ISignalTallySender interface
        public string Label
        {
            get => tallyReceiver.Label;
            set { tallyReceiver.Label = value; }
        }
        #endregion

    }

}
