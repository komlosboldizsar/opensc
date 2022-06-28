using Microsoft.CodeAnalysis;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs
{

    public partial class Vtr : ModelBase
    {

        #region Persistence, instantiation
        public override void Removed()
        {
            base.Removed();
            TitleChanged = null;
            StateChanged = null;
            SecondsFullChanged = null;
            SecondsElapsedChanged = null;
            SecondsRemainingChanged = null;
        }

        public Vtr()
        {
            resetStateAndData();
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            resetStateAndData();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = VtrDatabase.Instance;
        #endregion

        #region Property: Title
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        private string title;
        #endregion

        #region Property: State
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        private VtrState state = VtrState.Unknown;
        #endregion

        #region Property: SecondsFull, TimeFull
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_secondsFull_afterChange))]
        private int secondsFull;

        private void _secondsFull_afterChange(int oldValue, int newValue)
            => TimeFull = TimeSpan.FromSeconds(newValue);

        [AutoProperty]
        private TimeSpan timeFull;
        #endregion

        #region Property: SecondsElapsed, TimeElapsed
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_secondsElapsed_afterChange))]
        private int secondsElapsed;

        private void _secondsElapsed_afterChange(int oldValue, int newValue)
            => TimeElapsed = TimeSpan.FromSeconds(newValue);

        [AutoProperty]
        private TimeSpan timeElapsed;
        #endregion

        #region Property: SecondsRemaining, TimeRemaining
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_secondsRemaining_afterChange))]
        private int secondsRemaining;

        private void _secondsRemaining_afterChange(int oldValue, int newValue)
            => TimeRemaining = TimeSpan.FromSeconds(newValue);

        [AutoProperty]
        private TimeSpan timeRemaining;
        #endregion

        protected void resetStateAndData(VtrState baseState = VtrState.Unknown)
        {
            Title = null;
            State = baseState;
            SecondsFull = 0;
            SecondsElapsed = 0;
            SecondsRemaining = 0;
        }

    }

}
