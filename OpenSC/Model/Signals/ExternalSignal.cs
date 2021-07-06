using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OpenSC.Model.Signals
{

    public class ExternalSignal : ModelBase, ISignalSourceRegistered
    {

        #region Persistence, instantiation
        public ExternalSignal()
        {
            createTallies();
        }

        public override void Removed()
        {
            base.Removed();
            Name = null;
            IdChanged = null;
            NameChanged = null;
            CategoryChanged = null;
            RegisteredSourceSignalNameChanged = null;
            SignalLabelChanged = null;
        }
        #endregion

        #region Property: ID
        public delegate void IdChangedDelegate(ExternalSignal signal, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                int oldValue = id;
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ID));
                SignalLabelChanged?.Invoke(this, SignalLabel);
                RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!ExternalSignalDatabases.Signals.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(ExternalSignal signal, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == name)
                    return;
                string oldName = name;
                name = value;

                NameChanged?.Invoke(this, oldName, value);
                RaisePropertyChanged(nameof(Name));
                SignalLabelChanged?.Invoke(this, SignalLabel);
                RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));
                List<object> recursionChain = new List<object>();
                recursionChain.Add(this);
                RegisteredSourceSignalNameChanged?.Invoke(this, value, recursionChain);
                RaisePropertyChanged(nameof(RegisteredSourceSignalName));

            }
        }
        #endregion

        #region Property: Category
        public delegate void CategoryChangedDelegate(ExternalSignal signal, ExternalSignalCategory oldCategory, ExternalSignalCategory newCategory);
        public event CategoryChangedDelegate CategoryChanged;

        [PersistAs("category")]
        private ExternalSignalCategory category;

#pragma warning disable CS0169
        [TempForeignKey(ExternalSignalDatabases.DBNAME_CATEGORIES, nameof(category))]
        private int _category;
#pragma warning restore CS0169

        public ExternalSignalCategory Category
        {
            get { return category; }
            set
            {
                if (value == category)
                    return;
                ExternalSignalCategory oldCategory = category;
                category = value;
                CategoryChanged?.Invoke(this, oldCategory, value);
                RaisePropertyChanged(nameof(Category));
            }
        }
        #endregion

        #region Property: RegisteredSourceSignalName
        public string RegisteredSourceSignalName => name;
        public string GetRegisteredSourceSignalName(List<object> recursionChain = null) => name;
        public event RegisteredSourceSignalNameChangedDelegate RegisteredSourceSignalNameChanged;
        #endregion

        #region Property: RegisteredSourceSignal
        public event RegisteredSourceSignalChangedDelegate RegisteredSourceSignalChanged;
        public ISignalSourceRegistered RegisteredSourceSignal => this;
        public ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null) => this;
        #endregion

        #region Property: SignalLabel
        public event SignalLabelChangedDelegate SignalLabelChanged;
        public string SignalLabel => string.Format("(EXT. #{0}) {1}", ID, Name);            
        #endregion

        #region Property: SignalUniqueId
        public event SignalUniqueIdChangedDelegate SignalUniqueIdChanged;
        public string SignalUniqueId => string.Format("external.{0}", ID);
        #endregion

        #region Tallies
        private ExternalSignalTally redTally;
        private ExternalSignalTally yellowTally;
        private ExternalSignalTally greenTally;

        public IBidirectionalSignalTally RedTally => redTally;
        public IBidirectionalSignalTally YellowTally => yellowTally;
        public IBidirectionalSignalTally GreenTally => greenTally;

        private void createTallies()
        {
            redTally = new ExternalSignalTally(this);
            yellowTally = new ExternalSignalTally(this);
            greenTally = new ExternalSignalTally(this);
            createTallyBooleans();
        }
        #endregion

        #region Tally booleans
        private ExternalSignalTallyBoolean redTallyBoolean = null;
        private ExternalSignalTallyBoolean yellowTallyBoolean = null;
        private ExternalSignalTallyBoolean greenTallyBoolean = null;

        private void createTallyBooleans()
        {
            redTallyBoolean = new ExternalSignalTallyBoolean(this, redTally, SignalTallyColor.Red);
            yellowTallyBoolean = new ExternalSignalTallyBoolean(this, yellowTally, SignalTallyColor.Yellow);
            greenTallyBoolean = new ExternalSignalTallyBoolean(this, greenTally, SignalTallyColor.Green);
        }
        #endregion

    }

}
