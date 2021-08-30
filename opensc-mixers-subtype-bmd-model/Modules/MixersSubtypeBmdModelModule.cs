using OpenSC.Model;
using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;

namespace OpenSC.Modules
{

    [Module("mixers-subtype-bmd-model", "Mixers / BlackMagic Design ATEM (model)", "TODO")]
    [DependsOnModule(typeof(MixersModelModule))]
    public class MixersSubtypeBmdModelModule : SubtypeModelModuleBase<MixersModelModule>
    {

        protected override void registerModelTypes()
        {
            MixerTypeRegister.Instance.RegisterType<BmdMixer>();
        }

    }

}
