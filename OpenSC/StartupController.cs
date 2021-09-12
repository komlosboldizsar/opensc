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
                LogDispatcher.I(LOG_TAG, value);
                StatusChanged?.Invoke(value);
            }
        }
        #endregion

        private const string LOG_TAG = "StartupController";

        public static void Init()
        {
            ModuleLoader loader = new ModuleLoader();
            loader.LoadModules();
            SettingsManager.Instance.LoadSettings();
            MasterDatabase.Instance.LoadEverything();
            WindowManager.Instance.Init();
        }

        

    }

}
