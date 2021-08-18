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

        public override IMacroCommandArgument[] Arguments => new IMacroCommandArgument[] {
            new Arg0(),
            new Arg1(),
            new Arg2()
        };

        public override void Run(object[] argumentValues)
        {

            if (argumentValues.Length != Arguments.Length)
                return;

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

        protected override string getArgumentKey(int index, object value)
        {

            if (index == 0) {
                BmdMixer mixer = value as BmdMixer;
                if (mixer == null)
                    return "-1";
                return mixer.ID.ToString();
            }

            if (index == 1)
            {
                if (value == null)
                    return "-1";
                if (!(value is int))
                    return "-1";
                return value.ToString();
            }

            if (index == 2)
            {
                if (value == null)
                    return "-1";
                if (!(value is int))
                    return "-1";
                return value.ToString();
            }

            throw new ArgumentException();

        }

        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int mixerId))
                return null;
            if (!int.TryParse(keys[1], out int meBlockIndex))
                return null;
            if (!int.TryParse(keys[2], out int inputId))
                return null;

            BmdMixer mixer = MixerDatabase.Instance.GetTById(mixerId) as BmdMixer;
            if (mixer == null)
                return null;

            if ((meBlockIndex < 0) || (meBlockIndex > 3))
                return null;

            if ((inputId < 0) || (inputId > 99999))
                return null;

            return new object[]
            {
                mixer,
                meBlockIndex,
                inputId
            };

        }

        private static readonly object[] ARRAY_EMPTY = new object[] { };

        public class Arg0 : IMacroCommandArgument
        {
            public string Name => "Mixer";
            public string Description => "The BMD mixer that switches source.";
            public Type Type => typeof(BmdMixer);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public object[] GetPossibilities(object[] previousArgumentValues)
                => MixerDatabase.Instance.Where(m => m is BmdMixer).ToArray();
            public string GetStringForPossibility(object item)
                => ((Mixer)item).Name;
        }

        public class Arg1 : IMacroCommandArgument
        {
            public string Name => "M/E block";
            public string Description => "The index of the M/E block to set the source on. Index of first M/E block is 0.";
            public Type Type => typeof(int);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public string GetStringForPossibility(object item)
                => item?.ToString() ?? "";

            public object[] GetPossibilities(object[] previousArgumentValues)
                => ARRAY_EMPTY;

        }

        public class Arg2 : IMacroCommandArgument
        {
            public string Name => "Input";
            public string Description => "The ID of the input to switch to. Indexing of physical inputs start with 0. See BlackMagic Design manual for other details.";
            public Type Type => typeof(int);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public string GetStringForPossibility(object item)
                => item?.ToString() ?? "";

            public object[] GetPossibilities(object[] previousArgumentValues)
                => ARRAY_EMPTY;

        }

    }

}
