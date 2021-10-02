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

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new IntSettingEditor(setting);
        public IntSettingEditor() : base() => InitializeComponent();
        public IntSettingEditor(ISetting setting) : base(setting) => InitializeComponent();

        protected override void readValue() => valueNumericField.Value = ((IntSetting)setting).Value;
        protected override void writeValue() => ((IntSetting)setting).Value = (int)valueNumericField.Value;
        protected override void resetToDefault() => valueNumericField.Value = ((IntSetting)setting).DefaultValue;

        protected override void initEditor()
        {
            IntSetting typedSetting = (IntSetting)setting;
            valueNumericField.Minimum = typedSetting.MinValue ?? -99999;
            valueNumericField.Maximum = typedSetting.MaxValue ?? 99999;

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

    }

}
