using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Labelsets.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(RouterOutputsSourceSignalLabel), "The label from a given labelset associated with the source signal that is delegated to the given output of a router.")]
    public class RouterOutputsSourceSignalLabel : DynamicTextFunctionBase<RouterOutputsSourceSignalLabel.Substitute>
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

            private RouterOutput output;
            private Labelset labelset;
            private ISignalSource currentSource;

            public override void Init(object[] argumentObjects)
            {

                Router router = argumentObjects[0] as Router;
                if (router == null)
                {
                    CurrentValue = "?";
                    return;
                }

                output = argumentObjects[1] as RouterOutput;
                if (output == null)
                {
                    CurrentValue = "?";
                    return;
                }
                output.RegisteredSourceSignalChanged += registeredSourceSignalChangedHandler;

                labelset = argumentObjects[2] as Labelset;
                if (labelset == null)
                {
                    CurrentValue = "?";
                    return;
                }
                labelset.LabelTextChanged += labelsetTextChangedHandler;

                currentSource = output.RegisteredSourceSignal;
                if (currentSource == null)
                {
                    CurrentValue = "?";
                    return;
                }
                CurrentValue = labelset.GetLabel(currentSource).Text ?? "?";

            }

            private void labelsetTextChangedHandler(Label label, string newValue)
            {
                if ((label.AssociatedObject == currentSource) && (label.Labelset == labelset))
                    CurrentValue = newValue ?? "?";
            }

            private void registeredSourceSignalChangedHandler(ISignalSource signal, ISignalSourceRegistered registeredSignal, List<object> recursionChain)
            {
                if (signal != output)
                    return;
                currentSource = registeredSignal;
                CurrentValue = labelset.GetLabel(currentSource)?.Text ?? "?";
            }

        }

    }

}
