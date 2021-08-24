using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("routers-subtype-letich-model", "Routers / Leitch (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeLeitchModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
           // TODO : register LeitchRouter
        }

    }

}
