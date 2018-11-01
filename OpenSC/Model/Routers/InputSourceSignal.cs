using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class InputSourceSignal : IRouterInputSource
    {

        public Signal Signal { get; private set; }

        public InputSourceSignal(Signal signal)
        {
            this.Signal = signal;
            signal.NameChanged += nameChangedHandler;
            signal.RedTallyChanged += redTallyChangedHandler;
            signal.GreenTallyChanged += greenTallyChangedHandler;
        }

        public string SourceName
        {
            get => Signal?.Name;
        }

        public event RouterInputSourceSourceNameChanged SourceNameChanged;

        private void nameChangedHandler(Signal signal, string oldName, string newName)
        {
            SourceNameChanged?.Invoke(this, newName);
        }

        public bool RedTally => Signal.RedTally;

        public bool GreenTally => Signal.GreenTally;

        public event RouterInputSourceTallyChanged RedTallyChanged;
        public event RouterInputSourceTallyChanged GreenTallyChanged;

        private void redTallyChangedHandler(Signal signal, bool oldState, bool newState)
        {
            RedTallyChanged?.Invoke(this, newState);
        }

        private void greenTallyChangedHandler(Signal signal, bool oldState, bool newState)
        {
            GreenTallyChanged?.Invoke(this, newState);
        }

    }

}
