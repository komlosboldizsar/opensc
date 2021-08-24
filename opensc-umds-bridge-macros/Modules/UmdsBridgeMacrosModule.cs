using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.UMDs.Macros;

namespace OpenSC.Modules
{

    [Module("umds-bridge-macros", "UMDs (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsBridgeMacrosModule : MacrosBridgeModuleBase<UmdsModelModule>
    {

        protected override void registerMacroCommands()
        {
            MacroCommandRegister.Instance.RegisterCommand(new UmdDynamicTextMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new UmdStaticTextMacroCommand());
        }

        protected override void registerTriggerTypes()
        { }

    }

}
