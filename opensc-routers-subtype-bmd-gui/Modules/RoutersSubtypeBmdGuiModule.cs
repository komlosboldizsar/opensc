using OpenSC.GUI.Routers;
using OpenSC.Model.Routers.BlackMagicDesign;

namespace OpenSC.Modules
{

    [Module("routers-subtype-bmd-gui", "Routers / BlackMagic Design Videohub (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersSubtypeBmdModelModule))]
    public class RoutersSubtypeBmdGuiModule : SubtypeGuiModuleBase<RoutersSubtypeBmdModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            RouterEditorFormTypeRegister.Instance.RegisterFormType<BmdVideohub, BmdVideohubEditorForm>();
        }

    }

}
