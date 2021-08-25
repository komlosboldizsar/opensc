namespace OpenSC.Modules
{

    [Module("crosspointstores-gui", "Crosspoint stores (GUI)", "TODO")]
    [DependsOnModule(typeof(CrosspointstoresModelModule))]
    public class CrosspointstoresGuiModule : BasetypeGuiModuleBase<CrosspointstoresModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register CrosspointStoreList
        }

    }

}
