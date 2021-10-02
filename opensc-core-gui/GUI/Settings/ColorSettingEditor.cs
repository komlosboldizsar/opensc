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

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new ColorSettingEditor(setting);
        public ColorSettingEditor() : base() => InitializeComponent();
        public ColorSettingEditor(ISetting setting) : base(setting) => InitializeComponent();

        protected override void readValue() => storedColor = ((Setting<Color>)setting).Value;
        protected override void writeValue() => ((Setting<Color>)setting).Value = storedColor;
        protected override void resetToDefault() => storedColor = ((Setting<Color>)setting).DefaultValue;

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
