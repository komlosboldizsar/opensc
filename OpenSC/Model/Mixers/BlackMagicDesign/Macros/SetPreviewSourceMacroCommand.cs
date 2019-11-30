using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{
    public class SetPreviewSourceMacroCommand : SetPPSourceMacroCommandBase
    {
        public override string CommandCode => "Mixers.BMD.SetPreviewSource";

        public override string CommandName => "Set PVW source on a BMD mixer M/E block";

        public override string Description => "Set source for preview bus of an M/E block of a BlackMagic Design mixer.";

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
