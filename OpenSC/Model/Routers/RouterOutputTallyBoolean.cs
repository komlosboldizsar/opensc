using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterOutputTallyBoolean : SignalTallyBoolean
    {

        private RouterOutput routerOutput;

        public RouterOutputTallyBoolean(RouterOutput routerOutput, ISignalTallyState tally, SignalTallyColor color) :
            base(tally, color)
        {
            this.routerOutput = routerOutput;
            updateFields();
            routerOutput.Router.IdChanged += routerIdChangedHandler;
            routerOutput.Router.NameChanged += routerNameChangedHandler;
            routerOutput.IndexChanged += routerOutputIndexChangedHandler;
            routerOutput.NameChanged += routerOutputNameChangedHandler;
            tally.StateChanged += signalTallyChangedHandler;
            routerOutput.Removed += routerOutputRemovedHandler;
            register();
        }

        private void routerNameChangedHandler(Router router, string oldName, string newName)
            => updateDescription();

        private void routerIdChangedHandler(IModel router, int oldValue, int newValue)
        {
            updateName();
            updateDescription();
        }

        private void routerOutputNameChangedHandler(RouterOutput output, string oldName, string newName)
            => updateDescription();

        private void routerOutputIndexChangedHandler(RouterOutput input, int oldIndex, int newIndex)
        {
            updateName();
            updateDescription();
        }

        private void signalTallyChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState, List<object> recursionChain)
            => CurrentState = newState;

        private void routerOutputRemovedHandler(RouterOutput routerOutput)
            => unregister();

        protected override string getName()
            => string.Format("router.{0}.output.{1}.{2}tally", routerOutput.Router.ID, routerOutput.Index, getColorString(color));

        protected override string getDescription()
            => string.Format("Output [(#{0}) {1}] of router [(#{2}) {3}] has {4} tally.", routerOutput.Index, routerOutput.Name, routerOutput.Router.ID, routerOutput.Router.Name, getColorString(color));

    }

}
