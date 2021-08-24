using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers.Macros;
using OpenSC.Model.Routers.Triggers;

namespace OpenSC.Modules
{

    [Module("routers-bridge-macros", "Routers (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersBridgeMacrosModule : MacrosBridgeModuleBase<RoutersModelModule>
    {

        protected override void registerMacroCommands()
        {
            MacroCommandRegister.Instance.RegisterCommand(new SetRouterCrosspointMacroCommand());
        }

        protected override void registerTriggerTypes()
        {
            MacroTriggerRegister.Instance.RegisterTrigger(new RouterCrosspointChangedMacroTrigger());
            MacroTriggerRegister.Instance.RegisterTrigger(new RouterOutputInputChangedMacroTrigger());
        }

    }

}
