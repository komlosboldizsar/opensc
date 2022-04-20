using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Menus
{

    public static class CheckedSettingBindingHelper
    {

        public static void BindCheckedSetting(this ToolStripMenuItem toolStripMenuItem, Setting<bool> setting)
        {
            menuItemsBindings.TryGetValue(toolStripMenuItem, out Setting<bool> oldSetting);
            if (oldSetting == setting)
                return;
            if (setting != null)
            {
                menuItemsBindings[toolStripMenuItem] = setting;
            }
            else
            {
                menuItemsBindings.Remove(toolStripMenuItem);
            }
            if ((oldSetting != null) && (settingsBindings.TryGetValue(oldSetting, out List<ToolStripMenuItem> oldSettingsMenuItems)))
            {
                oldSettingsMenuItems.RemoveAll(mi => (mi == toolStripMenuItem));
                if (oldSettingsMenuItems.Count == 0)
                {
                    settingsBindings.Remove(oldSetting);
                    oldSetting.ValueChanged -= settingsValueChangedHandler;
                }
                if (setting == null)
                    toolStripMenuItem.Click -= toolStripMenuItemClickHandler;
            }
            if (setting != null)
            {
                if (!settingsBindings.TryGetValue(setting, out List<ToolStripMenuItem> settingsMenuItems))
                {
                    settingsMenuItems = new();
                    settingsBindings.Add(setting, settingsMenuItems);
                    setting.ValueChanged += settingsValueChangedHandler;
                }
                settingsMenuItems.Add(toolStripMenuItem);
                if (oldSetting == null)
                    toolStripMenuItem.Click += toolStripMenuItemClickHandler;
                toolStripMenuItem.Checked = setting.Value;
            }
        }

        private static void toolStripMenuItemClickHandler(object sender, EventArgs e)
        {
            if (!menuItemsBindings.TryGetValue((ToolStripMenuItem)sender, out Setting<bool> boundSetting))
                return;
            boundSetting.Value = !boundSetting.Value;
        }

        private static void settingsValueChangedHandler(ISetting setting, object oldValue, object newValue)
        {
            if (!settingsBindings.TryGetValue((Setting<bool>)setting, out List<ToolStripMenuItem> menuItems))
                return;
            menuItems.ForEach(mi => mi.Checked = (bool)newValue);
        }

        private static Dictionary<ToolStripMenuItem, Setting<bool>> menuItemsBindings = new();
        private static Dictionary<Setting<bool>, List<ToolStripMenuItem>> settingsBindings = new();
    
    }

}
