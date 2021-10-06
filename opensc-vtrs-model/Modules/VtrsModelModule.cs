using OpenSC.Model;
using OpenSC.Model.VTRs;

namespace OpenSC.Modules
{

    [Module("vtrs-model", "VTRs (model)", "TODO")]
    public class VtrsModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(VtrDatabase));
        }

        protected override void registerSerializers()
        { }

    }

}
