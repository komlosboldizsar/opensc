using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{
    [MacroCommand("Mixers.BMD.SetProgramSource", "Set PGM source on a BMD mixer M/E block", "Set source for program bus of an M/E block of a BlackMagic Design mixer.")]
    public class SetProgramSourceMacroCommand : SetPPSourceMacroCommandBase
    {
        protected override void setSource(BmdMixer mixer, int meBlockIndex, int inputId)
        {
            try
            {
                mixer.SetProgramSource(meBlockIndex, inputId);
            }
            catch
            {
                // Do something...
            }
        }
    }
}
