using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Macros
{

    [MacroCommand("Timers.StopTimer", "Stop timer", "Stop/pause timer operation.")]
    public class StopTimerMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentObjects) => (argumentObjects[0] as Timer)?.Stop();

        [MacroCommandArgument(0, "Timer", "Timer to stop", typeof(Timer), MacroArgumentKeyType.Integer)]
        public class Arg0 : MacroCommandArgumentDatabaseItem<Timer>
        {
            public Arg0() : base(TimerDatabase.Instance)
            { }
        }

    }

}
