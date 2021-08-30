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

        private static IModuleOld[] registeredModules = new IModuleOld[]
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
            foreach (IModuleOld module in registeredModules)
                module.RegisterDynamicTextFunctions();
        }

        public static void RegisterMacroCommandsAndTriggers()
        {
            foreach (IModuleOld module in registeredModules)
                module.RegisterMacroCommandsAndTriggers();
        }

        public static void RegisterDatabasePersisterSerializers()
        {
            foreach (IModuleOld module in registeredModules)
                module.RegisterDatabasePersisterSerializers();
        }

        public static void RegisterModelTypes()
        {
            foreach (IModuleOld module in registeredModules)
                module.RegisterModelTypes();
        }

        public static void RegisterDatabases()
        {
            foreach (IModuleOld module in registeredModules)
                module.RegisterDatabases();
        }

        public static void RegisterWindowTypes()
        {
            foreach (IModuleOld module in registeredModules)
                module.RegisterWindowTypes();
        }

        public static void RegisterMenus()
        {
            foreach (IModuleOld module in registeredModules)
                module.RegisterMenus();
        }

        public static void RegisterSettings()
        {
            foreach (IModuleOld module in registeredModules)
                module.RegisterSettings();
        }

        public static void MainWindowOpened()
        {
            foreach (IModuleOld module in registeredModules)
                module.MainWindowOpened();
        }

    }
}
