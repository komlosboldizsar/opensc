using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Helpers.Converters
{
    class EnumToStringConverter<TEnum> : EnumConverter<TEnum, string>
    {
        public EnumToStringConverter(Dictionary<TEnum, string> convertedValues = null) : base(convertedValues)
        {
        }
    }
}
