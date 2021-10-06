using OpenSC.Model;
using OpenSC.Model.Streams.DynamicTextFunctions;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("streams-bridge-dynamictexts", "Streams (bridge to dynamic texts)", "TODO")]
    [DependsOnModule(typeof(StreamsModelModule))]
    public class StreamsBridgeDynamictextsModule : DynamictextsBridgeModuleBase<StreamsModelModule>
    {

        protected override void registerDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new StreamState());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new StreamStateTranslated());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new StreamViewerCount());
        }

    }

}
