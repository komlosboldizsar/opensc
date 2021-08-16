using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Timers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Timers
{
    [WindowTypeName("timers.timerwindow")]
    public partial class TimerWindow : ChildWindowBase
    {

        private Model.Timers.Timer timer;

        public TimerWindow()
        {
            initWindow();
        }

        public TimerWindow(Model.Timers.Timer timer)
        {
            initWindow();
            initTimer(timer);
        }

        private void initWindow()
        {
            InitializeComponent();
            blinkingTimer.Elapsed += blinkEvent;
        }

        private void initTimer(Model.Timers.Timer timer)
        {

            this.timer = timer;
            timer.SecondsChanged += timerSecondsChangedHandler;
            timer.NameChanged += timerNameChangedHandler;
            timer.IdChanged += timerIdChangedHandler;
            timer.RunningStateChanged += timerRunningStateChangedHandler;
            timer.ModeChanged += timerModeChangedHandler;
            timer.ReachedZero += timerReachedZeroHandler;
            timer.Reseted += timerResetedHandler;

            updateTimeLabel();
            updateTitle();
            updateWindowTitle();
            updateButtons();
            updateModeImages();
            updateRunningStateImage();

        }

        private void timerResetedHandler(Model.Timers.Timer timer)
        {
            updateButtons();
        }

        private const int BLINKING_SPEED = 700;
        System.Timers.Timer blinkingTimer = new System.Timers.Timer(BLINKING_SPEED)
        {
            AutoReset = true,
            Enabled = true
        };
        private bool blinking = false;
        private bool blinkingEnabled = true;

        private void timerReachedZeroHandler(Model.Timers.Timer timer)
        {
            if (timer != this.timer)
                return;
            updateRunningStateImage();
            blinkingTimer.Stop();
            blinkingTimer.Start();
            blinking = true;
        }

        private void timerModeChangedHandler(Model.Timers.Timer timer, Model.Timers.TimerMode oldMode, Model.Timers.TimerMode newMode)
        {
            if (timer != this.timer)
                return;
            updateButtons();
            updateModeImages();
        }

        private void timerRunningStateChangedHandler(Model.Timers.Timer timer, bool oldState, bool newState)
        {
            if (timer != this.timer)
                return;
            updateButtons();
            updateRunningStateImage();
            blinking = false;
        }

        private void timerIdChangedHandler(IModel timer, int oldValue, int newValue)
        {
            if (timer != this.timer)
                return;
            updateWindowTitle();
        }

        private void timerNameChangedHandler(IModel timer, string oldTitle, string newTitle)
        {
            if (timer != this.timer)
                return;
            updateTitle();
            updateWindowTitle();
        }

        private void timerSecondsChangedHandler(Model.Timers.Timer timer, int oldValue, int newValue)
        {
            if (timer != this.timer)
                return;
            updateTimeLabel();
        }

        private void updateTitle()
        {
            string newTitle = "(no timer associated)";
            if (timer != null)
                newTitle = timer.Name;
            titleLabel.Text = newTitle;
        }

        private void updateWindowTitle()
        {
            string newTitle = "no timer associated";
            int newId = 0;
            if (timer != null) {
                newTitle = timer.Name;
                newId = timer.ID;
            }
            this.Text = string.Format("Timer: (#{0}) {1}", newId, newTitle);
        }

        private void updateTimeLabel()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateTimeLabel()));
                return;
            }
            if (timer == null)
            {
                timeLabel.Text = "NO TMR";
                return;
            }
            timeLabel.Text = timer.TimeSpan.ToString(@"hh\:mm\:ss");
            updateTimeBackgroundLabel();
        }

        private void updateButtons()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateButtons()));
                return;
            }
            if (timer.Mode == Model.Timers.TimerMode.Clock)
            {
                startButton.Enabled = false;
                stopButton.Enabled = false;
                resetButton.Enabled = false;
            }
            else
            {
                startButton.Enabled = !timer.Running && !(timer.Mode == Model.Timers.TimerMode.Backwards && timer.Seconds == 0);
                stopButton.Enabled = timer.Running;
                resetButton.Enabled = true;
            }
        }

        private static readonly Image MODE_IMAGE_CLOCK_INACTIVE = global::OpenSC.Properties.Resources.timer_clock_inactive;
        private static readonly Image MODE_IMAGE_CLOCK_ACTIVE = global::OpenSC.Properties.Resources.timer_clock;
        private static readonly Image MODE_IMAGE_FORWARDS_INACTIVE = global::OpenSC.Properties.Resources.timer_forward_inactive;
        private static readonly Image MODE_IMAGE_FORWARDS_ACTIVE = global::OpenSC.Properties.Resources.timer_forward;
        private static readonly Image MODE_IMAGE_BACKWARDS_INACTIVE = global::OpenSC.Properties.Resources.timer_backward_inactive;
        private static readonly Image MODE_IMAGE_BACKWARDS_ACTIVE = global::OpenSC.Properties.Resources.timer_backward;

        private void updateModeImages()
        {
            modeImageClock.Image = (timer.Mode == Model.Timers.TimerMode.Clock) ? MODE_IMAGE_CLOCK_ACTIVE : MODE_IMAGE_CLOCK_INACTIVE;
            modeImageForwards.Image = (timer.Mode == Model.Timers.TimerMode.Forwards) ? MODE_IMAGE_FORWARDS_ACTIVE : MODE_IMAGE_FORWARDS_INACTIVE;
            modeImageBackwards.Image = (timer.Mode == Model.Timers.TimerMode.Backwards) ? MODE_IMAGE_BACKWARDS_ACTIVE : MODE_IMAGE_BACKWARDS_INACTIVE;
        }

        private static readonly Image RUNNING_STATE_IMAGE_STOPPED = global::OpenSC.Properties.Resources.timer_stopped;
        private static readonly Image RUNNING_STATE_IMAGE_PAUSED = global::OpenSC.Properties.Resources.timer_paused;
        private static readonly Image RUNNING_STATE_IMAGE_RUNNING = global::OpenSC.Properties.Resources.timer_running;

        private void updateRunningStateImage()
        {

            if (InvokeRequired)
            {
                Invoke(new Action(() => updateRunningStateImage()));
                return;
            }

            if (timer == null)
            {
                runningStateImage.Image = null;
                return;
            }

            if(timer.Running)
            {
                if((timer.Mode == Model.Timers.TimerMode.Backwards) && (timer.Seconds == 0))
                {
                    runningStateImage.Image = RUNNING_STATE_IMAGE_PAUSED;
                    return;
                }
                runningStateImage.Image = RUNNING_STATE_IMAGE_RUNNING;
            }
            else
            {
                runningStateImage.Image = RUNNING_STATE_IMAGE_STOPPED;
            }

        }

        private bool showAllSegments = false;

        private void updateTimeBackgroundLabel()
        {
            timeLabelBackground.Text = showAllSegments && (timer != null) ? "88:88:88" : "";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (timer != null)
                timer.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (timer != null)
                timer.Stop();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (timer != null)
                timer.Reset();
        }

        private void TimerWindow_Load(object sender, EventArgs e)
        {
            timeLabel.Parent = timeLabelBackground;
            foreach (ToolStripMenuItem menuItem in setColorMenuItem.DropDownItems)
                menuItem.Click += colorSetMenuItemClikHandler;
        }

        private void colorSetMenuItemClikHandler(object sender, EventArgs e)
        {

            ToolStripMenuItem eventSource = sender as ToolStripMenuItem;
            if (eventSource == null)
                return;

            string colorCode = eventSource.Tag as string;
            if (colorCode == null)
                return;

            Color color = (Color)(new ColorConverter()).ConvertFromString(colorCode);
            TimeLabelColor = color;
        }

        private void buttonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonsContextItem.Checked = !buttonsContextItem.Checked;
            ButtonsVisible = buttonsContextItem.Checked;
        }

        private void showAllSegmentsContextItem_Click(object sender, EventArgs e)
        {
            showAllSegmentsContextItem.Checked = !showAllSegmentsContextItem.Checked;
            showAllSegments = showAllSegmentsContextItem.Checked;
            updateTimeBackgroundLabel();
        }

        private void blinkEvent(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => blinkEvent(sender, e)));
                return;
            }

            bool visibility = true;
            if(blinkingEnabled && blinking)
                visibility = !timeLabel.Visible;
            timeLabel.Visible = timeLabelBackground.Visible = visibility;
        }

        private void blinkWhenExpiredContextItem_Click(object sender, EventArgs e)
        {
            blinkWhenExpiredContextItem.Checked = !blinkWhenExpiredContextItem.Checked;
            BlinkingEnabled = blinkWhenExpiredContextItem.Checked;
        }

        #region Persistable attributes
        private bool ButtonsVisible
        {
            get { return buttonsPanel.Visible; }
            set
            {
                buttonsPanel.Visible = value;
                RequestRepersist();
            }
        }

        private Color TimeLabelColor
        {
            get { return timeLabel.ForeColor; }
            set
            {
                timeLabel.ForeColor = value;
                RequestRepersist();
            }
        }

        private bool BlinkingEnabled
        {
            get { return blinkingEnabled; }
            set
            {
                blinkingEnabled = value;
                RequestRepersist();
            }
        }
        #endregion

        #region Persistence
        private const string PERSISTENCE_KEY_TIMER_ID = "timer_id";
        private const string PERSISTENCE_KEY_BLINKING_ENABLED = "blinking_enabled";
        private const string PERSISTENCE_KEY_TIME_LABEL_COLOR = "time_label_color";
        private const string PERSISTENCE_KEY_BUTTONS_VISIBLE = "buttons_visible";

        protected override void restoreBeforeOpen(Dictionary<string, object> keyValuePairs)
        {

            base.restoreBeforeOpen(keyValuePairs);

            initTimer(TimerDatabase.Instance.GetTById((int)keyValuePairs[PERSISTENCE_KEY_TIMER_ID]));
            ButtonsVisible = (bool)keyValuePairs[PERSISTENCE_KEY_BUTTONS_VISIBLE];
            TimeLabelColor = (Color)keyValuePairs[PERSISTENCE_KEY_TIME_LABEL_COLOR];
            BlinkingEnabled = (bool)keyValuePairs[PERSISTENCE_KEY_BLINKING_ENABLED];

        }

        public override Dictionary<string, object> GetKeyValuePairs()
        {
            var dict = base.GetKeyValuePairs();
            dict.Add(PERSISTENCE_KEY_TIMER_ID, timer?.ID);
            dict.Add(PERSISTENCE_KEY_BLINKING_ENABLED, BlinkingEnabled);
            dict.Add(PERSISTENCE_KEY_TIME_LABEL_COLOR, TimeLabelColor);
            dict.Add(PERSISTENCE_KEY_BUTTONS_VISIBLE, ButtonsVisible);
            return dict;
        }
        #endregion

    }
}
