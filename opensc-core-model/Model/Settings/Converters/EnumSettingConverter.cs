using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{

    [SettingConverter(typeof(EnumSetting))]
    class EnumSettingConverter : ISettingConverter
    {

        public string Serialize(ISetting setting) => setting.ObjValue?.ToString();

        public void Deserialize(ISetting setting, string serialized)
        {
            EnumSetting enumSetting = setting as EnumSetting;
            if (enumSetting == null)
                return;
            if (Enum.TryParse(enumSetting.EnumType, serialized, out object deserializedEnum))
                setting.ObjValue = deserializedEnum;
        }

    }

}
