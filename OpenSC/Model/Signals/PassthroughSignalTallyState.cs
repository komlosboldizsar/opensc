using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class PassthroughSignalTallyState : ISignalTallyState
    {

        public PassthroughSignalTallyState(ISignalSource parentSignalSource)
        {
            ParentSignalSource = parentSignalSource;
        }

        public ISignalSource ParentSignalSource { get; private set; }

        public bool State => previousElement?.State ?? false;

        public bool GetState(List<object> recursionChain = null)
        {
            if (recursionChain?.Contains(this) ?? false)
                return false; // endless loop
            if (recursionChain == null)
                recursionChain = new List<object>();
            recursionChain.Add(this);
            return previousElement?.GetState(recursionChain) ?? false;
        }

        public event StateChangedHandler StateChanged;

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
                    StateChanged?.Invoke(ParentSignalSource, this, newState);
            }
        }

        private void previousElementStateChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState)
            => StateChanged?.Invoke(ParentSignalSource, this, newState);

    }

}
