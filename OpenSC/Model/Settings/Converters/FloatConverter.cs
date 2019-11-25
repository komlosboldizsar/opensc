using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{

    [SettingValueConverter(typeof(float))]
    class FloatConverter : ISettingValueConverter
    {

        public string Serialize(object obj)
        {
            return obj.ToString();
        }

        public object Deserialize(string serialized)
        {
            if (!float.TryParse(serialized, out float result))
                return 0;
            return result;
        }

    }

}
