namespace OpenSC.Modules
{

    [Module("streams-gui", "Streams (GUI)", "TODO")]
    [DependsOnModule(typeof(StreamsModelModule))]
    public class StreamsGuiModule : BasetypeGuiModuleBase<StreamsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register StreamList
        }

    }

}
