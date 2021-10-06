using OpenSC.Model.Routers;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(RouterOutputInputName), "The name of the input that is connected to the given output of a router.")]
    public class RouterOutputInputName : DynamicTextFunctionBase<RouterOutputInputName.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the router.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Router>
        {
            public Arg0() : base(RouterDatabase.Instance)
            { }
        }

        [DynamicTextFunctionArgument(1, "Index of the output.")]
        public class Arg1 : DynamicTextFunctionArgumentBase
        {
            public Arg1() : base(typeof(RouterOutput), DynamicTextFunctionArgumentType.Integer)
            { }
            protected override object _getObjectByKey(object key, object[] previousArgumentObjects)
                => (previousArgumentObjects[0] as Router)?.GetOutput((int)key);
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private RouterInput currentInput;

            public override void Init(object[] argumentObjects)
            {

                Router router = argumentObjects[0] as Router;
                if (router == null)
                {
                    CurrentValue = "?";
                    return;
                }

                RouterOutput output = router.GetOutput((int)argumentObjects[1]);
                if (output == null)
                {
                    CurrentValue = "?";
                    return;
                }
                output.CurrentInputChanged += currentInputChangedHandler;

                currentInput = output.CurrentInput;
                if (currentInput == null)
                {
                    CurrentValue = "?";
                    return;
                }
                CurrentValue = output.CurrentInput.Name;
                currentInput.NameChanged += currentInputNameChangedHandler;

            }

            private void currentInputNameChangedHandler(RouterInput input, string oldName, string newName)
                => CurrentValue = newName;

            private void currentInputChangedHandler(RouterOutput output, RouterInput newInput)
            {
                if (currentInput != null)
                    currentInput.NameChanged -= currentInputNameChangedHandler;
                currentInput = newInput;
                if (currentInput != null)
                    currentInput.NameChanged += currentInputNameChangedHandler;
                CurrentValue = currentInput.Name;
            }

        }

    }

}
