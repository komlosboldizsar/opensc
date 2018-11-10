using OpenSC.GUI;
using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.BlackMagicDesign;
using OpenSC.Model.Routers.DynamicTextFunctions;
using OpenSC.Model.Routers.Leitch;
using OpenSC.Model.Routers.Virtual;
using OpenSC.Model.Variables;
using OpenSC.Model.VTRs.DynamicTextFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules.Routers
{

    class RoutersModule : IModule
    {

        public void MainWindowOpened()
        {
        }

        public void ProgramStarted()
        {
        }

        public void RegisterDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new RouterOutputSource());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new RouterOutputInputName());
        }

        public void RegisterDatabasePersisterSerializers()
        {
            DatabasePersister<Router>.RegisterSerializer(new RouterInputXmlSerializer());
            DatabasePersister<Router>.RegisterSerializer(new RouterOutputXmlSerializer());
        }

        public void RegisterModelTypes()
        {
            RegisterRouterType<VirtualRouter, VirtualRouterEditorForm>();
            RegisterRouterType<BmdVideohub, BmdVideohubEditorForm>();
            RegisterRouterType<LeitchRouter, LeitchRouterEditorForm>();
        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(RouterDatabase));
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(RouterList));
            WindowTypeRegister.RegisterWindowType(typeof(RouterControlForm));
        }

        public void RegisterMenus()
        {

            var routersMenu = MenuManager.Instance.TopMenu["Routers"];

            var routersListMenu = routersMenu["Routers list"];
            routersListMenu.ClickHandler = (menu, tag) => new RouterList().ShowAsChild();

            routersMenu.AddSeparator("sep1");

            MenuItem.MenuClickHandler routerCrosspointsSubMenuClickHandler = (menu, tag) => new RouterControlForm((Router)tag).ShowAsChild();
            foreach (Router router in RouterDatabase.Instance.ItemsAsList)
            {
                string menuId = string.Format("router-{0}", router.ID);
                var routerCrosspointsSubMenu = routersMenu.AddChild(menuId, router.Name, null, router, routerCrosspointsSubMenuClickHandler);
            }

        }

        public void RegisterSettings()
        {

        }

        public void RegisterRouterType<TRouter, TRouterEditorForm>()
            where TRouter : Router
            where TRouterEditorForm : IModelEditorForm<Router>, new()
        {
            Type routerType = typeof(TRouter);
            string typeCode = routerType.GetTypeCode();
            if (string.IsNullOrEmpty(typeCode))
                throw new Exception();
            RouterTypeNameConverter.AddKnownType(typeCode, typeof(TRouter));
            RouterEditorFormTypeRegister.Instance.RegisterFormType<TRouter, TRouterEditorForm>();
        }

    }

}
