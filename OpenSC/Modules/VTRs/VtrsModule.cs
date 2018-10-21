using OpenSC.GUI;
using OpenSC.GUI.Menus;
using OpenSC.GUI.UMDs;
using OpenSC.GUI.VTRs;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.VTRs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules.VTRs
{
    class VtrsModule : IModule
    {
        public void MainWindowOpened()
        {
        }

        public void ProgramStarted()
        {
        }

        public void RegisterModelTypes()
        {
            //RegisterVtrType<CasparCgPlayout, /**/>("casparcg");
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

        public void RegisterVtrType<TVtr, TVtrEditorForm>(string typeCode)
            where TVtr : Vtr
            where TVtrEditorForm : IModelEditorForm<Vtr>, new()
        {
            VtrTypeNameConverter.AddKnownType(typeCode, typeof(TVtr));
            VtrEditorFormTypeRegister.Instance.RegisterFormType<TVtr, TVtrEditorForm>();
        }

    }
}
