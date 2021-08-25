namespace OpenSC.Modules
{

    [Module("umds-gui", "UMDs (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsModelModule))]
    public class UmdsGuiModule : BasetypeGuiModuleBase<UmdsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register UmdList
        }

    }

}
