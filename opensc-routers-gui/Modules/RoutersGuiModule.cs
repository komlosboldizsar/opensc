using OpenSC.GUI.Routers;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("routers-gui", "Routers (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersGuiModule : BasetypeGuiModuleBase<RoutersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<RouterList>();
            WindowTypeRegister.RegisterWindowType<RouterControlForm>();
            WindowTypeRegister.RegisterWindowType<RouterControlTableForm>();
        }

    }

}
