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

    [SettingEditorControlType(typeof(Enum))]
    public partial class EnumSettingEditor : SettingEditorBase
    {

        public EnumSettingEditor() : base() => InitializeComponent();
        public EnumSettingEditor(ISetting setting) : base(setting) => InitializeComponent();

        private void EnumSettingEditor_Load(object sender, EventArgs e)
        {
            EnumSetting enumSetting = (EnumSetting)setting;
            IComboBoxAdapter enumComboBoxAdapter = new EnumComboBoxAdapter<Enum>(enumSetting.EnumType, enumSetting.Translations, enumSetting.NullTranslation);
            dropDown.SetAdapterAsDataSource(enumComboBoxAdapter);
            dropDown.SelectByValue(enumSetting.Value);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (setting == null)
                return;
            try
            {
                ((EnumSetting)setting).Value = (Enum)dropDown.SelectedValue;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (setting != null)
                dropDown.SelectByValue(((EnumSetting)setting).Value);
        }

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new EnumSettingEditor(setting);

    }

}
