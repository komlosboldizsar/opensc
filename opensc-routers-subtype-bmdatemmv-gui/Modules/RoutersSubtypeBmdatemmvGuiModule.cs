using OpenSC.GUI.Routers;
using OpenSC.Model.Routers.BmdAtemMv;

namespace OpenSC.Modules
{

    [Module("routers-subtype-bmdatemmv-gui", "Router / BMD ATEM Multiview (GUI)", "TODO")]
    [DependsOnModule(typeof(RoutersSubtypeBmdatemmvModelModule))]
    public class RoutersSubtypeBmdatemmvGuiModule : SubtypeGuiModuleBase<RoutersSubtypeBmdatemmvModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            RouterEditorFormTypeRegister.Instance.RegisterFormType<BmdAtemMvRouter, BmdAtemMvRouterEditorForm>();
        }

    }

}
