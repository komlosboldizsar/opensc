using OpenSC.Model.General;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterInput : SignalForwarder, ISystemObject
    {

        public RouterInput() : base() => SystemObjectRegister.Instance.Register(this);

        public RouterInput(string name, Router router, int index) : base()
        {
            this.name = name;
            this.Router = router;
            this.Index = index;
            SystemObjectRegister.Instance.Register(this);
            router.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
            generateGlobalId();
        }

        public void Restored()
        { }

        public virtual void TotallyRestored()
        { }

        #region Property: GlobalID
        public override event PropertyChangedTwoValuesDelegate<ISystemObject, string> GlobalIdChanged;

        private string globalId;
        public override string GlobalID  => globalId;
        private void updateGlobalId(string value) => this.setProperty(ref globalId, value, GlobalIdChanged);

        private void generateGlobalId()
        {
            if (Router == null)
            {
                updateGlobalId(null);
                return;
            }
            updateGlobalId($"{Router.GlobalID}.input.{Index}");
        }
        #endregion

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
                ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(Name));
            }
        }

        public delegate void NameChangedDelegate(RouterInput input, string oldName, string newName);
        public event NameChangedDelegate NameChanged;
        #endregion

        #region Property: Router
        public Router Router { get; private set; }

        internal void AssignParentRouter(Router router)
        {
            if (Router != null)
                return;
            Router = router;
            router.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
            generateGlobalId();
        }

        public void RemovedFromRouter(Router router)
        {
            if (router != Router)
                return;
            Router = null;
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
                ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(Index));
                generateGlobalId();
            }
        }

        public delegate void IndexChangedDelegate(RouterInput input, int oldIndex, int newIndex);
        public event IndexChangedDelegate IndexChanged;
        #endregion

        #region Property: IsTieline
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
                ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(IsTieline));
            }
        }

        public delegate void IsTielineChangedDelegate(RouterInput input, bool oldValue, bool newValue);
        public event IsTielineChangedDelegate IsTielineChanged;
        #endregion

        #region Property: TielineCost
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
                ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(TielineCost));
            }
        }

        public delegate void TielineCostChangedDelegate(RouterInput input, int? oldValue, int? newValue);
        public event TielineCostChangedDelegate TielineCostChanged;
        #endregion

        #region Property: TielineIsReserved
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
                ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(TielineIsReserved));
            }
        }

        public delegate void TielineIsReservedChangedDelegate(RouterInput input, bool? oldValue, bool? newValue);
        public event TielineIsReservedChangedDelegate TielineIsReservedChanged;
        #endregion

        #region Source restoration
        // "Temp foreign key"
        public string _sourceUniqueId;

        public void RestoreSource()
        {
            if (_sourceUniqueId != null)
                AssignSource(SignalRegister.Instance[_sourceUniqueId]);
            TielineCost = _tielineCost;
            TielineIsReserved = _tielineIsReserved;
        }
        #endregion

        #region ToString()
        public override string ToString() => string.Format("(#{0}) {1}", index, name);
        #endregion

    }

}
