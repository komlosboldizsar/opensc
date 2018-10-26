using OpenSC.GUI.Menus;
using OpenSC.GUI.Timers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Timers;
using OpenSC.Model.Timers.DynamicTextFunctions;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules.Timers
{
    class TimersModule : IModule
    {

        public void ProgramStarted()
        {
        }

        public void MainWindowOpened()
        {
        }

        public void RegisterDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new TimerTotalSeconds());
        }

        public void RegisterModelTypes()
        {

        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(TimerDatabase));
        }

        public void RegisterWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(TimerList));
            WindowTypeRegister.RegisterWindowType(typeof(TimerEditWindow));
            WindowTypeRegister.RegisterWindowType(typeof(TimerWindow));
        }

        public void RegisterMenus()
        {
            var timersMenu = MenuManager.Instance.TopMenu["Timers"]["Timer list"];
            timersMenu.ClickHandler = (menu, tag) => new TimerList().ShowAsChild();
        }

        public void RegisterSettings()
        {

        }

    }
}
