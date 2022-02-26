using OpenSC.Model;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.BmdAtem;

namespace OpenSC.Modules
{

    [Module("umds-subtype-bmdatem-model", "UMDs / BMD ATEM (model)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    [DependsOnModule(typeof(MixersSubtypeBmdModelModule))]
    public class UmdsSubtypeBmdatemModelModule : SubtypeModelModuleBase<UmdsModelModule>
    {

        protected override void registerModelTypes()
        {
            UmdTypeRegister.Instance.RegisterType<BmdAtem>();
        }

        protected override void registerSerializers()
        { }

    }

}
