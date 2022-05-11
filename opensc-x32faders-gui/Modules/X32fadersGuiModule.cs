using OpenSC.GUI.Menus;
using OpenSC.GUI.X32Faders;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.X32Faders;

namespace OpenSC.Modules
{

    [Module("x32faders-gui", "X32 faders (GUI)", "TODO")]
    [DependsOnModule(typeof(X32fadersModelModule))]
    public class X32fadersGuiModule : BasetypeGuiModuleBase<X32fadersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<X32FaderList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Audio";
        public const string MENUGROUP_ID = "x32faders";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 10;
        public const string SUBMENU_SALVOS_LABEL = "X32 fader list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_SALVOS_LABEL].ClickHandler = (menu, tag) => new X32FaderList().ShowAsChild();
        }

    }

}
