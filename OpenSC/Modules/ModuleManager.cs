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
            new UMDs.UmdsModule(),
            new Routers.RoutersModule(),
            new Streams.StreamsModule(),
            new VTRs.VtrsModule(),
            new Mixers.MixersModule()
        };

        #region Own initialization
        public static void Init()
        { }
        #endregion

        public static void RegisterDynamicTextFunctions()
        {
            foreach (IModule module in registeredModules)
                module.RegisterDynamicTextFunctions();
        }

        public static void RegisterDatabasePersisterSerializers()
        {
            foreach (IModule module in registeredModules)
                module.RegisterDatabasePersisterSerializers();
        }

        public static void RegisterModelTypes()
        {
            foreach (IModule module in registeredModules)
                module.RegisterModelTypes();
        }

        public static void RegisterDatabases()
        {
            foreach (IModule module in registeredModules)
                module.RegisterDatabases();
        }

        public static void RegisterWindowTypes()
        {
            foreach (IModule module in registeredModules)
                module.RegisterWindowTypes();
        }

        public static void RegisterMenus()
        {
            foreach (IModule module in registeredModules)
                module.RegisterMenus();
        }

        public static void RegisterSettings()
        {
            foreach (IModule module in registeredModules)
                module.RegisterSettings();
        }

        public static void MainWindowOpened()
        {
            foreach (IModule module in registeredModules)
                module.MainWindowOpened();
        }

    }
}
