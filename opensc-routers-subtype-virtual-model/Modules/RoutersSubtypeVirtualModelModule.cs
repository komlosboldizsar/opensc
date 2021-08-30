using OpenSC.Model;
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

    }

}
