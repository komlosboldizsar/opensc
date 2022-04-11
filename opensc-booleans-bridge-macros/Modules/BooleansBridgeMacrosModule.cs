using OpenSC.Model;
using OpenSC.Model.Macros;
//using OpenSC.Model.Variables.Macros;
using OpenSC.Model.Variables.Triggers;

namespace OpenSC.Modules
{

    [Module("booleans-bridge-macros", "Booleans (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(BooleansModelModule))]
    public class BooleansBridgeMacrosModule : MacrosBridgeModuleBase<BooleansModelModule>
    {

        protected override void registerMacroCommands()
        {
            //MacroCommandRegister.Instance.RegisterCommand(new XY());
        }

        protected override void registerTriggerTypes()
        {
            MacroTriggerRegister.Instance.RegisterTrigger(new BooleanStateChangedMacroTrigger());
        }

    }

}
