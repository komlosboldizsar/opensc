using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Streams;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Streams
{

    [WindowTypeName("streams.streamlist")]
    public partial class StreamList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "stream";
        protected override string SubjectPlural { get; } = "streams";

        protected override IModelTypeRegister TypeRegister { get; } = StreamTypeRegister.Instance;
        protected override IModelEditorFormTypeRegister EditorFormTypeRegister { get; } = StreamEditorFormTypeRegister.Instance;

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<Stream>(this, StreamDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Stream> table, ItemListFormBaseManager<Stream>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Stream> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: state image
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(30);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((stream, cell) => { cell.Value = stateImageConverter.Convert(stream.State); });
            builder.AddChangeEvent(nameof(Stream.State));

            // Column: state label
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((stream, cell) => { cell.Value = stateLabelConverter.Convert(stream.State); });
            builder.AddChangeEvent(nameof(Stream.State));

            // Column: viewer count
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Viewer count");
            builder.Width(100);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((stream, cell) => { cell.Value = (stream.State == StreamState.Running) ? stream.ViewerCount.ToString() : string.Empty; });
            builder.AddChangeEvent(nameof(Stream.ViewerCount));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private static readonly EnumToStringConverter<StreamState> stateLabelConverter = new EnumToStringConverter<StreamState>() {
            { StreamState.Unknown, "unknown" },
            { StreamState.NotRunning, "not running" },
            { StreamState.NotStarted, "not started" },
            { StreamState.Running, "running" },
            { StreamState.Ended, "ended" },
        };

        private static readonly EnumToBitmapConverter<StreamState> stateImageConverter = new EnumToBitmapConverter<StreamState>() {
            { StreamState.Unknown, Icons._16_stream_unknown },
            { StreamState.NotRunning, Icons._16_stream_not_running },
            { StreamState.NotStarted, Icons._16_stream_not_started },
            { StreamState.Running, Icons._16_stream_running },
            { StreamState.Ended, Icons._16_stream_ended },
        };

    }

}