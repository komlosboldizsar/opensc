using OpenSC.Model;
using OpenSC.Model.VTRs;

namespace OpenSC.Modules
{

    [Module("vtrs-subtype-casparcg-model", "VTRs / CasparCG (model)", "TODO")]
    [DependsOnModule(typeof(VtrsModelModule))]
    public class VtrsSubtypeCasparcgModelModule : SubtypeModelModuleBase<VtrsModelModule>
    {

        protected override void registerModelTypes()
        {
            VtrTypeRegister.Instance.RegisterType<CasparCgPlayout>();
        }

    }

}
