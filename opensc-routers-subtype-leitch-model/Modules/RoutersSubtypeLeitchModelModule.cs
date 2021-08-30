using OpenSC.Model;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Leitch;

namespace OpenSC.Modules
{

    [Module("routers-subtype-letich-model", "Routers / Leitch (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeLeitchModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
            RouterTypeRegister.Instance.RegisterType<LeitchRouter>();
        }

    }

}
