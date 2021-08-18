using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(RouterOutputSource), "The name of the source that is connected to the given output of a router.")]
    class RouterOutputSource : DynamicTextFunctionBase<RouterOutputSource.Substitute>
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

            public override void Init(object[] argumentObjects)
            {

                Router router = argumentObjects[0] as Router;
                if (router == null)
                {
                    CurrentValue = "?";
                    return;
                }

                RouterOutput output = argumentObjects[1] as RouterOutput;
                if (output == null)
                {
                    CurrentValue = "?";
                    return;
                }

                output.RegisteredSourceSignalNameChanged += outputSourceSignalNameChangedHandler;
                CurrentValue = output.RegisteredSourceSignalName;

            }

            private void outputSourceSignalNameChangedHandler(ISignalSource inputSource, string newName, List<object> recursionChain)
                => CurrentValue = newName;

        }

    }

}
