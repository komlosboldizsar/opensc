using OpenSC.GUI.Signals.BooleanTallies;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("booleantallies-gui", "Boolean tallies (GUI)", "TODO")]
    [DependsOnModule(typeof(BooleantalliesModelModule))]
    public class BooleantalliesGuiModule : BasetypeGuiModuleBase<BooleantalliesModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<BooleanTallyList>();
        }

    }

}
