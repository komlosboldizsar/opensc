using OpenSC.GUI.Macros;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("macros-gui", "Macros (GUI)", "TODO")]
    [DependsOnModule(typeof(MacrosModelModule))]
    public class MacrosGuiModule : BasetypeGuiModuleBase<MacrosModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<MacroList>();
        }

    }

}
