using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{
    public class CutTransitionMacroCommand : TransitionMacroCommandBase
    {
        public override string CommandCode => "Mixers.BMD.CutTransition";

        public override string CommandName => "CUT transition on a BMD mixer.";

        public override string Description => "Perform CUT transition on a BlackMagic Design mixer.";

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
