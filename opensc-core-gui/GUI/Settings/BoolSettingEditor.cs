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

    [SettingEditorControlType(typeof(bool))]
    public partial class BoolSettingEditor : SettingEditorBase
    {

        public BoolSettingEditor() : base() => InitializeComponent();
        public BoolSettingEditor(ISetting setting) : base(setting) => InitializeComponent();

        private void IntSettingEditor_Load(object sender, EventArgs e) => checkBox.Checked = ((Setting<bool>)setting).Value;

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (setting == null)
                return;
            try
            {
                ((Setting<bool>)setting).Value = checkBox.Checked;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (setting != null)
                checkBox.Checked = ((Setting<bool>)setting).Value;
        }

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new BoolSettingEditor(setting);

    }

}
