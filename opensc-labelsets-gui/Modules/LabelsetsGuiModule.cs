using OpenSC.GUI.Menus;
using OpenSC.GUI.Labelsets;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("labelsets-gui", "Labelsets (GUI)", "TODO")]
    [DependsOnModule(typeof(LabelsetsModelModule))]
    public class LabelsetsGuiModule : BasetypeGuiModuleBase<LabelsetsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<LabelsetList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Labelsets";
        public const string MENUGROUP_ID = "labelsets";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 5;
        public const string SUBMENU_LABEL = "Labelset list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_LABEL].ClickHandler = (menu, tag) => new LabelsetList().ShowAsChild();
        }

    }

}
