using OpenSC.GUI.Menus;
using OpenSC.GUI.GpioInterfaces;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.GpioInterfaces;

namespace OpenSC.Modules
{

    [Module("gpiointerfaces-gui", "GPIO interfaces (GUI)", "TODO")]
    [DependsOnModule(typeof(GpiointerfacesModelModule))]
    public class GpiointerfacesGuiModule : BasetypeGuiModuleBase<GpiointerfacesModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<GpioInterfaceList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "GPIO";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_GPIOINTERFACES_LABEL = "GPIO interface list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_GPIOINTERFACES_LABEL].ClickHandler = (menu, tag) => new GpioInterfaceList().ShowAsChild();
        }

    }

}
