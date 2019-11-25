using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Macros
{

    public class TimerMacroCommands : MacroCommandRegister.IMacroCommandCollection
    {

        public static readonly IMacroCommand StartTimer = new StartTimerMacroCommand();

        public static readonly IMacroCommand StopTimer = new StopTimerMacroCommand();

        public static readonly IMacroCommand ResetTimer = new ResetTimerMacroCommand();

        public static readonly MacroCommandRegister.IMacroCommandCollection Instance = new TimerMacroCommands();
        public IMacroCommand[] CommandsToRegister => new IMacroCommand[] {
            StartTimer,
            StopTimer,
            ResetTimer
        };

    }

}
