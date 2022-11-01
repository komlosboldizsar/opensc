using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{
    internal class LeitchRouterOutputCollection : RouterOutputCollection,
        IHeterogenousCollection
    {

        public LeitchRouterOutputCollection(Router owner) : base(owner) { }

        protected override RouterOutput createInstance(string typeCode)
            => new LeitchRouterOutput();

        public override Type GetType(string typeCode) => typeof(LeitchRouterOutput);
        public string GetTypeCode(Type type) => "leitch";

    }
}
