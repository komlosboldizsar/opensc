using OpenSC.Model;
using OpenSC.Model.Labelsets.DynamicTextFunctions;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("routers-labelsets-bridge-dynamictexts", "Routers with labelsets (bridge to dynamic texts)", "TODO")]
    [DependsOnModule(typeof(LabelsetsModelModule))]
    public class RoutersLabelsetsBridgeDynamictextsModule : DynamictextsBridgeModuleBase<RoutersModelModule, LabelsetsModelModule>
    {

        protected override void registerDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new RouterOutputsInputLabel());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new RouterOutputsSourceSignalLabel());
        }

    }

}
