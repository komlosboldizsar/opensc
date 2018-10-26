using OpenSC.GUI.Menus;
using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;
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
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(DynamicTextDatabase));
        }

        private static void registerWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType(typeof(DynamicTextList));
        }

        private static void registerMenus() {
            var variablesMenu = MenuManager.Instance.TopMenu["Variables"];
            var dynamicTextsMenu = variablesMenu["Dynamic texts"];
            dynamicTextsMenu.ClickHandler = (menu, tag) => new DynamicTextList().ShowAsChild();
        }

    }

}
