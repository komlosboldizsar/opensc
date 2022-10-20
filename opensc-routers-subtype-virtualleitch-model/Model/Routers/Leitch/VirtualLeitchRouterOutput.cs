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
            if (Lock.State != RouterOutputLockState.Clear)
                return;
            if ((Protect.State != RouterOutputLockState.Clear) || (panelId != LockProtectOwnerPanelId))
                return;
            AssignSource(input);
        }

        public void AssignPreset(RouterInput input, int panelId)
        {
            if (Lock.State != RouterOutputLockState.Clear)
                return;
            if ((Protect.State != RouterOutputLockState.Clear) && (panelId != LockProtectOwnerPanelId))
                return;
            PresetInput = input;
            presetClear = false;
        }

        private bool presetClear = true;
        public void ClearPreset()
        {
            PresetInput = null;
            presetClear = true;
        }

        public void ExecutePreset()
        {
            if (presetClear)
                return;
            AssignSource(PresetInput);
            presetClear = true;
        }

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
                bool @locked = (Lock.State != RouterOutputLockState.Clear);
                bool @protected = (Protect.State != RouterOutputLockState.Clear);
                VirtualLeitchRouterOutputLockOwner lockProtectOwnerObject = null;
                if (@locked || @protected)
                    lockProtectOwnerObject = new VirtualLeitchRouterOutputLockOwner(value);
                Lock.Owner = @locked ? lockProtectOwnerObject : null;
                Protect.Owner = @protected ? lockProtectOwnerObject : null;
            }
        }

        public int LockStatusCode
        {
            get
            {
                if (Lock.State != RouterOutputLockState.Clear)
                    return 1;
                if (Protect.State != RouterOutputLockState.Clear)
                    return 2;
                return 0;
            }
        }

        public void SetLock(int state, int panelId)
        {
            bool localRequest = (panelId == VirtualLeitchRouter.PanelIdSetting.Value);
            switch (state)
            {
                case 0:
                    bool lockOwned = (panelId == LockProtectOwnerPanelId);
                    bool force = (panelId == VirtualLeitchRouter.FORCE_UNLOCK_PANEL_ID);
                    if (lockOwned || force)
                    {
                        Lock.State = RouterOutputLockState.Clear;
                        Protect.State = RouterOutputLockState.Clear;
                        LockProtectOwnerPanelId = -1;
                    }
                    break;
                case 1:
                    if ((Lock.State == RouterOutputLockState.Clear) && (Protect.State == RouterOutputLockState.Clear))
                    {
                        Lock.State = localRequest ? RouterOutputLockState.LockedLocal : RouterOutputLockState.LockedRemote;
                        Protect.State = RouterOutputLockState.Clear;
                        LockProtectOwnerPanelId = panelId;
                    }
                    break;
                case 2:
                    if ((Lock.State == RouterOutputLockState.Clear) && (Protect.State == RouterOutputLockState.Clear))
                    {
                        Lock.State = RouterOutputLockState.Clear;
                        Protect.State = localRequest ? RouterOutputLockState.LockedLocal : RouterOutputLockState.LockedRemote;
                        LockProtectOwnerPanelId = panelId;
                    }
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

        protected override RouterOutputLockInfo LockInfo { get; } = new RouterOutputLockInfo(true, RouterOutputLockOwnerKnowLevel.Detailed);
        protected override RouterOutputLockInfo ProtectInfo { get; } = new RouterOutputLockInfo(true, RouterOutputLockOwnerKnowLevel.Detailed);

    }

}
