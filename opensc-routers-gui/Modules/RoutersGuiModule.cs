using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Routers;

namespace OpenSC.Modules
{

    [Module("routers-gui", "Routers (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersGuiModule : BasetypeGuiModuleBase<RoutersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<RouterList>();
            WindowTypeRegister.RegisterWindowType<RouterControlForm>();
            WindowTypeRegister.RegisterWindowType<RouterControlTableForm>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Routers";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_ROUTERS_LABEL = "Router list";
        public const string SUBMENU_ALLCROSSPOINTS_LABEL = "All crosspoints";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_ROUTERS_LABEL].ClickHandler = (menu, tag) => new RouterList().ShowAsChild();
            menuGroup[SUBMENU_ALLCROSSPOINTS_LABEL].ClickHandler = (menu, tag) => new RouterControlTableForm(RouterDatabase.Instance).ShowAsChild();
        }

    }

}
