using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OpenSC.Model.Signals
{

    public class ExternalSignal : ModelBase, ISignalSourceRegistered
    {

        public ExternalSignal()
        {
            createTallies();
        }

        public override void Restored()
        {
            updateTallyBooleans();
        }

        public override void Removed()
        {

            base.Removed();

            Name = null;
            IdChanged = null;
            NameChanged = null;
            CategoryChanged = null;
            SourceSignalNameChanged = null;
            SignalLabelChanged = null;

            unregisterTallyBooleans();

        }

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
                SignalLabelChanged?.Invoke(this, getSignalLabel());
                RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));

                createTallyBooleansAfterIdChange();

            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!ExternalSignalDatabases.Signals.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

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
                SignalLabelChanged?.Invoke(this, getSignalLabel());
                RaisePropertyChanged(nameof(ISignalSourceRegistered.SignalLabel));
                SourceSignalNameChanged?.Invoke(this, value);
                RaisePropertyChanged(nameof(SourceSignalName));

            }
        }


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

        #region Property: SourceSignalName
        public string SourceSignalName
        {
            get => name;
        }

        public string GetSourceSignalName(List<object> recursionChain = null)
            => name;

        public event SourceSignalNameChangedDelegate SourceSignalNameChanged;
        #endregion

        #region Property: RegisteredSourceSignal
        public ISignalSourceRegistered RegisteredSourceSignal
        {
            get => this;
        }

        public ISignalSourceRegistered GetRegisteredSourceSignal(List<object> recursionChain = null)
            => this;
        #endregion

        string ISignalSourceRegistered.SignalLabel
        {
            get => getSignalLabel();
        }

        private string getSignalLabel()
            => string.Format("(EXT. #{0}) {1}", ID, Name);

        string ISignalSourceRegistered.SignalUniqueId
        {
            get => string.Format("external.{0}", ID);
        }

        public event SignalLabelChangedDelegate SignalLabelChanged;

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
        }
        #endregion

        #region Tally booleans
        private TallyBoolean redTallyBoolean = null;
        private TallyBoolean yellowTallyBoolean = null;
        private TallyBoolean greenTallyBoolean = null;

        private void createBooleans()
        {
            redTallyBoolean = new TallyBoolean(this, redTally, SignalTallyColor.Red);
            yellowTallyBoolean = new TallyBoolean(this, yellowTally, SignalTallyColor.Yellow);
            greenTallyBoolean = new TallyBoolean(this, greenTally, SignalTallyColor.Green);
            BooleanRegister.Instance.RegisterBoolean(redTallyBoolean);
            BooleanRegister.Instance.RegisterBoolean(yellowTallyBoolean);
            BooleanRegister.Instance.RegisterBoolean(greenTallyBoolean);
        }

        private void createTallyBooleansAfterIdChange()
        {
            if ((redTallyBoolean == null) || (yellowTallyBoolean == null) || (greenTallyBoolean == null))
                createBooleans();
        }

        private void updateTallyBooleans()
        {
            redTallyBoolean?.Update();
            yellowTallyBoolean?.Update();
            greenTallyBoolean?.Update();
        }

        private void unregisterTallyBooleans()
        {
            if(redTallyBoolean != null)
                BooleanRegister.Instance.UnregisterBoolean(redTallyBoolean);
            if (yellowTallyBoolean != null)
                BooleanRegister.Instance.UnregisterBoolean(greenTallyBoolean);
            if (greenTallyBoolean != null)
                BooleanRegister.Instance.UnregisterBoolean(greenTallyBoolean);
        }

        private class TallyBoolean : BooleanBase
        {

            private ExternalSignal signal;

            private ExternalSignalTally tally;

            private SignalTallyColor color;

            public TallyBoolean(ExternalSignal signal, ExternalSignalTally tally, SignalTallyColor color) :
                base(getName(signal, color), getColor(color), getDescription(signal, color))
            {
                this.signal = signal;
                this.tally = tally;
                this.color = color;
                signal.IdChanged += idChangedHandler;
                signal.NameChanged += nameChangedHandler;
                tally.StateChanged += tallyChangedHandler;
            }

            private void tallyChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState)
            {
                CurrentState = newState;
            }

            public void Update()
            {
                Name = getName(signal, color);
                Description = getDescription(signal, color);
            }

            private void idChangedHandler(ExternalSignal signal, int oldValue, int newValue)
            {
                Name = getName(signal, color);
                Description = getDescription(signal, color);
            }

            private void nameChangedHandler(ExternalSignal signal, string oldName, string newName)
            {
                Description = getDescription(signal, color);
            }

            private static string getName(ExternalSignal signal, SignalTallyColor color)
                =>  string.Format("signal.{0}.{1}tally", signal.ID, getColorString(color));

            private static Color getColor(SignalTallyColor color)
            {
                switch (color)
                {
                    case SignalTallyColor.Red:
                        return Color.Red;
                    case SignalTallyColor.Yellow:
                        return Color.Yellow;
                    case SignalTallyColor.Green:
                        return Color.Green;
                }
                return Color.White;
            }

            private static string getDescription(ExternalSignal signal, SignalTallyColor color)
                => string.Format("Signal [(#{0}) {1}] has {2} tally.", signal.ID, signal.Name, getColorString(color));

            private static string getColorString(SignalTallyColor color)
            {
                switch (color)
                {
                    case SignalTallyColor.Red:
                        return "red";
                    case SignalTallyColor.Yellow:
                        return "yellow";
                    case SignalTallyColor.Green:
                        return "green";
                }
                return "unknown";
            }

        }
        #endregion

        protected override void afterUpdate()
        {
            base.afterUpdate();
            ExternalSignalDatabases.Signals.ItemUpdated(this);
        }

    }

}
