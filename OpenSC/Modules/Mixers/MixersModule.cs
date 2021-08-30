using OpenSC.GUI;
using OpenSC.GUI.Menus;
using OpenSC.GUI.Mixers;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Mixers;
using OpenSC.Model.Variables;
using System;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Mixers.BlackMagicDesign;
using OpenSC.Model.Mixers.DynamicTextFunctions;
using OpenSC.Model.Macros;
using OpenSC.Model.Mixers.BlackMagicDesign.Macros;

namespace OpenSC.Modules.Mixers
{

    class MixersModule : IModuleOld
    {

        public void MainWindowOpened()
        {
        }

        public void ProgramStarted()
        {
        }

        public void RegisterDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new MixerProgramInputName());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new MixerPreviewInputName());
        }

        public void RegisterDatabasePersisterSerializers()
        {
            DatabasePersister<Mixer>.RegisterSerializer(new MixerInputXmlSerializer());
        }

        public void RegisterModelTypes()
        {
            RegisterMixerType<BmdMixer, BmdMixerEditorForm>();
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

        public void RegisterMacroCommandsAndTriggers()
        {
            MacroCommandRegister.Instance.RegisterCommandCollection(BmdMixerMacroCommands.Instance);
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
