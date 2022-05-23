using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{
    public class LeitchRouterOutput : RouterOutput
    {

        public LeitchRouterOutput() : base()
        { }

        public LeitchRouterOutput(string name, Router router, int index) : base(name, router, index)
        { }

        private int lockProtectOwnerPanelId = -1;
        public int LockProtectOwnerPanelId
        {
            get => lockProtectOwnerPanelId;
            internal set
            {
                if (lockProtectOwnerPanelId == value)
                    return;
                lockProtectOwnerPanelId = value;
                bool @locked = (LockState != RouterOutputLockState.Clear);
                bool @protected = (ProtectState != RouterOutputLockState.Clear);
                LeitchRouterOutputLockOwner lockProtectOwnerObject = null;
                if (@locked || @protected)
                    lockProtectOwnerObject = new LeitchRouterOutputLockOwner(value);
                LockOwner = @locked ? lockProtectOwnerObject : null;
                ProtectOwner = @protected ? lockProtectOwnerObject : null;
            }
        }

        #region Supported lock operations
        public override bool LocksSupported => true;
        public override bool ProtectsSupported => true;

        public override RouterOutputLockOwnerKnowLevel LockOwnerKnowLevel => RouterOutputLockOwnerKnowLevel.Detailed;
        public override RouterOutputLockOwnerKnowLevel ProtectOwnerKnowLevel => RouterOutputLockOwnerKnowLevel.Detailed;
        #endregion

    }

}
