using OpenSC.GUI.Menus;
using OpenSC.GUI.Mixers;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("mixers-gui", "Mixers (GUI)", "TODO")]
    [DependsOnModule(typeof(MixersModelModule))]
    public class MixersGuiModule : BasetypeGuiModuleBase<MixersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MixerList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Mixers";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "Mixer list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new MixerList().ShowAsChild();
        }

    }

}
