using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Signals;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public partial class RouterOutput : SignalForwarder, ISignalSourceRegistered, ISystemObject
    {

        public RouterOutput() : base()
        {
            createAllLocks();
            this.CurrentSourceChanged += currentSourceChangedHandler;
            SystemObjectRegister.Instance.Register(this);
        }

        public RouterOutput(string name, Router router, int index)
            : base()
        {
            createAllLocks();
            this.name = name;
            this.Router = router;
            this.Index = index;
            this.CurrentSourceChanged += currentSourceChangedHandler;
            SystemObjectRegister.Instance.Register(this);
            registerAsSignal();
            router.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
            generateGlobalId();
        }

        public void Restored()
        {
            createTallyBooleans();
        }

        public virtual void TotallyRestored()
        { }

        public delegate void RemovedDelegate(RouterOutput routerOutput);
        public event RemovedDelegate Removed;

        #region Property: GlobalID
        public override event PropertyChangedTwoValuesDelegate<ISystemObject, string> GlobalIdChanged;

        private string globalId;
        public override string GlobalID => globalId;
        private void updateGlobalId(string value) => this.setProperty(ref globalId, value, GlobalIdChanged);

        private void generateGlobalId()
        {
            if (Router == null)
            {
                updateGlobalId(null);
                return;
            }
            updateGlobalId($"{Router.GlobalID}.output.{Index}");
        }
        #endregion

        #region Property: Name
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_name_afterChange))]
        [AutoProperty.Validator(nameof(ValidateName))]
        private string name;

        private void _name_afterChange(string oldValue, string newValue)
        {
            SignalLabelChanged?.Invoke(this, getSignalLabel());
            ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));
            Router?.NotifyLocalOutputNameChanged(this);
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Router
        public Router Router { get; private set; }

        internal void AssignParentRouter(Router router)
        {
            if (Router != null)
                return;
            Router = router;
            router.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
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
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_index_afterChange))]
        private int index;

        private void _index_afterChange(int oldValue, int newValue)
        {
            SignalLabelChanged?.Invoke(this, getSignalLabel());
            ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));
            SignalUniqueIdChanged?.Invoke(this, SignalUniqueId);
            ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalUniqueId));
            generateGlobalId();
        }
        #endregion
        
        #region Source assignment
        public override void AssignSource(ISignalSource source) // when source already changed
        {
            bool isNull = (source == null);
            RouterInput sourceRouterInput = source as RouterInput;
            if (!isNull && (sourceRouterInput == null))
                return;
            if (!isNull && (sourceRouterInput.Router != Router))
                throw new ArgumentException();
            base.AssignSource(source);
            string logMessage = $"Router crosspoint updated. Router: [{Router}], destination: #{Index}, source: #{sourceRouterInput?.Index.ToString() ?? "-"}.";
            LogDispatcher.I(Router.LOG_TAG, logMessage);
        }

        public void RequestCrosspointUpdate(RouterInput input) => Router?.RequestCrosspointUpdate(this, input);
        #endregion

        #region Property: CurrentInput
        public RouterInput CurrentInput => CurrentSource as RouterInput;

        public delegate void CurrentInputChangedDelegate(RouterOutput output, RouterInput newInput);
        public event CurrentInputChangedDelegate CurrentInputChanged;

        private void currentSourceChangedHandler(ISignalDestination signalDestination, ISignalSource newSource)
        {
            CurrentInputChanged?.Invoke(this, newSource as RouterInput);
            ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(CurrentInput));
        }
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

        #region Locks
        public RouterOutputLock Lock { get; private set; }
        public RouterOutputLock Protect { get; private set; }

        private void createAllLocks()
        {
            Lock = new RouterOutputLock(this, RouterOutputLockType.Lock, LockInfo);
            Protect = new RouterOutputLock(this, RouterOutputLockType.Protect, ProtectInfo);
            AllLocks = new RouterOutputLock[] { Lock, Protect };
        }

        internal RouterOutputLock[] AllLocks { get; private set; }

        protected virtual RouterOutputLockInfo LockInfo { get; } = RouterOutputLockInfo.NotSupported;
        protected virtual RouterOutputLockInfo ProtectInfo { get; } = RouterOutputLockInfo.NotSupported;

        public RouterOutputLock GetLock(RouterOutputLockType type) => type switch
        {
            RouterOutputLockType.Lock => Lock,
            RouterOutputLockType.Protect => Protect,
            _ => null
        };
        #endregion

        #region ToString()
        public override string ToString() => string.Format("(#{0}) {1}", index, name);
        #endregion

        #region Signals
        private void registerAsSignal() => SignalRegister.Instance.Register(this);
        private void unregisterAsSignal() => SignalRegister.Instance.Unregister(this);
        #endregion

        #region Tally booleans
        private RouterOutputTallyBoolean redTallyBoolean = null;
        private RouterOutputTallyBoolean yellowTallyBoolean = null;
        private RouterOutputTallyBoolean greenTallyBoolean = null;

        private void createTallyBooleans()
        {
            redTallyBoolean = new RouterOutputTallyBoolean(this, RedTally);
            yellowTallyBoolean = new RouterOutputTallyBoolean(this, YellowTally);
            greenTallyBoolean = new RouterOutputTallyBoolean(this, GreenTally);
        }
        #endregion

    }

}
