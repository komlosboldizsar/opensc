namespace OpenSC.Modules
{

    [Module("routers-gui", "Routers (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersGuiModule : BasetypeGuiModuleBase<RoutersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register RouterList
            // TODO: register Router controller forms
        }

    }

}
