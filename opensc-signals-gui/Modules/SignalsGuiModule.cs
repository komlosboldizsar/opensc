using OpenSC.GUI.Signals;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("signals-gui", "Signals (GUI)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class SignalsGuiModule : BasetypeGuiModuleBase<SignalsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<SignalList>();
            WindowTypeRegister.RegisterWindowType<ExternalSignalList>();
            WindowTypeRegister.RegisterWindowType<ExternalSignalCategoryList>();
        }

    }

}
