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
        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new BoolSettingEditor(setting);
        public BoolSettingEditor() : base() => InitializeComponent();
        public BoolSettingEditor(ISetting setting) : base(setting) => InitializeComponent();
        protected override void readValue() => checkBox.Checked = ((Setting<bool>)setting).Value;
        protected override void writeValue() => ((Setting<bool>)setting).Value = checkBox.Checked;
        protected override void resetToDefault() => checkBox.Checked = ((Setting<bool>)setting).DefaultValue;
    }
}
