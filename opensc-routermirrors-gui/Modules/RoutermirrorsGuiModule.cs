using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers.Mirrors;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("routermirrors-gui", "Router mirrors (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutermirrorsModelModule))]
    public class RoutermirrorsGuiModule : BasetypeGuiModuleBase<RoutermirrorsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<RouterMirrorList>();
        }

        protected override void registerMenus()
        {
            var routersMenu = MenuManager.Instance.TopMenu["Routers"];
            var routerMirrorsListMenu = routersMenu["Router mirrors list"];
            routerMirrorsListMenu.ClickHandler = (menu, tag) => new RouterMirrorList().ShowAsChild();
        }

    }

}
