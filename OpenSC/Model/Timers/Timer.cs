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

    public class Timer
    {

        public event TimerIdChangingDelegate IdChanging;
        public event TimerIdChangedDelegate IdChanged;

        public int id = 0;

        public int ID
        {
            get { return id; }
            set
            {
                int oldValue = id;
                IdChanging?.Invoke(this, oldValue, value);
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
            }
        }

        public event TimerTitleChangingDelegate TitleChanging;
        public event TimerTitleChangedDelegate TitleChanged;

        private string title = "Test";
        public string Title
        {
            get { return title; }
            set
            {
                string oldTitle = title;
                TitleChanging?.Invoke(this, oldTitle, value);
                title = value;
                TitleChanged?.Invoke(this, oldTitle, value);
            }
        }

        public event TimerSecondsChangingDelegate SecondsChanging;
        public event TimerSecondsChangedDelegate SecondsChanged;

        private int seconds = 0;
        public int Seconds
        {
            get { return seconds; }
            set {
                int oldValue = seconds;
                SecondsChanging?.Invoke(this, oldValue, value);
                seconds = value;
                SecondsChanged?.Invoke(this, oldValue, value);
            }
        }

        public TimeSpan TimeSpan
        {
            get { return TimeSpan.FromSeconds(seconds); }
            set { Seconds = (int)value.TotalSeconds; }
        }

        public event TimerCountdownSecondsChangingDelegate CountdownSecondsChanging;
        public event TimerCountdownSecondsChangedDelegate CountdownSecondsChanged;

        private int countdownSeconds = 5;
        public int CountdownSeconds
        {
            get { return countdownSeconds; }
            set
            {
                int oldValue = countdownSeconds;
                CountdownSecondsChanging?.Invoke(this, oldValue, value);
                countdownSeconds = value;
                CountdownSecondsChanged?.Invoke(this, oldValue, value);
            }
        }

        public event TimerRunningStateChangingDelegate RunningStateChanging;
        public event TimerRunningStateChangedDelegate RunningStateChanged;

        public event TimerStartingDelegate Starting;
        public event TimerStartedDelegate Started;

        public event TimerStoppingDelegate Stopping;
        public event TimerStoppedDelegate Stopped;

        public event TimerResetingDelegate Reseting;
        public event TimerResetedDelegate Reseted;

        private bool running = false;
        public bool Running
        {
            get { return running; }
            private set
            {

                bool oldValue = running;

                RunningStateChanging?.Invoke(this, oldValue, value);
                if (value == true)
                    Starting?.Invoke(this);
                else
                    Stopping?.Invoke(this);

                running = value;

                RunningStateChanged?.Invoke(this, oldValue, value);
                if (value == true)
                    Started?.Invoke(this);
                else
                    Stopped?.Invoke(this);

            }
        }

        public event TimerModeChangingDelegate ModeChanging;
        public event TimerModeChangedDelegate ModeChanged;

        private TimerMode mode = TimerMode.Backwards;

        public TimerMode Mode
        {
            get { return mode; }
            set
            {
                TimerMode oldValue = mode;
                ModeChanging?.Invoke(this, oldValue, value);
                mode = value;
                ModeChanged?.Invoke(this, oldValue, value);
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
            Reseting?.Invoke(this);
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

    }
}
