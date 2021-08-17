using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public interface IMacroCommand
    {
        string Code { get; }
        string Name { get; }
        string Description { get; }
        IMacroCommandArgument[] Arguments { get; }
        void Run(object[] argumentValues);
        MacroCommandWithArguments GetWithArgumentsByKeys(string[] argumentKeys);
        MacroCommandWithArguments GetWithArgumentsByKeysConvertImmediately(string[] argumentKeys);
        MacroCommandWithArguments GetWithArguments(object[] argumentValues);
        object[] GetArgumentsByKeys(string[] argumentKeys);
        string[] GetArgumentKeys(object[] argumentsValues);

    }
}
