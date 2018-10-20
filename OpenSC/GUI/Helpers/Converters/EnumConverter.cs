using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Helpers.Converters
{

    class EnumConverter<TEnum, TConvertedValue>
    {

        private Dictionary<TEnum, TConvertedValue> convertedValues = new Dictionary<TEnum, TConvertedValue>();

        public EnumConverter(Dictionary<TEnum, TConvertedValue> convertedValues)
        {
            if (convertedValues == null)
                throw new ArgumentNullException();
            foreach (var item in convertedValues)
                this.convertedValues.Add(item.Key, item.Value);
        }

        public TConvertedValue Convert(TEnum key)
        {
            if (!convertedValues.TryGetValue(key, out TConvertedValue convertedValue))
                return default(TConvertedValue);
            return convertedValue;
        }

    }

}
