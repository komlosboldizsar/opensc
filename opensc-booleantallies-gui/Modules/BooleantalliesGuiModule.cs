using OpenSC.GUI.Menus;
using OpenSC.GUI.Signals.BooleanTallies;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("booleantallies-gui", "Boolean tallies (GUI)", "TODO")]
    [DependsOnModule(typeof(BooleantalliesModelModule))]
    public class BooleantalliesGuiModule : BasetypeGuiModuleBase<BooleantalliesModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<BooleanTallyList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Variables";
        public const string MENUGROUP_ID = "booleantallies";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 10;
        public const string SUBMENU_LABEL = "Boolean tally list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new BooleanTallyList().ShowAsChild();
        }

    }

}
