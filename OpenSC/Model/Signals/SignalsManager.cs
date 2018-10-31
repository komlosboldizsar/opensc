using OpenSC.GUI.Menus;
using OpenSC.GUI.Signals;
using OpenSC.GUI.WorkspaceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    class SignalsManager
    {

        public static void ProgramStarted()
        {
            registerSettings();
            registerModelTypes();
            registerDatabases();
            registerWindowTypes();
            registerMenus();
        }

        public static void MainWindowOpened()
        { }

        private static void registerSettings()
        {
        }

        private static void registerModelTypes()
        {
        }

        private static void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(SignalDatabases.SignalCategoryDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(SignalDatabases.SignalDatabase));
        }

        private static void registerWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(SignalList));
            WindowTypeRegister.RegisterWindowType(typeof(SignalCategoryList));
        }

        private static void registerMenus()
        {
            var signalsMenu = MenuManager.Instance.TopMenu["Signals"];
            var signalsSubMenu = signalsMenu["Signals"];
            signalsSubMenu.ClickHandler = (menu, tag) => new SignalList().ShowAsChild();
            var categoriesSubMenu = signalsMenu["Categories"];
            categoriesSubMenu.ClickHandler = (menu, tag) => new SignalCategoryList().ShowAsChild();
        }

    }

}
