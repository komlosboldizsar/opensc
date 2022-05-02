using OpenSC.GUI.Variables;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("custombooleans-subtype-flipflop-gui", "Custom booleans / FlipFlop (GUI)", "TODO")]
    [DependsOnModule(typeof(CustombooleansSubtypeFlipflopModelModule))]
    public class CustombooleansSubtypeFlipflopGuiModule : SubtypeGuiModuleBase<CustombooleansSubtypeFlipflopModelModule>
    {

        protected override void registerPersistableWindowTypes()
        { }

        protected override void registerSubtypeEditorWindowTypes()
        {
            CustomBooleanEditorFormTypeRegister.Instance.RegisterFormType<FlipFlopBoolean, FlipFlopBooleanEditorForm>();
        }

    }

}
