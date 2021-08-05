using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{

    public class VirtualLeitchRouterOutput : RouterOutput
    {
        public VirtualLeitchRouterOutput() : base()
        { }

        public VirtualLeitchRouterOutput(string name, Router router, int index) : base(name, router, index)
        { }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            RouterInput associatedInput = Router.GetInput(_associatedInputIndex);
            if (associatedInput != null)
                AssignSource(associatedInput);
        }

        public int _associatedInputIndex;

    }

}
