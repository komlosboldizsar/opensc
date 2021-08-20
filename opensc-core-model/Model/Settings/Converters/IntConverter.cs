using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{

    [SettingValueConverter(typeof(int))]
    class IntConverter : ISettingValueConverter
    {
        public string Serialize(object obj) => obj.ToString();
        public object Deserialize(string serialized) => (int.TryParse(serialized, out int result) ? result : 0);
    }
}
