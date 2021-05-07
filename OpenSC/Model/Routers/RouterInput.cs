using OpenSC.Model.General;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterInput : INotifyPropertyChanged, ISignalDestination, ISignalSource
    {

        public RouterInput()
        { }

        public RouterInput(string name, Router router, int index)
        {
            this.name = name;
            this.Router = router;
            this.Index = index;
        }

        public void Restored()
        {
            createTallies();
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
            }
        }

        public delegate void NameChangedDelegate(RouterInput input, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        public Router Router { get; internal set; }

        public void RemovedFromRouter(Router router)
        {
            if (router != Router)
                return;
        }

        private int index;

        public int Index
        {
            get { return index; }
            internal set { index = value; }
        }

        ISignalSource source;

        public ISignalSource CurrentSource
        {
            get { return source; }
            set
            {

                if (value == source)
                    return;
                ISignalSource oldSource = source;

                SourceChanging?.Invoke(this, oldSource, value);

                if (source != null)
                {
                    source.RegisteredSourceSignalNameChanged -= sourceSignalNameChangedHandler;
                }
                
                source = value;
                IsTieline = (source is RouterOutput);

                SourceChanged?.Invoke(this, oldSource, value);
                PropertyChanged?.Invoke(nameof(CurrentSource));

                // TODO: fire events like RegisteredSourceSignalNameChanged
                //SourceNameChanged?.Invoke(this, source?.SignalLabel);

                if (source != null)
                {
                    source.RegisteredSourceSignalNameChanged += sourceSignalNameChangedHandler;
                }

                redTally.PreviousElement = source.RedTally;
                yellowTally.PreviousElement = source.YellowTally;
                greenTally.PreviousElement = source.GreenTally;

            }
        }

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

        public void AssignSource(ISignalSource source)
        {
            CurrentSource = source as ISignalSourceRegistered;
        }

        public delegate void SourceChangingDelegate(RouterInput input, ISignalSource oldSource, ISignalSource newSource);
        public event SourceChangingDelegate SourceChanging;

        public delegate void SourceChangedDelegate(RouterInput input, ISignalSource oldSource, ISignalSource newSource);
        public event SourceChangedDelegate SourceChanged;

        // "Temp foreign key"
        public string _sourceSignalUniqueId;

        public void RestoreSource()
        {
            if (_sourceSignalUniqueId != null)
                CurrentSource = SignalRegister.Instance.GetSignalByUniqueId(_sourceSignalUniqueId);
            TielineCost = _tielineCost;
            TielineIsReserved = _tielineIsReserved;
        }

        public string RegisteredSourceSignalName
        {
            get => source?.RegisteredSourceSignalName;
        }

        public string GetRegisteredSourceSignalName(List<object> recursionChain = null)
        {
            if (source == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return "(cyclic tieline)";
            recursionChain.Add(this);
            return source.GetRegisteredSourceSignalName(recursionChain);
        }

        public delegate void RouterInputSourceNameChanged(RouterInput input, string newName);
        public event RouterInputSourceNameChanged SourceNameChanged;

        public event RegisteredSourceSignalNameChangedDelegate RegisteredSourceSignalNameChanged;
        public event RegisteredSourceSignalChangedDelegate RegisteredSourceSignalChanged;

        #region Property: RegisteredSourceSignal
        public ISignalSourceRegistered RegisteredSourceSignal
            => GetRegisteredSourceSignal();

        public ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null)
        {
            if (source == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return null;
            recursionChain.Add(this);
            return source.GetRegisteredSourceSignal(recursionChain);
        }
        #endregion

        private void sourceSignalNameChangedHandler(ISignalSource inputSource, string newName)
        {
            SourceNameChanged?.Invoke(this, newName);
        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;
        #endregion

        #region Tieline properties
        private bool isTieline;

        public bool IsTieline
        {
            get => isTieline;
            private set
            {
                if (value == isTieline)
                    return;
                isTieline = value;
                IsTielineChanged?.Invoke(this, !isTieline, isTieline);
                PropertyChanged?.Invoke(nameof(IsTieline));
            }
        }

        public delegate void IsTielineChangedDelegate(RouterInput input, bool oldValue, bool newValue);
        public event IsTielineChangedDelegate IsTielineChanged;

        // Temporal until restore
        public int _tielineCost;

        private int tielineCost;

        public int? TielineCost
        {
            get => (IsTieline) ? (int?)tielineCost : null;
            set
            {
                if (!IsTieline)
                    return;
                if (value == tielineCost)
                    return;
                int? oldValue = tielineCost;
                tielineCost = (int)value;
                TielineCostChanged?.Invoke(this, oldValue, tielineCost);
                PropertyChanged?.Invoke(nameof(TielineCost));
            }
        }

        public delegate void TielineCostChangedDelegate(RouterInput input, int? oldValue, int? newValue);
        public event TielineCostChangedDelegate TielineCostChanged;

        // Temporal until restore
        public bool _tielineIsReserved;

        private bool tielineIsReserved;

        public bool? TielineIsReserved
        {
            get => (IsTieline) ? (bool?)tielineIsReserved : null;
            set
            {
                /*if (!IsTieline)
                    return;*/
                if (value == tielineIsReserved)
                    return;
                bool? oldValue = tielineIsReserved;
                tielineIsReserved = (bool)value;
                TielineIsReservedChanged?.Invoke(this, oldValue, tielineIsReserved);
                PropertyChanged?.Invoke(nameof(TielineIsReserved));
            }
        }

        public delegate void TielineIsReservedChangedDelegate(RouterInput input, bool? oldValue, bool? newValue);
        public event TielineIsReservedChangedDelegate TielineIsReservedChanged;
        #endregion

    }

}
