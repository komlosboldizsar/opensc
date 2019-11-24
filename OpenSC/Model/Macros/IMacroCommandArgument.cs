using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public interface IMacroCommandArgument
    {
        string Name { get; }

        string Description { get; }

        Type Type { get; }

        object[] GetPossibilities(object[] previousArgumentValues);

        string GetStringForPossibility(object item);

    }
}
