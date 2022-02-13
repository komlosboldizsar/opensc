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
    [EditorForSettingValue(typeof(bool))]
    public partial class BoolEditor : SettingEditorBase
    {
        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new BoolEditor(setting);
        public BoolEditor() : base() => InitializeComponent();
        public BoolEditor(ISetting setting) : base(setting) => InitializeComponent();
        protected override void readValue() => checkBox.Checked = ((Setting<bool>)setting).Value;
        protected override void writeValue() => ((Setting<bool>)setting).Value = checkBox.Checked;
        protected override void resetToDefault() => checkBox.Checked = ((Setting<bool>)setting).DefaultValue;
    }
}
