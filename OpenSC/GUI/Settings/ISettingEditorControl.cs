using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Settings
{
    public interface ISettingEditorControl
    {
        ISettingEditorControl GetInstanceForSetting(ISetting setting);
    }
}
