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

        public override void Restored()
        { }

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

        public delegate void IdChangedDelegate(Vtr vtr, int oldValue, int newValue);
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
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!VtrDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public delegate void NameChangedDelegate(Vtr vtr, string oldName, string newName);
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
            }
        }

        public delegate void TitleChangedDelegate(Vtr vtr, string oldTitle, string newTitle);
        public event TitleChangedDelegate TitleChanged;

        private string title;

        public string Title
        {
            get { return title; }
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

        public delegate void StateChangedDelegate(Vtr vtr, VtrState oldState, VtrState newState);
        public event StateChangedDelegate StateChanged;

        private VtrState state = VtrState.Stopped;

        public VtrState State
        {
            get { return state; }
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

        public delegate void SecondsFullChangedDelegate(Vtr vtr, int oldValue, int newValue);
        public event SecondsFullChangedDelegate SecondsFullChanged;

        private int secondsFull;

        public int SecondsFull
        {
            get { return secondsFull; }
            protected set
            {
                if (value == secondsFull)
                    return;
                int oldValue = secondsFull;
                secondsFull = value;
                SecondsFullChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(SecondsFull));
            }
        }

        public TimeSpan TimeFull
        {
            get { return TimeSpan.FromSeconds(secondsFull); }
        }

        public delegate void SecondsElapsedChangedDelegate(Vtr vtr, int oldValue, int newValue);
        public event SecondsElapsedChangedDelegate SecondsElapsedChanged;

        private int secondsElapsed;

        public int SecondsElapsed
        {
            get { return secondsElapsed; }
            protected set
            {
                if (value == secondsElapsed)
                    return;
                int oldValue = secondsElapsed;
                secondsElapsed = value;
                SecondsElapsedChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(SecondsElapsed));
            }
        }

        public TimeSpan TimeElapsed
        {
            get { return TimeSpan.FromSeconds(secondsElapsed); }
        }
        
        public delegate void SecondsRemainingChangedDelegate(Vtr vtr, int oldValue, int newValue);
        public event SecondsRemainingChangedDelegate SecondsRemainingChanged;

        private int secondsRemaining;

        public int SecondsRemaining
        {
            get { return secondsRemaining; }
            protected set
            {
                if (value == secondsRemaining)
                    return;
                int oldValue = secondsRemaining;
                secondsRemaining = value;
                SecondsRemainingChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(SecondsRemaining));
            }
        }

        public TimeSpan TimeRemaining
        {
            get { return TimeSpan.FromSeconds(secondsRemaining); }
        }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            VtrDatabase.Instance.ItemUpdated(this);
        }

    }
}
