using OpenSC.GUI.Menus;
using OpenSC.GUI.UMDs;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("umds-gui", "UMDs (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsGuiModule : BasetypeGuiModuleBase<UmdsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<UmdList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "UMDs";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "UMD list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new UmdList().ShowAsChild();
        }

    }

}
