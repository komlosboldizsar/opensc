using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Routers;

namespace OpenSC.Modules
{

    [Module("routers-gui", "Routers (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersGuiModule : BasetypeGuiModuleBase<RoutersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<RouterList>();
            WindowTypeRegister.RegisterWindowType<RouterControlForm>();
            WindowTypeRegister.RegisterWindowType<RouterControlTableForm>();
        }

        protected override void registerMenus()
        {
            var routersMenu = MenuManager.Instance.TopMenu["Routers"];

            var routersListMenu = routersMenu["Routers list"];
            routersListMenu.ClickHandler = (menu, tag) => new RouterList().ShowAsChild();

            var allCrosspointsMenu = routersMenu["All crosspoints"];
            allCrosspointsMenu.ClickHandler = (menu, tag) => new RouterControlTableForm(RouterDatabase.Instance).ShowAsChild();

            /* routersMenu.AddSeparator("sep3");

            MenuItem.MenuClickHandler routerCrosspointsSubMenuClickHandler = (menu, tag) => new RouterControlTableForm((Router)tag).ShowAsChild();
            foreach (Router router in RouterDatabase.Instance.ItemsAsList)
            {
                string menuId = string.Format("router-{0}", router.ID);
                var routerCrosspointsSubMenu = routersMenu.AddChild(menuId, router.Name, null, router, routerCrosspointsSubMenuClickHandler);
            } */ // TODO: After databases loaded

        }

    }

}
