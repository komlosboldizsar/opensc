using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("signals-model", "Signals (model)", "TODO")]
    [DependsOnModule(typeof(BooleansModelModule))]
    public class SignalsModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(Model.Signals.ExternalSignalDatabases.ExternalSignalDatabase));
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(Model.Signals.ExternalSignalDatabases.ExternalSignalCategoryDatabase));
        }

    }

}
