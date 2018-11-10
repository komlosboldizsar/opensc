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

    static class Program
    {

        private const string LOG_TAG = "ProgramEntry";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Logger
            FileLogger logger = new FileLogger(Application.StartupPath, "opensc");
            LogDispatcher.I(LOG_TAG, "Main() started, created file logger.");

            // Init Win32 GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Thread helpers init
            ThreadHelpers.InvokeHelper.Init();

            // Start components
            StartupController.ProgramStarted();
            StartupController.GuiInitializable();

            // Main message loop
            GUI.MainForm.Instance.Load += mainFormOpenedHandler;
            Application.Run(GUI.MainForm.Instance);

        }

        private static void mainFormOpenedHandler(object sender, EventArgs e)
        {
            StartupController.MainWindowOpened();
        }

    }

}
