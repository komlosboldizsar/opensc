using OpenSC.GUI.Menus;

namespace OpenSC.GUI.GeneralComponents.Menus
{
    public delegate void AssociatedMenuItemChangedDelegate(MenuItem oldAssociatedMenuItem, MenuItem newAssociatedMenuItem);
    public interface IManageableCustomMenu
    {
        MenuItem AssociatedMenuItem { get; }
        event AssociatedMenuItemChangedDelegate AssociatedMenuItemChanged;
        System.Windows.Forms.ToolStripItemCollection DropDownItems { get; }
        int StartPosition { get; }
    }
}
