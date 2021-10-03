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
using OpenSC.GUI.GeneralComponents.DropDowns;

namespace OpenSC.GUI.Settings
{

    [EditorForSetting(typeof(Enum))]
    public partial class EnumSettingEditor : SettingEditorBase
    {

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new EnumSettingEditor(setting);
        public EnumSettingEditor() : base() => InitializeComponent();
        public EnumSettingEditor(ISetting setting) : base(setting) => InitializeComponent();

        protected override void readValue() => dropDown.SelectByValue(((EnumSetting)setting).Value);
        protected override void writeValue() => ((EnumSetting)setting).Value = (Enum)dropDown.SelectedValue;
        protected override void resetToDefault() => dropDown.SelectByValue(((EnumSetting)setting).DefaultValue);

        protected override void initEditor()
        {
            EnumSetting enumSetting = (EnumSetting)setting;
            IComboBoxAdapter enumComboBoxAdapter = new EnumComboBoxAdapter<Enum>(enumSetting.EnumType, enumSetting.Translations, enumSetting.NullTranslation);
            dropDown.SetAdapterAsDataSource(enumComboBoxAdapter);
        }

    }

}
