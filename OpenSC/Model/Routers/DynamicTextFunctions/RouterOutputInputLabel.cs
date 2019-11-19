using OpenSC.Model.Routers;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.DynamicTextFunctions
{

    class RouterOutputInputLabel : IDynamicTextFunction
    {

        public string FunctionName => nameof(RouterOutputInputLabel);

        public string Description => "The label associated with the input that is connected to the given output of a router.";

        public int ParameterCount => 3;

        public DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer,
            DynamicTextFunctionArgumentType.Integer,
            DynamicTextFunctionArgumentType.Integer
        };

        public string[] ArgumentDescriptions => new string[]
        {
            "ID of the router.",
            "Index of the output.",
            "ID of the labelset."
        };

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Router router = RouterDatabase.Instance.GetTById((int)arguments[0]);
            Labelset labelset = LabelsetDatabase.Instance.GetTById((int)arguments[2]);
            return new Substitute(router, (int)arguments[1], labelset);
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private Router router;
            private RouterOutput output;
            private RouterInput currentInput;
            private Labelset labelset;

            public Substitute(Router router, int outputIndex, Labelset labelset)
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

                if (labelset == null)
                {
                    CurrentValue = "?";
                    return;
                }
                this.labelset = labelset;

                currentInput = output.Crosspoint;
                output.CrosspointChanged += crosspointChangedHandler;
                CurrentValue = labelset.GetText(output.Crosspoint);
                // TODO: Labelset text changes
                /*if (output.Crosspoint != null)
                    output.Crosspoint.NameChanged += crosspointNameChangedHandler;*/

            }

            // TODO: Labelset text changes
            /*private void crosspointNameChangedHandler(RouterInput input, string oldName, string newName)
            {
                CurrentValue = newName;
            }*/

            private void crosspointChangedHandler(RouterOutput output, RouterInput newInput)
            {
                // TODO: Labelset text changes
                /*if (currentInput != null)
                    currentInput.NameChanged -= crosspointNameChangedHandler;
                currentInput = newInput;
                if (currentInput != null)
                    currentInput.NameChanged += crosspointNameChangedHandler;
                CurrentValue = output.InputName;*/
            }

        }

    }

}
