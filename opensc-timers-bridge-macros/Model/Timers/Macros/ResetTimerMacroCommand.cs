using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Macros
{

    [MacroCommand("Timers.ResetTimer", "Reset timer", "Reset timer to zero or base value.")]
    public class ResetTimerMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentObjects) => (argumentObjects[0] as Timer)?.Reset();

        [MacroCommandArgument(0, "Timer", "Timer to reset")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<Timer>
        {
            public Arg0() : base(TimerDatabase.Instance)
            { }
        }

    }

}
