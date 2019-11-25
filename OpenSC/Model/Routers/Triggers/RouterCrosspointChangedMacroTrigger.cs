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
            : base("Routers.OutputSourceChanged",
                  "Crosspoint change on a router",
                  "Observe all outputs for crosspoint change on a router.",
                  humanReadable)
        {
            addArgument("Router",
                "The router to observe.",
                typeof(Router),
                (prev) => RouterDatabase.Instance.ToArray(),
                router => ((Router)router).Name);
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

            throw new ArgumentException();

        }


        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int routerId))
                return null;

            Router router = RouterDatabase.Instance.GetTById(routerId);
            if (router == null)
                return null;

            return new object[] { router };

        }

        private const string HUMAN_READABLE_ERROR = "???";

        private static string humanReadable(object[] args)
        {
            if (args.Length < 1)
                return HUMAN_READABLE_ERROR;

            Router router = args[0] as Router;
            if (router == null)
                return HUMAN_READABLE_ERROR;

            return string.Format("Source for any output changes on router #{0} ({1}).", router.ID, router.Name);

        }

    }

}
