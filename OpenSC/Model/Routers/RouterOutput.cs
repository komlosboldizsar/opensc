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

    public class RouterOutput : ISignalSourceRegistered, INotifyPropertyChanged
    {

        public RouterOutput()
        { }

        public RouterOutput(string name, Router router, int index)
        {
            this.name = name;
            this.Router = router;
            this.Index = index;
            createTallies();
            registerAsSignal();
        }

        public void Restored()
        {
            createTallies();
            registerAsSignal();
        }

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

        public Router Router { get; internal set; }

        public void RemovedFromRouter(Router router)
        {
            if (router != Router)
                return;
            unregisterAsSignal();
        }

        private int index;

        public int Index
        {
            get { return index; }
            internal set
            {
                if (value == index)
                    return;
                unregisterAsSignal();
                int oldIndex = index;
                index = value;
                registerAsSignal();
                IndexChanged?.Invoke(this, oldIndex, value);
                PropertyChanged?.Invoke(nameof(Index));
                SignalLabelChanged?.Invoke(this, getSignalLabel());
                PropertyChanged?.Invoke(nameof(ISignalSourceRegistered.SignalLabel));
            }
        }


        public delegate void IndexChangedDelegate(RouterOutput output, int oldIndex, int newIndex);
        public event IndexChangedDelegate IndexChanged;

        private RouterInput crosspoint;

        public RouterInput Crosspoint
        {
            get { return crosspoint; }
            internal set
            {

                unsubscribeCrosspointEvents();

                crosspoint = value;

                string logMessage = string.Format("Router crosspoint updated. Router ID: {0}, destination: {1}, source: {2}.",
                    Router.ID,
                    Index,
                    value.Index);
                LogDispatcher.I(Router.LOG_TAG, logMessage);

                CrosspointChanged?.Invoke(this, value);
                RouterMacroTriggers.RouterOutputSourceChanged.Call(Router, this);
                Router.NotifyCrosspointChanged(this);

                redTally.PreviousElement = value.RedTally;
                yellowTally.PreviousElement = value.YellowTally;
                greenTally.PreviousElement = value.GreenTally;

                subscribeCrosspointEvents();

                fireChangeEventsAtCrosspointChange();

            }
        }

        public delegate void CrosspointChangedDelegate(RouterOutput output, RouterInput newInput);
        public event CrosspointChangedDelegate CrosspointChanged;

        private void subscribeCrosspointEvents()
        {
            if (crosspoint == null)
                return;
            crosspoint.SourceNameChanged += crosspointSourceNameChangedHandler;
        }

        private void unsubscribeCrosspointEvents()
        {
            if (crosspoint == null)
                return;
            crosspoint.SourceNameChanged -= crosspointSourceNameChangedHandler;
        }

        private void fireChangeEventsAtCrosspointChange()
        {
            if(crosspoint == null)
            {
                SourceSignalChanged?.Invoke(this, null);
                SourceSignalNameChanged?.Invoke(this, null);
            }
            else
            {
                SourceSignalChanged?.Invoke(this, crosspoint.RegisteredSourceSignal);
                SourceSignalNameChanged?.Invoke(this, crosspoint.SourceSignalName);
            }
        }

        private void crosspointSourceNameChangedHandler(RouterInput input, string newName)
        {
            SourceSignalNameChanged?.Invoke(this, newName);
        }

        public string InputName
        {
            get => crosspoint?.Name;
        }

        #region Property: SourceSignalName
        public string SourceSignalName
        {
            get => GetSourceSignalName();
        }

        public string GetSourceSignalName(List<object> recursionChain = null)
        {
            if (crosspoint == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return "(cyclic tieline)";
            recursionChain.Add(this);
            return crosspoint.GetSourceSignalName(recursionChain);
        }

        public event SourceSignalNameChangedDelegate SourceSignalNameChanged;
        #endregion

        #region Property: RegisteredSourceSignal
        public ISignalSourceRegistered RegisteredSourceSignal
            => GetRegisteredSourceSignal();

        public ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null)
        {
            if (crosspoint == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return null;
            recursionChain.Add(this);
            return crosspoint.GetRegisteredSourceSignal(recursionChain);
        }

        public delegate void SourceSignalChangedDelegate(RouterOutput output, ISignalSourceRegistered newSignal);
        public event SourceSignalChangedDelegate SourceSignalChanged;
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

        #region Tallies
        private BidirectionalPassthroughSignalTally redTally;
        private BidirectionalPassthroughSignalTally yellowTally;
        private BidirectionalPassthroughSignalTally greenTally;

        public IBidirectionalSignalTally RedTally => redTally;
        public IBidirectionalSignalTally YellowTally => yellowTally;
        public IBidirectionalSignalTally GreenTally => greenTally;

        private void createTallies()
        {
            redTally = new BidirectionalPassthroughSignalTally(this);
            yellowTally = new BidirectionalPassthroughSignalTally(this);
            greenTally = new BidirectionalPassthroughSignalTally(this);
        }
        #endregion

    }

}
