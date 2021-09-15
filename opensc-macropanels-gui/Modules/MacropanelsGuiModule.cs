using OpenSC.GUI.Macros;
using OpenSC.GUI.Menus;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("macropanels-gui", "Macro panels (GUI)", "TODO")]
    [DependsOnModule(typeof(MacropanelsModelModule))]
    public class MacropanelsGuiModule : BasetypeGuiModuleBase<MacropanelsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MacroPanelList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Macros";
        public const string MENUGROUP_ID = "macropanels";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 5;
        public const string SUBMENU_LABEL = "Macro panel list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new MacroPanelList().ShowAsChild();
        }

    }

}
