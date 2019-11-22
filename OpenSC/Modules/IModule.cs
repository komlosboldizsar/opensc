using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules
{
    interface IModule
    {
        void ProgramStarted();
        void MainWindowOpened();
        void RegisterDynamicTextFunctions();
        void RegisterMacroCommandsAndTriggers();
        void RegisterDatabasePersisterSerializers();
        void RegisterModelTypes();
        void RegisterDatabases();
        void RegisterWindowTypes();
        void RegisterMenus();
        void RegisterSettings();
    }
}
