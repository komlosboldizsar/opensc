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
        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new StringSettingEditor(setting);
        public StringSettingEditor() : base() => InitializeComponent();
        public StringSettingEditor(ISetting setting) : base(setting) => InitializeComponent();
        protected override void readValue() => valueTextBox.Text = ((Setting<string>)setting).Value;
        protected override void writeValue() => ((Setting<string>)setting).Value = valueTextBox.Text;
        protected override void resetToDefault() => valueTextBox.Text = ((Setting<string>)setting).DefaultValue;
    }
}
