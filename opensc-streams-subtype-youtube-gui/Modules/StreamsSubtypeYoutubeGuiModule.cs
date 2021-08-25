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
            // TODO: register YoutubeStreammEditorFor for YoutubeStream
        }

    }

}
