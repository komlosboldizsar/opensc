using OpenSC.GUI.WorkspaceManager;
using OpenSC.Logger;
using OpenSC.Model;
using OpenSC.Model.Settings;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using OpenSC.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC
{

    internal class StartupController
    {

        private const string LOG_TAG = "StartupController";

        public static void ProgramStarted()
        {
            InitSettings();
            InitModules();
            InitDatabases();
        }

        public static void GuiInitializable()
        {
            InitGUI();
            InitMenus();
        }

        public static void MainWindowOpened()
        {
            ModuleManager.MainWindowOpened();
        }

        private static void InitSettings()
        {
            ModuleManager.RegisterSettings();
            SettingsManager.Instance.LoadSettings();
        }

        private static void InitModules()
        {
            ModuleManager.Init();
            ModuleManager.RegisterModelTypes();
            ModuleManager.RegisterDynamicTextFunctions();
        }

        private static void InitDatabases()
        {

            // Register databases
            VariablesManager.RegisterDatabases();
            SignalsManager.RegisterDatabases();
            ModuleManager.RegisterDatabasePersisterSerializers();
            ModuleManager.RegisterDatabases();

            // Load
            MasterDatabase.Instance.LoadEverything();

            // Log
            LogDispatcher.I(LOG_TAG, "Databases initialized and loaded.");

        }

        private static void InitGUI()
        {

            // Register window types
            VariablesManager.RegisterWindowTypes();
            SignalsManager.RegisterWindowTypes();
            ModuleManager.RegisterWindowTypes();

            // Init window manager
            WindowManager.Instance.Init();

            // Log
            LogDispatcher.I(LOG_TAG, "Workspace and window manager initialized.");

        }

        private static void InitMenus()
        {
            VariablesManager.RegisterMenus();
            SignalsManager.RegisterMenus();
            ModuleManager.RegisterMenus();
        }

    }

}
