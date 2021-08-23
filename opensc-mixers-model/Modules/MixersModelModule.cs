using OpenSC.Model;
using OpenSC.Model.Mixers;

namespace OpenSC.Modules
{

    [Module("mixers-model", "Mixers (model)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class MixersModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MixerDatabase));
        }

    }

}
