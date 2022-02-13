using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers.CrosspointBooleans;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("crosspointbooleans-gui", "Crosspoint booleans (GUI)", "TODO")]
    [DependsOnModule(typeof(CrosspointbooleansModelModule))]
    public class CrosspointbooleansGuiModule : BasetypeGuiModuleBase<CrosspointbooleansModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<CrosspointBooleanList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Routers";
        public const string MENUGROUP_ID = "crosspointbooleans";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 10;
        public const string SUBMENU_LABEL = "Crosspoint boolean list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new CrosspointBooleanList().ShowAsChild();
        }

    }

}
