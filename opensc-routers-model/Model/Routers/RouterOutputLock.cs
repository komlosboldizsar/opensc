using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public class RouterOutputLock : INotifyPropertyChanged
    {

        public readonly RouterOutput Output;
        public readonly RouterOutputLockType Type;
        public readonly bool Supported;
        public readonly RouterOutputLockOwnerKnowLevel OwnerKnowLevel;

        #region Instantiation
        internal RouterOutputLock(RouterOutput output, RouterOutputLockType type, bool supported, RouterOutputLockOwnerKnowLevel ownerKnowLevel)
        {
            Output = output;
            Type = type;
            Supported = supported;
            OwnerKnowLevel = ownerKnowLevel;
        }

        internal RouterOutputLock(RouterOutput output, RouterOutputLockType type, RouterOutputLockInfo info)
            : this(output, type, info.Supported, info.OwnerKnowLevel) { }
        #endregion

        #region Property: State
        public event PropertyChangedTwoValuesDelegate<RouterOutputLock, RouterOutputLockState> StateChanged;
        private RouterOutputLockState state;

        public RouterOutputLockState State
        {
            get => state;
            set => this.setProperty(ref state, value, StateChanged);
        }
        #endregion

        #region Property: Owner
        public event PropertyChangedTwoValuesDelegate<RouterOutputLock, RouterOutputLockOwner> OwnerChanged;
        private RouterOutputLockOwner owner;

        public RouterOutputLockOwner Owner
        {
            get => owner;
            set => this.setProperty(ref owner, value, OwnerChanged);
        }
        #endregion

        #region Operations: do, undo, force undo
        public void Do()
        {
            if ((State == RouterOutputLockState.Locked) || (State == RouterOutputLockState.LockedLocal))
                return;
            foreach (RouterOutputLock otherLock in Output.AllLocks)
                if ((otherLock != this) && (otherLock.State != RouterOutputLockState.Clear))
                    throw new RouterOutputHasOtherLockException(this, otherLock);
            if (State == RouterOutputLockState.LockedRemote)
                throw new RouterOutputLockedByOtherUserException(this);
            Output.Router.RequestLockOperation(Output, Type, RouterOutputLockOperationType.Lock);
        }

        public void Undo()
        {
            if (State == RouterOutputLockState.Clear)
                return;
            if (State == RouterOutputLockState.LockedRemote)
                throw new RouterOutputLockedByOtherUserException(this);
            Output.Router.RequestLockOperation(Output, Type, RouterOutputLockOperationType.Unlock);
        }

        public void ForceUndo()
        {
            if (State == RouterOutputLockState.Clear)
                return;
            Output.Router.RequestLockOperation(Output, Type, RouterOutputLockOperationType.ForceUnlock);
        }
        #endregion

        #region State update from router
        internal void StateUpdateFromRouter(RouterOutputLockState newState) => State = newState;
        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedDelegate PropertyChanged;
        public void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(propertyName);
        #endregion

    }
}
