using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Timers;
using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = OpenSC.Model.Timers.Timer;

namespace OpenSC.GUI.Timers
{
    [WindowTypeName("timers.timerlist")]
    public partial class TimerList : ChildWindowWithTitle
    {
        public TimerList()
        {
            InitializeComponent();
            HeaderText = "List of timers";
        }

        private void TimerList_Load(object sender, EventArgs e)
        {
            loadTimers();
            TimerDatabase.Instance.ChangedItems += timerDatabaseElementsChangedHandler;
        }

        private void TimerList_FormClosed(object sender, FormClosedEventArgs e)
        {
            TimerDatabase.Instance.ChangedItems -= timerDatabaseElementsChangedHandler;
        }

        private void timerDatabaseElementsChangedHandler(DatabaseBase<Timer> database)
        {
            loadTimers();
        }

        private void loadTimers()
        {
            timerListTable.Rows.Clear();
            foreach(Timer timer in TimerDatabase.Instance.ItemsAsList)
            {
                var row = new TimerListTableRow(timer);
                timerListTable.Rows.Add(row);
            }
        }

        private static readonly Bitmap MODE_IMAGE_FORWARDS = Properties.Resources._16_timer_forward;
        private static readonly Bitmap MODE_IMAGE_BACKWARDS = Properties.Resources._16_timer_backward;
        private static readonly Bitmap MODE_IMAGE_CLOCK = Properties.Resources._16_timer_clock;

        private static readonly string MODE_LABEL_FORWARDS = "stopper";
        private static readonly string MODE_LABEL_BACKWARDS = "countdown";
        private static readonly string MODE_LABEL_CLOCK = "clock";

        private static readonly Bitmap BUTTON_IMAGE_START = Properties.Resources._16_timer_running;
        private static readonly Bitmap BUTTON_IMAGE_STOP = Properties.Resources._16_timer_stopped;
        private static readonly Bitmap BUTTON_IMAGE_RESET = Properties.Resources._16_timer_reset;

        private static readonly Bitmap STATE_IMAGE_RUNNING = Properties.Resources._16_timer_running;
        private static readonly Bitmap STATE_IMAGE_STOPPED = Properties.Resources._16_timer_stopped;
        private static readonly Bitmap STATE_IMAGE_NOTSHOWN = Properties.Resources.empty_transparent;

        private class TimerListTableRow: DataGridViewRow
        {

            private Timer timer;

            private DataGridViewTextBoxCell idCell;
            private DataGridViewTextBoxCell titleCell;
            private DataGridViewImageCell modeImageCell;
            private DataGridViewTextBoxCell modeLabelCell;
            private DataGridViewImageCell runningStateCell;
            private DataGridViewTextBoxCell currentValueCell;
            private DataGridViewTextBoxCell startValueCell;
            private DataGridViewButtonCell editButtonCell;
            private DataGridViewButtonCell deleteButtonCell;
            private DataGridViewImageButtonCell startButtonCell;
            private DataGridViewImageButtonCell stopButtonCell;
            private DataGridViewImageButtonCell resetButtonCell;
            private DataGridViewButtonCell openTimerWindowButtonCell;

            public TimerListTableRow(Timer timer)
            {
                this.timer = timer;
                addCells();
                loadData();
                subscribeTimerEvents();
            }

            private void addCells()
            {
                idCell = new DataGridViewTextBoxCell();
                this.Cells.Add(idCell);

                titleCell = new DataGridViewTextBoxCell();
                this.Cells.Add(titleCell);

                modeImageCell = new DataGridViewImageCell()
                {
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                modeImageCell.Style.Padding = new Padding(2);
                this.Cells.Add(modeImageCell);

                modeLabelCell = new DataGridViewTextBoxCell();
                this.Cells.Add(modeLabelCell);

                runningStateCell = new DataGridViewImageCell()
                {
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                runningStateCell.Style.Padding = new Padding(2);
                this.Cells.Add(runningStateCell);

                currentValueCell = new DataGridViewTextBoxCell();
                this.Cells.Add(currentValueCell);

                startValueCell = new DataGridViewTextBoxCell();
                this.Cells.Add(startValueCell);

                editButtonCell = new DataGridViewButtonCell()
                {
                    Value = "Edit"
                };
                this.Cells.Add(editButtonCell);

                deleteButtonCell = new DataGridViewButtonCell()
                {
                    Value = "Delete"
                };
                this.Cells.Add(deleteButtonCell);

                startButtonCell = new DataGridViewImageButtonCell()
                {
                    Image = BUTTON_IMAGE_START
                };
                startButtonCell.ImagePadding = new Padding(2);
                this.Cells.Add(startButtonCell);

                stopButtonCell = new DataGridViewImageButtonCell()
                {
                    Image = BUTTON_IMAGE_STOP
                };
                stopButtonCell.ImagePadding = new Padding(2);
                this.Cells.Add(stopButtonCell);

                resetButtonCell = new DataGridViewImageButtonCell()
                {
                    Image = BUTTON_IMAGE_RESET
                };
                resetButtonCell.ImagePadding = new Padding(2);
                this.Cells.Add(resetButtonCell);

                openTimerWindowButtonCell = new DataGridViewButtonCell()
                {
                    Value = "Open"
                };
                this.Cells.Add(openTimerWindowButtonCell);

            }

            private void loadData()
            {
                updateTimerSettings();
                updateTimerCurrentValue();
                updateTimmerRunningState();
                updateButtonsEnableState();
            }

            private void updateTimerSettings()
            {
                idCell.Value = string.Format("#{0}", timer.ID);
                titleCell.Value = timer.Title;
                modeLabelCell.Value = convertModeToLabel(timer.Mode);
                modeImageCell.Value = convertModeToImage(timer.Mode);
                startValueCell.Value = (timer.Mode == TimerMode.Backwards) ? TimeSpan.FromSeconds(timer.CountdownSeconds).ToString(@"hh\:mm\:ss") : "";
            }

            private void updateTimerCurrentValue()
            {
                currentValueCell.Value = timer.TimeSpan.ToString(@"hh\:mm\:ss");
            }

            private void updateTimmerRunningState()
            {
                if (timer.Mode == TimerMode.Clock)
                    runningStateCell.Value = STATE_IMAGE_NOTSHOWN;
                else
                    runningStateCell.Value = timer.Running ? STATE_IMAGE_RUNNING : STATE_IMAGE_STOPPED;
            }

            private void updateButtonsEnableState()
            {
                startButtonCell.Enabled = timer.CanStart;
                stopButtonCell.Enabled = timer.CanStop;
                resetButtonCell.Enabled = timer.CanReset;
            }

            private void subscribeTimerEvents()
            {
                timer.TitleChanged += timerTitleChangedHandler;
                timer.IdChanged += timerIdChangedHandler;
                timer.SecondsChanged += timerSecondsChangedHandler;
                timer.RunningStateChanged += timerRunningStateChanged;
                timer.CountdownSecondsChanged += timerCountdownSecondsChanged;
                timer.ModeChanged += timerModeChanged;
                timer.OperationsChanged += timerOperationsChanged;
            }

            public void HandleCellClick(object sender, DataGridViewCellEventArgs e)
            {

                // Edit
                if(e.ColumnIndex == editButtonCell.ColumnIndex)
                {
                    var editWindow = new TimerEditWindow(timer);
                    editWindow.ShowAsChild();
                    return;
                }

                // Delete
                if(e.ColumnIndex == deleteButtonCell.ColumnIndex)
                {
                    TimerDatabase.Instance.Remove(timer);
                    return;
                }

                // Start
                if (e.ColumnIndex == startButtonCell.ColumnIndex)
                {
                    timer.Start();
                    return;
                }

                // Stop
                if (e.ColumnIndex == stopButtonCell.ColumnIndex)
                {
                    timer.Stop();
                    return;
                }

                // Reset
                if (e.ColumnIndex == resetButtonCell.ColumnIndex)
                {
                    timer.Reset();
                    return;
                }

                // Open
                if (e.ColumnIndex == openTimerWindowButtonCell.ColumnIndex)
                {
                    var timerWindow = new TimerWindow(timer);
                    timerWindow.ShowAsChild();
                    return;
                }

            }

            private void timerSecondsChangedHandler(Timer timer, int oldValue, int newValue)
            {
                if (timer != this.timer)
                    return;
                updateTimerCurrentValue();
            }

            private void timerIdChangedHandler(Timer timer, int oldValue, int newValue)
            {
                if (timer != this.timer)
                    return;
                updateTimerSettings();
            }

            private void timerTitleChangedHandler(Timer timer, string oldTitle, string newTitle)
            {
                if (timer != this.timer)
                    return;
                updateTimerSettings();
            }

            private void timerOperationsChanged(Timer timer)
            {
                if (timer != this.timer)
                    return;
                updateButtonsEnableState();
            }

            private void timerModeChanged(Timer timer, TimerMode oldMode, TimerMode newMode)
            {
                if (timer != this.timer)
                    return;
                updateTimerSettings();
                updateTimmerRunningState();
            }

            private void timerCountdownSecondsChanged(Timer timer, int oldValue, int newValue)
            {
                if (timer != this.timer)
                    return;
                updateTimerSettings();
            }

            private void timerRunningStateChanged(Timer timer, bool oldState, bool newState)
            {
                if (timer != this.timer)
                    return;
                updateTimmerRunningState();
            }

            private string convertModeToLabel(TimerMode mode)
            {
                switch (mode)
                {
                    case TimerMode.Forwards:
                        return MODE_LABEL_FORWARDS;
                    case TimerMode.Backwards:
                        return MODE_LABEL_BACKWARDS;
                    case TimerMode.Clock:
                        return MODE_LABEL_CLOCK;
                }
                return string.Empty;
            }

            private Bitmap convertModeToImage(TimerMode mode)
            {
                switch (mode)
                {
                    case TimerMode.Forwards:
                        return MODE_IMAGE_FORWARDS;
                    case TimerMode.Backwards:
                        return MODE_IMAGE_BACKWARDS;
                    case TimerMode.Clock:
                        return MODE_IMAGE_CLOCK;
                }
                return null;
            }


        }

        private void timerListTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender != timerListTable)
                return;
            TimerListTableRow rowObject = timerListTable.Rows[e.RowIndex] as TimerListTableRow;
            if (rowObject != null)
                rowObject.HandleCellClick(sender, e);
        }
    }
}