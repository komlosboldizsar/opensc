using Microsoft.CodeAnalysis;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers
{

    public partial class Timer : ModelBase
    {

        #region Persistence, instantiation
        public override void Removed()
        {
            base.Removed();
            SecondsChanged = null;
            CountdownSecondsChanged = null;
            RunningStateChanged = null;
            Started = null;
            Stopped = null;
            Reseted = null;
            OperationsChanged = null;
            ModeChanged = null;
            ReachedZero = null;
            innerTimer?.Dispose();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = TimerDatabase.Instance;
        #endregion

        #region Property: Seconds, TimeSpan
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_seconds_afterChange))]
        [AutoProperty.Validator(nameof(ValidateSeconds))]
        private int seconds = 0;

        private void _seconds_afterChange(int oldValue, int newValue) => TimeSpan = TimeSpan.FromSeconds(newValue);

        public void ValidateSeconds(int seconds)
        {
            if (seconds < 0)
                throw new ArgumentException();
        }

        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_timeSpan_afterChange))]
        private TimeSpan timeSpan;

        private void _timeSpan_afterChange(TimeSpan oldValue, TimeSpan newValue) => Seconds = (int)newValue.TotalSeconds;
        #endregion

        #region Property: CountdownSeconds
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateCountdownSeconds))]
        [PersistAs("countdown_seconds")]
        private int countdownSeconds = 5;

        public void ValidateCountdownSeconds(int value)
        {
            if (value < 0)
                throw new ArgumentException();
        }
        #endregion

        #region Property: Running
        [AutoProperty(SetterAccessibility = Accessibility.Private)]
        [AutoProperty.Event("RunningStateChanged")]
        [AutoProperty.AfterChange(nameof(_running_afterChange))]
        private bool running = false;

        private void _running_afterChange(bool oldValue, bool newValue)
        {
            if (newValue)
                Started?.Invoke(this);
            else
                Stopped?.Invoke(this);
            OperationsChanged?.Invoke(this);
        } 
        #endregion

        #region Propety: Mode
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_mode_afterChange))]
        [PersistAs("mode")]
        private TimerMode mode = TimerMode.Backwards;

        private void _mode_afterChange(TimerMode oldValue, TimerMode newValue)
        {
            Running = false;
            if (newValue == TimerMode.Backwards)
                Seconds = CountdownSeconds;
            else if (newValue == TimerMode.Forwards)
                Seconds = 0;
            OperationsChanged?.Invoke(this);
        }
        #endregion

        #region State events
        public event PropertyChangedNoValueDelegate<Timer> Started;
        public event PropertyChangedNoValueDelegate<Timer> Stopped;
        public event PropertyChangedNoValueDelegate<Timer> Reseted;
        public event PropertyChangedNoValueDelegate<Timer> ReachedZero;
        #endregion

        #region Possible operations
        public event PropertyChangedNoValueDelegate<Timer> OperationsChanged;

        public bool CanStart => ((mode != TimerMode.Clock) && !Running);
        public bool CanStop => ((mode != TimerMode.Clock) && Running);
        public bool CanReset => (mode != TimerMode.Clock);
        #endregion

        #region Operations
        public void Start()
        {
            Running = true;
            resetInnerTimer();
        }

        public void Stop() => Running = false;

        public void Reset()
        {
            switch (mode)
            {
                case TimerMode.Forwards:
                    Seconds = 0;
                    break;
                case TimerMode.Backwards:
                    Seconds = CountdownSeconds;
                    break;
                case TimerMode.Clock:
                    // Do nothing
                    break;
            }
            firstReachedZeroEvent = true;
            Reseted?.Invoke(this);
            resetInnerTimer();
        }
        #endregion

        #region Timer
        private System.Timers.Timer innerTimer;

        public Timer()
        {
            innerTimer = new System.Timers.Timer(1000);
            innerTimer.Elapsed += innerTimerTick;
            innerTimer.AutoReset = true;
            innerTimer.Enabled = true;
            Reset();
        }

        bool firstReachedZeroEvent = true;

        private void innerTimerTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (mode)
            {
                case TimerMode.Forwards:
                    if (!running)
                        return;
                    Seconds++;
                    break;
                case TimerMode.Backwards:
                    if (!running)
                        return;
                    if (Seconds > 0)
                        Seconds--;
                    if ((Seconds == 0) && firstReachedZeroEvent)
                    {
                        firstReachedZeroEvent = false;
                        ReachedZero?.Invoke(this);
                    }
                    break;
                case TimerMode.Clock:
                    Seconds = (int)DateTime.Now.TimeOfDay.TotalSeconds;
                    break;
            }

        }

        private void resetInnerTimer()
        {
            innerTimer.Stop();
            innerTimer.Start();
        }
        #endregion

    }
}
