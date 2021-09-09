using OpenSC.Model;
using OpenSC.Model.Signals.BooleanTallies;

namespace OpenSC.Modules
{

    [Module("booleantallies-model", "Boolean tallies (model)", "TODO")]
    [DependsOnModule(typeof(BooleansModelModule))]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class BooleantalliesModelModule : BasetypeModuleBase
    {
        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(BooleanTallyDatabase));
        }

        protected override void registerSerializers()
        { }

    }

}
