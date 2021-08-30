using OpenSC.GUI.Routers.CrosspointStores;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("crosspointstores-gui", "Crosspoint stores (GUI)", "TODO")]
    [DependsOnModule(typeof(CrosspointstoresModelModule))]
    public class CrosspointstoresGuiModule : BasetypeGuiModuleBase<CrosspointstoresModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<CrosspointStoreList>();
        }

    }

}
