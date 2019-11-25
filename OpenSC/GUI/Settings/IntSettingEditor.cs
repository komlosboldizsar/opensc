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

    [SettingEditorControlType(typeof(int))]
    public partial class IntSettingEditor : SettingEditorBase
    {

        public IntSettingEditor():
            base()
        {
            InitializeComponent();
        }

        public IntSettingEditor(ISetting setting):
            base(setting)
        {
            InitializeComponent();
            valueTextBox.Text = ((IntSetting)setting).Value.ToString();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            if (setting == null)
                return;

            try
            {
                if (!int.TryParse(valueTextBox.Text, out int intValue))
                    throw new ArgumentException("Invalid integer.");
                ((IntSetting)setting).Value = intValue;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (setting != null)
                valueTextBox.Text = ((IntSetting)setting).Value.ToString();
        }

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting)
        {
            return new IntSettingEditor(setting);
        }

    }
}
