using OpenSC.Model;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Leitch;

namespace OpenSC.Modules
{

    [Module("routers-subtype-virtualleitch-model", "Routers / Virtual Leitch (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersSubtypeVirtualleitchModelModule : SubtypeModelModuleBase<RoutersModelModule>
    {

        protected override void registerModelTypes()
        {
            RouterTypeRegister.Instance.RegisterType<VirtualLeitchRouter>();
        }

    }

}
