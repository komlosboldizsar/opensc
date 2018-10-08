using OpenSC.Model.Timers;
using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = OpenSC.Model.Timers.Timer;

namespace OpenSC.GUI.Timers
{
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
            TimerDatabase.Instance.ElementsChanged += timerDatabaseElementsChangedHandler;
        }

        private void TimerList_FormClosed(object sender, FormClosedEventArgs e)
        {
            TimerDatabase.Instance.ElementsChanged -= timerDatabaseElementsChangedHandler;
        }

        private void timerDatabaseElementsChangedHandler()
        {
            loadTimers();
        }

        private void loadTimers()
        {
            timerListTable.Rows.Clear();
            foreach(Timer timer in TimerDatabase.Instance.Timers)
            {
                var row = new TimerListTableRow(timer);
                timerListTable.Rows.Add(row);
            }
        }

        private static readonly Bitmap MODE_IMAGE_FORWARDS = Properties.Resources.timer_forward;
        private static readonly Bitmap MODE_IMAGE_BACKWARDS = Properties.Resources.timer_backward;
        private static readonly Bitmap MODE_IMAGE_CLOCK = Properties.Resources.timer_clock;

        private static readonly string MODE_LABEL_FORWARDS = "stopper";
        private static readonly string MODE_LABEL_BACKWARDS = "countdown";
        private static readonly string MODE_LABEL_CLOCK = "clock";

        private static readonly Bitmap BUTTON_IMAGE_START = Properties.Resources.timer_running;
        private static readonly Bitmap BUTTON_IMAGE_STOP = Properties.Resources.timer_stopped;
        private static readonly Bitmap BUTTON_IMAGE_RESET = Properties.Resources.timer_paused;

        private class TimerListTableRow: DataGridViewRow
        {

            private Timer timer;

            private DataGridViewTextBoxCell idCell;
            private DataGridViewTextBoxCell titleCell;
            private DataGridViewImageCell modeImageCell;
            private DataGridViewTextBoxCell modeLabelCell;
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
                subscribeTimerEvents();
            }

            private void addCells()
            {
                idCell = new DataGridViewTextBoxCell()
                {
                    Value = string.Format("#{0}", timer.ID)
                };
                this.Cells.Add(idCell);

                titleCell = new DataGridViewTextBoxCell()
                {
                    Value = timer.Title
                };
                this.Cells.Add(titleCell);

                modeImageCell = new DataGridViewImageCell()
                {
                    Value = convertModeToImage(timer.Mode),
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                modeImageCell.Style.Padding = new Padding(2);
                this.Cells.Add(modeImageCell);

                modeLabelCell = new DataGridViewTextBoxCell()
                {
                    Value = convertModeToLabel(timer.Mode)
                };
                this.Cells.Add(modeLabelCell);

                currentValueCell = new DataGridViewTextBoxCell()
                {
                    Value = timer.TimeSpan.ToString(@"hh\:mm\:ss")
                };
                this.Cells.Add(currentValueCell);

                startValueCell = new DataGridViewTextBoxCell()
                {
                    Value = (timer.Mode == TimerMode.Backwards) ? TimeSpan.FromSeconds(timer.CountdownSeconds).ToString(@"hh\:mm\:ss") : ""
                };
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

            private void subscribeTimerEvents()
            {
                timer.TitleChanged += timerTitleChangedHandler;
                timer.IdChanged += timerIdChangedHandler;
                timer.SecondsChanged += timerSecondsChangedHandler;
            }

            public void HandleCellClick(object sender, DataGridViewCellEventArgs e)
            {
                switch (e.ColumnIndex)
                {
                    case 6: // Edit
                        var editWindow = new TimerEditWindow(timer);
                        editWindow.ShowAsChild();
                        break;
                    case 7: // Delete
                        break;
                    case 8: // Start
                        break;
                    case 9: // Stop
                        break;
                    case 10: // Reset
                        break;
                    case 11: // Open
                        var timerWindow = new TimerWindow(timer);
                        timerWindow.ShowAsChild();
                        break;
                }
            }

            private void timerSecondsChangedHandler(Timer timer, int oldValue, int newValue)
            {
                if (timer != this.timer)
                    return;
                currentValueCell.Value = timer.TimeSpan.ToString(@"hh\:mm\:ss");
            }

            private void timerIdChangedHandler(Timer timer, int oldValue, int newValue)
            {
                if (timer != this.timer)
                    return;
                idCell.Value = string.Format("#{0}", timer.ID);
            }

            private void timerTitleChangedHandler(Timer timer, string oldTitle, string newTitle)
            {
                if (timer != this.timer)
                    return;
                titleCell.Value = timer.Title;
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