using OpenSC.Model;
using OpenSC.Model.Labelsets.DynamicTextFunctions;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("labelsets-bridge-dynamictexts", "Labelsets (bridge to dynamic texts)", "TODO")]
    [DependsOnModule(typeof(LabelsetsModelModule))]
    public class LabelsetsBridgeDynamictextsModule : DynamictextsBridgeModuleBase<LabelsetsModelModule>
    {

        protected override void registerDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new ObjectLabel());
        }

    }

}
