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

    [SettingEditorControlType(typeof(Color))]
    public partial class ColorSettingEditor : SettingEditorBase
    {

        public ColorSettingEditor() : base() => InitializeComponent();
        public ColorSettingEditor(ISetting setting) : base(setting) => InitializeComponent();

        private void IntSettingEditor_Load(object sender, EventArgs e) => storedColor = ((Setting<Color>)setting).Value;

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (setting == null)
                return;
            try
            {
                ((Setting<Color>)setting).Value = storedColor;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (setting != null)
                storedColor = ((Setting<Color>)setting).Value;
        }

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new ColorSettingEditor(setting);

        private void selectColorButton_Click(object sender, EventArgs e)
        {
            colorDialog.Color = storedColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
                storedColor = colorDialog.Color;
        }

        private Color _storedColor;

        private Color storedColor
        {
            get => _storedColor;
            set
            {
                _storedColor = value;
                selectColorButton.BackColor = value;
            }
        }

    }

}
