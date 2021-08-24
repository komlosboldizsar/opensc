using OpenSC.Model;
using OpenSC.Model.Routers.CrosspointBooleans;

namespace OpenSC.Modules
{

    [Module("crosspointbooleans-model", "Crosspoint booleans (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    [DependsOnModule(typeof(BooleansModelModule))]
    public class CrosspointbooleansModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(CrosspointBooleanDatabase));
        }

    }

}
