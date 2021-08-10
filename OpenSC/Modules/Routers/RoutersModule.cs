using OpenSC.GUI;
using OpenSC.GUI.Menus;
using OpenSC.GUI.Routers;
using OpenSC.GUI.Routers.Mirrors;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.BlackMagicDesign;
using OpenSC.Model.Routers.CrosspointStores;
using OpenSC.Model.Routers.DynamicTextFunctions;
using OpenSC.Model.Routers.Leitch;
using OpenSC.Model.Routers.Macros;
using OpenSC.Model.Routers.Mirrors;
using OpenSC.Model.Routers.Triggers;
using OpenSC.Model.Routers.Virtual;
using OpenSC.Model.Settings;
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
            DynamicTextFunctionRegister.Instance.RegisterFunction(new RouterOutputInputLabel());
        }

        public void RegisterDatabasePersisterSerializers()
        {
            DatabasePersister<Router>.RegisterSerializer(new RouterInputXmlSerializer());
            DatabasePersister<Router>.RegisterSerializer(new RouterOutputXmlSerializer());
            DatabasePersister<Router>.RegisterSerializer(new VirtualRouterOutputXmlSerializer());
            DatabasePersister<Router>.RegisterSerializer(new LeitchRouterOutputXmlSerializer());
            DatabasePersister<Router>.RegisterSerializer(new VirtualLeitchRouterOutputXmlSerializer());
            DatabasePersister<Router>.RegisterSerializer(new BmdVideohubOutputXmlSerializer());
            DatabasePersister<RouterMirror>.RegisterSerializer(new RouterMirrorInputAssociationXmlSerializer());
            DatabasePersister<RouterMirror>.RegisterSerializer(new RouterMirrorOutputAssociationXmlSerializer());
            DatabasePersister<Labelset>.RegisterSerializer(new LabelXmlSerializer());
        }

        public void RegisterModelTypes()
        {
            RegisterRouterType<VirtualRouter, VirtualRouterEditorForm>();
            RegisterRouterType<BmdVideohub, BmdVideohubEditorForm>();
            RegisterRouterType<LeitchRouter, LeitchRouterEditorForm>();
            RegisterRouterType<VirtualLeitchRouter, VirtualLeitchRouterEditorForm>();
        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(RouterDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(RouterMirrorDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(LabelsetDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(CrosspointStoreDatabase));
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(RouterList));
            WindowTypeRegister.RegisterWindowType(typeof(RouterControlForm));
            WindowTypeRegister.RegisterWindowType(typeof(RouterControlTableForm));
            WindowTypeRegister.RegisterWindowType(typeof(RouterMirrorList));
            WindowTypeRegister.RegisterWindowType(typeof(LabelsetList));
        }

        public void RegisterMenus()
        {

            var routersMenu = MenuManager.Instance.TopMenu["Routers"];

            var routersListMenu = routersMenu["Routers list"];
            routersListMenu.ClickHandler = (menu, tag) => new RouterList().ShowAsChild();

            var routerMirrorsListMenu = routersMenu["Router mirrors list"];
            routerMirrorsListMenu.ClickHandler = (menu, tag) => new RouterMirrorList().ShowAsChild();

            var labelsetsListMenu = routersMenu["Labelsets list"];
            labelsetsListMenu.ClickHandler = (menu, tag) => new LabelsetList().ShowAsChild();

            routersMenu.AddSeparator("sep1");

            var allCrosspointsMenu = routersMenu["All crosspoints"];
            allCrosspointsMenu.ClickHandler = (menu, tag) => new RouterControlTableForm(RouterDatabase.Instance).ShowAsChild();

            routersMenu.AddSeparator("sep2");

            MenuItem.MenuClickHandler routerCrosspointsSubMenuClickHandler = (menu, tag) => new RouterControlTableForm((Router)tag).ShowAsChild();
            foreach (Router router in RouterDatabase.Instance.ItemsAsList)
            {
                string menuId = string.Format("router-{0}", router.ID);
                var routerCrosspointsSubMenu = routersMenu.AddChild(menuId, router.Name, null, router, routerCrosspointsSubMenuClickHandler);
            }

        }

        public void RegisterSettings()
        {
            SettingsManager.Instance.RegisterSetting(LeitchRouter.PanelIdSetting);
            SettingsManager.Instance.RegisterSetting(VirtualLeitchRouter.PanelIdSetting);
        }

        public void RegisterMacroCommandsAndTriggers()
        {
            MacroCommandRegister.Instance.RegisterCommandCollection(RouterMacroCommands.Instance);
            MacroTriggerRegister.Instance.RegisterTriggerCollection(RouterMacroTriggers.Instance);
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
