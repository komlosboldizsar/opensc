using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{
    public class LeitchRouterOutput : RouterOutput
    {


        private int lockProtectOwnerPanelId = -1;
        public int LockProtectOwnerPanelId
        {
            get => lockProtectOwnerPanelId;
            internal set
            {
                if (lockProtectOwnerPanelId == value)
                    return;
                lockProtectOwnerPanelId = value;
                bool @locked = (Lock.State != RouterOutputLockState.Clear);
                bool @protected = (Protect.State != RouterOutputLockState.Clear);
                LeitchRouterOutputLockOwner lockProtectOwnerObject = null;
                if (@locked || @protected)
                    lockProtectOwnerObject = new LeitchRouterOutputLockOwner(value);
                Lock.Owner = @locked ? lockProtectOwnerObject : null;
                Protect.Owner = @protected ? lockProtectOwnerObject : null;
            }
        }

        protected override RouterOutputLockInfo LockInfo { get; } = new RouterOutputLockInfo(true, RouterOutputLockOwnerKnowLevel.Detailed);
        protected override RouterOutputLockInfo ProtectInfo { get; } = new RouterOutputLockInfo(true, RouterOutputLockOwnerKnowLevel.Detailed);

    }

}
