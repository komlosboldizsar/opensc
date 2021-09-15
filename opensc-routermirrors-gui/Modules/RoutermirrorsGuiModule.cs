using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers.Mirrors;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("routermirrors-gui", "Router mirrors (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutermirrorsModelModule))]
    public class RoutermirrorsGuiModule : BasetypeGuiModuleBase<RoutermirrorsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<RouterMirrorList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Routers";
        public const string MENUGROUP_ID = "routermirrors";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 10;
        public const string SUBMENU_LABEL = "Router mirror list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new RouterMirrorList().ShowAsChild();
        }

    }

}
