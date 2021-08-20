using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{
    public class VtrTypeNameConverter : TypeNameConverterBase
    {
        private static Dictionary<string, Type> KNOWN_TYPES = new Dictionary<string, Type>() { };
        protected override Dictionary<string, Type> knownTypes => KNOWN_TYPES;
        public static void AddKnownType(string typeCode, Type type) => KNOWN_TYPES.Add(typeCode, type);
    }
}
