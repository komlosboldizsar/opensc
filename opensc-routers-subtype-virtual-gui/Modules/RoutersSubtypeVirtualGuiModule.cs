using OpenSC.GUI.Routers;
using OpenSC.Model.Routers.Virtual;

namespace OpenSC.Modules
{

    [Module("routers-subtype-virtual-gui", "Routers / Virtual (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersSubtypeVirtualModelModule))]
    public class RoutersSubtypeVirtualGuiModule : SubtypeGuiModuleBase<RoutersSubtypeVirtualModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            RouterEditorFormTypeRegister.Instance.RegisterFormType<VirtualRouter, VirtualRouterEditorForm>();
        }

    }

}
