using OpenSC.GUI.Menus;
using OpenSC.GUI.Timers;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("timers-gui", "Timers (GUI)", "TODO")]
    [DependsOnModule(typeof(TimersModelModule))]
    public class TimersGuiModule : BasetypeGuiModuleBase<TimersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<TimerList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Timers";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "Timer list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new TimerList().ShowAsChild();
        }

    }

}
