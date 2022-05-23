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
            if (LockState != RouterOutputLockState.Clear)
                return;
            if ((ProtectState != RouterOutputLockState.Clear) || (panelId != LockProtectOwnerPanelId))
                return;
            AssignSource(input);
        }

        public void AssignPreset(RouterInput input, int panelId)
        {
            if (LockState != RouterOutputLockState.Clear)
                return;
            if ((ProtectState != RouterOutputLockState.Clear) || (panelId != LockProtectOwnerPanelId))
                return;
            PresetInput = input;
        }

        public void ClearPreset() => PresetInput = null;

        public void ExecutePreset() => AssignSource(PresetInput);

        public RouterInput PresetInput { get; private set; } = null;

        public int _associatedInputIndex;

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
                VirtualLeitchRouterOutputLockOwner lockProtectOwnerObject = null;
                if (@locked || @protected)
                    lockProtectOwnerObject = new VirtualLeitchRouterOutputLockOwner(value);
                LockOwner = @locked ? lockProtectOwnerObject : null;
                ProtectOwner = @protected ? lockProtectOwnerObject : null;
            }
        }

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
            bool lockOwned = (panelId == VirtualLeitchRouter.PanelIdSetting.Value);
            switch (state)
            {
                case 0:
                    LockState = RouterOutputLockState.Clear;
                    ProtectState = RouterOutputLockState.Clear;
                    LockProtectOwnerPanelId = -1;
                    break;
                case 1:
                    LockState = lockOwned ? RouterOutputLockState.LockedLocal : RouterOutputLockState.LockedRemote;
                    ProtectState = RouterOutputLockState.Clear;
                    LockProtectOwnerPanelId = panelId;
                    break;
                case 2:
                    LockState = RouterOutputLockState.Clear;
                    ProtectState = lockOwned ? RouterOutputLockState.LockedLocal : RouterOutputLockState.LockedRemote;
                    LockProtectOwnerPanelId = panelId;
                    break;
            }
        }

        public void SetLock(RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
        {
            if (lockOperationType == RouterOutputLockOperationType.Unlock)
            {
                SetLock(0, VirtualLeitchRouter.PanelIdSetting.Value);
                return;
            }
            if (lockOperationType == RouterOutputLockOperationType.ForceUnlock)
            {
                SetLock(0, VirtualLeitchRouter.FORCE_UNLOCK_PANEL_ID);
                return;
            }
            if (lockType == RouterOutputLockType.Lock)
            {
                SetLock(1, VirtualLeitchRouter.PanelIdSetting.Value);
                return;
            }
            if (lockType == RouterOutputLockType.Protect)
            {
                SetLock(2, VirtualLeitchRouter.PanelIdSetting.Value);
                return;
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
