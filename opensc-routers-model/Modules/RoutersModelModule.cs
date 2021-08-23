using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("routers-model", "Routers (model)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class RoutersModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(RoutersModelModule));
        }

    }

}
