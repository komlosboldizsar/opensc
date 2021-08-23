using OpenSC.Model;
using OpenSC.Model.Macros;

namespace OpenSC.Modules
{

    [Module("macropanels-model", "Macro panels (model)", "TODO")]
    [DependsOnModule(typeof(MacropanelsModelModule))]
    public class MacropanelsModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroPanelDatabase));
        }

    }

}
