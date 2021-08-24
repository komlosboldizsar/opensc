using OpenSC.Model;
using OpenSC.Model.Routers.Mirrors;

namespace OpenSC.Modules
{

    [Module("routermirrors-model", "Router mirrors (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutermirrorsModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(RouterMirrorDatabase));
        }

    }

}
