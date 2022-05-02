using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("custombooleans-gui", "Custom booleans (GUI)", "TODO")]
    [DependsOnModule(typeof(CustombooleansModelModule))]
    public class CustombooleansGuiModule : BasetypeGuiModuleBase<CustombooleansModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<CustomBooleanList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Variables";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_CUSTOMBOOLEANS_LABEL = "Custom boolean list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_CUSTOMBOOLEANS_LABEL].ClickHandler = (menu, tag) => new CustomBooleanList().ShowAsChild();
        }

    }

}
