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
        object[] GetArgumentsByKeys(string[] argumentKeys);
        string[] GetArgumentKeys(object[] argumentObjects);
        MacroTriggerWithArguments GetWithArgumentsByKeys(string[] argumentKeys);
        MacroTriggerWithArguments GetWithArguments(object[] argumentObjects);
        void Activate(MacroTriggerWithArguments triggerWithArguments);
        void Deactivate(MacroTriggerWithArguments triggerWithArguments);
        string HumanReadable(object[] argumentObjects);
    }

}
