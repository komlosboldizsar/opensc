using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OpenSC.Model.Signals
{

    public class ExternalSignal : ModelBase, ISignal
    {

        public override void Restored()
        {
            updateTallyBooleans();
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
                RaisePropertyChanged(nameof(ISignal.SignalLabel));

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
                RaisePropertyChanged(nameof(ISignal.SignalLabel));

            }
        }


        public delegate void CategoryChangedDelegate(ExternalSignal signal, ExternalSignalCategory oldCategory, ExternalSignalCategory newCategory);
        public event CategoryChangedDelegate CategoryChanged;

        [PersistAs("category")]
        private ExternalSignalCategory category;

        [TempForeignKey(ExternalSignalDatabases.DBNAME_CATEGORIES, nameof(category))]
        private int _category;

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

        string ISignal.SignalLabel
        {
            get => getSignalLabel();
        }

        private string getSignalLabel()
            => string.Format("(EXT. #{0}) #1", ID, Name);

        public event SignalLabelChangedDelegate SignalLabelChanged;

        #region Tallies
        public event SignalTallyChangedDelegate RedTallyChanged;

        private bool redTally;

        public bool RedTally
        {
            get { return redTally; }
            private set
            {
                if (value == redTally)
                    return;
                bool oldState = redTally;
                redTally = value;
                RedTallyChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(RedTally));
            }
        }
        
        public event SignalTallyChangedDelegate GreenTallyChanged;

        private bool greenTally;

        public bool GreenTally
        {
            get { return greenTally; }
            private set
            {
                if (value == greenTally)
                    return;
                bool oldState = greenTally;
                greenTally = value;
                GreenTallyChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(GreenTally));
            }
        }
        #endregion

        #region Tally sources
        private List<ISignalTallySource> redTallySources = new List<ISignalTallySource>();
        private List<ISignalTallySource> greenTallySources = new List<ISignalTallySource>();

        public void IsTalliedFrom(ISignalTallySource source, SignalTallyType type, bool isTallied)
        {

            List<ISignalTallySource> tallySourceList = getTallySourceListByType(type);
            if (tallySourceList == null)
                return;

            lock (tallySourceList)
            {
                if (isTallied && !tallySourceList.Contains(source))
                    tallySourceList.Add(source);
                if (!isTallied && tallySourceList.Contains(source))
                    tallySourceList.Remove(source);
            }

            setTallyValueByType(type, (tallySourceList.Count > 0));

        }

        private List<ISignalTallySource> getTallySourceListByType(SignalTallyType type)
        {
            switch (type)
            {
                case SignalTallyType.Red:
                    return redTallySources;
                case SignalTallyType.Green:
                    return greenTallySources;
            }
            return null;
        }

        private void setTallyValueByType(SignalTallyType type, bool isTallied)
        {
            switch (type)
            {
                case SignalTallyType.Red:
                    RedTally = isTallied;
                    break;
                case SignalTallyType.Green:
                    GreenTally = isTallied;
                    break;
            }
        }
        #endregion

        #region Tally booleans
        private IBoolean redTallyBoolean = null;
        private IBoolean greenTallyBoolean = null;

        private void createBooleans()
        {
            redTallyBoolean = new TallyBoolean(this, TallyBoolean.TallyColor.Red);
            greenTallyBoolean = new TallyBoolean(this, TallyBoolean.TallyColor.Green);
            BooleanRegister.Instance.RegisterBoolean(redTallyBoolean);
            BooleanRegister.Instance.RegisterBoolean(greenTallyBoolean);
        }

        private void createTallyBooleansAfterIdChange()
        {
            if ((redTallyBoolean == null) || (greenTallyBoolean == null))
                createBooleans();
        }

        private void updateTallyBooleans()
        {
            (redTallyBoolean as TallyBoolean)?.Update();
            (greenTallyBoolean as TallyBoolean)?.Update();
        }

        private class TallyBoolean : BooleanBase
        {

            private ExternalSignal signal;

            private TallyColor color;

            public TallyBoolean(ExternalSignal signal, TallyColor color):
                base(getName(signal, color), getColor(color), getDescription(signal, color))
            {
                this.signal = signal;
                this.color = color;
                signal.IdChanged += idChangedHandler;
                signal.NameChanged += nameChangedHandler;
                switch (color)
                {
                    case TallyColor.Red:
                        CurrentState = signal.RedTally;
                        signal.RedTallyChanged += tallyChangedHandler;
                        break;
                    case TallyColor.Green:
                        CurrentState = signal.GreenTally;
                        signal.GreenTallyChanged += tallyChangedHandler;
                        break;
                }
            }

            public void Update()
            {
                Name = getName(signal, color);
                Description = getDescription(signal, color);
            }

            private void tallyChangedHandler(ISignal signal, bool oldState, bool newState)
            {
                CurrentState = newState;
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

            private static string getName(ExternalSignal signal, TallyColor color)
                =>  string.Format("signal.{0}.{1}tally", signal.ID, getColorString(color));

            private static Color getColor(TallyColor color)
            {
                switch (color)
                {
                    case TallyColor.Red:
                        return Color.Red;
                    case TallyColor.Green:
                        return Color.Green;
                }
                return Color.White;
            }

            private static string getDescription(ExternalSignal signal, TallyColor color)
                => string.Format("Signal [(#{0}) {1}] has {2} tally.", signal.ID, signal.Name, getColorString(color));

            private static string getColorString(TallyColor color)
            {
                switch (color)
                {
                    case TallyColor.Red:
                        return "red";
                    case TallyColor.Green:
                        return "green";
                }
                return "unknown";
            }

            public enum TallyColor
            {
                Red,
                Green
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
