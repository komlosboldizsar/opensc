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
        }

        public virtual void TotallyRestored()
        { }

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

        public event SignalLabelChangedDelegate SignalLabelChanged;
        #endregion

        #region Property: SignalUniqueId
        public string SignalUniqueId
            => string.Format("router.{0}.output.{1}", Router.ID, (Index + 1));

        public event SignalUniqueIdChangedDelegate SignalUniqueIdChanged;
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

    }

}
