using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class ExternalSignalTallyBoolean : SignalTallyBoolean
    {

        private ExternalSignal signal;

        public ExternalSignalTallyBoolean(ExternalSignal signal, ExternalSignalTally tally, SignalTallyColor color) :
            base(tally, color)
        {
            this.signal = signal;
            updateFields();
            signal.IdChanged += signalIdChangedHandler;
            signal.NameChanged += signalNameChangedHandler;
            tally.StateChanged += signalTallyChangedHandler;
            signal.ModelRemoved += signalRemovedHandler;
            if (signal.ID > 0)
                register();
        }

        private void signalIdChangedHandler(ExternalSignal signal, int oldValue, int newValue)
        {
            updateName();
            updateDescription();
            if (!registered)
                register();
        }

        private void signalNameChangedHandler(ExternalSignal signal, string oldName, string newName)
            => updateDescription();

        private void signalTallyChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState, List<object> recursionChain)
            => CurrentState = newState;

        private void signalRemovedHandler(IModel model)
            => unregister();

        protected override string getName()
            => string.Format("signal.{0}.{1}tally", signal.ID, getColorString(color));

        protected override string getDescription()
            => string.Format("Signal [(#{0}) {1}] has {2} tally.", signal.ID, signal.Name, getColorString(color));

    }

}
