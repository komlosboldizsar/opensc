using OpenSC.GUI.Menus;
using OpenSC.GUI.UMDs;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.UMDs;
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

    }
}
