using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Virtual;

namespace OpenSC.Modules
{

    [Module("routers-subtype-virtual-model", "Routers / Virtual (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeVirtualModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
            RouterTypeRegister.Instance.RegisterType<VirtualRouter>();
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterSerializer(new VirtualRouterOutputXmlSerializer());
        }

    }

}
