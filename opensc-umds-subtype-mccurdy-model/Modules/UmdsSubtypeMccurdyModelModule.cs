using OpenSC.Model;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;

namespace OpenSC.Modules
{

    [Module("umds-subtype-mccurdy-model", "UMDs / McCurdy (model)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsSubtypeMccurdyModelModule : SubtypeModelModuleBase<UmdsModelModule>
    {

        protected override void registerModelTypes()
        {
            UmdTypeRegister.Instance.RegisterType<McCurdyUMD1>();
            UmdTypeRegister.Instance.RegisterType<McCurdyUMD1T>();
        }

    }

}
