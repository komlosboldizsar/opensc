using OpenSC.GUI;
using OpenSC.GUI.Menus;
using OpenSC.GUI.Mixers;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Mixers;
using OpenSC.Model.Variables;
using System;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules.Mixers
{

    class MixersModule : IModule
    {

        public void MainWindowOpened()
        {
        }

        public void ProgramStarted()
        {
        }

        public void RegisterDynamicTextFunctions()
        {
            //DynamicTextFunctionRegister.Instance.RegisterFunction(new /*?*/());
        }

        public void RegisterDatabasePersisterSerializers()
        {
            DatabasePersister<Mixer>.RegisterSerializer(new MixerInputXmlSerializer());
        }

        public void RegisterModelTypes()
        {
            //RegisterMixerType</*?*/, /*?*/>();
        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MixerDatabase));
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(MixerList));
        }

        public void RegisterMenus()
        {

            var mixersMenu = MenuManager.Instance.TopMenu["Mixers"];

            var mixersListMenu = mixersMenu["Mixers list"];
            mixersListMenu.ClickHandler = (menu, tag) => new MixerList().ShowAsChild();

        }

        public void RegisterSettings()
        {

        }

        public void RegisterMixerType<TMixer, TMixerEditorForm>()
            where TMixer : Mixer
            where TMixerEditorForm : IModelEditorForm<Mixer>, new()
        {
            Type mixerType = typeof(TMixer);
            string typeCode = mixerType.GetTypeCode();
            if (string.IsNullOrEmpty(typeCode))
                throw new Exception();
            MixerTypeNameConverter.AddKnownType(typeCode, typeof(TMixer));
            MixerEditorFormTypeRegister.Instance.RegisterFormType<TMixer, TMixerEditorForm>();
        }

    }

}
