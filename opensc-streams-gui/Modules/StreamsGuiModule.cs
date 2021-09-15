using OpenSC.GUI.Menus;
using OpenSC.GUI.Streams;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("streams-gui", "Streams (GUI)", "TODO")]
    [DependsOnModule(typeof(StreamsModelModule))]
    public class StreamsGuiModule : BasetypeGuiModuleBase<StreamsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<StreamList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Streams";
        public const string MENUGROUP_ID = MenuManager.GROUP_ID_BASE;
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "Stream list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new StreamList().ShowAsChild();
        }

    }

}
