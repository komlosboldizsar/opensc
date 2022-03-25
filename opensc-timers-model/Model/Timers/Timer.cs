using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers
{

    public class Timer : ModelBase
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
        public event PropertyChangedTwoValuesDelegate<Timer, int> SecondsChanged;

        private int seconds = 0;
        public int Seconds
        {
            get => seconds;
            set
            {
                if (!this.setProperty(ref seconds, value, SecondsChanged, validator: ValidateSeconds))
                    return;
                RaisePropertyChanged(nameof(TimeSpan));
            }
        }

        public void ValidateSeconds(int seconds)
        {
            if (seconds < 0)
                throw new ArgumentException();
        }

        public TimeSpan TimeSpan
        {
            get => TimeSpan.FromSeconds(seconds);
            set => Seconds = (int)value.TotalSeconds;
        }
        #endregion

        #region Property: CountdownSeconds
        public event PropertyChangedTwoValuesDelegate<Timer, int> CountdownSecondsChanged;

        [PersistAs("countdown_seconds")]
        private int countdownSeconds = 5;
        public int CountdownSeconds
        {
            get => countdownSeconds;
            set => this.setProperty(ref countdownSeconds, value, CountdownSecondsChanged, validator: ValidateCountdownSeconds);
        }

        public void ValidateCountdownSeconds(int value)
        {
            if (value < 0)
                throw new ArgumentException();
        }
        #endregion

        #region Property: Running
        public event PropertyChangedTwoValuesDelegate<Timer, bool> RunningStateChanged;

        private bool running = false;
        public bool Running
        {
            get => running;
            private set
            {
                if (!this.setProperty(ref running, value, RunningStateChanged))
                    return;
                if (value == true)
                    Started?.Invoke(this);
                else
                    Stopped?.Invoke(this);
                OperationsChanged?.Invoke(this);
            }
        }
        #endregion

        #region Propety: Mode
        public event PropertyChangedTwoValuesDelegate<Timer, TimerMode> ModeChanged;

        [PersistAs("mode")]
        private TimerMode mode = TimerMode.Backwards;

        public TimerMode Mode
        {
            get => mode;
            set
            {
                AfterChangePropertyDelegate<TimerMode> afterChangeDelegate = (ov, nv) => {
                    Running = false;
                    if (nv == TimerMode.Backwards)
                        Seconds = CountdownSeconds;
                    else if (nv == TimerMode.Forwards)
                        Seconds = 0;
                };
                this.setProperty(ref mode, value, ModeChanged, null, afterChangeDelegate);
                OperationsChanged?.Invoke(this);
            }
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

        public bool CanStart => !Running;
        public bool CanStop =>  Running;
        public bool CanReset => true;
        #endregion

        #region Operations
        public void Start()
        {
            Running = true;
            resetInnerTimer();
        }

        public void Stop()
        {
            Running = false;
        }

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
