using OpenSC.Model;
using OpenSC.Model.Mixers.DynamicTextFunctions;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("mixers-bridge-dynamictexts", "Mixers (bridge to dynamic texts)", "TODO")]
    [DependsOnModule(typeof(MixersModelModule))]
    public class MixersBridgeDynamictextsModule : DynamictextsBridgeModuleBase<MixersModelModule>
    {

        protected override void registerDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new MixerPreviewInputName());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new MixerProgramInputName());
        }

    }

}
