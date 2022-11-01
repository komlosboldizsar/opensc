using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BlackMagicDesign
{
    internal class BmdVideohubOutputCollection : RouterOutputCollection
    {

        public BmdVideohubOutputCollection(Router owner) : base(owner) { }

        protected override RouterOutput createInstance(string typeCode)
            => new BmdVideohubOutput();

        public override Type GetType(string typeCode) => typeof(BmdVideohubOutput);

    }
}
