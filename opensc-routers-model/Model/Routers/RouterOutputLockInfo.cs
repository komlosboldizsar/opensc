using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public record RouterOutputLockInfo(bool Supported, RouterOutputLockOwnerKnowLevel OwnerKnowLevel)
    {
        public static readonly RouterOutputLockInfo NotSupported = new(false, RouterOutputLockOwnerKnowLevel.None);
    }
}
