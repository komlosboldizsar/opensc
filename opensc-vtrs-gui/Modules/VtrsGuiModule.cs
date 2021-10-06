using OpenSC.GUI.Menus;
using OpenSC.GUI.VTRs;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("vtrs-gui", "VTRs (GUI)", "TODO")]
    [DependsOnModule(typeof(VtrsModelModule))]
    public class VtrsGuiModule : BasetypeGuiModuleBase<VtrsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<VtrList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "VTRs";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "VTR list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new VtrList().ShowAsChild();
        }

    }

}
