namespace OpenSC.Modules
{

    [Module("signals-gui", "Signals (GUI)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class SignalsGuiModule : BasetypeGuiModuleBase<SignalsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register SignalList
            // TODO: register ExternalSignalList
            // TODO: register ExternalSignalCategoryList
        }

    }

}
