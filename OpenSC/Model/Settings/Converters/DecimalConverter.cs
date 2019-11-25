using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{

    [SettingValueConverter(typeof(decimal))]
    class DecimalConverter : ISettingValueConverter
    {

        public string Serialize(object obj)
        {
            return obj.ToString();
        }

        public object Deserialize(string serialized)
        {
            if (!decimal.TryParse(serialized, out decimal result))
                return 0;
            return result;
        }

    }

}
