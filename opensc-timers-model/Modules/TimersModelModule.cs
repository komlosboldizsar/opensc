using OpenSC.Model;
using OpenSC.Model.Timers;

namespace OpenSC.Modules
{

    [Module("timers-model", "Timers (model)", "TODO")]
    public class TimersModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(TimerDatabase));
        }

    }

}
