using OpenSC.Model;
using OpenSC.Model.Signals.TallyCopying;

namespace OpenSC.Modules
{

    [Module("tallycopying-model", "Tally copying model", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class TallycopyingModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(TallyCopyDatabase));
        }

        protected override void registerSerializers()
        { }

    }

}
