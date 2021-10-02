using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{
    [SettingValueConverter(typeof(OpenFilePath))]
    class OpenFilePathConverter : ISettingValueConverter
    {
        public string Serialize(object obj) => (OpenFilePath)obj;
        public object Deserialize(string serialized) => (OpenFilePath)serialized;
    }
}
