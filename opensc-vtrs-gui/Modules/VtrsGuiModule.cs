namespace OpenSC.Modules
{

    [Module("vtrs-gui", "VTRs (GUI)", "TODO")]
    [DependsOnModule(typeof(VtrsModelModule))]
    public class VtrsGuiModule : BasetypeGuiModuleBase<VtrsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register VtrList
        }

    }

}
