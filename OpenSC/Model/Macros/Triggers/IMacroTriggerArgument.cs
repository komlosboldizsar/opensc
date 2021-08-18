using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public interface IMacroTriggerArgument
    {
        int Index { get; }
        string Name { get; }
        string Description { get; }
        Type ObjectType { get; }
        MacroArgumentKeyType KeyType { get; }
        object GetObjectByKey(string key, object[] previousArgumentObjects);
        string GetKeyByObject(object obj);
        IEnumerable<object> GetPossibilities(object[] previousArgumentValues);
        string GetStringForPossibility(object item);
    }
}
