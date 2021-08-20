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

        protected ISetting setting { get; private set; }

        public SettingEditorBase()
        {
            InitializeComponent();
        }

        public SettingEditorBase(ISetting setting)
        {
            if (setting == null)
                return;
            InitializeComponent();
            this.setting = setting;
            showSettingMetadata();
        }

        private void showSettingMetadata()
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
