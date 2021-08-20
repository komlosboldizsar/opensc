using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.BlackMagicDesign.Macros
{

    public abstract class SetPPSourceMacroCommandBase : MacroCommandBase
    {

        protected override void _run(object[] argumentValues)
        {
            BmdMixer mixer = argumentValues[0] as BmdMixer;
            if (mixer == null)
                return;
            if (!int.TryParse(argumentValues[1]?.ToString(), out int meBlockIndex))
                return;
            if (!int.TryParse(argumentValues[2]?.ToString(), out int inputId))
                return;
            setSource(mixer, meBlockIndex, inputId);
        }

        protected abstract void setSource(BmdMixer mixer, int meBlockIndex, int inputId);

        [MacroCommandArgument(0, "Mixer", "The BMD mixer that switches source.")]
        public class Arg0 : MacroCommandArgumentDatabaseSubTypeItem<Mixer, BmdMixer>
        {
            public Arg0() : base(MixerDatabase.Instance)
            { }
        }


        [MacroCommandArgument(1, "M/E block", "The index of the M/E block to set the source on. Index of first M/E block is 0.")]
        public class Arg1 : MacroCommandArgumentInt
        {
            public Arg1() : base(0, 3)
            { }
        }

        [MacroCommandArgument(2, "Input", "The ID of the input to switch to. Indexing of physical inputs start with 0. See BlackMagic Design manual for other details.")]
        public class Arg2 : MacroCommandArgumentInt
        {
            public Arg2() : base(0, 99999)
            { }
        }

    }

}
