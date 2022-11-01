using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public class RouterOutputCollection : ComponentCollection<Router, RouterOutput, RouterOutputCollection>
    {

        public RouterOutputCollection(Router owner) : base(owner)
        { }

        protected override RouterOutput createInstance(string typeCode) => new();
        protected override string InstanceNameTemplate => "Output #{0}";

    }
}
