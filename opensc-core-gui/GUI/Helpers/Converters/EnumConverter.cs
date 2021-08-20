using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Helpers.Converters
{

    public class EnumConverter<TEnum, TConvertedValue>: IEnumerable<KeyValuePair<TEnum, TConvertedValue>>
    {

        private Dictionary<TEnum, TConvertedValue> convertedValues = new Dictionary<TEnum, TConvertedValue>();

        public EnumConverter(Dictionary<TEnum, TConvertedValue> convertedValues)
        {
            if (convertedValues != null)
                foreach (var item in convertedValues)
                    this.convertedValues.Add(item.Key, item.Value);
        }

        public TConvertedValue Convert(TEnum enumValue)
        {
            if (!convertedValues.TryGetValue(enumValue, out TConvertedValue convertedValue))
                return default(TConvertedValue);
            return convertedValue;
        }

        #region Implement IEnumerable, so we can use collection initializers
        public IEnumerator<KeyValuePair<TEnum, TConvertedValue>> GetEnumerator() => convertedValues.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => convertedValues.GetEnumerator();

        public void Add(TEnum enumValue, TConvertedValue convertedValue)
        {
            convertedValues.Add(enumValue, convertedValue);
        }
        #endregion

    }

}
