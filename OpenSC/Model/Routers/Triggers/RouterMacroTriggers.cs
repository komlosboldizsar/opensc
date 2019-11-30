using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Triggers
{

    public class RouterMacroTriggers : MacroTriggerRegister.IMacroTriggerCollection
    {

        public static readonly IMacroTrigger RouterOutputSourceChanged = new RouterOutputSourceChangedMacroTrigger();
        
        public static readonly IMacroTrigger RouterCrosspointChanged = new RouterCrosspointChangedMacroTrigger();
        
        public static readonly MacroTriggerRegister.IMacroTriggerCollection Instance = new RouterMacroTriggers();

        public IMacroTrigger[] TriggersToRegister => new IMacroTrigger[] {
            RouterOutputSourceChanged,
            RouterCrosspointChanged
        };

    }

}
