using OpenSC.GUI.Routers;
using OpenSC.GUI.WorkspaceManager;
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

        public void RegisterModelTypes()
        {

        }

        public void RegisterDatabases()
        {
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(RouterControlForm));
        }

        public void RegisterMenus()
        {

        }

        public void RegisterSettings()
        {

        }

    }
}
