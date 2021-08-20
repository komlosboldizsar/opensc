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
using System.Diagnostics;

namespace OpenSC.GUI.Settings
{

    [SettingEditorControlType(typeof(string))]
    public partial class StringSettingEditor : SettingEditorBase
    {

        public StringSettingEditor():
            base()
        {
            InitializeComponent();
        }

        public StringSettingEditor(ISetting setting):
            base(setting)
        {
            InitializeComponent();
            valueTextBox.Text = ((Setting<string>)setting).Value;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (setting != null)
                ((Setting<string>)setting).Value = valueTextBox.Text;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (setting != null)
                valueTextBox.Text = ((Setting<string>)setting).Value;
        }

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting)
        {
            return new StringSettingEditor(setting);
        }

    }
}
