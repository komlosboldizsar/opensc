using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{

    public class Vtr : ModelBase
    {

        #region Persistence, instantiation
        public override void Removed()
        {
            base.Removed();
            IdChanged = null;
            NameChanged = null;
            TitleChanged = null;
            StateChanged = null;
            SecondsFullChanged = null;
            SecondsElapsedChanged = null;
            SecondsRemainingChanged = null;
        }
        #endregion

        #region Property: ID
        public delegate void IdChangedDelegate(Vtr vtr, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

        public override int ID
        {
            get => id;
            set
            {
                ValidateId(value);
                int oldValue = id;
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ID));
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!VtrDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public delegate void NameChangedDelegate(Vtr vtr, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                RaisePropertyChanged(nameof(Name));
            }
        }
        #endregion

        #region Property: Title
        public delegate void TitleChangedDelegate(Vtr vtr, string oldTitle, string newTitle);
        public event TitleChangedDelegate TitleChanged;

        private string title;

        public string Title
        {
            get => title;
            protected set
            {
                if (value == title)
                    return;
                string oldTitle = title;
                title = value;
                TitleChanged?.Invoke(this, oldTitle, value);
                RaisePropertyChanged(nameof(Title));
            }
        }
        #endregion

        #region Property: State
        public delegate void StateChangedDelegate(Vtr vtr, VtrState oldState, VtrState newState);
        public event StateChangedDelegate StateChanged;

        private VtrState state = VtrState.Stopped;

        public VtrState State
        {
            get => state;
            protected set
            {
                if (value == state)
                    return;
                VtrState oldState = state;
                state = value;
                StateChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(State));
            }
        }
        #endregion

        #region Property: SecondsFull, TimeFull
        public delegate void SecondsFullChangedDelegate(Vtr vtr, int oldValue, int newValue);
        public event SecondsFullChangedDelegate SecondsFullChanged;

        private int secondsFull;

        public int SecondsFull
        {
            get => secondsFull;
            protected set
            {
                if (value == secondsFull)
                    return;
                int oldValue = secondsFull;
                secondsFull = value;
                SecondsFullChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(SecondsFull));
                RaisePropertyChanged(nameof(TimeFull));
            }
        }

        public TimeSpan TimeFull => TimeSpan.FromSeconds(secondsFull);
        #endregion

        #region Property: SecondsElapsed, TimeElapsed
        public delegate void SecondsElapsedChangedDelegate(Vtr vtr, int oldValue, int newValue);
        public event SecondsElapsedChangedDelegate SecondsElapsedChanged;

        private int secondsElapsed;

        public int SecondsElapsed
        {
            get => secondsElapsed;
            protected set
            {
                if (value == secondsElapsed)
                    return;
                int oldValue = secondsElapsed;
                secondsElapsed = value;
                SecondsElapsedChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(SecondsElapsed));
                RaisePropertyChanged(nameof(TimeElapsed));
            }
        }

        public TimeSpan TimeElapsed => TimeSpan.FromSeconds(secondsElapsed);
        #endregion

        #region Property: SecondsRemaining, TimeRemaining
        public delegate void SecondsRemainingChangedDelegate(Vtr vtr, int oldValue, int newValue);
        public event SecondsRemainingChangedDelegate SecondsRemainingChanged;

        private int secondsRemaining;

        public int SecondsRemaining
        {
            get => secondsRemaining;
            protected set
            {
                if (value == secondsRemaining)
                    return;
                int oldValue = secondsRemaining;
                secondsRemaining = value;
                SecondsRemainingChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(SecondsRemaining));
                RaisePropertyChanged(nameof(TimeRemaining));
            }
        }

        public TimeSpan TimeRemaining => TimeSpan.FromSeconds(secondsRemaining);
        #endregion

    }

}
