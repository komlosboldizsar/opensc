using OpenSC.Model;
using OpenSC.Model.Variables.DynamicTextFunctions;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("dynamictexts-functions-datetime", "Dynamictexts, datetime functions", "TODO")]
    public class DynamictextsDatetimeModule : DynamictextsFunctionsModuleBase
    {

        protected override void registerDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new DatetimeDotnetFormat());
        }

    }

}
