using OpenSC.GUI.Menus;
using OpenSC.GUI.Streams;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules.Streams
{
    class StreamsModule: IModule
    {

        public void ProgramStarted()
        {
        }

        public void MainWindowOpened()
        {
        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(StreamDatabase));
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(StreamList));
        }

        public void RegisterMenus()
        {
            var streamsMenu = MenuManager.Instance.TopMenu["Streams"]["Stream list"];
            streamsMenu.ClickHandler = (menu, tag) => new StreamList().ShowAsChild();
        }
    }
}
