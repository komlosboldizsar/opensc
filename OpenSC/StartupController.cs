using OpenSC.GUI.WorkspaceManager;
using OpenSC.Logger;
using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.Settings;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using OpenSC.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC
{

    internal class StartupController
    {

        private const string LOG_TAG = "StartupController";

        public static void Init()
        {
            ModuleLoader loader = new ModuleLoader();
            loader.LoadModules();
            log("Loading settings...");
            SettingsManager.Instance.LoadSettings();
            log("Loading databases...");
            MasterDatabase.Instance.LoadEverything();
            log("Loading workspace...");
            WindowManager.Instance.Init();
            log("Ready.");
        }

        private static void log(string message) => LogDispatcher.I(LOG_TAG, message);

    }

}
