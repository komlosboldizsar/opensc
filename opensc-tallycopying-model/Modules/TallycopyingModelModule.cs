using OpenSC.Model;
using OpenSC.Model.Signals.TallyCopying;

namespace OpenSC.Modules
{

    [Module("tallycopying-model", "Tally copying model", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class TallycopyingModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(TallyCopyDatabase));
        }

    }

}
