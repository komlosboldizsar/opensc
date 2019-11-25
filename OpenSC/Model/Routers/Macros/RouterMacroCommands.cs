using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Macros
{

    public class RouterMacroCommands : MacroCommandRegister.IMacroCommandCollection
    {

        public static readonly IMacroCommand SetRouterCrosspoint = new SetRouterCrosspointMacroCommand();

        public static readonly MacroCommandRegister.IMacroCommandCollection Instance = new RouterMacroCommands();
        public IMacroCommand[] CommandsToRegister => new IMacroCommand[] {
            SetRouterCrosspoint
        };

    }

}
