using OpenSC.GUI.Streams;
using OpenSC.Model.Streams;

namespace OpenSC.Modules
{

    [Module("streams-subtype-youtube-gui", "Streams / YouTube (GUI)", "TODO")]
    [DependsOnModule(typeof(StreamsSubtypeYoutubeModelModule))]
    public class StreamsSubtypeYoutubeGuiModule : SubtypeGuiModuleBase<StreamsSubtypeYoutubeModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            StreamEditorFormTypeRegister.Instance.RegisterFormType<YoutubeStream, YoutubeStreamEditorForm>();
        }

    }

}
