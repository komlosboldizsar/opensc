using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Macros
{

    [MacroCommand("Routers.SetCrosspoint", "Set router crosspoint", "Set a single router crosspoint. Switch an output to a selected input.")]
    public class SetRouterCrosspointMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentValues)
        {
            Router router = argumentValues[0] as Router;
            if (router == null)
                return;
            RouterOutput output = argumentValues[1] as RouterOutput;
            if ((output == null) || (output.Router != router))
                return;
            RouterInput input = argumentValues[2] as RouterInput;
            if ((input == null) || (input.Router != router))
                return;
            output.RequestCrosspointUpdate(input);
        }

        [MacroCommandArgument(0, "Router", "The router that executes the crosspoint change.")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<Router>
        {
            public Arg0() : base(RouterDatabase.Instance)
            { }
        }

        [MacroCommandArgument(1, "Router output", "The router output that switches to an input.")]
        public class Arg1 : MacroCommandArgumentBase
        {
            public Arg1() : base(typeof(RouterOutput), MacroArgumentKeyType.Integer)
            { }
            protected override object _getObjectByKey(string key, object[] previousArgumentObjects)
            {
                if (!int.TryParse(key, out int keyInt))
                    return null;
                return (previousArgumentObjects[0] as Router)?.GetOutput(keyInt);
            }
            protected override IEnumerable<object> _getPossibilities(object[] previousArgumentObjects) => (previousArgumentObjects[0] as Router)?.Outputs;
        }

        [MacroCommandArgument(2, "Router input", "The router input to switch to.")]
        public class Arg2 : MacroCommandArgumentBase
        {
            public Arg2() : base(typeof(RouterInput), MacroArgumentKeyType.Integer)
            { }
            protected override object _getObjectByKey(string key, object[] previousArgumentObjects)
            {
                if (!int.TryParse(key, out int keyInt))
                    return null;
                return (previousArgumentObjects[0] as Router)?.GetInput(keyInt);
            }
            protected override IEnumerable<object> _getPossibilities(object[] previousArgumentObjects) => (previousArgumentObjects[0] as Router)?.Inputs;
        }

    }

}
