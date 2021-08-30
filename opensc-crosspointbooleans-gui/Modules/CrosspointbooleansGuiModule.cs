using OpenSC.GUI.Routers.CrosspointBooleans;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("crosspointbooleans-gui", "Crosspoint booleans (GUI)", "TODO")]
    [DependsOnModule(typeof(CrosspointbooleansModelModule))]
    public class CrosspointbooleansGuiModule : BasetypeGuiModuleBase<CrosspointbooleansModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<CrosspointBooleanList>();
        }

    }

}
