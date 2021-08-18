using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{
    public class AutoTransitionMacroCommand : TransitionMacroCommandBase
    {
        public override string CommandCode => "Mixers.BMD.AutoTransition";

        public override string CommandName => "AUTO transition on a BMD mixer.";

        public override string Description => "Perform AUTO transition on a BlackMagic Design mixer.";

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
