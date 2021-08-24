using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Mixers.BlackMagicDesign.Macros;

namespace OpenSC.Modules
{

    [Module("mixers-subtype-bmd-bridge-macros", "Mixers / BlackMagic Design ATEM (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(MixersSubtypeBmdModule))]
    public class MixersSubtypeBmdBridgeMacrosModule : MacrosBridgeModuleBase<MixersSubtypeBmdModule>
    {

        protected override void registerMacroCommands()
        {
            MacroCommandRegister.Instance.RegisterCommand(new AutoTransitionMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new CutTransitionMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new SetPreviewSourceMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new SetProgramSourceMacroCommand());
        }

        protected override void registerTriggerTypes()
        { }

    }

}
