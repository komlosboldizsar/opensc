using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{
    public interface ISettingConverter
    {
        string Serialize(ISetting setting);
        void Deserialize(ISetting setting, string serialized);
    }
}
