using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Routers.Triggers;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterOutput : SignalForwarder, ISignalSourceRegistered, INotifyPropertyChanged
    {

        public RouterOutput() : base()
        {
            this.CurrentSourceChanged += currentSourceChangedHandler;
        }

        public RouterOutput(string name, Router router, int index)
            : base()
        {
            this.name = name;
            this.Router = router;
            this.Index = index;
            this.CurrentSourceChanged += currentSourceChangedHandler;
            registerAsSignal();
        }

        public void Restored()
        {
            registerAsSignal();
            createTallyBooleans();
        }

        public virtual void TotallyRestored()
        { }

        public delegate void RemovedDelegate(RouterOutput routerOutput);
        public event RemovedDelegate Removed;

        #region Property: Name
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                PropertyChanged?.Invoke(nameof(Name));
                SignalLabelChanged?.Invoke(this, getSignalLabel());
                PropertyChanged?.Invoke(nameof(ISignalSourceRegistered.SignalLabel));
            }
        }

        public delegate void NameChangedDelegate(RouterOutput output, string oldName, string newName);
        public event NameChangedDelegate NameChanged;
        #endregion

        #region Property: Router
        public Router Router { get; private set; }

        internal void AssignParentRouter(Router router)
        {
            if (Router != null)
                return;
            Router = router;
        }

        public void RemovedFromRouter(Router router)
        {
            if (router != Router)
                return;
            Router = null;
            unregisterAsSignal();
            Removed?.Invoke(this);
        }
        #endregion

        #region Property: Index
        private int index;

        public int Index
        {
            get => index;
            set
            {
                if (value == index)
                    return;
                int oldValue = index;
                index = value;
                IndexChanged?.Invoke(this, oldValue, value);
                PropertyChanged?.Invoke(nameof(Index));
                SignalLabelChanged?.Invoke(this, getSignalLabel());
                PropertyChanged?.Invoke(nameof(ISignalSourceRegistered.SignalLabel));
                SignalUniqueIdChanged?.Invoke(this, SignalUniqueId);
                PropertyChanged?.Invoke(nameof(ISignalSourceRegistered.SignalUniqueId));
            }
        }

        public delegate void IndexChangedDelegate(RouterOutput input, int oldIndex, int newIndex);
        public event IndexChangedDelegate IndexChanged;
        #endregion
        
        #region Source assignment
        public override void AssignSource(ISignalSource source) // when source already changed
        {
            RouterInput sourceRouterInput = source as RouterInput;
            if (sourceRouterInput == null)
                return;
            if (sourceRouterInput.Router != Router)
                throw new ArgumentException();
            base.AssignSource(source);
            string logMessage = string.Format("Router crosspoint updated. Router: [(#{0}) {1}], destination: #{2}, source: #{3}.",
                    Router.ID,
                    Router.Name,
                    Index,
                    sourceRouterInput.Index);
            LogDispatcher.I(Router.LOG_TAG, logMessage);
            CurrentInputChanged?.Invoke(this, source as RouterInput);
            RouterMacroTriggers.RouterOutputSourceChanged.Call(Router, this);
        }

        public void RequestCrosspointUpdate(RouterInput input)
        {
            Router?.RequestCrosspointUpdate(this, input);
        }
        #endregion

        #region Property: CurrentInput
        public RouterInput CurrentInput => CurrentSource as RouterInput;

        public delegate void CurrentInputChangedDelegate(RouterOutput output, RouterInput newInput);
        public event CurrentInputChangedDelegate CurrentInputChanged;

        private void currentSourceChangedHandler(ISignalDestination signalDestination, ISignalSource newSource)
            => CurrentInputChanged?.Invoke(this, newSource as RouterInput);
        #endregion

        #region Property: SignalLabel
        string ISignalSourceRegistered.SignalLabel
            => getSignalLabel();

        private string getSignalLabel()
            => string.Format("[(#{2}) {3}] output of router [(#{0}) {1}]", Router.ID, Router.Name, (Index + 1), Name);

        public event PropertyChangedOneValueDelegate<ISignalSourceRegistered, string> SignalLabelChanged;
        #endregion

        #region Property: SignalUniqueId
        public string SignalUniqueId
            => string.Format("router.{0}.output.{1}", Router.ID, (Index + 1));

        public event PropertyChangedOneValueDelegate<ISignalSourceRegistered, string> SignalUniqueIdChanged;
        #endregion

        #region Property: LocksSupported, LockState
        public virtual bool LocksSupported => true;
        public virtual bool LockOwnerKnown => true;

        private RouterOutputLockState lockState;

        public RouterOutputLockState LockState
        {
            get => lockState;
            protected set
            {
                if (value == lockState)
                    return;
                RouterOutputLockState oldValue = value;
                lockState = value;
                LockStateChanged?.Invoke(this, oldValue, value);
                PropertyChanged?.Invoke(nameof(LockState));
            }
        }

        public delegate void LockStateChangedDelegate(RouterOutput output, RouterOutputLockState oldLockState, RouterOutputLockState newLockState);
        public event LockStateChangedDelegate LockStateChanged;

        internal void LockStateUpdateFromRouter(RouterOutputLockState newState)
            => LockState = newState;

        public void RequestLock()
        {
            if ((LockState == RouterOutputLockState.Locked) || (LockState == RouterOutputLockState.LockedLocal))
                return;
            if (ProtectState != RouterOutputLockState.Clear)
                throw new Exception("This output is locked! You must force unprotect before locking.");
            if (LockState == RouterOutputLockState.LockedRemote)
                throw new Exception("This output is already locked by another user! You must force unlock before.");
            Router.RequestLockOperation(this, RouterOutputLockType.Lock, RouterOutputLockOperationType.Lock);
        }

        public void RequestUnlock()
        {
            if (LockState == RouterOutputLockState.Clear)
                return;
            if (LockState == RouterOutputLockState.LockedRemote)
                throw new Exception("This output is locked by another user! You must use the force unlock function.");
            Router.RequestLockOperation(this, RouterOutputLockType.Lock, RouterOutputLockOperationType.Unlock);
        }

        public void RequestForceUnlock()
        {
            if (LockState == RouterOutputLockState.Clear)
                return;
            Router.RequestLockOperation(this, RouterOutputLockType.Lock, RouterOutputLockOperationType.ForceUnlock);
        }
        #endregion

        #region Property: ProtectsSupported, ProtectState
        public virtual bool ProtectsSupported => false;
        public virtual bool ProtectOwnerKnown => true;

        private RouterOutputLockState protectState;

        public RouterOutputLockState ProtectState
        {
            get => protectState;
            protected set
            {
                if (value == protectState)
                    return;
                RouterOutputLockState oldValue = value;
                protectState = value;
                ProtectStateChanged?.Invoke(this, oldValue, value);
                PropertyChanged?.Invoke(nameof(ProtectState));
            }
        }

        public delegate void ProtectStateChangedDelegate(RouterOutput output, RouterOutputLockState oldProtectState, RouterOutputLockState newProtectState);
        public event ProtectStateChangedDelegate ProtectStateChanged;

        internal void ProtectStateUpdateFromRouter(RouterOutputLockState newState)
            => ProtectState = newState;

        public void RequestProtect()
        {
            if ((ProtectState == RouterOutputLockState.Locked) || (ProtectState == RouterOutputLockState.LockedLocal))
                return;
            if (LockState != RouterOutputLockState.Clear)
                throw new Exception("This output is locked! You must force unlock before protecting.");
            if (ProtectState == RouterOutputLockState.LockedRemote)
                throw new Exception("This output is already protected by another user! You must force unprotect before.");
            Router.RequestLockOperation(this, RouterOutputLockType.Protect, RouterOutputLockOperationType.Lock);
        }

        public void RequestUnprotect()
        {
            if (ProtectState == RouterOutputLockState.Clear)
                return;
            if (ProtectState == RouterOutputLockState.LockedRemote)
                throw new Exception("This output is protected by another user! You must use the force unprotect function.");
            Router.RequestLockOperation(this, RouterOutputLockType.Protect, RouterOutputLockOperationType.Unlock);
        }

        public void RequestForceUnprotect()
        {
            if (ProtectState == RouterOutputLockState.Clear)
                return;
            Router.RequestLockOperation(this, RouterOutputLockType.Protect, RouterOutputLockOperationType.ForceUnlock);
        }
        #endregion

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;
        #endregion

        #region Signals
        private void registerAsSignal()
        {
            SignalRegister.Instance.RegisterSignal(this);
        }

        private void unregisterAsSignal()
        {
            SignalRegister.Instance.UnregisterSignal(this);
        }
        #endregion

        #region Tally booleans
        private RouterOutputTallyBoolean redTallyBoolean = null;
        private RouterOutputTallyBoolean yellowTallyBoolean = null;
        private RouterOutputTallyBoolean greenTallyBoolean = null;

        private void createTallyBooleans()
        {
            redTallyBoolean = new RouterOutputTallyBoolean(this, RedTally, SignalTallyColor.Red);
            yellowTallyBoolean = new RouterOutputTallyBoolean(this, YellowTally, SignalTallyColor.Yellow);
            greenTallyBoolean = new RouterOutputTallyBoolean(this, GreenTally, SignalTallyColor.Green);
        }
        #endregion

    }

}
