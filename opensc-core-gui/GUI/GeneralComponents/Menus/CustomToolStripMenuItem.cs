using OpenSC.GUI.Menus;
using System;
using System.Linq;

namespace OpenSC.GUI.GeneralComponents.Menus
{

    public class CustomToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem, IManageableCustomMenu
    {

        public MenuItem AssociatedMenuItem { get; private set; }
        public event AssociatedMenuItemChangedDelegate AssociatedMenuItemChanged;
        public int StartPosition { get; } = 0;
        private MenuItemManager manager;

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
        private void clickHandler(object sender, EventArgs e) => AssociatedMenuItem?.HandleClick();

    }

}
