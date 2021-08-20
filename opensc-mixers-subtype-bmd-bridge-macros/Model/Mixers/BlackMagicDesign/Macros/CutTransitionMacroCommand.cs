using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{
    [MacroCommand("Mixers.BMD.CutTransition", "CUT transition on a BMD mixer.", "Perform CUT transition on a BlackMagic Design mixer.")]
    public class CutTransitionMacroCommand : TransitionMacroCommandBase
    {
        protected override void transition(BmdMixer mixer, int meBlockIndex)
        {
            try
            {
                mixer.CutTransition(meBlockIndex);
            }
            catch
            {
                // Do something...
            }
        }
    }
}
