using OpenSC.GUI;
using OpenSC.GUI.Menus;
using OpenSC.GUI.UMDs;
using OpenSC.GUI.VTRs;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Variables;
using OpenSC.Model.VTRs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules.VTRs
{
    class VtrsModule : IModuleOld
    {
        public void MainWindowOpened()
        {
        }

        public void ProgramStarted()
        {
        }

        public void RegisterDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new Model.VTRs.DynamicTextFunctions.VtrRemainingTimeHhMmSs());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new Model.VTRs.DynamicTextFunctions.VtrElapsedTimeHhMmSs());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new Model.VTRs.DynamicTextFunctions.VtrState());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new Model.VTRs.DynamicTextFunctions.VtrStateTranslated());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new Model.VTRs.DynamicTextFunctions.VtrTitle());
        }

        public void RegisterDatabasePersisterSerializers()
        {
        }

        public void RegisterModelTypes()
        {
            RegisterVtrType<CasparCgPlayout, CasparCgPlayoutEditorForm>();
        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(VtrDatabase));
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(VtrList));
        }

        public void RegisterMenus()
        {
            var vtrsMenu = MenuManager.Instance.TopMenu["VTR"]["VTR list"];
            vtrsMenu.ClickHandler = (menu, tag) => new VtrList().ShowAsChild();
        }

        public void RegisterSettings()
        {

        }

        public void RegisterMacroCommandsAndTriggers()
        {

        }

        public void RegisterVtrType<TVtr, TVtrEditorForm>()
            where TVtr : Vtr
            where TVtrEditorForm : IModelEditorForm<Vtr>, new()
        {
            Type vtrType = typeof(TVtr);
            string typeCode = vtrType.GetTypeCode();
            if (string.IsNullOrEmpty(typeCode))
                throw new Exception();
            VtrTypeNameConverter.AddKnownType(typeCode, typeof(TVtr));
            VtrEditorFormTypeRegister.Instance.RegisterFormType<TVtr, TVtrEditorForm>();
        }

    }
}
