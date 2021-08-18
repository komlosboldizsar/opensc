using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Triggers
{

    [MacroTrigger("Routers.OutputInputChanged", "Input delegated to a router output changed", "Observe a single output for input change on a router.")]
    class RouterOutputInputChangedMacroTrigger : MacroTriggerBase<RouterOutputInputChangedMacroTrigger.ActivationData>
    {

        [MacroTriggerArgument(0, "Router", "The router to observe.")]
        public class Arg0 : MacroTriggerArgumentDatabaseItem<Router>
        {
            public Arg0() : base(RouterDatabase.Instance)
            { }
        }

        [MacroTriggerArgument(1, "Output", "Output of the selected router to observe.")]
        public class Arg1 : MacroTriggerArgumentBase
        {
            public Arg1() : base(typeof(RouterOutput), MacroArgumentKeyType.Integer)
            { }
            protected override object _getObjectByKey(string key, object[] previousArgumentObjects)
            {
                if (!int.TryParse(key, out int keyInt))
                    return null;
                return (previousArgumentObjects[0] as Router)?.GetOutput(keyInt);
            }
            protected override IEnumerable<object> _getPossibilities(object[] previousArgumentObjects) => (previousArgumentObjects[0] as Router)?.Outputs;
        }

        internal class ActivationData : MacroTriggerWithArgumentsActivationData
        {
            public RouterOutput RouterOutput { get; private set; }
            public RouterOutput.CurrentInputChangedDelegate RouterOutputCurrentInputChangedHandler { get; private set; }
            public ActivationData(RouterOutput routerOutput, RouterOutput.CurrentInputChangedDelegate routerOutputCurrentInputChangedHandler)
            {
                RouterOutput = routerOutput;
                RouterOutputCurrentInputChangedHandler = routerOutputCurrentInputChangedHandler;
            }
        }

        protected override void _activate(MacroTriggerWithArguments triggerWithArguments)
        {
            object[] argumentObjects = triggerWithArguments.ArgumentObjects;
            Router router = argumentObjects[0] as Router;
            if (router == null)
                return;
            RouterOutput routerOutput = argumentObjects[1] as RouterOutput;
            if (routerOutput == null)
                return;
            RouterOutput.CurrentInputChangedDelegate routerOutputCurrentInputChangedHandler = (ro, ri) => triggerWithArguments.Fire();
            routerOutput.CurrentInputChanged += routerOutputCurrentInputChangedHandler;
            ActivationData activationData = new ActivationData(routerOutput, routerOutputCurrentInputChangedHandler);
            triggerWithArguments.Activated(activationData);
        }

        protected override void _deactivate(MacroTriggerWithArguments triggerWithArguments, ActivationData activationData)
        {
            activationData.RouterOutput.CurrentInputChanged -= activationData.RouterOutputCurrentInputChangedHandler;
            triggerWithArguments.Deactivated();
        }

        protected override string _humanReadable(object[] argumentObjects)
        {
            Router router = argumentObjects[0] as Router;
            if (router == null)
                return null;
            RouterOutput routerOutput = argumentObjects[1] as RouterOutput;
            if (routerOutput == null)
                return null;
            return string.Format("Source for output [{0}] changed on router [{1}].", routerOutput, router);
        }

    }

}
