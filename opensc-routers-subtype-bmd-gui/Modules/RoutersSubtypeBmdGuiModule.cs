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
            // TODO: register BmdRouterEditorForm for BmdRouter
        }

    }

}
