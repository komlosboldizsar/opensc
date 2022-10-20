using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers.Leitch.Triggers;

namespace OpenSC.Modules
{

    [Module("routers-subtype-virtualleitch-bridge-macros", "Routers / Virtual Leitch (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeVirtualleitchBridgeMacrosModule : MacrosBridgeModuleBase<RoutersModelModule>
    {

        protected override void registerMacroCommands()
        { }

        protected override void registerTriggerTypes()
        {
            MacroTriggerRegister.Instance.RegisterTrigger(new VirtualLeitchCustomCommandReceivedMacroTrigger());
        }

    }

}
