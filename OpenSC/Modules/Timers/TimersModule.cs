using OpenSC.Model;
using OpenSC.Model.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules.Timers
{
    class TimersModule : IModule
    {

        public void ProgramStarted()
        {
        }

        public void MainWindowOpened()
        {
        }

        public void RegisterDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(TimerDatabase));
        }

    }
}
