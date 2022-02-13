using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("dynamictexts-gui", "Dynamic texts (GUI)", "TODO")]
    [DependsOnModule(typeof(DynamictextsModelModule))]
    public class DynamictextsGuiModule : BasetypeGuiModuleBase<DynamictextsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<DynamicTextList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Variables";
        public const string MENUGROUP_ID = "dynamictexts";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE;
        public const string SUBMENU_LABEL = "Dynamic text list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new DynamicTextList().ShowAsChild();
        }

    }

}
