using OpenSC.GUI.Menus;
using System;

namespace OpenSC.Model.Variables
{

    public class VariablesManager
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
        }

        private static void registerWindowTypes()
        {
        }

        private static void registerMenus() {
            var variablesMenu = MenuManager.Instance.TopMenu["Variables"];
        }

    }

}
