using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.BlackMagicDesign;

namespace OpenSC.Modules
{

    [Module("routers-subtype-bmd-model", "Routers / BlackMagic Design Videohub (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeBmdModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
            RouterTypeRegister.Instance.RegisterType<BmdVideohub>();
        }

        protected override void registerSerializers()
        {
            DatabasePersister<Router>.RegisterSerializer(new BmdVideohubOutputXmlSerializer());
        }

    }

}
