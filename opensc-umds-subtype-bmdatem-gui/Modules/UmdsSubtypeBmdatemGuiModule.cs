using OpenSC.GUI.UMDs;
using OpenSC.Model.UMDs.BmdAtem;

namespace OpenSC.Modules
{

    [Module("umds-subtype-bmdatem-gui", "UMDs / BMD ATEM (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsSubtypeBmdatemModelModule))]
    public class UmdsSubtypeBmdatemGuiModule : SubtypeGuiModuleBase<UmdsSubtypeBmdatemModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            UmdEditorFormTypeRegister.Instance.RegisterFormType<BmdAtem, BmdAtemUmdEditorForm>();
        }

    }

}
