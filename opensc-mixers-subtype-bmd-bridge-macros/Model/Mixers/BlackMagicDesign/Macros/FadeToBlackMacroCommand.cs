using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{
    [MacroCommand("Mixers.BMD.FadeToBlack", "Fade to black on a BMD mixer.", "Perform fade to black transition on a BlackMagic Design mixer.")]
    public class FadeToBlackMacroCommand : TransitionMacroCommandBase
    {
        protected override void transition(BmdMixer mixer, int meBlockIndex)
        {
            try
            {
                mixer.FadeToBlack(meBlockIndex);
            }
            catch
            {
                // Do something...
            }
        }
    }
}
