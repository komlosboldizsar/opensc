using OpenSC.GUI.Routers.Mirrors;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("routermirrors-gui", "Router mirrors (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutermirrorsModelModule))]
    public class RoutermirrorsGuiModule : BasetypeGuiModuleBase<RoutermirrorsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<RouterMirrorList>();
        }

    }

}
