using OpenSC.GUI.Menus;
using OpenSC.Model.Settings;
using System;
using System.Linq;

namespace OpenSC.GUI.GeneralComponents.Menus
{

    public class CustomToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem, IManageableCustomMenu
    {

        public MenuItem AssociatedMenuItem { get; private set; }
#pragma warning disable CS0067
        public event AssociatedMenuItemChangedDelegate AssociatedMenuItemChanged;
#pragma warning restore CS0067
        public int StartPosition { get; } = 0;
#pragma warning disable IDE0052
        private readonly MenuItemManager manager;
#pragma warning restore IDE0052

        public CustomToolStripMenuItem(MenuItem associatedMenuItem)
        {
            AssociatedMenuItem = associatedMenuItem;
            manager = new MenuItemManager(this);
            subscribePropertyChangeEvents();
            updateProperties();
            Click += clickHandler;
        }

        private void subscribePropertyChangeEvents()
        {
            AssociatedMenuItem.TextChanged += textChangedHandler;
            AssociatedMenuItem.ImageChanged += imageChangedHandler;
            AssociatedMenuItem.TagChanged += tagChangedHandler;
            AssociatedMenuItem.BoundCheckedSettingChanged += boundCheckedSettingChangedHandler;
        }

        private void updateProperties()
        {
            Text = AssociatedMenuItem.Text;
            Image = AssociatedMenuItem.Image;
            Tag = AssociatedMenuItem.Tag;
        }

        private void textChangedHandler(MenuItem menuItem, string oldText, string newText) => Text = newText;
        private void imageChangedHandler(MenuItem menuItem, System.Drawing.Bitmap oldImage, System.Drawing.Bitmap newImage) => Image = newImage;
        private void tagChangedHandler(MenuItem menuItem, object oldTag, object newTag) => Tag = newTag;

        private void boundCheckedSettingChangedHandler(MenuItem menuItem, Setting<bool> oldSetting, Setting<bool> newSetting)
        {
            if (oldSetting != null)
                oldSetting.ValueChanged -= boundCheckedSettingsValueChangedHandler;
            if (newSetting != null)
            {
                Checked = newSetting.Value;
                newSetting.ValueChanged += boundCheckedSettingsValueChangedHandler;
            }
            else
            {
                Checked = false;
            }
        }

        private void boundCheckedSettingsValueChangedHandler(ISetting setting, object oldValue, object newValue)
            => Checked = (bool)newValue;

        private void clickHandler(object sender, EventArgs e)
        {
            AssociatedMenuItem?.HandleClick();
            Setting<bool> boundCheckedSetting = AssociatedMenuItem?.BoundCheckedSetting;
            if (boundCheckedSetting != null)
                boundCheckedSetting.Value = !boundCheckedSetting.Value;
        }

    }

}
