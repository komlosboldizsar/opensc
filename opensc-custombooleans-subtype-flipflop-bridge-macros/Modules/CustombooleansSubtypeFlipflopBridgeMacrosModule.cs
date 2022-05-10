using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Variables.Macros;

namespace OpenSC.Modules
{

    [Module("custombooleans-subtype-flipflop-bridge-macros", "Custom booleans / FlipFlop (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(CustombooleansSubtypeFlipflopModelModule))]
    public class CustombooleansSubtypeFlipflopBridgeMacrosModule : MacrosBridgeModuleBase<CustombooleansSubtypeFlipflopModelModule>
    {

        protected override void registerMacroCommands()
        {
            MacroCommandRegister.Instance.RegisterCommand(new AutoTransitionMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new CutTransitionMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new SetPreviewSourceMacroCommand());
        }

        protected override void registerTriggerTypes()
        { }

    }

}
