using OpenSC.Model.Persistence;
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
            CategoryChanged = null;
            RegisteredSourceSignalNameChanged = null;
            SignalLabelChanged = null;
        }
        #endregion

        #region ID validation and change
        protected override void validateIdForDatabase(int id)
        {
            if (!ExternalSignalDatabases.Signals.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        protected override void afterIdChange()
        {
            SignalLabelChanged?.Invoke(this, SignalLabel);
            RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));
            SignalUniqueIdChanged?.Invoke(this, SignalUniqueId);
            RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalUniqueId));
        }
        #endregion

        #region Name change
        protected override void afterNameChange()
        {
            base.afterNameChange();
            SignalLabelChanged?.Invoke(this, SignalLabel);
            RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));
            List<object> recursionChain = new List<object>();
            recursionChain.Add(this);
            RegisteredSourceSignalNameChanged?.Invoke(this, Name, recursionChain);
            RaisePropertyChanged(nameof(RegisteredSourceSignalName));
        }
        #endregion

        #region Property: Category
        public event PropertyChangedTwoValuesDelegate<ExternalSignal, ExternalSignalCategory> CategoryChanged;

        [PersistAs("category")]
        private ExternalSignalCategory category;

#pragma warning disable CS0169
        [TempForeignKey(ExternalSignalDatabases.DBNAME_CATEGORIES, nameof(category))]
        private int _category;
#pragma warning restore CS0169

        public ExternalSignalCategory Category
        {
            get => category;
            set => setProperty(this, ref category, value, CategoryChanged);
        }
        #endregion

        #region Property: RegisteredSourceSignalName
        public string RegisteredSourceSignalName => Name;
        public string GetRegisteredSourceSignalName(List<object> recursionChain = null) => Name;
        public event RegisteredSourceSignalNameChangedDelegate RegisteredSourceSignalNameChanged;
        #endregion

        #region Property: RegisteredSourceSignal
#pragma warning disable CS0067 // The event is never used
        public event RegisteredSourceSignalChangedDelegate RegisteredSourceSignalChanged;
#pragma warning restore CS0067
        public ISignalSourceRegistered RegisteredSourceSignal => this;
        public ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null) => this;
        #endregion

        #region Property: SignalLabel
        public event PropertyChangedOneValueDelegate<ISignalSourceRegistered, string> SignalLabelChanged;
        public string SignalLabel => string.Format("(EXT. #{0}) {1}", ID, Name);            
        #endregion

        #region Property: SignalUniqueId
        public event PropertyChangedOneValueDelegate<ISignalSourceRegistered, string> SignalUniqueIdChanged;
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
