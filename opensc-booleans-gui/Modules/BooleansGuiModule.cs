namespace OpenSC.Modules
{

    [Module("booleans-gui", "Booleans (GUI)", "TODO")]
    [DependsOnModule(typeof(BooleansModelModule))]
    public class BooleansGuiModule : BasetypeGuiModuleBase<BooleansModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register BooleanList
        }

    }

}
