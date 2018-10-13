using OpenSC.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules
{
    class ModuleManager
    {

        private static IModule[] registeredModules = new IModule[]
        {
            new Timers.TimersModule(),
            new Routers.RoutersModule()
        };

        #region Own initialization
        public static void Init()
        {
            subscribeEvents();
        }

        private static void subscribeEvents()
        {
            Program.ProgramStarted += programStartedHandler;
            MainForm.Instance.Load += mainWindowOpenedHandler;
        }
        #endregion

        #region Event handlers
        private static void programStartedHandler()
        {
            foreach (IModule module in registeredModules)
                module.ProgramStarted();
        }

        private static void mainWindowOpenedHandler(object sender, EventArgs e)
        {
            foreach (IModule module in registeredModules)
                module.MainWindowOpened();
        }
        #endregion

    }
}
