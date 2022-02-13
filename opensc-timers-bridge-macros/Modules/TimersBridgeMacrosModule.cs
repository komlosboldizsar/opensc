using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Timers.Macros;
using OpenSC.Model.Timers.Triggers;

namespace OpenSC.Modules
{

    [Module("timers-bridge-macros", "Timers (bridge to macros)", "TODO")]
    [DependsOnModule(typeof(TimersModelModule))]
    public class TimersBridgeMacrosModule : MacrosBridgeModuleBase<TimersModelModule>
    {

        protected override void registerMacroCommands()
        {
            MacroCommandRegister.Instance.RegisterCommand(new ResetTimerMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new StartTimerMacroCommand());
            MacroCommandRegister.Instance.RegisterCommand(new StopTimerMacroCommand());
        }

        protected override void registerTriggerTypes()
        {
            MacroTriggerRegister.Instance.RegisterTrigger(new TimerReachedValueMacroTrigger());
        }

    }

}
