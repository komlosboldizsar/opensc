namespace OpenSC.Modules
{

    [Module("dynamictexts-gui", "Dynamic texts (GUI)", "TODO")]
    [DependsOnModule(typeof(DynamictextsModelModule))]
    public class DynamictextsGuiModule : BasetypeGuiModuleBase<DynamictextsModelModule>
    {

        protected override void registerPersistableWindowTypes()
        {
            // TODO: register DynamicTextList
        }

    }

}
