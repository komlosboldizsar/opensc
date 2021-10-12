using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules
{

    [DependsOnModule(typeof(LabelsetsModelModule))]
    public abstract class LabelsetsBridgeModuleBase<TModuleFrom> : BridgeModuleBase<TModuleFrom, LabelsetsModelModule>
    {

        public override void Initialize()
        {
            startRegisteringLabelableObjects();
        }

        protected abstract void startRegisteringLabelableObjects();

    }

}
