using OpenSC.GUI.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.Menus
{
    public class CustomMenuStrip: System.Windows.Forms.MenuStrip
    {

        private MenuItem associatedMenuItem;

        public MenuItem AssociatedMenuItem
        {
            get { return associatedMenuItem; }
            set
            {
                if (value == associatedMenuItem)
                    return;
                unsubscribeModelEvents();
                removeDynamicChildren();
                associatedMenuItem = value;
                subscribeModelEvents();
                createChildren();
            }
        }

        public CustomMenuStrip()
        {
            subscribeModelEvents();
            createChildren();
        }

        private void subscribeModelEvents()
        {
            if (associatedMenuItem == null)
                return;
            associatedMenuItem.ChildAdded += childAddedHandler;
            associatedMenuItem.ChildRemoved += childRemovedHandler;
        }
        private void unsubscribeModelEvents()
        {
            if (associatedMenuItem == null)
                return;
            associatedMenuItem.ChildAdded -= childAddedHandler;
            associatedMenuItem.ChildRemoved -= childRemovedHandler;
        }

        private void createChildren()
        {
            if (associatedMenuItem == null)
                return;
            foreach (MenuItem menuItem in associatedMenuItem.Children)
                addChild(menuItem);
        }

        private void removeDynamicChildren()
        {
            for (int i = Items.Count - 1; i >= 0; i--)
                if (Items[i] is CustomToolStripMenuItem)
                    Items.RemoveAt(i);
        }

        private void addChild(MenuItem associatedMenuItem)
        {
            // UNSAFE: separators?
            System.Windows.Forms.ToolStripItem myChild = new CustomToolStripMenuItem(associatedMenuItem);
            Items.Add(myChild);
        }

        private void removeChild(MenuItem associatedMenuItem)
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                CustomToolStripMenuItem customMenuItem = Items[i] as CustomToolStripMenuItem;
                if ((customMenuItem != null) && (customMenuItem.AssociatedMenuItem == associatedMenuItem))
                    Items.RemoveAt(i);
            }
        }

        private void childAddedHandler(MenuItem parent, MenuItem child, string id)
            => addChild(child);

        private void childRemovedHandler(MenuItem parent, MenuItem child, string id)
            => removeChild(child);

    }
}
