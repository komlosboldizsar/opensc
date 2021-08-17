using OpenSC.Model.Macros;
using OpenSC.Model.Routers.CrosspointStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores.Macros
{
    [MacroCommand("CrosspointStores.Take", "Take/fire a crosspoint store", "Route a crosspoint store's stored input to it's stored input.")]

    public class TakeCrosspointStoreMacroCommand : MacroCommandBase
    {

        public override void Run(object[] argumentValues) => (argumentValues[0] as CrosspointStore)?.Take();

        [MacroCommandArgument(0, "Crosspoint store", "The crosspoint store to take/fire.", typeof(CrosspointStore), MacroArgumentKeyType.Integer)]
        public class Arg0 : MacroCommandArgumentDatabaseItem<CrosspointStore>
        {
            public Arg0() : base(CrosspointStoreDatabase.Instance)
            { }
        }

    }

}
