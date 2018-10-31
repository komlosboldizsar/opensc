using OpenSC.GUI.Menus;
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
            //MasterDatabase.Instance.RegisterSingletonDatabase(typeof(/**/));
        }

        private static void registerWindowTypes()
        {
            //WindowTypeRegister.RegisterWindowType(typeof(/**/));
        }

        private static void registerMenus()
        {
            //var variablesMenu = MenuManager.Instance.TopMenu[/**/];
            //var dynamicTextsMenu = variablesMenu[/**/];
            //dynamicTextsMenu.ClickHandler = (menu, tag) => new /**/.ShowAsChild();
        }

    }

}
