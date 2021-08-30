using OpenSC.GUI.Routers;
using OpenSC.Model.Routers.Leitch;

namespace OpenSC.Modules
{

    [Module("routers-subtype-leitch-gui", "Routers / Leitch (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersSubtypeLeitchModelModule))]
    public class RoutersSubtypeLeitchGuiModule : SubtypeGuiModuleBase<RoutersSubtypeLeitchModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            RouterEditorFormTypeRegister.Instance.RegisterFormType<LeitchRouter, LeitchRouterEditorForm>();
        }

    }

}
