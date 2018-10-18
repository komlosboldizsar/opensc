using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Timers
{
    public delegate void TimerIdChangingDelegate(Timer timer, int oldValue, int newValue);
    public delegate void TimerIdChangedDelegate(Timer timer, int oldValue, int newValue);

    public delegate void TimerTitleChangingDelegate(Timer timer, string oldTitle, string newTitle);
    public delegate void TimerTitleChangedDelegate(Timer timer, string oldTitle, string newTitle);

    public delegate void TimerSecondsChangingDelegate(Timer timer, int oldValue, int newValue);
    public delegate void TimerSecondsChangedDelegate(Timer timer, int oldValue, int newValue);

    public delegate void TimerCountdownSecondsChangingDelegate(Timer timer, int oldValue, int newValue);
    public delegate void TimerCountdownSecondsChangedDelegate(Timer timer, int oldValue, int newValue);

    public delegate void TimerRunningStateChangingDelegate(Timer timer, bool oldState, bool newState);
    public delegate void TimerRunningStateChangedDelegate(Timer timer, bool oldState, bool newState);

    public delegate void TimerModeChangingDelegate(Timer timer, TimerMode oldMode, TimerMode newMode);
    public delegate void TimerModeChangedDelegate(Timer timer, TimerMode oldMode, TimerMode newMode);

    public delegate void TimerStartingDelegate(Timer timer);
    public delegate void TimerStartedDelegate(Timer timer);

    public delegate void TimerStoppingDelegate(Timer timer);
    public delegate void TimerStoppedDelegate(Timer timer);

    public delegate void TimerResetingDelegate(Timer timer);
    public delegate void TimerResetedDelegate(Timer timer);

    public delegate void TimerOperationsChangingDelegate(Timer timer);
    public delegate void TimerOperationsChangedDelegate(Timer timer);

    public class Timer: IModel
    {

        public void Restored()
        {

        }

        public event TimerIdChangingDelegate IdChanging;
        public event TimerIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        public int id = 0;

        public int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                int oldValue = id;
                IdChanging?.Invoke(this, oldValue, value);
                IdChangingPCN?.Invoke();
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                IdChangedPCN?.Invoke();
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!TimerDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public event TimerTitleChangingDelegate TitleChanging;
        public event TimerTitleChangedDelegate TitleChanged;
        public event ParameterlessChangeNotifierDelegate TitleChangingPCN;
        public event ParameterlessChangeNotifierDelegate TitleChangedPCN;

        [PersistAs("title")]
        private string title = "Test";
        public string Title
        {
            get { return title; }
            set
            {
                ValidateTitle(value);
                string oldTitle = title;
                TitleChanging?.Invoke(this, oldTitle, value);
                TitleChangingPCN?.Invoke();
                title = value;
                TitleChanged?.Invoke(this, oldTitle, value);
                TitleChangedPCN?.Invoke();
            }
        }

        public void ValidateTitle(string title)
        {
            if (title == null)
                throw new ArgumentNullException();
            if (title == string.Empty)
                throw new ArgumentException();
        }

        public event TimerSecondsChangingDelegate SecondsChanging;
        public event TimerSecondsChangedDelegate SecondsChanged;
        public event ParameterlessChangeNotifierDelegate SecondsChangingPCN;
        public event ParameterlessChangeNotifierDelegate SecondsChangedPCN;

        private int seconds = 0;
        public int Seconds
        {
            get { return seconds; }
            set {
                if (value < 0)
                    throw new ArgumentException();
                int oldValue = seconds;
                SecondsChanging?.Invoke(this, oldValue, value);
                SecondsChangingPCN?.Invoke();
                seconds = value;
                SecondsChanged?.Invoke(this, oldValue, value);
                SecondsChangedPCN?.Invoke();
            }
        }

        public TimeSpan TimeSpan
        {
            get { return TimeSpan.FromSeconds(seconds); }
            set { Seconds = (int)value.TotalSeconds; }
        }

        public event TimerCountdownSecondsChangingDelegate CountdownSecondsChanging;
        public event TimerCountdownSecondsChangedDelegate CountdownSecondsChanged;
        public event ParameterlessChangeNotifierDelegate CountdownSecondsChangingPCN;
        public event ParameterlessChangeNotifierDelegate CountdownSecondsChangedPCN;

        [PersistAs("countdown_seconds")]
        private int countdownSeconds = 5;
        public int CountdownSeconds
        {
            get { return countdownSeconds; }
            set
            {
                
                int oldValue = countdownSeconds;
                CountdownSecondsChanging?.Invoke(this, oldValue, value);
                CountdownSecondsChangingPCN?.Invoke();
                countdownSeconds = value;
                CountdownSecondsChanged?.Invoke(this, oldValue, value);
                CountdownSecondsChangedPCN?.Invoke();
            }
        }

        public void ValidateCountdownSeconds(int value)
        {
            if (value < 0)
                throw new ArgumentException();
        }

        public event TimerRunningStateChangingDelegate RunningStateChanging;
        public event TimerRunningStateChangedDelegate RunningStateChanged;
        public event ParameterlessChangeNotifierDelegate RunningStateChangingPCN;
        public event ParameterlessChangeNotifierDelegate RunningStateChangedPCN;

        public event TimerStartingDelegate Starting;
        public event TimerStartedDelegate Started;
        public event ParameterlessChangeNotifierDelegate StartingPCN;
        public event ParameterlessChangeNotifierDelegate StartedPCN;

        public event TimerStoppingDelegate Stopping;
        public event TimerStoppedDelegate Stopped;
        public event ParameterlessChangeNotifierDelegate StoppingPCN;
        public event ParameterlessChangeNotifierDelegate StoppedPCN;

        public event TimerResetingDelegate Reseting;
        public event TimerResetedDelegate Reseted;
        public event ParameterlessChangeNotifierDelegate ResetingPCN;
        public event ParameterlessChangeNotifierDelegate ResetedPCN;

        private bool running = false;
        public bool Running
        {
            get { return running; }
            private set
            {

                bool oldValue = running;

                RunningStateChanging?.Invoke(this, oldValue, value);
                RunningStateChangingPCN?.Invoke();
                if (value == true)
                {
                    Starting?.Invoke(this);
                    StartingPCN?.Invoke();
                }
                else
                {
                    Stopping?.Invoke(this);
                    StoppingPCN?.Invoke();
                }
                OperationsChanging?.Invoke(this);
                OperationsChangingPCN?.Invoke();

                running = value;

                RunningStateChanged?.Invoke(this, oldValue, value);
                RunningStateChangedPCN?.Invoke();
                if (value == true)
                {
                    Started?.Invoke(this);
                    StartedPCN?.Invoke();
                }
                else
                {
                    Stopped?.Invoke(this);
                    StoppedPCN?.Invoke();
                }
                OperationsChanged?.Invoke(this);
                OperationsChangedPCN?.Invoke();

            }
        }

        public event TimerOperationsChangingDelegate OperationsChanging;
        public event TimerOperationsChangedDelegate OperationsChanged;
        public event ParameterlessChangeNotifierDelegate OperationsChangingPCN;
        public event ParameterlessChangeNotifierDelegate OperationsChangedPCN;

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

        public event TimerModeChangingDelegate ModeChanging;
        public event TimerModeChangedDelegate ModeChanged;
        public event ParameterlessChangeNotifierDelegate ModeChangingPCN;
        public event ParameterlessChangeNotifierDelegate ModeChangedPCN;

        [PersistAs("mode")]
        private TimerMode mode = TimerMode.Backwards;
        
        public TimerMode Mode
        {
            get { return mode; }
            set
            {
                TimerMode oldValue = mode;
                ModeChanging?.Invoke(this, oldValue, value);
                ModeChangingPCN?.Invoke();
                OperationsChanging?.Invoke(this);
                OperationsChangingPCN?.Invoke();
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
                ModeChangedPCN?.Invoke();
                OperationsChanged?.Invoke(this);
                OperationsChangedPCN?.Invoke();
            }
        }

        public delegate void BackwardsTimerReachedZeroDelegate(Timer sender);
        public event BackwardsTimerReachedZeroDelegate ReachedZero;
        public event ParameterlessChangeNotifierDelegate ReachedZeroPCN;

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
                        ReachedZeroPCN?.Invoke();
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
            Reseting?.Invoke(this);
            ResetingPCN?.Invoke();
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
            ResetedPCN?.Invoke();
            resetInnerTimer();
        }

        private void resetInnerTimer()
        {
            innerTimer.Stop();
            innerTimer.Start();
        }

    }
}
