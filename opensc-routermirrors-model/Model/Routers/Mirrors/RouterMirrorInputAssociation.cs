using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{

    public class RouterMirrorInputAssociation : RouterMirrorAssociation<RouterInput>
    {

        public RouterMirrorInputAssociation() : base()
        { }

        public RouterMirrorInputAssociation(RouterMirror parent, RouterInput itemA, RouterInput itemB)
            : base(parent, itemA, itemB)
        { }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            ItemA = Parent.RouterA?.Inputs[_itemA];
            ItemB = Parent.RouterB?.Inputs[_itemB];
        }

    }

}
