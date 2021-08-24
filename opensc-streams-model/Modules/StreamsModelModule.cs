using OpenSC.Model;
using OpenSC.Model.Streams;

namespace OpenSC.Modules
{

    [Module("streams-model", "Streams (model)", "TODO")]
    public class StreamsModelModule : BasetypeModuleBase
    {

        protected override void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(StreamDatabase));
        }

    }

}
