using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("custombooleans-model", "Custom booleans (model)", "TODO")]
    [DependsOnModule(typeof(BooleansModelModule))]
    public class CustombooleansModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(CustomBooleanDatabase));
        }

        protected override void registerSerializers()
        { }

    }

}
