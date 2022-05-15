using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.GpioInterfaces;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.GpioInterfaces
{

    public partial class GpioInterfaceEditorFormBase : ModelEditorFormBase
    {

        public GpioInterfaceEditorFormBase() : base() => InitializeComponent();
        public GpioInterfaceEditorFormBase(GpioInterface router) : base(router) => InitializeComponent();

        protected override void loadData()
        {
            base.loadData();
            GpioInterface gpioInterface = (GpioInterface)EditedModel;
            if (gpioInterface == null)
                return;
            initInputsTable();
            initOutputsTable();
        }

        protected override void validateFields()
        {
            base.validateFields();
            GpioInterface gpioInterface = (GpioInterface)EditedModel;
            if (gpioInterface == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            GpioInterface gpioInterface = (GpioInterface)EditedModel;
            if (gpioInterface == null)
                return;
        }

        private CustomDataGridView<GpioInterfaceInput> inputsTableCDGV;

        private void initInputsTable()
        {

            GpioInterface gpioInterface = (GpioInterface)EditedModel;
            inputsTableCDGV = createTable<GpioInterfaceInput>(inputsTableContainerPanel, ref this.inputsTable);
            CustomDataGridViewColumnDescriptorBuilder<GpioInterfaceInput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<GpioInterfaceInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Index; });
            builder.AddChangeEvent(nameof(GpioInterfaceInput.Index));
            builder.TextEditable(true);
            builder.CellEndEditHandlerMethod((input, cell, eventargs) =>
            {
                try
                {
                    int index = int.Parse(cell.Value.ToString());
                    if ((index < 0) || (index > 65535))
                        throw new ArgumentException("Index of router input must be between 0 and 65535.");
                    input.Index = index;
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cell.Value = input.Index;
                }
            });
            builder.AllowSystemObjectDrag();
            builder.BuildAndAdd();

            // Column: name
            builder = getColumnDescriptorBuilderForTable<GpioInterfaceInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(100);
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Name; });
            builder.AddChangeEvent(nameof(GpioInterfaceInput.Name));
            builder.TextEditable(true);
            builder.CellEndEditHandlerMethod((input, cell, eventargs) =>
            {
                try
                {
                    input.Name = cell.Value.ToString();
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cell.Value = input.Name;
                }
            });
            builder.AllowSystemObjectDrag();
            builder.BuildAndAdd();

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<GpioInterfaceInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((input, cell, e) => {
                string msgBoxText = $"Do you really want to delete input [{input}]?";
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    gpioInterface.RemoveInput(input);
            });
            builder.BuildAndAdd();

            // Bind collection
            inputsTableCDGV.BoundCollection = gpioInterface.Inputs;

        }

        private CustomDataGridView<GpioInterfaceOutput> outputsTableCDGV;

        private void initOutputsTable()
        {

            GpioInterface gpioInterface = (GpioInterface)EditedModel;
            outputsTableCDGV = createTable<GpioInterfaceOutput>(outputsTableContainerPanel, ref this.outputsTable);
            CustomDataGridViewColumnDescriptorBuilder<GpioInterfaceOutput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<GpioInterfaceOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Index; });
            builder.AddChangeEvent(nameof(GpioInterfaceOutput.Index));
            builder.TextEditable(true);
            builder.CellEndEditHandlerMethod((output, cell, eventargs) =>
            {
                try
                {
                    int index = int.Parse(cell.Value.ToString());
                    if ((index < 0) || (index > 65535))
                        throw new ArgumentException("Index of router input must be between 0 and 65535.");
                    output.Index = index;
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cell.Value = output.Index;
                }
            });
            builder.AllowSystemObjectDrag();
            builder.BuildAndAdd();

            // Column: name
            builder = getColumnDescriptorBuilderForTable<GpioInterfaceOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(100);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Name; });
            builder.AddChangeEvent(nameof(GpioInterfaceOutput.Name));
            builder.TextEditable(true);
            builder.CellEndEditHandlerMethod((output, cell, eventargs) =>
            {
                try
                {
                    output.Name = cell.Value.ToString();
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cell.Value = output.Name;
                }
            });
            builder.AllowSystemObjectDrag();
            builder.BuildAndAdd();

            // Column: source
            CustomDataGridViewComboBoxItem<IBoolean>[] sources = getAllDrivers();
            builder = getColumnDescriptorBuilderForTable<GpioInterfaceOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Source");
            builder.Width(300);
            builder.InitializerMethod((output, cell) => { });
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Driver; });
            builder.CellValueChangedHandlerMethod((output, cell, eventargs) => { output.Driver = cell.Value as IBoolean; });
            builder.DropDownPopulatorMethod((output, cell) => sources);
            builder.ReceiveObjectDrop().FilterByType<IBoolean>();
            builder.BuildAndAdd();

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<GpioInterfaceOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((output, cell, e) => {
                string msgBoxText = $"Do you really want to delete output [{output}]?";
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    gpioInterface.RemoveOutput(output);
            });
            builder.BuildAndAdd();

            // Bind collection
            outputsTableCDGV.BoundCollection = gpioInterface.Outputs;

        }

        public CustomDataGridView<T> createTable<T>(Control container, ref DataGridView originalTableMember)
            where T : class
        {
            var customTable = new CustomDataGridView<T>();
            container.Controls.Clear();
            container.Controls.Add(customTable);
            customTable.Dock = DockStyle.Fill;
            originalTableMember = customTable;
            return customTable;
        }

        private CustomDataGridViewColumnDescriptorBuilder<T> getColumnDescriptorBuilderForTable<T>(CustomDataGridView<T> table)
            where T : class
            => new CustomDataGridViewColumnDescriptorBuilder<T>(table);

        private class DriverDropDownItem : CustomDataGridViewComboBoxItem<IBoolean>
        {
            public DriverDropDownItem(IBoolean value) : base(value)
            { }
            public override string Label => $"[{Value.Identifier}] {Value.Description}";
        }

        private CustomDataGridViewComboBoxItem<IBoolean>[] getAllDrivers()
        {
            List<CustomDataGridViewComboBoxItem<IBoolean>> driverList = new List<CustomDataGridViewComboBoxItem<IBoolean>>();
            driverList.Add(new CustomDataGridViewComboBoxItem<IBoolean>.NullItem("(not connected)"));
            foreach (IBoolean boolean in BooleanRegister.Instance)
                driverList.Add(new DriverDropDownItem(boolean));
            return driverList.ToArray();
        }

        private void addInputButton_Click(object sender, EventArgs e) => ((GpioInterface)EditedModel).AddInput();
        private void addOutputButton_Click(object sender, EventArgs e) => ((GpioInterface)EditedModel).AddOutput();

    }

}
