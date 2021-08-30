using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers.CrosspointStores;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("crosspointstores-gui", "Crosspoint stores (GUI)", "TODO")]
    [DependsOnModule(typeof(CrosspointstoresModelModule))]
    public class CrosspointstoresGuiModule : BasetypeGuiModuleBase<CrosspointstoresModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<CrosspointStoreList>();
        }

        protected override void registerMenus()
        {
            var routersMenu = MenuManager.Instance.TopMenu["Routers"];
            var crosspointStoresListMenu = routersMenu["Crosspoint store list"];
            crosspointStoresListMenu.ClickHandler = (menu, tag) => new CrosspointStoreList().ShowAsChild();
        }

    }

}
