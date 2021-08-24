using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("mixers-subtype-bmd-model", "Mixers / BlackMagic Design ATEM (model)", "TODO")]
    [DependsOnModule(typeof(MixersModelModule))]
    public class MixersSubtypeBmdModelModule : SubtypeModelModuleBase<MixersModelModule>
    {

        protected override void registerModelTypes()
        {
           // TODO : register BmdVideohub
        }

    }

}
