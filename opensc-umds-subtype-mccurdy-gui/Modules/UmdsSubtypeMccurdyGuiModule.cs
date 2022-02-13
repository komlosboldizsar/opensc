using OpenSC.GUI.UMDs;
using OpenSC.Model.UMDs.McCurdy;

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
            UmdEditorFormTypeRegister.Instance.RegisterFormType<McCurdyUMD1, McCurdyUmd1EditorForm>();
            UmdEditorFormTypeRegister.Instance.RegisterFormType<McCurdyUMD1T, McCurdyUmd1TEditorForm>();
        }

    }

}
