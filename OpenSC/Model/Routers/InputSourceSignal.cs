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

        public string GetSourceName(List<object> recursionChain = null)
        {
            return SourceName;
        }

        public event RouterInputSourceSourceNameChanged SourceNameChanged;

        private void nameChangedHandler(Signal signal, string oldName, string newName)
        {
            SourceNameChanged?.Invoke(this, newName);
        }

        public bool RedTally => Signal.RedTally;

        public bool GreenTally => Signal.GreenTally;

        public bool GetRedTally(List<object> recursionChain)
        {
            return RedTally;
        }

        public bool GetGreenTally(List<object> recursionChain)
        {
            return GreenTally;
        }

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

        public override bool Equals(object obj)
        {
            InputSourceSignal objCasted = obj as InputSourceSignal;
            if (objCasted == null)
                return false;
            return (objCasted.Signal == Signal);
        }

        public override int GetHashCode()
        {
            var hashCode = -1518684056;
            hashCode = hashCode * -1521134295 + EqualityComparer<Signal>.Default.GetHashCode(Signal);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SourceName);
            hashCode = hashCode * -1521134295 + RedTally.GetHashCode();
            hashCode = hashCode * -1521134295 + GreenTally.GetHashCode();
            return hashCode;
        }

    }

}
