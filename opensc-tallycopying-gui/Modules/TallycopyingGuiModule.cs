namespace OpenSC.Modules
{

    [Module("tallycopying-gui", "Tally copying (GUI)", "TODO")]
    [DependsOnModule(typeof(TallycopyingModelModule))]
    public class TallycopyingGuiModule : BasetypeGuiModuleBase<TallycopyingModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register TallyCopyList
        }

    }

}
