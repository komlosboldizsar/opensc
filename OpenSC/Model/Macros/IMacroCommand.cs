using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public interface IMacroCommand
    {

        string CommandCode { get; }

        string CommandName { get; }

        string Description { get; }

        IMacroCommandArgument[] Arguments { get; }

        void Run(object[] argumentValues);

        MacroCommandWithArguments GetWithArguments(string[] argumentKeys);

        object[] GetArgumentsByKeys(string[] argumentKeys);

        string[] GetArgumentKeys(object[] argumentsValues);

    }

}
