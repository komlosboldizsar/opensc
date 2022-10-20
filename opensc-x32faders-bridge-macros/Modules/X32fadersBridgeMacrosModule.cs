using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.X32Faders.Macros;

namespace OpenSC.Modules
{

    [Module("x32faders-bridge-macros", "X32 faders (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(X32fadersModelModule))]
    public class X32fadersBridgeMacrosModule : MacrosBridgeModuleBase<X32fadersModelModule>
    {

        protected override void registerMacroCommands()
        {
            MacroCommandRegister.Instance.RegisterCommand(new DoMacroCommand());
        }

        protected override void registerTriggerTypes()
        { }

    }

}
