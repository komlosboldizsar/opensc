using OpenSC.GUI;
using OpenSC.GUI.Menus;
using OpenSC.GUI.UMDs;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;
using OpenSC.Model.UMDs.TSL31;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules.UMDs
{
    class UmdsModule : IModule
    {

        public void MainWindowOpened()
        {
        }

        public void ProgramStarted()
        {
        }

        public void RegisterModelTypes()
        {
            RegisterUmdType<McCurdyUMD1, McCurdyUmd1EditorForm>();
            RegisterUmdType<TSL31, Tsl31UmdEditorForm>();
            //RegisterUmdPortType<McCurdyPort, /**/>();
            //RegisterUmdPortType<TSL31Port, /**/>();
        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(UmdDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(UmdPortDatabase));
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(UmdList));
        }

        public void RegisterMenus()
        {
            var umdsMenu = MenuManager.Instance.TopMenu["UMD"]["UMD list"];
            umdsMenu.ClickHandler = (menu, tag) => new UmdList().ShowAsChild();
        }

        public void RegisterSettings()
        {

        }

        public void RegisterUmdType<TUmd, TUmdEditorForm>()
            where TUmd: UMD
            where TUmdEditorForm: IModelEditorForm<UMD>, new()
        {
            Type umdType = typeof(TUmd);
            string typeCode = umdType.GetTypeCode();
            if (string.IsNullOrEmpty(typeCode))
                throw new Exception();
            UmdTypeNameConverter.AddKnownType(typeCode, typeof(TUmd));
            UmdEditorFormTypeRegister.Instance.RegisterFormType<TUmd, TUmdEditorForm>();
        }

        public void RegisterUmdPortType<TUmdPort, TUmdPortEditorForm>()
            where TUmdPort : UmdPort
            where TUmdPortEditorForm : IModelEditorForm<UmdPort>, new()
        {
            Type umdPortType = typeof(TUmdPort);
            string typeCode = umdPortType.GetTypeCode();
            if (string.IsNullOrEmpty(typeCode))
                throw new Exception();
            UmdPortTypeNameConverter.AddKnownType(typeCode, typeof(TUmdPort));
            UmdPortEditorFormTypeRegister.Instance.RegisterFormType<TUmdPort, TUmdPortEditorForm>();
        }

    }
}
