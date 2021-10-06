using OpenSC.GUI.Menus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.GUI.GeneralComponents.Menus
{
    class MenuItemManager
    {

        private IManageableCustomMenu guiElement;

        public MenuItemManager(IManageableCustomMenu guiElement)
        {
            this.guiElement = guiElement;
            guiElement.AssociatedMenuItemChanged += associatedMenuItemChanged;
            subscribeToCollectionChangeEvents();
            createChildren();
        }

        private void associatedMenuItemChanged(MenuItem oldAssociatedMenuItem, MenuItem newAssociatedMenuItem)
        {
            unsubscribeFromCollectionChangeEvents(oldAssociatedMenuItem);
            removeDynamicChildren();
            subscribeToCollectionChangeEvents();
            createChildren();
        }

        private void subscribeToCollectionChangeEvents()
        {
            if (guiElement?.AssociatedMenuItem == null)
                return;
            guiElement.AssociatedMenuItem.GroupAdded += groupAddedHandler;
            guiElement.AssociatedMenuItem.GroupRemoved += groupRemovedHandler;
        }

        private void unsubscribeFromCollectionChangeEvents(MenuItem oldMenuItem)
        {
            if (oldMenuItem == null)
                return;
            oldMenuItem.GroupAdded -= groupAddedHandler;
            oldMenuItem.GroupRemoved -= groupRemovedHandler;
        }

        private void createChildren()
        {
            if (guiElement?.AssociatedMenuItem == null)
                return;
            foreach (MenuItemGroup menuItemGroup in guiElement.AssociatedMenuItem.Groups)
                foreach (MenuItem menuItem in menuItemGroup.Items)
                    itemAddedToGroupHandler(menuItemGroup, menuItem, null);
        }

        private void addChild(MenuItem associatedMenuItem)
        {
            Func<System.Windows.Forms.ToolStripItem, bool> lastBeforeLookupFunction = (toolStripItem) =>
            {
                CustomToolStripMenuItem toolStripItemCasted = toolStripItem as CustomToolStripMenuItem;
                if (toolStripItemCasted == null)
                    return false;
                MenuItem menuItem = toolStripItemCasted.AssociatedMenuItem;
                return ((menuItem.ParentGroup.Weight <= associatedMenuItem.ParentGroup.Weight) && (menuItem.Weight <= associatedMenuItem.Weight));
            };
            // Separator before
            System.Windows.Forms.ToolStripItem lastBefore = guiElement.DropDownItems.Cast<System.Windows.Forms.ToolStripItem>().Where(lastBeforeLookupFunction).LastOrDefault();
            int position = guiElement.StartPosition;
            if (lastBefore != null)
            {
                position = guiElement.DropDownItems.IndexOf(lastBefore) + 1;
                if (!(lastBefore is System.Windows.Forms.ToolStripSeparator))
                {
                    CustomToolStripMenuItem lastBeforeCasted = lastBefore as CustomToolStripMenuItem;
                    MenuItemGroup groupBefore = lastBeforeCasted?.AssociatedMenuItem.ParentGroup;
                    if ((groupBefore != associatedMenuItem.ParentGroup) && (groupBefore != null))
                        guiElement.DropDownItems.Insert(position++, new System.Windows.Forms.ToolStripSeparator());
                }
            }
            // Menu item
            System.Windows.Forms.ToolStripItem myChild = new CustomToolStripMenuItem(associatedMenuItem);
            guiElement.DropDownItems.Insert(position++, myChild);
            // Separator after
            if (guiElement.DropDownItems.Count > position)
            {
                System.Windows.Forms.ToolStripItem firstAfter = guiElement.DropDownItems[position];
                if (firstAfter != null)
                {
                    CustomToolStripMenuItem firstAfterCasted = firstAfter as CustomToolStripMenuItem;
                    MenuItemGroup groupAfter = firstAfterCasted?.AssociatedMenuItem.ParentGroup;
                    // Add if next is another group
                    if (!(firstAfter is System.Windows.Forms.ToolStripSeparator) && (groupAfter != associatedMenuItem.ParentGroup) && (groupAfter != null))
                        guiElement.DropDownItems.Insert(position++, new System.Windows.Forms.ToolStripSeparator());
                    // Remove is next is same group
                    if ((firstAfter is System.Windows.Forms.ToolStripSeparator) && (groupAfter == associatedMenuItem.ParentGroup) && (groupAfter != null))
                        guiElement.DropDownItems.Remove(firstAfter);
                }
            }
        }

        private void removeDynamicChildren()
        {
            for (int i = guiElement.DropDownItems.Count - 1; i >= 0; i--)
            {
                CustomToolStripMenuItem customMenuItem = guiElement.DropDownItems[i] as CustomToolStripMenuItem;
                if (customMenuItem != null)
                    guiElement.DropDownItems.RemoveAt(i);
            }
        }

        private void removeChild(MenuItem associatedMenuItem)
        {
            for (int i = guiElement.DropDownItems.Count - 1; i >= 0; i--)
            {
                CustomToolStripMenuItem customMenuItem = guiElement.DropDownItems[i] as CustomToolStripMenuItem;
                if ((customMenuItem != null) && (customMenuItem.AssociatedMenuItem == associatedMenuItem))
                    guiElement.DropDownItems.RemoveAt(i);
            }
        }

        private void groupAddedHandler(MenuItem menu, MenuItemGroup group, string id)
        {
            group.ItemAdded += itemAddedToGroupHandler;
            group.ItemRemoved += itemRemovedFromGroupHandler;
        }

        private void groupRemovedHandler(MenuItem menu, MenuItemGroup group, string id)
        {
            group.ItemAdded -= itemAddedToGroupHandler;
            group.ItemRemoved -= itemRemovedFromGroupHandler;
        }

        private void itemAddedToGroupHandler(MenuItemGroup group, MenuItem item, string id) => addChild(item);
        private void itemRemovedFromGroupHandler(MenuItemGroup group, MenuItem item, string id) => removeChild(item);

    }
}
