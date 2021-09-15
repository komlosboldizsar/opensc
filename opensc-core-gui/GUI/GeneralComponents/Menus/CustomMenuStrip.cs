using OpenSC.GUI.Menus;
using System;

namespace OpenSC.GUI.GeneralComponents.Menus
{
    public class CustomMenuStrip : System.Windows.Forms.MenuStrip, IManageableCustomMenu
    {

        public System.Windows.Forms.ToolStripItemCollection DropDownItems => Items;

        private MenuItem associatedMenuItem;

        public MenuItem AssociatedMenuItem
        {
            get => associatedMenuItem;
            set
            {
                if (value == associatedMenuItem)
                    return;
                MenuItem oldAssociatedMenuItem = associatedMenuItem;
                associatedMenuItem = value;
                AssociatedMenuItemChanged?.Invoke(oldAssociatedMenuItem, associatedMenuItem);
            }
        }

        public event AssociatedMenuItemChangedDelegate AssociatedMenuItemChanged;

        private int dynamicChildrenInsertPosition;

        public int DynamicChildrenInsertPosition
        {
            get => dynamicChildrenInsertPosition;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                dynamicChildrenInsertPosition = value;
            }
        }

        public int StartPosition => DynamicChildrenInsertPosition;

        private MenuItemManager manager;

        public CustomMenuStrip()
        {
            manager = new MenuItemManager(this);
        }

    }
}
