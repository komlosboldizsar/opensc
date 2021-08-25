namespace OpenSC.Modules
{

    [Module("booleantallies-gui", "Boolean tallies (GUI)", "TODO")]
    [DependsOnModule(typeof(BooleantalliesModelModule))]
    public class BooleantalliesGuiModule : BasetypeGuiModuleBase<BooleantalliesModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register BooleanTallyList
        }

    }

}
