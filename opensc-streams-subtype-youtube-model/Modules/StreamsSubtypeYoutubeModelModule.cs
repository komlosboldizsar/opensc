using OpenSC.Model;

namespace OpenSC.Modules
{

    [Module("streams-subtype-youtube-model", "Streams / YouTube (model)", "TODO")]
    [DependsOnModule(typeof(StreamsModelModule))]
    public class StreamsSubtypeYoutubeModelModule : SubtypeModelModuleBase<StreamsModelModule>
    {

        protected override void registerModelTypes()
        {
           // TODO : register YoutubeStream
        }

    }

}
