using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Macros
{

    public class StartTimerMacroCommand : SingleArgTimerMacroCommandBase
    {

        public override string CommandCode => "Timers.StartTimer";
        public override string CommandName => "Start timer";
        public override string Description => "Start/continue timer operation.";

        public StartTimerMacroCommand()
            : base("The timer to start")
        { }

        protected override void _run(Timer timer)
        {
            timer.Start();
        }

    }

}
