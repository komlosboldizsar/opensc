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
        public event PropertyChangedTwoValuesDelegate<Vtr, int> IdChanged;

        public int id = 0;

        public override int ID
        {
            get => id;
            set => setProperty(this, ref id, value, IdChanged, validator: ValidateId);
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
        public event PropertyChangedTwoValuesDelegate<Vtr, string> NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged);
        }
        #endregion

        #region Property: Title
        public event PropertyChangedTwoValuesDelegate<Vtr, string> TitleChanged;

        private string title;

        public string Title
        {
            get => title;
            protected set => setProperty(this, ref title, value, TitleChanged);
        }
        #endregion

        #region Property: State
        public event PropertyChangedTwoValuesDelegate<Vtr, VtrState> StateChanged;

        private VtrState state = VtrState.Stopped;

        public VtrState State
        {
            get => state;
            protected set => setProperty(this, ref state, value, StateChanged);
        }
        #endregion

        #region Property: SecondsFull, TimeFull
        public event PropertyChangedTwoValuesDelegate<Vtr, int> SecondsFullChanged;

        private int secondsFull;

        public int SecondsFull
        {
            get => secondsFull;
            protected set
            {
                if (!setProperty(this, ref secondsFull, value, SecondsFullChanged))
                    return;
                RaisePropertyChanged(nameof(TimeFull));
            }
        }

        public TimeSpan TimeFull => TimeSpan.FromSeconds(secondsFull);
        #endregion

        #region Property: SecondsElapsed, TimeElapsed
        public event PropertyChangedTwoValuesDelegate<Vtr, int> SecondsElapsedChanged;

        private int secondsElapsed;

        public int SecondsElapsed
        {
            get => secondsElapsed;
            protected set
            {
                if (!setProperty(this, ref secondsElapsed, value, SecondsElapsedChanged))
                    return;
                RaisePropertyChanged(nameof(TimeElapsed));
            }
        }

        public TimeSpan TimeElapsed => TimeSpan.FromSeconds(secondsElapsed);
        #endregion

        #region Property: SecondsRemaining, TimeRemaining
        public event PropertyChangedTwoValuesDelegate<Vtr, int> SecondsRemainingChanged;

        private int secondsRemaining;

        public int SecondsRemaining
        {
            get => secondsRemaining;
            protected set
            {
                if (!setProperty(this, ref secondsRemaining, value, SecondsRemainingChanged))
                    return;
                RaisePropertyChanged(nameof(TimeRemaining));
            }
        }

        public TimeSpan TimeRemaining => TimeSpan.FromSeconds(secondsRemaining);
        #endregion

    }

}
