using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.SerialPorts;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.SerialPorts
{

    [WindowTypeName("serialports.serialportlist")]
    public partial class SerialPortList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "serial port";
        protected override string SubjectPlural { get; } = "serial ports";

        protected override IModelEditorForm ModelEditorForm { get; } = new SerialPortEditorForm();

        protected override IItemListFormBaseManager createManager()
            => Manager = new ModelListFormBaseManager<SerialPort>(this, SerialPortDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<SerialPort> table, ItemListFormBaseManager<SerialPort>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<SerialPort> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: com port name
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("COM port");
            builder.Width(80);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.ComPortName; });
            builder.AddChangeEvent(nameof(SerialPort.ComPortName));

            // Column: state (is initialized?)
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(80);
            builder.UpdaterMethod((port, cell) => {
                cell.Style.BackColor = port.Initialized ? Color.LightGreen : Color.LightPink;
                cell.Value = port.Initialized ? "initialized" : "not initialized";
            });
            builder.AddChangeEvent(nameof(SerialPort.Initialized));

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
            builder.ButtonText("Deinitialize");
            builder.CellContentClickHandlerMethod((port, cell, e) => port.DeInit());

            // Column: monitor button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Monitor");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Monitor");
            builder.CellContentClickHandlerMethod((port, cell, e) => (new SerialPortMonitorForm(port)).ShowAsChild());

            // Column: baudrate
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Baudrate");
            builder.Width(70);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.BaudRate; });
            builder.AddChangeEvent(nameof(SerialPort.BaudRate));

            // Column: data bits
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Data bits");
            builder.Width(70);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.DataBits; });
            builder.AddChangeEvent(nameof(SerialPort.DataBits));

            // Column: stop bits
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Stop bits");
            builder.Width(70);
            builder.UpdaterMethod((port, cell) => { cell.Value = SerialPortSettingTranslators.STOPBITS.Convert(port.StopBits); });
            builder.AddChangeEvent(nameof(SerialPort.StopBits));

            // Column: parity
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Parity");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((port, cell) => { cell.Value = SerialPortSettingTranslators.PARITY.Convert(port.Parity); });
            builder.AddChangeEvent(nameof(SerialPort.Parity));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        

    }

}