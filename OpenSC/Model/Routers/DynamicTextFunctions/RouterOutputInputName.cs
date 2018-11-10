using OpenSC.Model.Routers;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.DynamicTextFunctions
{

    class RouterOutputInputName : IDynamicTextFunction
    {

        public string FunctionName => nameof(RouterOutputInputName);

        public string Description => "The name of the input that is connected to the given output of a router.";

        public int ParameterCount => 2;

        public DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer,
            DynamicTextFunctionArgumentType.Integer,
        };

        public string[] ArgumentDescriptions => new string[]
        {
            "ID of the router.",
            "Index of the output."
        };

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Router router = RouterDatabase.Instance.GetTById((int)arguments[0]);
            return new Substitute(router, (int)arguments[1]);
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private Router router;
            private RouterOutput output;
            private RouterInput currentInput;

            public Substitute(Router router, int outputIndex)
            {

                if (router == null)
                {
                    CurrentValue = "?";
                    return;
                }
                this.router = router;

                if (router.Outputs.Count < outputIndex)
                {
                    CurrentValue = "?";
                    return;
                }
                output = router.Outputs[outputIndex-1];

                currentInput = output.Crosspoint;
                output.CrosspointChanged += crosspointChangedHandler;
                CurrentValue = output.InputName;
                if (output.Crosspoint != null)
                    output.Crosspoint.NameChanged += crosspointNameChangedHandler;

            }

            private void crosspointNameChangedHandler(RouterInput input, string oldName, string newName)
            {
                CurrentValue = newName;
            }

            private void crosspointChangedHandler(RouterOutput output, RouterInput newInput)
            {
                if (currentInput != null)
                    currentInput.NameChanged -= crosspointNameChangedHandler;
                currentInput = newInput;
                if (currentInput != null)
                    currentInput.NameChanged += crosspointNameChangedHandler;
                CurrentValue = output.InputName;
            }

        }

    }

}
