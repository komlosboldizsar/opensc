using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{

    [SettingValueConverter(typeof(bool))]
    class BoolConverter : ISettingValueConverter
    {

        private const string TRUE = "true";
        private const string FALSE = "false";

        public string Serialize(object obj)
        {
            return (bool)obj ? TRUE : FALSE;
        }

        public object Deserialize(string serialized)
        {
            return (serialized == TRUE);
        }

    }

}
