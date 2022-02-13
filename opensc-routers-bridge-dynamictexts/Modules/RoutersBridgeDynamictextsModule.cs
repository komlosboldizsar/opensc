using OpenSC.Model;
using OpenSC.Model.Routers.DynamicTextFunctions;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("routers-bridge-dynamictexts", "Routers (bridge to dynamic texts)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersBridgeDynamictextsModule : DynamictextsBridgeModuleBase<RoutersModelModule>
    {

        protected override void registerDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new RouterOutputInputName());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new RouterOutputSource());
        }

    }

}
