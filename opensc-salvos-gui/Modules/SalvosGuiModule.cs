using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers.Salvos;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Routers.Salvos;

namespace OpenSC.Modules
{

    [Module("salvos-gui", "Salvos (GUI)", "TODO")]
    [DependsOnModule(typeof(SalvosModelModule))]
    public class SalvosGuiModule : BasetypeGuiModuleBase<SalvosModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<SalvoList>();
        }

        public const string TOPMENUGROUP_ID = MenuManager.GROUP_ID_MODULES;
        public const string TOPMENU_LABEL = "Routers";
        public const string MENUGROUP_ID = "salvos";
        public const int MENUGROUP_WEIGHT = MenuManager.GROUP_WEIGHT_BASE + 10;
        public const string SUBMENU_SALVOS_LABEL = "Salvo list";

        protected override void registerMenus()
        {
            MenuItem topMenu = MenuManager.Instance.TopMenu[TOPMENUGROUP_ID][TOPMENU_LABEL];
            MenuItemGroup menuGroup = topMenu[MENUGROUP_ID];
            menuGroup.Weight = MENUGROUP_WEIGHT;
            menuGroup[SUBMENU_SALVOS_LABEL].ClickHandler = (menu, tag) => new SalvoList().ShowAsChild();
        }

    }

}
