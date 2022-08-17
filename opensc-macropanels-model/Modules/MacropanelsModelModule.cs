using OpenSC.Model;
using OpenSC.Model.Macros;
using OpenSC.Model.Persistence;

namespace OpenSC.Modules
{

    [Module("macropanels-model", "Macro panels (model)", "TODO")]
    [DependsOnModule(typeof(MacropanelsModelModule))]
    public class MacropanelsModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroPanelDatabase));
        }

        protected override void registerSerializers()
        {
            SerializerRegister.RegisterSerializer(new MacroPanelElementXmlSerializer());
        }

    }

}
