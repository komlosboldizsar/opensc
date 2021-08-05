using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public enum RouterOutputLockOperationType
    {
        Lock,
        Unlock,
        ForceUnlock,
        Protect,
        Unprotect,
        ForceUnprotect
    }
}
