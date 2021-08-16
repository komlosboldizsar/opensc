using OpenSC.Model.Macros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores.Macros
{

    public class CrosspointStoreMacroCommands : MacroCommandRegister.IMacroCommandCollection
    {

        public static readonly IMacroCommand SetCrosspointStoreInput = new SetCrosspointStoreInputMacroCommand();
        public static readonly IMacroCommand SetCrosspointStoreOutput = new SetCrosspointStoreOutputMacroCommand();
        public static readonly IMacroCommand TakeCrosspointStore = new TakeCrosspointStoreMacroCommand();

        public static readonly MacroCommandRegister.IMacroCommandCollection Instance = new CrosspointStoreMacroCommands();
        public IMacroCommand[] CommandsToRegister => new IMacroCommand[] {
            SetCrosspointStoreInput,
            SetCrosspointStoreOutput,
            TakeCrosspointStore
        };

    }

}
