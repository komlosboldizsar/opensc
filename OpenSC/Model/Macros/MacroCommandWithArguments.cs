using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroCommandWithArguments
    {

        public IMacroCommand Command { get; private set; }

        public string[] argumentValueKeys;

        public object[] ArgumentValues { get; private set; }

        public MacroCommandWithArguments(IMacroCommand command, string[] argumentValueKeys)
        {
            this.Command = command;
            this.argumentValueKeys = argumentValueKeys;
        }

        public void Restored()
        {
            restoreArgumentValues();
        }

        private void restoreArgumentValues()
        {
            ArgumentValues = Command.GetArgumentsByKeys(argumentValueKeys);
        }

        public void Run()
        {
            Command.Run(ArgumentValues);
        }

    }
}
