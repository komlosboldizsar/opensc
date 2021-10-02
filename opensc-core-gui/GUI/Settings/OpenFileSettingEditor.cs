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

    [SettingEditorControlType(typeof(OpenFilePath))]
    public partial class OpenFileSettingEditor : SettingEditorBase
    {

        public override ISettingEditorControl GetInstanceForSetting(ISetting setting) => new OpenFileSettingEditor(setting);
        public OpenFileSettingEditor() : base() => InitializeComponent();
        public OpenFileSettingEditor(ISetting setting) : base(setting) => InitializeComponent();

        protected override void readValue()
        {
            filePathTextBox.Text = ((OpenFileSetting)setting).Value;
            copyToProgramFolderCheckbox.Checked = false;
        }
        protected override void writeValue()
        {
            if (copyToProgramFolderCheckbox.Visible && copyToProgramFolderCheckbox.Checked)
                filePathTextBox.Text = ((OpenFileSetting)setting).SetValueWithCopy(filePathTextBox.Text);
            else
                ((OpenFileSetting)setting).Value = filePathTextBox.Text;
            copyToProgramFolderCheckbox.Checked = false;
        }

        protected override void resetToDefault()
        {
            filePathTextBox.Text = ((OpenFileSetting)setting).Value;
            copyToProgramFolderCheckbox.Checked = false;
        }

        protected override void initEditor()
        {
            copyToProgramFolderCheckbox.Visible = ((OpenFileSetting)setting).CopyEnabled;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = ((OpenFileSetting)setting).FileFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePathTextBox.Text = openFileDialog.FileName;
            }
        }

    }

}
