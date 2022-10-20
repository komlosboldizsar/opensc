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

            AppDomain.CurrentDomain.UnhandledException += appDomainUnhandledExceptionHandler;

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

            Form mainForm = GUI.MainForm.Instance;

            // Start components
            StartupController.Init();

            // Main message loop
            mainForm.Load += mainFormOpenedHandler;
            Application.Run(GUI.MainForm.Instance);

        }

        private static void appDomainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (!(e.ExceptionObject is Exception ex))
                return;
            string logMessage = string.Format("Unhandled exception: [{0}]", ex.Message ?? "");
            LogDispatcher.E(LOG_TAG, logMessage);
            string stackTrace = ex.StackTrace;
            if (stackTrace != null)
            {
                string[] stackTraceLines = stackTrace.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                LogDispatcher.E(LOG_TAG, "-- STACK TRACE STARTS --");
                foreach (string stackTraceLine in stackTraceLines)
                    LogDispatcher.E(LOG_TAG, stackTraceLine);
                LogDispatcher.E(LOG_TAG, "-- STACK TRACE ENDS --");
            }
        }

        private static void mainFormOpenedHandler(object sender, EventArgs e)
        {
            LogDispatcher.I(LOG_TAG, "Main form opened.");
            splashScreen.Close();
        }

    }

}
