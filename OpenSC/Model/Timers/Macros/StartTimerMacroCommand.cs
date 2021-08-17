using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Macros
{

    [MacroCommand("Timers.StartTimer", "Start timer", "Start/continue timer operation.")]
    public class StartTimerMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentObjects) => (argumentObjects[0] as Timer)?.Start();

        [MacroCommandArgument(0, "Timer", "Timer to start", typeof(Timer), MacroArgumentKeyType.Integer)]
        public class Arg0 : MacroCommandArgumentDatabaseItem<Timer>
        {
            public Arg0() : base(TimerDatabase.Instance)
            { }
        }

    }

}
