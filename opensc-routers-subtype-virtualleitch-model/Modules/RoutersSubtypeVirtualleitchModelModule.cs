using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("routers-subtype-virtualleitch-model", "Routers / Virtual Leitch (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeVirtualleitchModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
           // TODO : register VirtualLeitchRouter
        }

    }

}
