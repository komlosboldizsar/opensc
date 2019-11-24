using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Macros
{

    public class SetRouterCrosspointMacroCommand : MacroCommandBase
    {

        public override string CommandCode => "Routers.SetCrosspoint";

        public override string CommandName => "Set router crosspoint";

        public override string Description => "Set a single router crosspoint. Switch an output to a selected input.";

        public override IMacroCommandArgument[] Arguments => new IMacroCommandArgument[] {
            new Arg0(),
            new Arg1(),
            new Arg2()
        };

        public override void Run(object[] argumentValues)
        {

            if (argumentValues.Length != Arguments.Length)
                return;

            Router router = argumentValues[0] as Router;
            if (router == null)
                return;

            RouterOutput output = argumentValues[1] as RouterOutput;
            if ((output == null) || (output.Router != router))
                return;

            RouterInput input = argumentValues[2] as RouterInput;
            if ((input == null) || (input.Router != router))
                return;

            output.Crosspoint = input;

        }

        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int routerId))
                return null;
            if (!int.TryParse(keys[1], out int outputIndex))
                return null;
            if (!int.TryParse(keys[2], out int inputIndex))
                return null;

            Router router = RouterDatabase.Instance.GetTById(routerId);
            if (router == null)
                return null;

            if ((router.Outputs.Count <= outputIndex) || (router.Inputs.Count <= inputIndex))
                return null;

            return new object[]
            {
                router,
                router.Outputs[outputIndex],
                router.Inputs[outputIndex]
            };

        }

        private static readonly object[] ARRAY_EMPTY = new object[] { };

        public class Arg0 : IMacroCommandArgument
        {
            public string Name => "Router";
            public string Description => "The router that executes the crosspoint change.";
            public Type Type => typeof(Router);
            public object[] GetPossibilities(object[] previousArgumentValues)
                => RouterDatabase.Instance.ToArray();
            public string GetStringForPossibility(object item)
                => ((Router)item).Name;
        }

        public class Arg1 : IMacroCommandArgument
        {
            public string Name => "Router output";
            public string Description => "The router output that switches to an input.";
            public Type Type => typeof(RouterOutput);
            public string GetStringForPossibility(object item)
                => ((RouterOutput)item).Name;

            public object[] GetPossibilities(object[] previousArgumentValues)
            {
                if (previousArgumentValues.Length < 1)
                    return ARRAY_EMPTY;
                Router router = previousArgumentValues[0] as Router;
                if (router == null)
                    return ARRAY_EMPTY;
                return router.Outputs.ToArray();
            }

        }

        public class Arg2 : IMacroCommandArgument
        {
            public string Name => "Router input";
            public string Description => "The router input to switch to.";
            public Type Type => typeof(RouterInput);
            public string GetStringForPossibility(object item)
                => ((RouterInput)item).Name;

            public object[] GetPossibilities(object[] previousArgumentValues)
            {
                if (previousArgumentValues.Length < 1)
                    return ARRAY_EMPTY;
                Router router = previousArgumentValues[0] as Router;
                if (router == null)
                    return ARRAY_EMPTY;
                return router.Inputs.ToArray();
            }

        }

    }

}
