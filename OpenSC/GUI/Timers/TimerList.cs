using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Timers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Timer = OpenSC.Model.Timers.Timer;

namespace OpenSC.GUI.Timers
{
    [WindowTypeName("timers.timerlist")]
    public partial class TimerList : ChildWindowWithTable
    {
        public TimerList()
        {
            InitializeComponent();
            HeaderText = "List of timers";
            initializeTable();
        }

        private CustomDataGridView<Timer> table;

        private void initializeTable()
        {

            table = CreateTable<Timer>();

            CustomDataGridViewColumnDescriptorBuilder<Timer> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((timer, cell) => { cell.Value = string.Format("#{0}", timer.ID); });
            builder.AddChangeEvent(nameof(Timer.ID));
            builder.BuildAndAdd();

            // Column: title
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Title");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = timer.Name; });
            builder.AddChangeEvent(nameof(Timer.Name));
            builder.BuildAndAdd();

            // Column: mode image
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(30);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = modeImageConverter.Convert(timer.Mode); });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.BuildAndAdd();

            // Column: mode label
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Mode");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((timer, cell) => { cell.Value = modeLabelConverter.Convert(timer.Mode); });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.BuildAndAdd();

            // Column: running state
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(30);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => {
                if (timer.Mode == TimerMode.Clock)
                    cell.Value = STATE_IMAGE_NOTSHOWN;
                else
                    cell.Value = timer.Running ? STATE_IMAGE_RUNNING : STATE_IMAGE_STOPPED;
            });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.BuildAndAdd();

            // Column: current value
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current value");
            builder.Width(100);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = timer.TimeSpan.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Timer.Seconds));
            builder.BuildAndAdd();

            // Column: start value
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Start value");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((timer, cell) => {
                cell.Value = (timer.Mode == TimerMode.Backwards) ? TimeSpan.FromSeconds(timer.CountdownSeconds).ToString(@"hh\:mm\:ss") : "";
            });
            builder.AddChangeEvent(nameof(Timer.CountdownSeconds));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((timer, cell, e) => {
                var editWindow = new TimerEditWindow(timer);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((timer, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this timer?\n(#{0}) {1}", timer.ID, timer.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    TimerDatabase.Instance.Remove(timer);
            });
            builder.BuildAndAdd();

            // Column: start button
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("");
            builder.Width(40);
            builder.ButtonImage(BUTTON_IMAGE_START);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((timer, cell) => { ((DataGridViewDisableButtonCell)cell).Enabled = timer.CanStart; });
            builder.CellContentClickHandlerMethod((timer, cell, e) => { timer.Start(); });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.AddChangeEvent(nameof(Timer.Running));
            builder.BuildAndAdd();

            // Column: stop button
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("");
            builder.Width(40);
            builder.ButtonImage(BUTTON_IMAGE_STOP);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((timer, cell) => { ((DataGridViewDisableButtonCell)cell).Enabled = timer.CanStop; });
            builder.CellContentClickHandlerMethod((timer, cell, e) => { timer.Stop(); });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.AddChangeEvent(nameof(Timer.Running));
            builder.BuildAndAdd();

            // Column: reset button
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("");
            builder.Width(40);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonImage(BUTTON_IMAGE_RESET);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((timer, cell) => { ((DataGridViewDisableButtonCell)cell).Enabled = timer.CanReset; });
            builder.CellContentClickHandlerMethod((timer, cell, e) => { timer.Reset(); });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.AddChangeEvent(nameof(Timer.Running));
            builder.BuildAndAdd();

            // Column: open timer window button
            builder = GetColumnDescriptorBuilderForTable<Timer>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Open window");
            builder.Width(100);
            builder.ButtonText("Open window");
            builder.CellContentClickHandlerMethod((timer, cell, e) => {
                var timerWindow = new TimerWindow(timer);
                timerWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = TimerDatabase.Instance;

        }

        private static readonly EnumToStringConverter<TimerMode> modeLabelConverter = new EnumToStringConverter<TimerMode>() {
            { TimerMode.Forwards, "stopper" },
            { TimerMode.Backwards, "countdown" },
            { TimerMode.Clock, "clock" },
        };

        private static readonly EnumToBitmapConverter<TimerMode> modeImageConverter = new EnumToBitmapConverter<TimerMode>() {
            { TimerMode.Forwards, Properties.Resources._16_timer_forward },
            { TimerMode.Backwards, Properties.Resources._16_timer_backward },
            { TimerMode.Clock, Properties.Resources._16_timer_clock },
        };

        private static readonly Bitmap BUTTON_IMAGE_START = Properties.Resources._16_timer_running;
        private static readonly Bitmap BUTTON_IMAGE_STOP = Properties.Resources._16_timer_stopped;
        private static readonly Bitmap BUTTON_IMAGE_RESET = Properties.Resources._16_timer_reset;

        private static readonly Bitmap STATE_IMAGE_RUNNING = Properties.Resources._16_timer_running;
        private static readonly Bitmap STATE_IMAGE_STOPPED = Properties.Resources._16_timer_stopped;
        private static readonly Bitmap STATE_IMAGE_NOTSHOWN = Properties.GeneralIcons.empty_transparent;

        private void addTimerButton_Click(object sender, EventArgs e)
        {
            var editWindow = new TimerEditWindow(null);
            editWindow.ShowAsChild();
        }
    }

}