using OpenSC.Model;
using OpenSC.Model.UMDs;

namespace OpenSC.Modules
{

    [Module("umds-model", "UMDs (model)", "TODO")]
    public class UmdsModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(UmdDatabase));
        }

    }

}
