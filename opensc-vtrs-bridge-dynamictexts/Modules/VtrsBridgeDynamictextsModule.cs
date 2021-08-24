using OpenSC.Model;
using OpenSC.Model.Variables;
using OpenSC.Model.VTRs.DynamicTextFunctions;

namespace OpenSC.Modules
{

    [Module("vtrs-bridge-dynamictexts", "VTRs (bridge to dynamic texts)", "TODO")]
    [DependsOnModule(typeof(VtrsModelModule))]
    public class VtrsBridgeDynamictextsModule : DynamictextsBridgeModuleBase<VtrsModelModule>
    {

        protected override void registerDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new VtrElapsedTimeHhMmSs());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new VtrRemainingTimeHhMmSs());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new VtrState());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new VtrStateTranslated());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new VtrTitle());
        }

    }

}
