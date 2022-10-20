using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public abstract class RouterOutputLockOwner
    {
        public abstract string Owner { get; }
        public sealed override string ToString() => Owner;
    }
}
