using OpenSC.Model.Macros;
using OpenSC.Model.UMDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Macros
{

    public class UmdStaticTextMacroCommand : MacroCommandBase
    {

        public override string CommandCode => "UMDs.StaticText";

        public override string CommandName => "Set an UMD to static mode";

        public override string Description => "Set the static text of an UMD and switch to static mode.";

        public override IMacroCommandArgument[] Arguments => new IMacroCommandArgument[] {
            new Arg0(),
            new Arg1()
        };

        public override void Run(object[] argumentValues)
        {

            if (argumentValues.Length != Arguments.Length)
                return;

            UMD umd = argumentValues[0] as UMD;
            if (umd == null)
                return;

            string text = argumentValues[1] as string;
            if (text == null)
                return;

            umd.StaticText = text;
            umd.UseStaticText = true;

        }

        protected override string getArgumentKey(int index, object value)
        {

            if (index == 0) {
                UMD umd = value as UMD;
                if (umd == null)
                    return "-1";
                return umd.ID.ToString();
            }

            if (index == 1)
            {
                string text = value as string;
                if (text == null)
                    return "";
                return text;
            }

            throw new ArgumentException();

        }

        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int umdId))
                return null;
            if (keys[1] == null)
                return null;

            UMD umd = UmdDatabase.Instance.GetTById(umdId);
            if (umd == null)
                return null;

            return new object[]
            {
                umd,
                keys[1]
            };

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
