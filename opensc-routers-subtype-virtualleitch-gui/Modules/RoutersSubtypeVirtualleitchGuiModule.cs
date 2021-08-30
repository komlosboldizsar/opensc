using OpenSC.GUI.Routers;
using OpenSC.Model.Routers.Leitch;

namespace OpenSC.Modules
{

    [Module("routers-subtype-virtualleitch-gui", "Router / Virtual Leitch (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersSubtypeVirtualleitchModelModule))]
    public class RoutersSubtypeVirtualleitchGuiModule : SubtypeGuiModuleBase<RoutersSubtypeVirtualleitchModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            RouterEditorFormTypeRegister.Instance.RegisterFormType<VirtualLeitchRouter, VirtualLeitchRouterEditorForm>();
        }

    }

}
