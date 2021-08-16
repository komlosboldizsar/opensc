using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public class RunMacroMacroCommand : MacroCommandBase
    {

        public override string CommandCode => "Macros.Run";

        public override string CommandName => "Run a macro";

        public override string Description => "Call and execute another macro";

        public override IMacroCommandArgument[] Arguments => new IMacroCommandArgument[] {
            new Arg0()
        };

        public override void Run(object[] argumentValues)
        {

            if (argumentValues.Length != Arguments.Length)
                return;

            Macro macro = argumentValues[0] as Macro;
            if (macro == null)
                return;

            macro?.Run();

        }

        protected override string getArgumentKey(int index, object value)
        {

            if (index == 0) {
                Macro macro = value as Macro;
                if (macro == null)
                    return "-1";
                return macro.ID.ToString();
            }

            throw new ArgumentException();

        }

        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int macroId))
                return null;

            Macro macro = MacroDatabase.Instance.GetTById(macroId);
            if (macro == null)
                return null;

            return new object[] { macro };

        }

        private static readonly object[] ARRAY_EMPTY = new object[] { };

        public class Arg0 : IMacroCommandArgument
        {
            public string Name => "Macro";
            public string Description => "The macro to execute.";
            public Type Type => typeof(Macro);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public object[] GetPossibilities(object[] previousArgumentValues)
                => MacroDatabase.Instance.ToArray();
            public string GetStringForPossibility(object item)
                => ((Macro)item).Name;
        }


    }

}
