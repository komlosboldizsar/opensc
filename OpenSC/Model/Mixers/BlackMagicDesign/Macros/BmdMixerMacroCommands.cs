using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{

    public class BmdMixerMacroCommands : MacroCommandRegister.IMacroCommandCollection
    {

        public static readonly IMacroCommand AutoTransition = new AutoTransitionMacroCommand();

        public static readonly IMacroCommand CutTransition = new CutTransitionMacroCommand();

        public static readonly MacroCommandRegister.IMacroCommandCollection Instance = new BmdMixerMacroCommands();
        public IMacroCommand[] CommandsToRegister => new IMacroCommand[] {
            AutoTransition,
            CutTransition
        };

    }

}
