using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("routers-subtype-bmd-model", "Routers / BlackMagic Design Videohub (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeBmdModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
           // TODO : register BmdVideohub
        }

    }

}
