using OpenSC.GUI.UMDs;
using OpenSC.Model.UMDs.Tsl50;

namespace OpenSC.Modules
{

    [Module("umds-subtype-tsl50-gui", "UMDs / TSL 5.0 (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsSubtypeTsl50ModelModule))]
    public class UmdsSubtypeTsl50GuiModule : SubtypeGuiModuleBase<UmdsSubtypeTsl50ModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            UmdEditorFormTypeRegister.Instance.RegisterFormType<Tsl50Display, Tsl50DisplayUmdEditorForm>();
        }

    }

}
