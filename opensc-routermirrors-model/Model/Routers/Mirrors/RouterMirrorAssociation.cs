using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{

    public abstract class RouterMirrorAssociation<TAssociated>
    {

        public RouterMirrorAssociation()
        { }

        public RouterMirrorAssociation(RouterMirror parent, TAssociated itemA, TAssociated itemB)
        {
            Parent = parent;
            ItemA = itemA;
            ItemB = itemB;
        }

        public virtual void TotallyRestored()
        { }

        public RouterMirror Parent { get; private set; }
        public TAssociated ItemA { get; protected set; }
        public TAssociated ItemB { get; protected set; }
        public int _itemA;
        public int _itemB;

        public virtual void AssignParent(RouterMirror parent)
        {
            Parent = parent;
        }

        public virtual void RemovedFromParent()
        {
            Parent = null;
        }

    }

}
