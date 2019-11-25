using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Triggers
{

    public class RouterMacroTriggers
    {

        public static readonly IMacroTrigger RouterCrosspointChanged = new RouterCrosspointChangedMacroTrigger();

    }

}
