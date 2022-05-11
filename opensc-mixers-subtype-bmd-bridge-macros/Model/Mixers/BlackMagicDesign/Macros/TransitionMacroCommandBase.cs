using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadHelpers;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{

    public abstract class TransitionMacroCommandBase : MacroCommandBase
    {

        protected override void _run(object[] argumentObjects)
        {
            BmdMixer mixer = argumentObjects[0] as BmdMixer;
            if (mixer == null)
                return;
            if (!int.TryParse(argumentObjects[1]?.ToString(), out int meBlockIndex))
                return;
            InvokeHelper.Invoke(() => transition(mixer, meBlockIndex));
        }

        protected abstract void transition(BmdMixer mixer, int meBlockIndex);

        [MacroCommandArgument(0, "Mixer", "The BMD mixer that performs the transition.")]
        public class Arg0 : MacroCommandArgumentDatabaseSubTypeItem<Mixer, BmdMixer>
        {
            public Arg0() : base(MixerDatabase.Instance)
            { }
        }

        [MacroCommandArgument(1, "M/E block", "The index of the M/E block to perform the transition on. Index of first M/E block is 0.")]
        public class Arg1 : MacroCommandArgumentInt
        {
            public Arg1() : base(0, 3)
            { }
        }

    }

}
