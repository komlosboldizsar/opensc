using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Virtual
{

    [TypeLabel("Virtual router")]
    [TypeCode("virtual")]
    class VirtualRouter : Router
    {

        public VirtualRouter()
        {
            State = RouterState.Ok;
            StateString = "OK";
        }

        public override void Restored()
        {
            base.Restored();
            State = RouterState.Ok;
            StateString = "OK";
        }

        protected override bool setCrosspoint(RouterOutput output, RouterInput input)
        {
            output.Crosspoint = input;
            return true;
        }

        protected override void updateAllCrosspoints()
        {
            Random r = new Random();
            int c = Inputs.Count;
            foreach (RouterOutput output in Outputs)
                output.Crosspoint = (c > 0) ? Inputs[r.Next(c-1)] : null;
        }

    }

}
