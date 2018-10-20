using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Helpers.Converters
{
    class EnumToBitmapConverter<TEnum> : EnumConverter<TEnum, Bitmap>
    {
        public EnumToBitmapConverter(Dictionary<TEnum, Bitmap> convertedValues = null) : base(convertedValues)
        {
        }
    }
}
