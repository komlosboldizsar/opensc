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
            // TODO: register VirtualRouterEditorForm for VirtualRouter
        }

    }

}
