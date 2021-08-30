using OpenSC.GUI.Signals.TallyCopying;
using OpenSC.GUI.WorkspaceManager;

namespace OpenSC.Modules
{

    [Module("tallycopying-gui", "Tally copying (GUI)", "TODO")]
    [DependsOnModule(typeof(TallycopyingModelModule))]
    public class TallycopyingGuiModule : BasetypeGuiModuleBase<TallycopyingModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            WindowTypeRegister.RegisterWindowType<TallyCopyList>();
        }

    }

}
