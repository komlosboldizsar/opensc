using OpenSC.GUI.Menus;
using OpenSC.GUI.UMDs;
using OpenSC.Model.UMDs.Tsl50;

namespace OpenSC.Modules
{

    [Module("umds-subtype-tsl50-gui", "UMDs / TSL 5.0 (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsSubtypeTsl50ModelModule))]
    public class UmdsSubtypeTsl50GuiModule : SubtypeGuiModuleBase<UmdsSubtypeTsl50ModelModule>
    {

        public override void Initialize()
        {
            base.Initialize();
            registerMenus();
        }

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            UmdEditorFormTypeRegister.Instance.RegisterFormType<Tsl50Display, Tsl50DisplayUmdEditorForm>();
        }

        public const string TOPMENUGROUP_ID = UmdsGuiModule.TOPMENUGROUP_ID;
        public const string TOPMENU_LABEL = UmdsGuiModule.TOPMENU_LABEL;
        public const string MENUGROUP_ID = "subtypes";
        public const int MENUGROUP_WEIGHT = UmdsGuiModule.MENUGROUP_WEIGHT + 10;
        public const string SUBMENU_LABEL = "TSL 5.0";
        public const string SUBMENU_GROUP_ID = "base";
        public const int SUBMENU_GROUP_WEIGHT = 10;
        public const string SUBMENU_ITEM_SCREENLIST_LABEL = "Screen list";

        protected void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            MenuItem subMenu = menuGroup[SUBMENU_LABEL];
            MenuItemGroup subMenuGroup = subMenu[SUBMENU_GROUP_ID];
            subMenuGroup.Weight = SUBMENU_GROUP_WEIGHT;
            subMenuGroup[SUBMENU_ITEM_SCREENLIST_LABEL].ClickHandler = (menu, tag) => new Tsl50ScreenList().ShowAsChild();
        }

    }

}
