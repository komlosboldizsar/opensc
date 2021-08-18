using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
    public interface IDynamicTextFunctionArgument
    {
        int Index { get; }
        string Description { get; }
        Type ObjectType { get; }
        DynamicTextFunctionArgumentType KeyType { get; }
        object GetObjectByKey(object key, object[] previousArgumentObjects);
    }
}
