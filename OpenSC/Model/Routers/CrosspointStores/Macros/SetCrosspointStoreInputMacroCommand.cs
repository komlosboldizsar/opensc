using OpenSC.Model.Macros;
using OpenSC.Model.Routers.CrosspointStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores.Macros
{

    [MacroCommand("CrosspointStores.SetInput", "Set input on a crosspoint store", "Change the stored input of a crosspoint store.")]
    public class SetCrosspointStoreInputMacroCommand : MacroCommandBase
    {

        protected override void _run(object[] argumentValues)
        {
            CrosspointStore crosspointStore = argumentValues[0] as CrosspointStore;
            if (crosspointStore == null)
                return;
            Router router = argumentValues[1] as Router;
            if (router == null)
                return;
            RouterInput input = argumentValues[2] as RouterInput;
            if ((input == null) || (input.Router != router))
                return;
            crosspointStore.StoredInput = input;
        }

        [MacroCommandArgument(0, "Crosspoint store", "The crosspoint store to change the stored input.", typeof(CrosspointStore), MacroArgumentKeyType.Integer)]
        public class Arg0 : MacroCommandArgumentDatabaseItem<CrosspointStore>
        {
            public Arg0() : base(CrosspointStoreDatabase.Instance)
            { }
        }

        [MacroCommandArgument(1, "Router", "The router that contains the input to store.", typeof(Router), MacroArgumentKeyType.Integer)]
        public class Arg1 : MacroCommandArgumentDatabaseItem<Router>
        {
            public Arg1() : base(RouterDatabase.Instance)
            { }
        }
        
        [MacroCommandArgument(2, "Router input", "The router input to change the crosspoint store's input to.", typeof(RouterInput), MacroArgumentKeyType.Integer)]
        public class Arg2 : MacroCommandArgumentBase
        {
            protected override object _getObjectByKey(string key, object[] previousArgumentObjects)
            {
                if (!int.TryParse(key, out int keyInt))
                    return null;
                return (previousArgumentObjects[1] as Router)?.GetInput(keyInt);
            }
            protected override IEnumerable<object> _getPossibilities(object[] previousArgumentObjects) => (previousArgumentObjects[1] as Router)?.Inputs;
        }

    }

}
