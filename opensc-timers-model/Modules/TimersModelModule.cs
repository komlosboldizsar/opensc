using OpenSC.Model;
using OpenSC.Model.Timers;

namespace OpenSC.Modules
{

    [Module("timers-model", "Timers (model)", "TODO")]
    public class TimersModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(TimerDatabase));
        }

    }

}
