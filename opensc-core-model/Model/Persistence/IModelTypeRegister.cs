using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    public interface IModelTypeRegister
    {
        string ConvertTypeToString(Type type);
        Type ConvertStringToType(string typeLabel);
        IEnumerable<Type> RegisteredTypes { get; }
    }
}
