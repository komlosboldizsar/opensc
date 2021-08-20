using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Triggers
{

    [MacroTrigger("Routers.CrosspointChanged", "Crosspoint change on a router", "Observe all outputs for crosspoint change on a router.")]
    class RouterCrosspointChangedMacroTrigger : MacroTriggerBase<RouterCrosspointChangedMacroTrigger.ActivationData>
    {

        [MacroTriggerArgument(0, "Router", "The router to observe.")]
        public class Arg0 : MacroTriggerArgumentDatabaseItem<Router>
        {
            public Arg0() : base(RouterDatabase.Instance)
            { }
        }

        internal class ActivationData : MacroTriggerWithArgumentsActivationData
        {
            public Router Router { get; private set; }
            public Router.CrosspointChangedDelegate RouterCrosspointChangedHandler { get; private set; }
            public ActivationData(Router router, Router.CrosspointChangedDelegate routerCrosspointChangedHandler)
            {
                Router = router;
                RouterCrosspointChangedHandler = routerCrosspointChangedHandler;
            }
        }

        protected override void _activate(MacroTriggerWithArguments triggerWithArguments)
        {
            object[] argumentObjects = triggerWithArguments.ArgumentObjects;
            Router router = argumentObjects[0] as Router;
            if (router == null)
                return;
            Router.CrosspointChangedDelegate routerCrosspointChangedHandler = (i, ov, nv) => {
                triggerWithArguments.Fire();
            };
            router.CrosspointChanged += routerCrosspointChangedHandler;
            ActivationData activationData = new ActivationData(router, routerCrosspointChangedHandler);
            triggerWithArguments.Activated(activationData);
        }

        protected override void _deactivate(MacroTriggerWithArguments triggerWithArguments, ActivationData activationData)
        {
            activationData.Router.CrosspointChanged -= activationData.RouterCrosspointChangedHandler;
            triggerWithArguments.Deactivated();
        }

        protected override string _humanReadable(object[] argumentObjects)
        {
            Router router = argumentObjects[0] as Router;
            if (router == null)
                return null;
            return string.Format("Source for any output changes on router [{0}].", router);
        }

    }

}
