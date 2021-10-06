using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{
    [Module("booleans-gui", "Booleans (GUI)", "TODO")]
    [DependsOnModule(typeof(BooleansModelModule))]
    public class BooleansGuiModule : BasetypeGuiModuleBase<BooleansModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<BooleanList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Variables";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_BOOLEANS_LABEL = "Booleans";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_BOOLEANS_LABEL].ClickHandler = (menu, tag) => new BooleanList().ShowAsChild();
        }

    }

}
