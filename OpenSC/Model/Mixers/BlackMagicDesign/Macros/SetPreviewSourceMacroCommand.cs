using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{
    [MacroCommand("Mixers.BMD.SetPreviewSource", "Set PVW source on a BMD mixer M/E block", "Set source for preview bus of an M/E block of a BlackMagic Design mixer.")]
    public class SetPreviewSourceMacroCommand : SetPPSourceMacroCommandBase
    {
        protected override void setSource(BmdMixer mixer, int meBlockIndex, int inputId)
        {
            try
            {
                mixer.SetPreviewSource(meBlockIndex, inputId);
            }
            catch
            {
                // Do something...
            }
        }
    }
}
