using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Macros
{

    public class UmdMacroCommands : MacroCommandRegister.IMacroCommandCollection
    {

        public static readonly IMacroCommand UmdStaticText = new UmdStaticTextMacroCommand();

        public static readonly IMacroCommand UmdDynamicText = new UmdDynamicTextMacroCommand();

        public static readonly MacroCommandRegister.IMacroCommandCollection Instance = new UmdMacroCommands();
        public IMacroCommand[] CommandsToRegister => new IMacroCommand[] {
            UmdStaticText,
            UmdDynamicText
        };

    }

}
