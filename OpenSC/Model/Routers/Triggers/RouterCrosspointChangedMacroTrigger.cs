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
                (prev) => ((prev[0] as Router)?.Outputs.ToArray()) ?? (new object[] { }),
                output => ((RouterOutput)output).Name);
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
