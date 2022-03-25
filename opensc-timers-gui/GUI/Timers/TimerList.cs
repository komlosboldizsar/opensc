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
    public partial class TimerList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "timer";
        protected override string SubjectPlural { get; } = "timers";

        protected override IModelEditorForm ModelEditorForm { get; } = new TimerEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<Timer>(this, TimerDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Timer> table, ItemListFormBaseManager<Timer>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Timer> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: mode image
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(30);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = modeImageConverter.Convert(timer.Mode); });
            builder.AddChangeEvent(nameof(Timer.Mode));

            // Column: mode label
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Mode");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((timer, cell) => { cell.Value = modeLabelConverter.Convert(timer.Mode); });
            builder.AddChangeEvent(nameof(Timer.Mode));

            // Column: running state
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(30);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = timer.Running ? STATE_IMAGE_RUNNING : STATE_IMAGE_STOPPED; });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.AddChangeEvent(nameof(Timer.Running));

            // Column: current value
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current value");
            builder.Width(100);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((timer, cell) => { cell.Value = timer.TimeSpan.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Timer.Seconds));

            // Column: start value
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Start value");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((timer, cell) => {
                cell.Value = (timer.Mode == TimerMode.Backwards) ? TimeSpan.FromSeconds(timer.CountdownSeconds).ToString(@"hh\:mm\:ss") : "";
            });
            builder.AddChangeEvent(nameof(Timer.CountdownSeconds));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

            // Column: start button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("");
            builder.Width(40);
            builder.ButtonImage(BUTTON_IMAGE_START);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((timer, cell) => { ((DataGridViewDisableButtonCell)cell).Enabled = timer.CanStart; });
            builder.CellContentClickHandlerMethod((timer, cell, e) => { timer.Start(); });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.AddChangeEvent(nameof(Timer.Running));

            // Column: stop button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.ImageButton);
            builder.Header("");
            builder.Width(40);
            builder.ButtonImage(BUTTON_IMAGE_STOP);
            builder.ButtonImagePadding(DEFAULT_IMAGE_BUTTON_PADDING);
            builder.UpdaterMethod((timer, cell) => { ((DataGridViewDisableButtonCell)cell).Enabled = timer.CanStop; });
            builder.CellContentClickHandlerMethod((timer, cell, e) => { timer.Stop(); });
            builder.AddChangeEvent(nameof(Timer.Mode));
            builder.AddChangeEvent(nameof(Timer.Running));

            // Column: reset button
            builder = builderGetterMethod();
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

            // Column: open timer window button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Open window");
            builder.Width(100);
            builder.ButtonText("Open window");
            builder.CellContentClickHandlerMethod((timer, cell, e) => {
                var timerWindow = new TimerWindow(timer);
                timerWindow.ShowAsChild();
            });

        }

        private static readonly EnumToStringConverter<TimerMode> modeLabelConverter = new EnumToStringConverter<TimerMode>() {
            { TimerMode.Forwards, "stopper" },
            { TimerMode.Backwards, "countdown" }
        };

        private static readonly EnumToBitmapConverter<TimerMode> modeImageConverter = new EnumToBitmapConverter<TimerMode>() {
            { TimerMode.Forwards, Icons._16_timer_forward },
            { TimerMode.Backwards, Icons._16_timer_backward }
        };

        private static readonly Bitmap BUTTON_IMAGE_START = Icons._16_timer_running;
        private static readonly Bitmap BUTTON_IMAGE_STOP = Icons._16_timer_stopped;
        private static readonly Bitmap BUTTON_IMAGE_RESET = Icons._16_timer_reset;

        private static readonly Bitmap STATE_IMAGE_RUNNING = Icons._16_timer_running;
        private static readonly Bitmap STATE_IMAGE_STOPPED = Icons._16_timer_stopped;
        private static readonly Bitmap STATE_IMAGE_NOTSHOWN = SolidIcons.empty_transparent;

    }

}