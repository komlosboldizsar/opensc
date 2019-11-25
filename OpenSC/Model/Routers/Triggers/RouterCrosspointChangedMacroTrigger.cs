using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Triggers
{

    class RouterCrosspointChangedMacroTrigger : MacroTriggerDefaultCallImplementations.AllArgumentsMatchStrict
    {

        public RouterCrosspointChangedMacroTrigger()
            : base("Routers.CrosspointChanged",
                  "Router crosspoint changed",
                  "Observe a single output for source change on a router.",
                  humanReadable)
        {
            addArgument("Router",
                "The router to observe.",
                typeof(Router),
                (prev) => RouterDatabase.Instance.ToArray(),
                router => ((Router)router).Name);
            addArgument("Output",
                "Output of the selected router to observe.",
                typeof(Router),
                argPossibilities1,
                output => ((RouterOutput)output).Name);
        }

        private static readonly object[] ARRAY_EMPTY = new object[] { };

        private static object[] argPossibilities1(object[] prev)
        {
            if (prev.Length < 1)
                return ARRAY_EMPTY;
            return ((prev[0] as Router)?.Outputs.ToArray()) ?? (new object[] { });
        }

        protected override string getArgumentKey(int index, object value)
        {

            if (index == 0)
            {
                Router router = value as Router;
                if (router == null)
                    return "-1";
                return router.ID.ToString();
            }

            if (index == 1)
            {
                RouterOutput routerOutput = value as RouterOutput;
                if (routerOutput == null)
                    return "-1";
                return routerOutput.Index.ToString();
            }

            throw new ArgumentException();

        }


        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int routerId))
                return null;
            if (!int.TryParse(keys[1], out int outputIndex))
                return null;

            Router router = RouterDatabase.Instance.GetTById(routerId);
            if (router == null)
                return null;

            if (router.Outputs.Count <= outputIndex)
                return null;

            return new object[]
            {
                router,
                router.Outputs[outputIndex]
            };

        }

        private const string HUMAN_READABLE_ERROR = "???";

        private static string humanReadable(object[] args)
        {
            if (args.Length < 2)
                return HUMAN_READABLE_ERROR;

            Router router = args[0] as Router;
            if (router == null)
                return HUMAN_READABLE_ERROR;

            RouterOutput routerOutput = args[1] as RouterOutput;
            if (routerOutput == null)
                return HUMAN_READABLE_ERROR;

            return string.Format("Source for output #{2} ({3}) changes on router #{0} ({1}).", router.ID, router.Name, routerOutput.Index, routerOutput.Name);

        }

    }

}
