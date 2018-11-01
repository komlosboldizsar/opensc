using OpenSC.GUI;
using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
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
        }

        public void RegisterDatabasePersisterSerializers()
        {
            DatabasePersister<Router>.RegisterSerializer(new RouterInputXmlSerializer());
            DatabasePersister<Router>.RegisterSerializer(new RouterOutputXmlSerializer());
        }

        public void RegisterModelTypes()
        {
            //RegisterRouterType</**/, /**/EditorForm>();
        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(RouterDatabase));
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(RouterControlForm));
        }

        public void RegisterMenus()
        {
            var routersMenu = MenuManager.Instance.TopMenu["Routers"];
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
