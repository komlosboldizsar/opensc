using OpenSC.Model.Routers;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(RouterOutputInputLabel), "The label associated with the input that is connected to the given output of a router.")]
    public class RouterOutputInputLabel : DynamicTextFunctionBase<RouterOutputInputLabel.Substitute>
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

        [DynamicTextFunctionArgument(2, "ID of the labelset.")]
        public class Arg2 : DynamicTextFunctionArgumentDatabaseItem<Labelset>
        {
            public Arg2() : base(LabelsetDatabase.Instance)
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private RouterInput currentInput;
            private Labelset labelset;

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

                Labelset labelset = argumentObjects[2] as Labelset;
                if (labelset == null)
                {
                    CurrentValue = "?";
                    return;
                }
                this.labelset = labelset;

                output.CurrentInputChanged += currentInputChangedHandler;
                labelset.LabelTextChanged += labelsetLabelChanged;

                currentInput = output.CurrentInput;
                if (currentInput == null)
                {
                    CurrentValue = "?";
                    return;
                }
                CurrentValue = labelset.GetText(currentInput);

            }

            private void labelsetLabelChanged(Labelset labelset, RouterInput routerInput, string oldText, string newText)
            {
                if ((labelset == this.labelset) && (currentInput == routerInput))
                    CurrentValue = newText;
            }

            private void currentInputChangedHandler(RouterOutput output, RouterInput newInput)
            {
                currentInput = newInput;
                CurrentValue = labelset.GetText(currentInput);
            }

        }

    }

}
