using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{
    public interface ISettingValueConverter
    {
        string Serialize(object obj);
        object Deserialize(string serialized);
    }
}
