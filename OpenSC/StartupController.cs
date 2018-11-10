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

        #region Property: Status
        public delegate void StatusChangedDelegate(string status);
        public static event StatusChangedDelegate StatusChanged;

        private static string status;

        public static string Status
        {
            get => status;
            set
            {
                status = value;
                StatusChanged?.Invoke(value);
            }
        }
        #endregion

        private const string LOG_TAG = "StartupController";

        public static void ProgramStarted()
        {
            InitSettings();
            InitModules();
            InitDatabases();
        }

        public static void GuiInitializable()
        {
            Status = "Initializing GUI...";
            InitGUI();
            InitMenus();
        }

        public static void MainWindowOpened()
        {
            ModuleManager.MainWindowOpened();
        }

        private static void InitSettings()
        {
            Status = "Registering settings...";
            ModuleManager.RegisterSettings();
            Status = "Loading settings...";
            SettingsManager.Instance.LoadSettings();
            Status = "Settings loaded.";
        }

        private static void InitModules()
        {
            Status = "Initializing module manager...";
            ModuleManager.Init();
            Status = "Registering model types...";
            ModuleManager.RegisterModelTypes();
            Status = "Registering dynamic text functions...";
            ModuleManager.RegisterDynamicTextFunctions();
        }

        private static void InitDatabases()
        {
            
            // Register databases
            Status = "Registering databases...";
            VariablesManager.RegisterDatabases();
            SignalsManager.RegisterDatabases();
            ModuleManager.RegisterDatabasePersisterSerializers();
            ModuleManager.RegisterDatabases();

            // Load
            Status = "Loading databases...";
            MasterDatabase.Instance.LoadEverything();
            Status = "Databases loaded.";

            // Log
            LogDispatcher.I(LOG_TAG, "Databases initialized and loaded.");

        }

        private static void InitGUI()
        {

            // Register window types
            Status = "Registering window types...";
            VariablesManager.RegisterWindowTypes();
            SignalsManager.RegisterWindowTypes();
            ModuleManager.RegisterWindowTypes();

            // Init window manager
            Status = "Initializing window manager...";
            WindowManager.Instance.Init();

            // Log
            LogDispatcher.I(LOG_TAG, "Workspace and window manager initialized.");

        }

        private static void InitMenus()
        {
            Status = "Registering menus...";
            VariablesManager.RegisterMenus();
            SignalsManager.RegisterMenus();
            ModuleManager.RegisterMenus();
        }

    }

}
