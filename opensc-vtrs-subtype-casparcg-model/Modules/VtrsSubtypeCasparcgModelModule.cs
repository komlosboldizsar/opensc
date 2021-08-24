using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("vtrs-subtype-casparcg-model", "VTRs / CasparCG (model)", "TODO")]
    [DependsOnModule(typeof(VtrsModelModule))]
    public class VtrsSubtypeCasparcgModelModule : SubtypeModelModuleBase<VtrsModelModule>
    {

        protected override void registerModelTypes()
        {
           // TODO : register CasparCgPlayout
        }

    }

}
