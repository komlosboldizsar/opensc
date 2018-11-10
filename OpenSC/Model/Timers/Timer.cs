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

        public override void Restored()
        { }

        public delegate void IdChangedDelegate(Timer timer, int oldValue, int newValue);
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
            if (!TimerDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public delegate void TitleChangedDelegate(Timer timer, string oldTitle, string newTitle);
        public event TitleChangedDelegate TitleChanged;

        [PersistAs("title")]
        private string title = "Test";
        public string Title
        {
            get { return title; }
            set
            {
                ValidateTitle(value);
                string oldTitle = title;
                title = value;
                TitleChanged?.Invoke(this, oldTitle, value);
                RaisePropertyChanged(nameof(Title));
            }
        }

        public void ValidateTitle(string title)
        {
            if (title == null)
                throw new ArgumentNullException();
            if (title == string.Empty)
                throw new ArgumentException();
        }

        public delegate void SecondsChangedDelegate(Timer timer, int oldValue, int newValue);
        public event SecondsChangedDelegate SecondsChanged;

        private int seconds = 0;
        public int Seconds
        {
            get { return seconds; }
            set {
                if (value < 0)
                    throw new ArgumentException();
                int oldValue = seconds;
                seconds = value;
                SecondsChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(Seconds));
            }
        }

        public TimeSpan TimeSpan
        {
            get { return TimeSpan.FromSeconds(seconds); }
            set { Seconds = (int)value.TotalSeconds; }
        }

        public delegate void CountdownSecondsChangedDelegate(Timer timer, int oldValue, int newValue);
        public event CountdownSecondsChangedDelegate CountdownSecondsChanged;

        [PersistAs("countdown_seconds")]
        private int countdownSeconds = 5;
        public int CountdownSeconds
        {
            get { return countdownSeconds; }
            set
            {
                
                int oldValue = countdownSeconds;
                countdownSeconds = value;
                CountdownSecondsChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(CountdownSeconds));
            }
        }

        public void ValidateCountdownSeconds(int value)
        {
            if (value < 0)
                throw new ArgumentException();
        }

        public delegate void RunningStateChangedDelegate(Timer timer, bool oldState, bool newState);
        public event RunningStateChangedDelegate RunningStateChanged;

        public delegate void StartedDelegate(Timer timer);
        public event StartedDelegate Started;

        public delegate void StoppedDelegate(Timer timer);
        public event StoppedDelegate Stopped;

        public delegate void ResetedDelegate(Timer timer);
        public event ResetedDelegate Reseted;

        private bool running = false;
        public bool Running
        {
            get { return running; }
            private set
            {

                bool oldValue = running;
                running = value;

                RunningStateChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(Running));
                if (value == true)
                {
                    Started?.Invoke(this);
                }
                else
                {
                    Stopped?.Invoke(this);
                }
                OperationsChanged?.Invoke(this);

            }
        }
    
        public delegate void OperationsChangedDelegate(Timer timer);
        public event OperationsChangedDelegate OperationsChanged;

        public bool CanStart
        {
            get => (mode != TimerMode.Clock) && !Running;
        }

        public bool CanStop
        {
            get => (mode != TimerMode.Clock) && Running;
        }

        public bool CanReset
        {
            get => mode != TimerMode.Clock;
        }

        public delegate void ModeChangedDelegate(Timer timer, TimerMode oldMode, TimerMode newMode);
        public event ModeChangedDelegate ModeChanged;

        [PersistAs("mode")]
        private TimerMode mode = TimerMode.Backwards;
        
        public TimerMode Mode
        {
            get { return mode; }
            set
            {
                TimerMode oldValue = mode;
                mode = value;
                if (oldValue != value)
                {
                    Running = false;
                    if (value == TimerMode.Backwards)
                        Seconds = CountdownSeconds;
                    else if (value == TimerMode.Forwards)
                        Seconds = 0;
                }
                ModeChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(Mode));
                OperationsChanged?.Invoke(this);
            }
        }

        public delegate void BackwardsTimerReachedZeroDelegate(Timer sender);
        public event BackwardsTimerReachedZeroDelegate ReachedZero;

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

        private void resetInnerTimer()
        {
            innerTimer.Stop();
            innerTimer.Start();
        }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            TimerDatabase.Instance.ItemUpdated(this);
        }

    }
}
