using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{

    public class VirtualLeitchRouterOutput : RouterOutput
    {
        public VirtualLeitchRouterOutput() : base()
        { }

        public VirtualLeitchRouterOutput(string name, Router router, int index) : base(name, router, index)
        { }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            RouterInput associatedInput = Router.GetInput(_associatedInputIndex);
            if (associatedInput != null)
                AssignSource(associatedInput);
        }

        public void AssignInput(RouterInput input, int panelId)
        {
            if (LockState == RouterOutputLockState.Clear)
                return;
            if ((ProtectState != RouterOutputLockState.Clear) || (panelId != LockOwnerPanelId))
                return;
            AssignSource(input);
        }

        public void AssignPreset(RouterInput input, int panelId)
        {
            if (LockState == RouterOutputLockState.Clear)
                return;
            if ((ProtectState != RouterOutputLockState.Clear) || (panelId != LockOwnerPanelId))
                return;
            PresetInput = input;
        }

        public void ClearPreset() => PresetInput = null;

        public void ExecutePreset() => AssignSource(PresetInput);

        public RouterInput PresetInput { get; private set; } = null;

        public int _associatedInputIndex;

        public int LockOwnerPanelId { get; private set; } = -1;

        public int LockStatusCode
        {
            get
            {
                if (LockState != RouterOutputLockState.Clear)
                    return 1;
                if (ProtectState != RouterOutputLockState.Clear)
                    return 2;
                return 0;
            }
        }

        public void SetLock(int state, int panelId)
        {
            bool lockOwned = (panelId == VirtualLeitchRouter.OWN_PANEL_ID);
            switch (state)
            {
                case 0:
                    LockState = RouterOutputLockState.Clear;
                    ProtectState = RouterOutputLockState.Clear;
                    LockOwnerPanelId = -1;
                    break;
                case 1:
                    LockState = lockOwned ? RouterOutputLockState.LockedLocal : RouterOutputLockState.LockedRemote;
                    ProtectState = RouterOutputLockState.Clear;
                    LockOwnerPanelId = panelId;
                    break;
                case 2:
                    LockState = RouterOutputLockState.Clear;
                    ProtectState = lockOwned ? RouterOutputLockState.LockedLocal : RouterOutputLockState.LockedRemote;
                    LockOwnerPanelId = panelId;
                    break;
            }
        }

        public void SetLock(RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
        {
            if (lockOperationType == RouterOutputLockOperationType.Unlock)
            {
                SetLock(0, VirtualLeitchRouter.OWN_PANEL_ID);
                return;
            }
            if (lockOperationType == RouterOutputLockOperationType.ForceUnlock)
            {
                SetLock(0, VirtualLeitchRouter.FORCE_UNLOCK_PANEL_ID);
                return;
            }
            if (lockType == RouterOutputLockType.Lock)
            {
                SetLock(1, VirtualLeitchRouter.OWN_PANEL_ID);
                return;
            }
            if (lockType == RouterOutputLockType.Protect)
            {
                SetLock(2, VirtualLeitchRouter.OWN_PANEL_ID);
                return;
            }
        }

        #region Supported lock operations
        public override bool LocksSupported => true;
        public override bool ProtectsSupported => true;

        public override bool LockOwnerKnown => true;
        public override bool ProtectOwnerKnown => true;
        #endregion

    }

}
