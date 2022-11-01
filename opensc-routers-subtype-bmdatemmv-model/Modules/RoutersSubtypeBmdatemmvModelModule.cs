using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.BmdAtemMv;
using OpenSC.Model.Settings;

namespace OpenSC.Modules
{

    [Module("routers-subtype-bmdatemmv-model", "Routers / BMD ATEM Multiview (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeBmdatemmvModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
            RouterTypeRegister.Instance.RegisterType<BmdAtemMvRouter>();
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterSerializer(new BmdAtemMvRouterOutputXmlSerializer());
        }

    }

}
