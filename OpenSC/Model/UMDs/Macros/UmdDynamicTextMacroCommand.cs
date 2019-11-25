using OpenSC.Model.Macros;
using OpenSC.Model.UMDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Macros
{

    public class UmdDynamicTextMacroCommand : MacroCommandBase
    {

        public override string CommandCode => "UMDs.DynamicText";

        public override string CommandName => "Set an UMD to dynamic mode";

        public override string Description => "Switch an UMD to dynamic mode without changing static text.";

        public override IMacroCommandArgument[] Arguments => new IMacroCommandArgument[] {
            new Arg0()
        };

        public override void Run(object[] argumentValues)
        {

            if (argumentValues.Length != Arguments.Length)
                return;

            UMD umd = argumentValues[0] as UMD;
            if (umd == null)
                return;

            umd.UseStaticText = false;

        }

        protected override string getArgumentKey(int index, object value)
        {

            if (index == 0) {
                UMD umd = value as UMD;
                if (umd == null)
                    return "-1";
                return umd.ID.ToString();
            }

            throw new ArgumentException();

        }

        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int umdId))
                return null;

            UMD umd = UmdDatabase.Instance.GetTById(umdId);
            if (umd == null)
                return null;

            return new object[] { umd };

        }

        private static readonly object[] ARRAY_EMPTY = new object[] { };

        public class Arg0 : IMacroCommandArgument
        {
            public string Name => "UMD";
            public string Description => "The UMD to change mode and text.";
            public Type Type => typeof(UMD);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public object[] GetPossibilities(object[] previousArgumentValues)
                => UmdDatabase.Instance.ToArray();
            public string GetStringForPossibility(object item)
                => ((UMD)item).Name;
        }

        public class Arg1 : IMacroCommandArgument
        {
            public string Name => "Text";
            public string Description => "Text to display on the UMD.";
            public Type Type => typeof(string);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.String;
            public string GetStringForPossibility(object item)
                => item?.ToString();
            public object[] GetPossibilities(object[] previousArgumentValues)
                => null;

        }


    }

}
