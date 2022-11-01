using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.BlackMagicDesign
{
    public class BmdVideohubOutput : RouterOutput
    {
        protected override RouterOutputLockInfo LockInfo { get; } = new RouterOutputLockInfo(true, RouterOutputLockOwnerKnowLevel.LocalOrRemote);
        protected override RouterOutputLockInfo ProtectInfo { get; } = RouterOutputLockInfo.NotSupported;
    }
}
