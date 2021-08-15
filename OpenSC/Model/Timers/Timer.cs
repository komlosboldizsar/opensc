using OpenSC.Model.Persistence;
using OpenSC.Model.Timers.Triggers;
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
            IdChanged = null;
            TitleChanged = null;
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

        #region Property: ID
        public event PropertyChangedTwoValuesDelegate<Timer, int> IdChanged;

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
            if (!TimerDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Title
        public event PropertyChangedTwoValuesDelegate<Timer, string> TitleChanged;

        [PersistAs("title")]
        private string title = "Test";
        public string Title
        {
            get => title;
            set => setProperty(this, ref title, value, TitleChanged, validator: ValidateTitle);
        }

        public void ValidateTitle(string title)
        {
            if (title == null)
                throw new ArgumentNullException();
            if (title == string.Empty)
                throw new ArgumentException();
        }
        #endregion

        #region Property: Seconds, TimeSpan
        public event PropertyChangedTwoValuesDelegate<Timer, int> SecondsChanged;

        private int seconds = 0;
        public int Seconds
        {
            get => seconds;
            set
            {
                if (!setProperty(this, ref seconds, value, SecondsChanged, validator: ValidateSeconds))
                    return;
                RaisePropertyChanged(nameof(TimeSpan));
                TimerMacroTriggers.TimerReachedValue.Call(this, seconds);
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
            set => setProperty(this, ref countdownSeconds, value, CountdownSecondsChanged, validator: ValidateCountdownSeconds);
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
                if (!setProperty(this, ref running, value, RunningStateChanged))
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
                setProperty(this, ref mode, value, ModeChanged, null, afterChangeDelegate);
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
