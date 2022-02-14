using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules
{

    [DependsOnModule(typeof(DynamictextsModelModule))]
    public abstract class DynamictextsFunctionsModuleBase : IModule
    {
        public void Initialize() => registerDynamicTextFunctions();
        protected abstract void registerDynamicTextFunctions();
    }

}
