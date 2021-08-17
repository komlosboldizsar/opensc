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

        public string CommandCode => Command?.Code;

        public object[] ArgumentValues { get; private set; }

        private string[] argumentKeys;

        public string[] ArgumentKeys
        {
            get => Command.GetArgumentKeys(ArgumentValues);
        }

        public MacroCommandWithArguments(IMacroCommand command, string[] argumentKeys, bool byKeys)
        {
            this.Command = command;
            this.argumentKeys = argumentKeys;
        }

        public MacroCommandWithArguments(IMacroCommand command, object[] argumentValues)
        {
            this.Command = command;
            this.ArgumentValues = argumentValues;
        }

        public void Restored()
        {
            restoreArgumentValues();
        }

        private void restoreArgumentValues()
        {
            if (argumentKeys != null)
                ArgumentValues = Command.GetArgumentsByKeys(argumentKeys);
        }

        public void Run()
        {
            if (ArgumentValues != null)
                Command.Run(ArgumentValues);
        }

    }
}
