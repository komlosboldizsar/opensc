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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC
{

    public delegate void ProgramStartedDelegate();

    static class Program
    {

        public static event ProgramStartedDelegate ProgramStarted;

        private const string LOG_TAG = "ProgramEntry";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

                FileLogger logger = new FileLogger(Application.StartupPath, "opensc");

                LogDispatcher.I(LOG_TAG, "Main() started, created file logger.");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                VariablesManager.ProgramStarted();
                SignalsManager.ProgramStarted();
                ModuleManager.Init();
                ProgramStarted?.Invoke();

                ModuleManager.RegisterDynamicTextFunctions();

                // TODO: init somewhere else :)
                ModuleManager.RegisterModelTypes();

                InitDatabases();
                InitWorkspaceManager();

                // TODO: init somewhere else :)
                ModuleManager.RegisterMenus();
                ModuleManager.RegisterSettings();
                SettingsManager.Instance.LoadSettings();

                Application.Run(GUI.MainForm.Instance);

        }

        private static void InitWorkspaceManager()
        {
            ModuleManager.RegisterWindowTypes();
            WindowManager.Instance.Init();
            LogDispatcher.I(LOG_TAG, "Workspace and window manager initialized.");
        }

        static void InitDatabases()
        {
            ModuleManager.RegisterDatabasePersisterSerializers();
            ModuleManager.RegisterDatabases();
            MasterDatabase.Instance.LoadEverything();
            LogDispatcher.I(LOG_TAG, "Databases initialized and loaded.");
        }

    }
}
