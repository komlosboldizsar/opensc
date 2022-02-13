using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules
{


    [DependsOnModule(typeof(MacrosModelModule))]
    public abstract class MacrosBridgeModuleBase<TModuleFrom> : BridgeModuleBase<TModuleFrom, MacrosModelModule>
    {

        public override void Initialize()
        {
            registerMacroCommands();
            registerTriggerTypes();
        }

        protected abstract void registerMacroCommands();
        protected abstract void registerTriggerTypes();

    }

}
