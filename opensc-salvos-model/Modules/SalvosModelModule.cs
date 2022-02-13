using OpenSC.Model;
using OpenSC.Model.Routers.Salvos;

namespace OpenSC.Modules
{

    [Module("salvos-model", "Salvos (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class SalvosModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(SalvoDatabase));
        }

        protected override void registerSerializers()
        { }

    }

}
