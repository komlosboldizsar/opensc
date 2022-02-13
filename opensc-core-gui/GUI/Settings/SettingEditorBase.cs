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

        public virtual ISettingEditorControl GetInstanceForSetting(ISetting setting) => new SettingEditorBase(setting);

        public SettingEditorBase() => InitializeComponent();

        public SettingEditorBase(ISetting setting)
        {
            if (setting == null)
                return;
            InitializeComponent();
            this.setting = setting;
            showSettingMetadata();
        }

        protected ISetting setting { get; private set; }

        private void showSettingMetadata()
        {
            settingTitleLabel.Text = setting.HumanReadableTitle;
            settingDescriptionLabel.Text = setting.HumanReadableDescription;
        }

        private void SettingEditorBase_Load(object sender, EventArgs e)
        {
            initEditor();
            readValue();
        }

        protected virtual void initEditor() { }
        protected virtual void readValue() { }

        private void resetToDefaultButton_Click(object sender, EventArgs e) => resetToDefault();
        protected virtual void resetToDefault() { }

        private void resetToCurrentButton_Click(object sender, EventArgs e) => resetToCurrent();
        protected virtual void resetToCurrent() => readValue();

        private void saveButton_Click(object sender, EventArgs e) => saveSetting();

        protected virtual void saveSetting()
        {
            if (setting == null)
                return;
            try
            {
                writeValue();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual void writeValue() { }

    }
}
