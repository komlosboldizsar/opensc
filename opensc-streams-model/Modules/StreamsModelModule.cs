using OpenSC.Model;
using OpenSC.Model.Streams;

namespace OpenSC.Modules
{

    [Module("streams-model", "Streams (model)", "TODO")]
    public class StreamsModelModule : ModelModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(StreamDatabase));
        }

    }

}
