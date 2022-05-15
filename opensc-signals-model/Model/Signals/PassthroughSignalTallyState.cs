using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class PassthroughSignalTallyState : ObjectBase, ISignalTallyState
    {

        public PassthroughSignalTallyState(ISignalSource parentSignalSource, SignalTallyColor color)
        {
            ParentSignalSource = parentSignalSource;
            Color = color;
        }

        public ISignalSource ParentSignalSource { get; private init; }
        public SignalTallyColor Color { get; private init; }

        #region Property: State
        public bool State => previousElement?.State ?? false;

        public bool GetState(List<object> recursionChain = null)
        {
            if (recursionChain?.Contains(this) ?? false)
                return false; // endless loop
            return previousElement?.GetState(recursionChain.ExtendRecursionChain(this)) ?? false;
        }

        public event StateChangedHandler StateChanged;
        #endregion

        #region Property: PreviousElement
        private ISignalTallyState previousElement = null;

        public ISignalTallyState PreviousElement
        {
            get => previousElement;
            set
            {
                bool previousState = false;
                if (previousElement != null)
                {
                    previousState = previousElement.State;
                    previousElement.StateChanged -= previousElementStateChangedHandler;
                }
                previousElement = value;
                bool newState = false;
                if (previousElement != null)
                {
                    previousElement.StateChanged += previousElementStateChangedHandler;
                    newState = previousElement.State;
                }
                if (previousState != newState)
                {
                    StateChanged?.Invoke(ParentSignalSource, this, newState, RecursionChainHelpers.CreateRecursionChain(this));
                    ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(State));
                }
            }
        }

        private void previousElementStateChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState, List<object> recursionChain)
        {
            if (recursionChain?.Contains(this) == true)
                return;
            StateChanged?.Invoke(ParentSignalSource, this, newState, recursionChain.ExtendRecursionChain(this));
            ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(State));
        }
        #endregion

    }

}
