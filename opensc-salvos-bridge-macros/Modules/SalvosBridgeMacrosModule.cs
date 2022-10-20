using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers.Salvos.Macros;

namespace OpenSC.Modules
{

    [Module("salvos-bridge-macros", "Salvos (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(SalvosModelModule))]
    public class SalvosBridgeMacrosModule : MacrosBridgeModuleBase<SalvosModelModule>
    {

        protected override void registerMacroCommands()
        {
            MacroCommandRegister.Instance.RegisterCommand(new TakeSalvoMacroCommand());
        }

        protected override void registerTriggerTypes()
        { }

    }

}
