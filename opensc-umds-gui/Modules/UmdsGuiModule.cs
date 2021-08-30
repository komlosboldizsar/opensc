using OpenSC.GUI.UMDs;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("umds-gui", "UMDs (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsGuiModule : BasetypeGuiModuleBase<UmdsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<UmdList>();
        }

    }

}
