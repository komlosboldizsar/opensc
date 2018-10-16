using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    class UmdPortTypeNameConverter : ITypeNameConverter
    {

        public Type ConvertStringToType(string typeLabel)
        {
            if (typeLabel == "tsl31")
                return typeof(TSL31.TSL31Port);
            if (typeLabel == "mccurdy")
                return typeof(McCurdy.McCurdyPort);
            return null;
        }

        public string ConvertTypeToString(Type type)
        {
            if (type == typeof(TSL31.TSL31Port))
                return "tsl31";
            if (type == typeof(McCurdy.McCurdyPort))
                return "mccurdy";
            return "";
        }

    }
}
