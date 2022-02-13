using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;

namespace OpenSC.Modules
{

    [Module("routers-model", "Routers (model)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class RoutersModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(RouterDatabase));
        }

        protected override void registerSerializers()
        {
            DatabasePersister<Router>.RegisterSerializer(new RouterInputXmlSerializer());
            DatabasePersister<Router>.RegisterSerializer(new RouterOutputXmlSerializer());
        }

    }

}
