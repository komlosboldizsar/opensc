using OpenSC.Model;
using OpenSC.Model.Routers;

namespace OpenSC.Modules
{

    [Module("labelsets-model", "Labelsets (model)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class LabelsetsModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(LabelsetDatabase));
        }

    }

}
