using OpenSC.GUI.Menus;
using OpenSC.GUI.UMDs;
using OpenSC.Model.UMDs.BmdSmartView;

namespace OpenSC.Modules
{

    [Module("umds-subtype-bmdsmartview-gui", "UMDs / BMD SmartView (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsSubtypeBmdsmartviewModelModule))]
    public class UmdsSubtypeBmdsmartviewGuiModule : SubtypeGuiModuleBase<UmdsSubtypeBmdsmartviewModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            UmdEditorFormTypeRegister.Instance.RegisterFormType<BmdSmartViewDisplay, BmdSmartViewDisplayEditorForm>();
        }

        public const string TOPMENUGROUP_ID = UmdsGuiModule.TOPMENUGROUP_ID;
        public const string TOPMENU_LABEL = UmdsGuiModule.TOPMENU_LABEL;
        public const string MENUGROUP_ID = "subtypes";
        public const int MENUGROUP_WEIGHT = UmdsGuiModule.MENUGROUP_WEIGHT + 10;
        public const string SUBMENU_LABEL = "BMD SmartView";
        public const string SUBMENU_GROUP_ID = "base";
        public const int SUBMENU_GROUP_WEIGHT = 10;
        public const string SUBMENU_ITEM_SCREENLIST_LABEL = "Unit list";

        protected void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            MenuItem subMenu = menuGroup[SUBMENU_LABEL];
            MenuItemGroup subMenuGroup = subMenu[SUBMENU_GROUP_ID];
            subMenuGroup.Weight = SUBMENU_GROUP_WEIGHT;
            subMenuGroup[SUBMENU_ITEM_SCREENLIST_LABEL].ClickHandler = (menu, tag) => new BmdSmartViewUnitList().ShowAsChild();
        }

    }

}
