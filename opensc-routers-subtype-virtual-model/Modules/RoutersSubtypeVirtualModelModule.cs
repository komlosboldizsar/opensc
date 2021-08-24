using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("routers-subtype-virtual-model", "Routers / Virtual (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeVirtualModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
           // TODO : register VirtualRouter
        }

    }

}
