using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Macros
{

    public class ResetTimerMacroCommand : SingleArgTimerMacroCommandBase
    {

        public override string CommandCode => "Timers.ResetTimer";
        public override string CommandName => "Reset timer";
        public override string Description => "Reset timer to zero or base value.";

        public ResetTimerMacroCommand()
            : base("The timer to reset")
        { }

        protected override void _run(Timer timer)
        {
            timer.Reset();
        }

    }

}
