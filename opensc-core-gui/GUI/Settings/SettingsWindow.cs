using OpenSC.GUI.WorkspaceManager;
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

    [WindowTypeName("settings")]
    public partial class SettingsWindow : ChildWindowWithTitle
    {

        public static SettingsWindow Instance { get; } = new SettingsWindow();

        private SettingsWindow()
        {
            InitializeComponent();
            tabControl.TabPages.Clear();
            loadSettings();
        }

        private void loadSettings()
        {
            foreach(var setting in SettingsManager.Instance.RegisteredSettings)
            {
                ISettingEditorControl editorControl = SettingEditorTypeRegister.Instance.GetEditorForSetting(setting);
                Control editorControlCasted = editorControl as Control;
                if (editorControlCasted != null)
                {
                    editorControlCasted.Dock = DockStyle.Top;
                    TabPage pageForCategory = getPageForCategory(setting.Category);
                    Control.ControlCollection pagesControlCollection = pageForCategory.Controls;
                    pagesControlCollection.Add(editorControlCasted);
                    pagesControlCollection.SetChildIndex(editorControlCasted, 0);
                }
            }
        }

        Dictionary<string, TabPage> tabPagesForCategories = new Dictionary<string, TabPage>();

        private TabPage getPageForCategory(string category, bool create = true)
        {
            if(!tabPagesForCategories.TryGetValue(category, out TabPage page) && create)
            {
                page = new TabPage(category);
                page.AutoScroll = true;
                tabControl.TabPages.Add(page);
                tabPagesForCategories.Add(category, page);
            }
            return page;
        }

        #region Persistence
        private const string PERSISTENCE_KEY_TABPAGE = "tabpage";

        protected override void restoreBeforeOpen(Dictionary<string, object> keyValuePairs)
        {
            base.restoreBeforeOpen(keyValuePairs);
            string tabPageId = keyValuePairs[PERSISTENCE_KEY_TABPAGE].ToString();
            TabPage tabPage = getPageForCategory(tabPageId, false);
            if (tabPage != null)
                tabControl.SelectedTab = tabPage;
        }

        public override Dictionary<string, object> GetKeyValuePairs()
        {
            var dict = base.GetKeyValuePairs();
            dict.Add(PERSISTENCE_KEY_TABPAGE, (string)tabControl.SelectedTab.Text);
            return dict;
        }
        #endregion

    }
}
