using OpenSC.Model;
using OpenSC.Model.Macros;

namespace OpenSC.Modules
{

    [Module("macros-model", "Macros (model)", "TODO")]
    public class MacrosModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroDatabase));
        }

    }

}
