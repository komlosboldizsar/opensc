using OpenSC.Logger;
using System;
using System.IO;
using System.Windows.Forms;

namespace OpenSC
{

    static class Program
    {

        private const string LOG_TAG = "ProgramEntry";

        private static GUI.SplashScreen splashScreen = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Logger
            string loggerDirectory = Application.StartupPath + Path.DirectorySeparatorChar + "log";
            FileLogger logger = new FileLogger(loggerDirectory, "opensc");
            LogDispatcher.I(LOG_TAG, "Main() started, created file logger.");

            // Init Win32 GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Splash screen
            splashScreen = new GUI.SplashScreen();
            splashScreen.Show();
            LogDispatcher.I(LOG_TAG, "Initializing components...");
            Application.DoEvents();

            // Thread helpers init
            ThreadHelpers.InvokeHelper.Init();

            // Start components
            StartupController.Init();

            // Main message loop
            GUI.MainForm.Instance.Load += mainFormOpenedHandler;
            Application.Run(GUI.MainForm.Instance);

        }

        private static void mainFormOpenedHandler(object sender, EventArgs e)
        {
            LogDispatcher.I(LOG_TAG, "Main form opened.");
            splashScreen.Close();
        }

    }

}
