using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{
    [MacroCommand("Mixers.BMD.AutoTransition", "AUTO transition on a BMD mixer.", "Perform AUTO transition on a BlackMagic Design mixer.")]
    public class AutoTransitionMacroCommand : TransitionMacroCommandBase
    {
        protected override void transition(BmdMixer mixer, int meBlockIndex)
        {
            try
            {
                mixer.AutoTransition(meBlockIndex);
            }
            catch
            {
                // Do something...
            }
        }
    }
}
