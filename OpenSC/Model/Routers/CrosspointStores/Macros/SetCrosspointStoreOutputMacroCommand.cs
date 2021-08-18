using OpenSC.Model.Macros;
using OpenSC.Model.Routers.CrosspointStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores.Macros
{

    [MacroCommand("CrosspointStores.SetOutput", "Set output on a crosspoint store", "Change the stored output of a crosspoint store.")]
    public class SetCrosspointStoreOutputMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentValues)
        {
            CrosspointStore crosspointStore = argumentValues[0] as CrosspointStore;
            if (crosspointStore == null)
                return;
            Router router = argumentValues[1] as Router;
            if (router == null)
                return;
            RouterOutput output = argumentValues[2] as RouterOutput;
            if ((output == null) || (output.Router != router))
                return;
            crosspointStore.StoredOutput = output;
        }

        [MacroCommandArgument(0, "Crosspoint store", "The crosspoint store to change the stored output.")]
        public class Arg0 : MacroCommandArgumentDatabaseItem<CrosspointStore>
        {
            public Arg0() : base(CrosspointStoreDatabase.Instance)
            { }
        }

        [MacroCommandArgument(1, "Router", "The router that contains the output to store.")]
        public class Arg1 : MacroCommandArgumentDatabaseItem<Router>
        {
            public Arg1() : base(RouterDatabase.Instance)
            { }
        }

        [MacroCommandArgument(2, "Router output", "The router output to change the crosspoint store's output to.")]
        public class Arg2 : MacroCommandArgumentBase
        {
            protected override object _getObjectByKey(string key, object[] previousArgumentObjects)
            {
                if (!int.TryParse(key, out int keyInt))
                    return null;
                return (previousArgumentObjects[1] as Router)?.GetOutput(keyInt);
            }
            public override string GetKeyByObject(object obj) => (obj as RouterOutput)?.Index.ToString();
            protected override IEnumerable<object> _getPossibilities(object[] previousArgumentObjects) => (previousArgumentObjects[1] as Router)?.Outputs;
        }

    }

}
