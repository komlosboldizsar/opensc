namespace OpenSC.Modules
{

    [Module("crosspointbooleans-gui", "Crosspoint booleans (GUI)", "TODO")]
    [DependsOnModule(typeof(CrosspointbooleansModelModule))]
    public class CrosspointbooleansGuiModule : BasetypeGuiModuleBase<CrosspointbooleansModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register CrosspointBooleanList
        }

    }

}
