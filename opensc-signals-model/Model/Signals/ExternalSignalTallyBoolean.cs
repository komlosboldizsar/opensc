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

        public ExternalSignalTallyBoolean(ExternalSignalTally tally) :
            base(tally)
        {
            externalSignal = ((ExternalSignal)tally.ParentSignalSource);
            updateFields();
            externalSignal.IdChanged += signalIdChangedHandler;
            externalSignal.NameChanged += signalNameChangedHandler;
            tally.StateChanged += signalTallyChangedHandler;
            externalSignal.ModelRemoved += signalRemovedHandler;
            if (externalSignal.ID > 0)
                register();
        }

        private ExternalSignal externalSignal;

        private void signalIdChangedHandler(IModel signal, int oldValue, int newValue)
        {
            updateName();
            updateDescription();
            if (!registered)
                register();
        }

        private void signalNameChangedHandler(IModel signal, string oldName, string newName) => updateDescription();
        private void signalTallyChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState, List<object> recursionChain) => CurrentState = newState;
        private void signalRemovedHandler(IModel model) => unregister();

        protected override string getName() => string.Format("signal.{0}.{1}tally", externalSignal.ID, getColorString(color));
        protected override string getDescription() => string.Format("Signal [(#{0}) {1}] has {2} tally.", externalSignal.ID, externalSignal.Name, getColorString(color));

    }

}
