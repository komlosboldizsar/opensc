using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{
    internal class VirtualLeitchRouterOutputCollection : RouterOutputCollection,
        IHeterogenousCollection
    {

        public VirtualLeitchRouterOutputCollection(Router owner) : base(owner) { }

        protected override RouterOutput createInstance(string typeCode)
            => new VirtualLeitchRouterOutput();

        public override Type GetType(string typeCode) => typeof(VirtualLeitchRouterOutput);
        public string GetTypeCode(Type type) => "virtual_leitch";

    }
}
