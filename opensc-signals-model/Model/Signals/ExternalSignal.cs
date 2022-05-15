using OpenSC.Model.General;
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

        #region ID change
        protected override void afterIdChange()
        {
            SignalLabelChanged?.Invoke(this, SignalLabel);
            RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));
            SignalUniqueIdChanged?.Invoke(this, SignalUniqueId);
            RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalUniqueId));
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = ExternalSignalDatabases.Signals;
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
        [TempForeignKey(nameof(category))]
        private string _category;
#pragma warning restore CS0169

        public ExternalSignalCategory Category
        {
            get => category;
            set => this.setProperty(ref category, value, CategoryChanged);
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
            redTally = new ExternalSignalTally(this, SignalTallyColor.Red);
            yellowTally = new ExternalSignalTally(this, SignalTallyColor.Yellow);
            greenTally = new ExternalSignalTally(this, SignalTallyColor.Green);
            createTallyBooleans();
        }
        #endregion

        #region Tally booleans
        private ExternalSignalTallyBoolean redTallyBoolean = null;
        private ExternalSignalTallyBoolean yellowTallyBoolean = null;
        private ExternalSignalTallyBoolean greenTallyBoolean = null;

        private void createTallyBooleans()
        {
            redTallyBoolean = new ExternalSignalTallyBoolean(redTally);
            yellowTallyBoolean = new ExternalSignalTallyBoolean(yellowTally);
            greenTallyBoolean = new ExternalSignalTallyBoolean(greenTally);
        }
        #endregion

    }

}
