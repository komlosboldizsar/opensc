namespace OpenSC.Modules
{

    [Module("routermirrors-gui", "Router mirrors (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutermirrorsModelModule))]
    public class RoutermirrorsGuiModule : BasetypeGuiModuleBase<RoutermirrorsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register RouterMirrorList
        }

    }

}
