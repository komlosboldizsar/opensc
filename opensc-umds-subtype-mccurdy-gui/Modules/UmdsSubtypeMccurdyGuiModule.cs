namespace OpenSC.Modules
{

    [Module("umds-subtype-mccurdy-gui", "UMDs / McCurdy and McCurdy with tally (GUI)", "TODO")]
    [DependsOnModule(typeof(UmdsSubtypeMccurdyModelModule))]
    public class UmdsSubtypeMccurdyGuiModule : SubtypeGuiModuleBase<UmdsSubtypeMccurdyModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            // TODO: register McCurdyUmd1EditorForm for McCurdyUMD1
            // TODO: register McCurdyUmd1TEditorForm for McCurdyUMD1T
        }

    }

}
