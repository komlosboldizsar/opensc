using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Virtual
{
    internal class VirtualRouterOutputCollection : RouterOutputCollection,
        IHeterogenousCollection
    {

        public VirtualRouterOutputCollection(Router owner) : base(owner) { }

        protected override RouterOutput createInstance(string typeCode)
            => new VirtualRouterOutput();

        public override Type GetType(string typeCode) => typeof(VirtualRouterOutput);
        public string GetTypeCode(Type type) => "virtual";

    }
}
