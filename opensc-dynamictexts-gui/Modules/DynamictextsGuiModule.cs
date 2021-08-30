using OpenSC.GUI.Variables;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("dynamictexts-gui", "Dynamic texts (GUI)", "TODO")]
    [DependsOnModule(typeof(DynamictextsModelModule))]
    public class DynamictextsGuiModule : BasetypeGuiModuleBase<DynamictextsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<DynamicTextList>();
        }

    }

}
