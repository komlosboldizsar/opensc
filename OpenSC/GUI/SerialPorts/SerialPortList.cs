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
    public partial class SerialPortList : ChildWindowWithTable
    {

        public SerialPortList()
        {
            InitializeComponent();
            initializeTable();
            //loadAddableUmdPortTypes();
        }

        private CustomDataGridView<SerialPort> table;

        private void initializeTable()
        {

            table = CreateTable<SerialPort>();

            CustomDataGridViewColumnDescriptorBuilder<SerialPort> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((port, cell) => { cell.Value = string.Format("#{0}", port.ID); });
            builder.AddChangeEvent(nameof(SerialPort.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(200);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.Name; });
            builder.AddChangeEvent(nameof(SerialPort.Name));
            builder.BuildAndAdd();

            // Column: com port name
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("COM port");
            builder.Width(80);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.ComPortName; });
            builder.AddChangeEvent(nameof(SerialPort.ComPortName));
            builder.BuildAndAdd();

            // Column: state (is initialized?)
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(80);
            builder.UpdaterMethod((port, cell) => {
                cell.Style.BackColor = port.Initialized ? Color.LightGreen : Color.LightPink;
                cell.Value = port.Initialized ? "initialized" : "not initialized";
            });
            builder.AddChangeEvent(nameof(SerialPort.Initialized));
            builder.BuildAndAdd();

            // Column: initialize button
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Initialize");
            builder.Width(70);
            builder.ButtonText("Initialize");
            builder.CellContentClickHandlerMethod((port, cell, e) => port.Init());
            builder.BuildAndAdd();

            // Column: deinitialize button
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Deinitialize");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Deinitialize");
            builder.CellContentClickHandlerMethod((port, cell, e) => port.DeInit());
            builder.BuildAndAdd();

            // Column: baudrate
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Baudrate");
            builder.Width(70);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.BaudRate; });
            builder.AddChangeEvent(nameof(SerialPort.BaudRate));
            builder.BuildAndAdd();

            // Column: data bits
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Data bits");
            builder.Width(70);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.DataBits; });
            builder.AddChangeEvent(nameof(SerialPort.DataBits));
            builder.BuildAndAdd();

            // Column: stop bits
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Stop bits");
            builder.Width(70);
            builder.UpdaterMethod((port, cell) => { cell.Value = stopBitsToStringConverter.Convert(port.StopBits); });
            builder.AddChangeEvent(nameof(SerialPort.StopBits));
            builder.BuildAndAdd();

            // Column: parity
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Parity");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((port, cell) => { cell.Value = parityToStringConverter.Convert(port.Parity); });
            builder.AddChangeEvent(nameof(SerialPort.Parity));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((port, cell, e) => new SerialPortEditorForm(port).ShowAsChild());
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<SerialPort>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((port, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this serial port?\n(#{0}) {1}", port.ID, port.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    SerialPortDatabase.Instance.Remove(port);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = SerialPortDatabase.Instance;

        }

        private static readonly EnumToStringConverter<System.IO.Ports.Parity> parityToStringConverter = new EnumToStringConverter<System.IO.Ports.Parity>() {
            { System.IO.Ports.Parity.None, "none" },
            { System.IO.Ports.Parity.Even, "even" },
            { System.IO.Ports.Parity.Odd, "odd" },
            { System.IO.Ports.Parity.Mark, "mark" },
            { System.IO.Ports.Parity.Space, "space" }
        };

        private static readonly EnumToStringConverter<System.IO.Ports.StopBits> stopBitsToStringConverter = new EnumToStringConverter<System.IO.Ports.StopBits>() {
            { System.IO.Ports.StopBits.None, "none" },
            { System.IO.Ports.StopBits.One, "1" },
            { System.IO.Ports.StopBits.OnePointFive, "1.5" },
            { System.IO.Ports.StopBits.Two, "2" },
        };

        private void addSerialPortButton_Click(object sender, EventArgs e)
        {
            new SerialPortEditorForm(null).ShowAsChild();
        }

        /*private void loadAddableUmdPortTypes()
        {
            foreach (Type type in UmdPortEditorFormTypeRegister.Instance.RegisteredTypes)
            {
                string label = type.GetTypeLabel();
                ToolStripMenuItem contextMenuItem = new ToolStripMenuItem(label)
                {
                    Tag = type
                };
                contextMenuItem.Click += addUmdPortMenuItemClick;
                addableUmdPortTypesMenu.Items.Add(contextMenuItem);
            }
        }

        private static void addUmdPortMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem typedSender = sender as ToolStripMenuItem;
            Type newUmdPortType = typedSender?.Tag as Type;
            IModelEditorForm<UmdPort> editorForm = UmdPortEditorFormTypeRegister.Instance.GetFormForType(newUmdPortType);
            (editorForm as ChildWindowBase)?.ShowAsChild();
        }*/

    }

}