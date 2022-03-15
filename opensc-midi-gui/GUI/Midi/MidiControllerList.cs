using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Midi;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Midi
{

    [WindowTypeName("midi.midicontrollerlist")]
    public partial class MidiControllerList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "MIDI controller";
        protected override string SubjectPlural { get; } = "MIDI controllers";

        protected override IModelEditorForm ModelEditorForm { get; } = new MidiControllerEditorForm();

        protected override IItemListFormBaseManager createManager()
            => Manager = new ModelListFormBaseManager<MidiController>(this, MidiControllerDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<MidiController> table, ItemListFormBaseManager<MidiController>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<MidiController> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: com port name
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Device ID");
            builder.Width(80);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.DeviceId; });
            builder.AddChangeEvent(nameof(MidiController.DeviceId));

            // Column: state (is initialized?)
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(80);
            builder.UpdaterMethod((port, cell) => {
                cell.Style.BackColor = port.Initialized ? Color.LightGreen : Color.LightPink;
                cell.Value = port.Initialized ? "initialized" : "not initialized";
            });
            builder.AddChangeEvent(nameof(MidiController.Initialized));

            // Column: initialize button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Initialize");
            builder.Width(70);
            builder.ButtonText("Initialize");
            builder.CellContentClickHandlerMethod((port, cell, e) => port.Init());

            // Column: deinitialize button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Deinitialize");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Deinitialize");
            builder.CellContentClickHandlerMethod((port, cell, e) => port.DeInit());

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}