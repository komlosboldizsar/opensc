using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Settings
{
    public partial class SettingsWindow : ChildWindowWithTitle
    {

        public SettingsWindow()
        {
            InitializeComponent();
            tabControl.TabPages.Clear();
            loadSettings();
        }

        private void loadSettings()
        {
            foreach(var setting in SettingsManager.Instance.RegisteredSettings)
            {
                ISettingEditorControl editorControl = SettingEditorTypeRegister.GetEditorForSetting(setting);
                Control editorControlCasted = editorControl as Control;
                if (editorControlCasted != null)
                    getPageForCategory(setting.Category).Controls.Add(editorControlCasted);
            }
        }

        Dictionary<string, TabPage> tabPagesForCategories = new Dictionary<string, TabPage>();

        private TabPage getPageForCategory(string category)
        {
            if(!tabPagesForCategories.TryGetValue(category, out TabPage page))
            {
                page = new TabPage(category);
                page.AutoScroll = true;
                tabControl.TabPages.Add(page);
            }
            return page;
        }

    }
}
