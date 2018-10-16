using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    interface ITypeNameConverter
    {
        string ConvertTypeToString(Type type);
        Type ConvertStringToType(string typeLabel);
    }
}
