using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Macros
{

    public class StopTimerMacroCommand : SingleArgTimerMacroCommandBase
    {

        public override string CommandCode => "Timers.StopTimer";
        public override string CommandName => "Stop timer";
        public override string Description => "Stop/pause timer operation.";

        public StopTimerMacroCommand()
            : base("The timer to stop")
        { }

        protected override void _run(Timer timer)
        {
            timer.Stop();
        }

    }

}
