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
        }

        private void IntSettingEditor_Load(object sender, EventArgs e)
        {

            IntSetting typedSetting = (IntSetting)setting;
            valueNumericField.Minimum = typedSetting.MinValue ?? -99999;
            valueNumericField.Maximum = typedSetting.MaxValue ?? 99999;
            valueNumericField.Value = typedSetting.Value;

            List<string> hintTexts = new List<string>();
            if (typedSetting.MinValue != null)
                hintTexts.Add(string.Format("min: {0}", typedSetting.MinValue));
            if (typedSetting.MaxValue != null)
                hintTexts.Add(string.Format("max: {0}", typedSetting.MaxValue));

            if (hintTexts.Count > 0)
                minMaxHintLabel.Text = string.Format("({0})", string.Join(", ", hintTexts.ToArray()));
            else
                minMaxHintLabel.Text = "";

        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            if (setting == null)
                return;

            try
            {
                ((IntSetting)setting).Value = (int)valueNumericField.Value;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (setting != null)
                valueNumericField.Value = ((IntSetting)setting).Value;
        }

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting)
        {
            return new IntSettingEditor(setting);
        }

    }
}
