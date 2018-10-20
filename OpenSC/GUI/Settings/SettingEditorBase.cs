using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenSC.Model.Settings;

namespace OpenSC.GUI.Settings
{

    public partial class SettingEditorBase : UserControl, ISettingEditorControl
    {

        private ISetting setting;

        public SettingEditorBase()
        {
            InitializeComponent();
        }

        public SettingEditorBase(ISetting setting = null)
        {
            if (setting == null)
                return;
            InitializeComponent();
            this.setting = setting;
            showSettingData();
        }

        private void showSettingData()
        {
            settingTitleLabel.Text = setting.HumanReadableTitle;
            settingDescriptionLabel.Text = setting.HumanReadableDescription;
        }

        public virtual ISettingEditorControl GetInstanceForSetting(ISetting setting)
        {
            return new SettingEditorBase(setting);
        }

    }
}
