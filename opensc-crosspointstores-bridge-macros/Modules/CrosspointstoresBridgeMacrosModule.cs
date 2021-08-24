using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers.CrosspointStores.Macros;

namespace OpenSC.Modules
{

    [Module("crosspointstores-bridge-macros", "Crosspoint stores (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(CrosspointstoresModelModule))]
    public class CrosspointstoresBridgeMacrosModule : MacrosBridgeModuleBase<CrosspointstoresModelModule>
    {

        protected override void registerMacroCommands()
        {
            MacroCommandRegister.Instance.RegisterCommand(new SetCrosspointStoreInputMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new SetCrosspointStoreOutputMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new TakeCrosspointStoreMacroCommand());
        }

        protected override void registerTriggerTypes()
        { }

    }

}
