using OpenSC.Model;
using OpenSC.Model.Routers.Mirrors;

namespace OpenSC.Modules
{

    [Module("routermirrors-model", "Router mirrors (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutermirrorsModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(RouterMirrorDatabase));
        }

    }

}
