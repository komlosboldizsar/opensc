namespace OpenSC.Modules
{

    [Module("timers-gui", "Timers (GUI)", "TODO")]
    [DependsOnModule(typeof(TimersModelModule))]
    public class TimersGuiModule : BasetypeGuiModuleBase<TimersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register TimerList
        }

    }

}
