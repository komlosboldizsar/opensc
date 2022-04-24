using OpenSC.GUI.UMDs;
using OpenSC.Model.UMDs.Tsl31;

namespace OpenSC.Modules
{

    [Module("umds-subtype-tsl31-gui", "UMDs / TSL 3.1 (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsSubtypeTsl31ModelModule))]
    public class UmdsSubtypeTsl31GuiModule : SubtypeGuiModuleBase<UmdsSubtypeTsl31ModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            UmdEditorFormTypeRegister.Instance.RegisterFormType<Tsl31, Tsl31UmdEditorForm>();
        }

    }

}
