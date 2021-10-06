using OpenSC.GUI.Menus;
using OpenSC.GUI.SerialPorts;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("serialports-gui", "Serial ports (GUI)", "TODO")]
    [DependsOnModule(typeof(SerialportsModelModule))]
    public class SerialportsGuiModule : BasetypeGuiModuleBase<SerialportsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<SerialPortList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Serial ports";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "Serial port list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new SerialPortList().ShowAsChild();
        }

    }

}
