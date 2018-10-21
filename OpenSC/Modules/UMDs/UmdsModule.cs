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
            //RegisterUmdType<McCurdyUMD1, /**/>("mccurdy");
            //RegisterUmdType<TSL31, /**/>("tsl31");
            //RegisterUmdPortType<McCurdyPort, /**/>("mccurdy");
            //RegisterUmdPortType<TSL31Port, /**/>("tsl31");
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

        public void RegisterUmdType<TUmd, TUmdEditorForm>(string typeCode)
            where TUmd: UMD
            where TUmdEditorForm: IModelEditorForm<UMD>, new()
        {
            UmdTypeNameConverter.AddKnownType(typeCode, typeof(TUmd));
            UmdEditorFormTypeRegister.Instance.RegisterFormType<TUmd, TUmdEditorForm>();
        }

        public void RegisterUmdPortType<TUmdPort, TUmdPortEditorForm>(string typeCode)
            where TUmdPort : UmdPort
            where TUmdPortEditorForm : IModelEditorForm<UmdPort>, new()
        {
            UmdPortTypeNameConverter.AddKnownType(typeCode, typeof(TUmdPort));
            UmdPortEditorFormTypeRegister.Instance.RegisterFormType<TUmdPort, TUmdPortEditorForm>();
        }

    }
}
