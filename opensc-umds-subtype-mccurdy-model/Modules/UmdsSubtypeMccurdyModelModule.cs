using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("umds-subtype-mccurdy-model", "UMDs / McCurdy (model)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsSubtypeMccurdyModelModule : SubtypeModelModuleBase<UmdsModelModule>
    {

        protected override void registerModelTypes()
        {
           // TODO : register McCurdyUMD1
           // TODO : register McCurdyUMD1T
        }

    }

}
