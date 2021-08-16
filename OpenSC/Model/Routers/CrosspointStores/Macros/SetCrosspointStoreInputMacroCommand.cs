using OpenSC.Model.Macros;
using OpenSC.Model.Routers.CrosspointStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores.Macros
{

    public class SetCrosspointStoreInputMacroCommand : MacroCommandBase
    {

        public override string CommandCode => "CrosspointStores.SetInput";

        public override string CommandName => "Set input on a crosspoint store";

        public override string Description => "Change the stored input of a crosspoint store.";

        public override IMacroCommandArgument[] Arguments => new IMacroCommandArgument[] {
            new Arg0(),
            new Arg1(),
            new Arg2()
        };

        public override void Run(object[] argumentValues)
        {

            if (argumentValues.Length != Arguments.Length)
                return;

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

        protected override string getArgumentKey(int index, object value)
        {

            if (index == 0) {
                CrosspointStore crosspointStore = value as CrosspointStore;
                if (crosspointStore == null)
                    return "-1";
                return crosspointStore.ID.ToString();
            }

            if (index == 1)
            {
                Router router = value as Router;
                if (router == null)
                    return "-1";
                return router.ID.ToString();
            }

            if (index == 2)
            {
                RouterInput routerInput = value as RouterInput;
                if (routerInput == null)
                    return "-1";
                return routerInput.Index.ToString();
            }

            throw new ArgumentException();

        }

        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int crosspointStoreId))
                return null;
            if (!int.TryParse(keys[1], out int routerId))
                return null;
            if (!int.TryParse(keys[2], out int inputIndex))
                return null;

            CrosspointStore crosspointStore = CrosspointStoreDatabase.Instance.GetTById(crosspointStoreId);
            if (crosspointStore == null)
                return null;

            Router router = RouterDatabase.Instance.GetTById(routerId);
            if (router == null)
                return null;

            if (router.Inputs.Count <= inputIndex)
                return null;

            return new object[]
            {
                crosspointStore,
                router,
                router.Inputs[inputIndex]
            };

        }

        private static readonly object[] ARRAY_EMPTY = new object[] { };

        public class Arg0 : IMacroCommandArgument
        {
            public string Name => "CrosspointStore";
            public string Description => "The crosspoint store to change the stored input.";
            public Type Type => typeof(CrosspointStore);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public object[] GetPossibilities(object[] previousArgumentValues)
                => CrosspointStoreDatabase.Instance.ToArray();
            public string GetStringForPossibility(object item)
                => ((CrosspointStore)item).Name;
        }

        public class Arg1 : IMacroCommandArgument
        {
            public string Name => "Router";
            public string Description => "The router that contains the input to store.";
            public Type Type => typeof(RouterOutput);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public object[] GetPossibilities(object[] previousArgumentValues)
                => RouterDatabase.Instance.ToArray();
            public string GetStringForPossibility(object item)
                => ((Router)item).Name;
        }

        public class Arg2 : IMacroCommandArgument
        {
            public string Name => "Router input";
            public string Description => "The router input to change the crosspoint store's input to.";
            public Type Type => typeof(RouterInput);
            public MacroArgumentKeyType KeyType => MacroArgumentKeyType.Integer;
            public string GetStringForPossibility(object item)
                => ((RouterInput)item).Name;

            public object[] GetPossibilities(object[] previousArgumentValues)
            {
                if (previousArgumentValues.Length < 1)
                    return ARRAY_EMPTY;
                Router router = previousArgumentValues[1] as Router;
                if (router == null)
                    return ARRAY_EMPTY;
                return router.Inputs.ToArray();
            }

        }

    }

}
