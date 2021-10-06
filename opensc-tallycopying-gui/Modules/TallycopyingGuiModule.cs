using OpenSC.GUI.Menus;
using OpenSC.GUI.Signals.TallyCopying;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("tallycopying-gui", "Tally copying (GUI)", "TODO")]
    [DependsOnModule(typeof(TallycopyingModelModule))]
    public class TallycopyingGuiModule : BasetypeGuiModuleBase<TallycopyingModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<TallyCopyList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Signals";
        public const string MENUGROUP_ID = "tallycopying";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 10;
        public const string SUBMENU_LABEL = "Tally copy list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new TallyCopyList().ShowAsChild();
        }

    }

}
