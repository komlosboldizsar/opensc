using OpenSC.GUI.Mixers;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("mixers-gui", "Mixers (GUI)", "TODO")]
    [DependsOnModule(typeof(MixersModelModule))]
    public class MixersGuiModule : BasetypeGuiModuleBase<MixersModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MixerList>();
        }

    }

}
