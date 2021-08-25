namespace OpenSC.Modules
{

    [Module("labelsets-gui", "Labelsets (GUI)", "TODO")]
    [DependsOnModule(typeof(LabelsetsModelModule))]
    public class LabelsetsGuiModule : BasetypeGuiModuleBase<LabelsetsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register LabelsetList
        }

    }

}
