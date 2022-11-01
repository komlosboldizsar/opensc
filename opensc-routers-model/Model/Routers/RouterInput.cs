using Microsoft.CodeAnalysis;
using OpenSC.Model.General;
using OpenSC.Model.Signals;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public partial class RouterInput : SignalForwarder, ISystemObject, IComponent<Router, RouterInput, RouterInputCollection>
    {

        public RouterInput() : base()
            => SystemObjectRegister.Instance.Register(this);

        public void Removed()
        {
            deassignParent();
            NameChanged = null;
            IndexChanged = null;
            IsTielineChanged = null;
            TielineCostChanged = null;
            TielineIsReservedChanged = null;
        }

        #region Property: GlobalID
        public override event PropertyChangedTwoValuesDelegate<ISystemObject, string> GlobalIdChanged;

        private string globalId;
        public override string GlobalID  => globalId;
        private void updateGlobalId(string value) => this.setProperty(ref globalId, value, GlobalIdChanged);

        private void generateGlobalId()
        {
            if (Parent == null)
            {
                updateGlobalId(null);
                return;
            }
            updateGlobalId($"{Parent.GlobalID}.input.{Index}");
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
                Parent?.NotifyLocalInputNameChanged(this);
            }
        }

        public delegate void NameChangedDelegate(RouterInput input, string oldName, string newName);
        public event NameChangedDelegate NameChanged;
        #endregion

        #region Property: Router
        public Router Parent { get; private set; }
        private RouterInputCollection parentCollection;

        public void AssignParent(Router router, RouterInputCollection parentCollection)
        {
            if (Parent != null)
                return;
            Parent = router;
            this.parentCollection = parentCollection;
            Parent.GlobalIdChanged += Parent_GlobalIdChanged;
            generateGlobalId();
        }

        public void deassignParent()
        {
            if (Parent != null)
                Parent.GlobalIdChanged -= Parent_GlobalIdChanged;
            Parent = null;
            parentCollection = null;
            Removed();
        }

        private void Parent_GlobalIdChanged(ISystemObject item, string oldValue, string newValue)
            => generateGlobalId();
        #endregion

        #region Property: Index
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_index_beforeChange))]
        [AutoProperty.AfterChange(nameof(generateGlobalId))]
        private int index;

        private void _index_beforeChange(int newValue)
            => parentCollection?.CheckKey(this, newValue);
        #endregion

        #region Property: IsTieline
        [AutoProperty(SetterAccessibility = Accessibility.Private)]
        private bool isTieline;
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
