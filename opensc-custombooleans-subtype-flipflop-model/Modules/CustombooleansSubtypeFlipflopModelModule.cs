using OpenSC.Model.Settings;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("custombooleans-subtype-flipflop-model", "Custom booleans / FlipFlop (model)", "TODO")]
    [DependsOnModule(typeof(CustombooleansModelModule))]
    public class CustombooleansSubtypeFlipflopModelModule : SubtypeModelModuleBase<CustombooleansModelModule>
    {

        protected override void registerModelTypes()
        {
            CustomBooleanTypeRegister.Instance.RegisterType<FlipFlopBoolean>();
        }

        protected override void registerSerializers()
        { }

    }

}
