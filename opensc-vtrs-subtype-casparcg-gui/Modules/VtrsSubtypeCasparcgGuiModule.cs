namespace OpenSC.Modules
{

    [Module("routers-subtype-bmd-gui", "VTRs / CasparCG (GUI)", "TODO")]
    [DependsOnModule(typeof(VtrsSubtypeCasparcgModelModule))]
    public class VtrsSubtypeCasparcgGuiModule : SubtypeGuiModuleBase<VtrsSubtypeCasparcgModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            // TODO: register CasparCgPlayoutEditorForm for CasparCgPlayout
        }

    }

}
