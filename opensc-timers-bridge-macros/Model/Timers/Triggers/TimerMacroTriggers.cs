using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers.Triggers
{

    public class TimerMacroTriggers : MacroTriggerRegister.IMacroTriggerCollection
    {

        public static readonly IMacroTrigger TimerReachedValue = new TimerReachedValueMacroTrigger();
        
        public static readonly MacroTriggerRegister.IMacroTriggerCollection Instance = new TimerMacroTriggers();

        public IMacroTrigger[] TriggersToRegister => new IMacroTrigger[] {
            TimerReachedValue
        };

    }

}
