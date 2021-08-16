using OpenSC.Model.Macros;
using OpenSC.Model.Routers.CrosspointStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores.Macros
{

    public class TakeCrosspointStoreMacroCommand : MacroCommandBase
    {

        public override string CommandCode => "CrosspointStores.Take";

        public override string CommandName => "Take/fire a crosspoint store";

        public override string Description => "Route a crosspoint store's stored input to it's stored input.";

        public override IMacroCommandArgument[] Arguments => new IMacroCommandArgument[] {
            new Arg0()
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

            throw new ArgumentException();

        }

        public override object[] GetArgumentsByKeys(string[] keys)
        {

            if (keys.Length != Arguments.Length)
                return null;

            if (!int.TryParse(keys[0], out int crosspointStoreId))
                return null;

            CrosspointStore crosspointStore = CrosspointStoreDatabase.Instance.GetTById(crosspointStoreId);
            if (crosspointStore == null)
                return null;

            return new object[]
            {
                crosspointStore
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

    }

}
