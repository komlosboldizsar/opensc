using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Streams;
using OpenSC.Model.Timers;
using OpenSC.Model.UMDs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Streams
{

    [WindowTypeName("streams.streamlist")]
    public partial class StreamList : ChildWindowWithTable
    {

        public StreamList()
        {
            InitializeComponent();
            HeaderText = "List of streams";
            initializeTable();
        }

        private CustomDataGridView<Stream> table;

        private void initializeTable()
        {

            table = CreateTable<Stream>();

            CustomDataGridViewColumnDescriptorBuilder<Stream> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<Stream>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((stream, cell) => { cell.Value = string.Format("#{0}", stream.ID); });
            builder.AddChangeEvent(nameof(Stream.IdChangedPCN));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<Stream>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(200);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((stream, cell) => { cell.Value = stream.Name; });
            builder.AddChangeEvent(nameof(Stream.NameChangedPCN));
            builder.BuildAndAdd();

            // Column: state image
            builder = GetColumnDescriptorBuilderForTable<Stream>();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(30);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((stream, cell) => { cell.Value = stateImageConverter.Convert(stream.State); });
            builder.AddChangeEvent(nameof(Stream.StateChangedPCN));
            builder.BuildAndAdd();

            // Column: state label
            builder = GetColumnDescriptorBuilderForTable<Stream>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((stream, cell) => { cell.Value = stateLabelConverter.Convert(stream.State); });
            builder.AddChangeEvent(nameof(Stream.StateChangedPCN));
            builder.BuildAndAdd();

            // Column: viewer count
            builder = GetColumnDescriptorBuilderForTable<Stream>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Viewer count");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((stream, cell) => { cell.Value = (stream.ViewerCount != null) ? stream.ViewerCount.ToString() : string.Empty; });
            builder.AddChangeEvent(nameof(Stream.ViewerCountChangedPCN));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<Stream>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((stream, cell, e) => {
                /*var editWindow = new StreamEditWindow();
                editWindow.ShowAsChild();*/
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<Stream>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((stream, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this stream?\n(#{0}) {1}", stream.ID, stream.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    StreamDatabase.Instance.Remove(stream);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundDatabase = StreamDatabase.Instance;

        }

        private static readonly EnumToStringConverter<StreamState> stateLabelConverter = new EnumToStringConverter<StreamState>() {
            { StreamState.Unknown, "unknown" },
            { StreamState.NotRunning, "not running" },
            { StreamState.NotStarted, "not started" },
            { StreamState.Running, "running" },
            { StreamState.Ended, "ended" },
        };

        private static readonly EnumToBitmapConverter<StreamState> stateImageConverter = new EnumToBitmapConverter<StreamState>() {
            { StreamState.Unknown, Properties.Resources._16_stream_unknown },
            { StreamState.NotRunning, Properties.Resources._16_stream_not_running },
            { StreamState.NotStarted, Properties.Resources._16_stream_not_started },
            { StreamState.Running, Properties.Resources._16_stream_running },
            { StreamState.Ended, Properties.Resources._16_stream_ended },
        };

    }

}