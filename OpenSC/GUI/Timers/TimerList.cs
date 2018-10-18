using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
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
    public partial class TimerList : ChildWindowWithTitleAndTable<Timer>
    {
        public TimerList()
        {
            InitializeComponent();
            HeaderText = "List of timers";
            initializeTable();
        }

        private void initializeTable()
        {

            CustomDataGridViewColumnDescriptorBuilder<Timer> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((timer, cell) => { cell.Value = string.Format("#{0}", timer.ID); });
            builder.AddChangeEvent(nameof(Timer.IdChangedPCN));
            builder.BuildAndAdd();

            // Column: title
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Title");
            builder.Width(200);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = timer.Title; });
            builder.AddChangeEvent(nameof(Timer.TitleChangedPCN));
            builder.BuildAndAdd();

            // Column: mode image
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(50);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = convertModeToImage(timer.Mode); });
            builder.AddChangeEvent(nameof(Timer.ModeChangedPCN));
            builder.BuildAndAdd();

            // Column: mode label
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Mode");
            builder.Width(200);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((timer, cell) => { cell.Value = convertModeToLabel(timer.Mode); });
            builder.AddChangeEvent(nameof(Timer.ModeChangedPCN));
            builder.BuildAndAdd();

            // Column: running state
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(50);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => {
                if (timer.Mode == TimerMode.Clock)
                    cell.Value = STATE_IMAGE_NOTSHOWN;
                else
                    cell.Value = timer.Running ? STATE_IMAGE_RUNNING : STATE_IMAGE_STOPPED;
            });
            builder.AddChangeEvent(nameof(Timer.ModeChangedPCN));
            builder.BuildAndAdd();

            // Column: current value
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current value");
            builder.Width(200);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = timer.TimeSpan.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Timer.SecondsChangedPCN));
            builder.BuildAndAdd();

            // Column: start value
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Start value");
            builder.Width(200);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((timer, cell) => {
                cell.Value = (timer.Mode == TimerMode.Backwards) ? TimeSpan.FromSeconds(timer.CountdownSeconds).ToString(@"hh\:mm\:ss") : "";
            });
            builder.AddChangeEvent(nameof(Timer.CountdownSecondsChangedPCN));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((timer, cell, e) => {
                var editWindow = new TimerEditWindow(timer);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete buton
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((timer, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this timer?\n(#{0}) {1}", timer.ID, timer.Title);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    TimerDatabase.Instance.Remove(timer);
            });
            builder.BuildAndAdd();

            // Column: start buton
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("");
            builder.Width(70);
            builder.ButtonImage(BUTTON_IMAGE_START);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((timer, cell) => { ((DataGridViewDisableButtonCell)cell).Enabled = timer.CanStart; });
            builder.CellContentClickHandlerMethod((timer, cell, e) => { timer.Start(); });
            builder.AddChangeEvent(nameof(Timer.ModeChangedPCN));
            builder.AddChangeEvent(nameof(Timer.RunningStateChangedPCN));
            builder.BuildAndAdd();

            // Column: stop buton
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("");
            builder.Width(70);
            builder.ButtonImage(BUTTON_IMAGE_STOP);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((timer, cell) => { ((DataGridViewDisableButtonCell)cell).Enabled = timer.CanStop; });
            builder.CellContentClickHandlerMethod((timer, cell, e) => { timer.Stop(); });
            builder.AddChangeEvent(nameof(Timer.ModeChangedPCN));
            builder.AddChangeEvent(nameof(Timer.RunningStateChangedPCN));
            builder.BuildAndAdd();

            // Column: reset buton
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonImage(BUTTON_IMAGE_RESET);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((timer, cell) => { ((DataGridViewDisableButtonCell)cell).Enabled = timer.CanReset; });
            builder.CellContentClickHandlerMethod((timer, cell, e) => { timer.Reset(); });
            builder.AddChangeEvent(nameof(Timer.ModeChangedPCN));
            builder.AddChangeEvent(nameof(Timer.RunningStateChangedPCN));
            builder.BuildAndAdd();

            // Column: open timer window buton
            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Open window");
            builder.Width(100);
            builder.ButtonText("Open window");
            builder.CellContentClickHandlerMethod((timer, cell, e) => {
                var timerWindow = new TimerWindow(timer);
                timerWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Bind
            table.BoundDatabase = TimerDatabase.Instance;

        }

        private static readonly string MODE_LABEL_FORWARDS = "stopper";
        private static readonly string MODE_LABEL_BACKWARDS = "countdown";
        private static readonly string MODE_LABEL_CLOCK = "clock";

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

        private static readonly Bitmap MODE_IMAGE_FORWARDS = Properties.Resources._16_timer_forward;
        private static readonly Bitmap MODE_IMAGE_BACKWARDS = Properties.Resources._16_timer_backward;
        private static readonly Bitmap MODE_IMAGE_CLOCK = Properties.Resources._16_timer_clock;

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

        private static readonly Bitmap BUTTON_IMAGE_START = Properties.Resources._16_timer_running;
        private static readonly Bitmap BUTTON_IMAGE_STOP = Properties.Resources._16_timer_stopped;
        private static readonly Bitmap BUTTON_IMAGE_RESET = Properties.Resources._16_timer_reset;

        private static readonly Bitmap STATE_IMAGE_RUNNING = Properties.Resources._16_timer_running;
        private static readonly Bitmap STATE_IMAGE_STOPPED = Properties.Resources._16_timer_stopped;
        private static readonly Bitmap STATE_IMAGE_NOTSHOWN = Properties.Resources.empty_transparent;

    }

}