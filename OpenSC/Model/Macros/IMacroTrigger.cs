using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public interface IMacroTrigger
    {
        string Code { get; }
        string Name { get; }
        string Description { get; }
        int ArgumentCount { get; }
        IMacroTriggerArgument[] Arguments { get; }
        MacroTriggerWithArguments GetWithArgumentsByKeys(string[] argumentKeys);
        MacroTriggerWithArguments GetWithArguments(object[] argumentValues);
        object[] GetArgumentsByKeys(string[] argumentKeys);
        string[] GetArgumentKeys(object[] argumentsValues);
        void Register(MacroTriggerWithArguments triggerWithArguments);
        void Unregister(MacroTriggerWithArguments triggerWithArguments);
        void Call(params object[] arguments);
        string HumanReadable(object[] argumentsValues);
    }

}
