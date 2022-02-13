using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers.CrosspointStores;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("crosspointstores-gui", "Crosspoint stores (GUI)", "TODO")]
    [DependsOnModule(typeof(CrosspointstoresModelModule))]
    public class CrosspointstoresGuiModule : BasetypeGuiModuleBase<CrosspointstoresModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<CrosspointStoreList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Routers";
        public const string MENUGROUP_ID = "crosspointstores";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 10;
        public const string SUBMENU_LABEL = "Crosspoint store list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new CrosspointStoreList().ShowAsChild();
        }

    }

}
