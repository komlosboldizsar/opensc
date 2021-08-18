using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{
    [SettingValueConverter(typeof(double))]
    class DoubleConverter : ISettingValueConverter
    {
        public string Serialize(object obj) => obj.ToString();
        public object Deserialize(string serialized)
            => (double.TryParse(serialized, out double result) ? result : 0);
    }
}
