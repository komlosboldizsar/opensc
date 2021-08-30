using OpenSC.GUI.Timers;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("timers-gui", "Timers (GUI)", "TODO")]
    [DependsOnModule(typeof(TimersModelModule))]
    public class TimersGuiModule : BasetypeGuiModuleBase<TimersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<TimerList>();
        }

    }

}
