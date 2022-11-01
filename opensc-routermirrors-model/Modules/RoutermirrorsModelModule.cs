using OpenSC.Model;
using OpenSC.Model.Persistence;
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

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterCompleteSerializer(new RouterMirrorInputAssociationXmlSerializer());
            SerializerRegister.RegisterCompleteSerializer(new RouterMirrorOutputAssociationXmlSerializer());
        }

    }

}
