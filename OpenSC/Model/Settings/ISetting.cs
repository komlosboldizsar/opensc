using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings
{

    public delegate void SettingValueChangedDelegate(ISetting setting, object oldValue, object newValue);

    public interface ISetting
    {

        string Key { get; }

        string HumanReadableTitle { get; }

        string HumanReadableDescription { get; }

        object ObjValue { get; set; }

        Type Type { get; }

        event SettingValueChangedDelegate ValueChanged;

    }
}
