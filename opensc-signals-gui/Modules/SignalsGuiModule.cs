using OpenSC.GUI.Menus;
using OpenSC.GUI.Signals;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("signals-gui", "Signals (GUI)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class SignalsGuiModule : BasetypeGuiModuleBase<SignalsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<SignalList>();
            WindowTypeRegister.RegisterWindowType<ExternalSignalList>();
            WindowTypeRegister.RegisterWindowType<ExternalSignalCategoryList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Signals";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_SIGNALLIST_LABEL = "All signals";
        public const string SUBMENU_EXTERNALSIGNALLIST_LABEL = "External signals";
        public const string SUBMENU_EXTERNALSIGNALCATEGORYLIST_LABEL = "External signal categories";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_SIGNALLIST_LABEL].ClickHandler = (menu, tag) => new SignalList().ShowAsChild();
            menuGroup[SUBMENU_EXTERNALSIGNALLIST_LABEL].ClickHandler = (menu, tag) => new ExternalSignalList().ShowAsChild();
            menuGroup[SUBMENU_EXTERNALSIGNALCATEGORYLIST_LABEL].ClickHandler = (menu, tag) => new ExternalSignalCategoryList().ShowAsChild();
        }

    }

}
