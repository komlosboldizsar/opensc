using OpenSC.GUI.Menus;
using OpenSC.GUI.Timers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Timers;
using OpenSC.Model.Timers.DynamicTextFunctions;
using OpenSC.Model.Timers.Macros;
using OpenSC.Model.Timers.Triggers;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules.Timers
{
    class TimersModule : IModuleOld
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
            DynamicTextFunctionRegister.Instance.RegisterFunction(new TimerTotalMinutes());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new TimerHhMmSs());
        }

        public void RegisterDatabasePersisterSerializers()
        {
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

        public void RegisterMacroCommandsAndTriggers()
        {
            MacroCommandRegister.Instance.RegisterCommandCollection(TimerMacroCommands.Instance);
            MacroTriggerRegister.Instance.RegisterTriggerCollection(TimerMacroTriggers.Instance);
        }

    }
}
