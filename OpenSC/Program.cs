using OpenSC.Model;
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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ModuleManager.Init();
            ProgramStarted?.Invoke();

            InitDatabases();

            Application.Run(GUI.MainForm.Instance);

        }

        static void InitDatabases()
        {
            ModuleManager.RegisterDatabases();
            MasterDatabase.Instance.LoadEverything();
        }

    }
}
