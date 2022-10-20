using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch.Triggers
{

    [MacroTrigger("Routers.VirtualLeitch.CustomCommandReceived", "Input delegated to a router output changed", "Observe a single output for input change on a router.")]
    class VirtualLeitchCustomCommandReceivedMacroTrigger : MacroTriggerBase<VirtualLeitchCustomCommandReceivedMacroTrigger.ActivationData>
    {

        [MacroTriggerArgument(0, "Router", "The router to observe.")]
        public class Arg0 : MacroTriggerArgumentDatabaseSubTypeItem<Router, VirtualLeitchRouter>
        {
            public Arg0() : base(RouterDatabase.Instance)
            { }
        }

        [MacroTriggerArgument(1, "Command", "Custom command text to observe.")]
        public class Arg1 : MacroTriggerArgumentString
        {
            public Arg1() : base()
            { }
        }

        internal class ActivationData : MacroTriggerWithArgumentsActivationData
        {
            public VirtualLeitchRouter Router { get; private set; }
            public string Command { get; private set; }
            public VirtualLeitchRouter.CustomCommandReceivedHandler RouterCustomCommandReceivedHandler { get; private set; }
            public ActivationData(VirtualLeitchRouter router, string command, VirtualLeitchRouter.CustomCommandReceivedHandler routerCustomCommandReceivedHandler)
            {
                Router = router;
                Command = command;
                RouterCustomCommandReceivedHandler = routerCustomCommandReceivedHandler;
            }
        }

        protected override void _activate(MacroTriggerWithArguments triggerWithArguments)
        {
            object[] argumentObjects = triggerWithArguments.ArgumentObjects;
            if (argumentObjects[0] is not VirtualLeitchRouter router)
                return;
            if (argumentObjects[1] is not string command)
                return;
            VirtualLeitchRouter.CustomCommandReceivedHandler routerCustomCommandReceivedHandler = (r, c) =>
            {
                if ((r == router) && (c == command))
                    triggerWithArguments.Fire();
            };
            router.CustomCommandReceived += routerCustomCommandReceivedHandler;
            ActivationData activationData = new ActivationData(router, command, routerCustomCommandReceivedHandler);
            triggerWithArguments.Activated(activationData);
        }

        protected override void _deactivate(MacroTriggerWithArguments triggerWithArguments, ActivationData activationData)
        {
            activationData.Router.CustomCommandReceived += activationData.RouterCustomCommandReceivedHandler; 
            triggerWithArguments.Deactivated();
        }

        protected override string _humanReadable(object[] argumentObjects)
        {
            if (argumentObjects[0] is not VirtualLeitchRouter router)
                return null;
            if (argumentObjects[1] is not string command)
                return null;
            return $"Custom command [{command}] received on virtual leitch router [{router}]";
        }

    }

}
