using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BmdAtemMv
{
    internal class BmdAtemMvRouterOutputCollection : RouterOutputCollection,
        IHeterogenousCollection
    {

        public BmdAtemMvRouterOutputCollection(Router owner) : base(owner) { }

        protected override RouterOutput createInstance(string typeCode)
            => new BmdAtemMvRouterOutput();

        public override Type GetType(string typeCode) => typeof(BmdAtemMvRouterOutput);
        public string GetTypeCode(Type type) => "bmdatemmv";

    }
}
