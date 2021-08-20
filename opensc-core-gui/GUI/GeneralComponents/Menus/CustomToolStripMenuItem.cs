using OpenSC.GUI.Menus;
using System;

namespace OpenSC.GUI.GeneralComponents.Menus
{
    public class CustomToolStripMenuItem: System.Windows.Forms.ToolStripMenuItem
    {

        public MenuItem AssociatedMenuItem { get; private set; }

        public CustomToolStripMenuItem(MenuItem associatedMenuItem)
        {

            this.AssociatedMenuItem = associatedMenuItem;

            subscribeModelEvents();
            updateProperties();
            createChildren();

            Click += clickHandler;

        }

        private void subscribeModelEvents()
        {
            AssociatedMenuItem.ChildAdded += childAddedHandler;
            AssociatedMenuItem.ChildRemoved += childRemovedHandler;
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

        private void createChildren()
        {
            foreach (MenuItem menuItem in AssociatedMenuItem.Children)
                addChild(menuItem);
        }

        private void addChild(MenuItem associatedMenuItem)
        {
            System.Windows.Forms.ToolStripItem myChild;
            if (associatedMenuItem is SeparatorMenuItem)
                myChild = new System.Windows.Forms.ToolStripSeparator();
            else
                myChild = new CustomToolStripMenuItem(associatedMenuItem);
            DropDownItems.Add(myChild);
        }

        private void removeChild(MenuItem associatedMenuItem)
        {
            for (int i = DropDownItems.Count - 1; i >= 0; i--) {
                CustomToolStripMenuItem customMenuItem = DropDownItems[i] as CustomToolStripMenuItem;
                if ((customMenuItem != null) && (customMenuItem.AssociatedMenuItem == associatedMenuItem))
                    DropDownItems.RemoveAt(i);                    
            }
        }

        private void textChangedHandler(MenuItem menuItem, string oldText, string newText)
            => Text = newText;

        private void imageChangedHandler(MenuItem menuItem, System.Drawing.Bitmap oldImage, System.Drawing.Bitmap newImage)
            => Image = newImage;

        private void tagChangedHandler(MenuItem menuItem, object oldTag, object newTag)
            => Tag = newTag;

        private void childAddedHandler(MenuItem parent, MenuItem child, string id)
            => addChild(child);

        private void childRemovedHandler(MenuItem parent, MenuItem child, string id)
            => removeChild(child);

        private void clickHandler(object sender, EventArgs e)
        {
            AssociatedMenuItem?.HandleClick();
        }

    }
}
