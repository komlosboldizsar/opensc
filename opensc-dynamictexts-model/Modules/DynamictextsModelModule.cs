using OpenSC.Model;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("dynamictexts-model", "Dynamic texts (model)", "TODO")]
    public class DynamictextsModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(DynamicTextDatabase));
        }

    }

}
