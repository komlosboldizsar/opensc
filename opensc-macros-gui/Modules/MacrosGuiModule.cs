using OpenSC.GUI.Macros;
using OpenSC.GUI.Menus;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("macros-gui", "Macros (GUI)", "TODO")]
    [DependsOnModule(typeof(MacrosModelModule))]
    public class MacrosGuiModule : BasetypeGuiModuleBase<MacrosModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MacroList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Macros";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "Macro list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new MacroList().ShowAsChild();
        }

    }

}
