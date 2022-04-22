using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{

    public partial class RouterEditorFormBase : ModelEditorFormBase
    {

        public RouterEditorFormBase() : base() => InitializeComponent();
        public RouterEditorFormBase(Router router) : base(router) => InitializeComponent();

        protected override void loadData()
        {
            base.loadData();
            Router router = (Router)EditedModel;
            if (router == null)
                return;
            initInputsTable();
            initOutputsTable();
        }

        protected override void validateFields()
        {
            base.validateFields();
            Router router = (Router)EditedModel;
            if (router == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Router router = (Router)EditedModel;
            if (router == null)
                return;
        }

        private static readonly Color CELL_COST_BACK_IS_TIELINE = Color.White;
        private static readonly Color CELL_COST_BACK_NOT_TIELINE = Color.Gray;

        private CustomDataGridView<RouterInput> inputsTableCDGV;

        private void initInputsTable()
        {

            Router router = (Router)EditedModel;
            inputsTableCDGV = createTable<RouterInput>(inputsTableContainerPanel, ref this.inputsTable);
            CustomDataGridViewColumnDescriptorBuilder<RouterInput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Index; });
            builder.AddChangeEvent(nameof(RouterInput.Index));
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
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(100);
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Name; });
            builder.AddChangeEvent(nameof(RouterInput.Name));
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

            // Column: source
            CustomDataGridViewComboBoxItem<ISignalSourceRegistered>[] sources = getAllSources();
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Source");
            builder.Width(300);
            builder.InitializerMethod((input, cell) => { });
            builder.UpdaterMethod((input, cell) => { cell.Value = input.CurrentSource; });
            builder.CellValueChangedHandlerMethod((input, cell, eventargs) => { input.AssignSource(cell.Value as ISignalSource); });
            builder.DropDownPopulatorMethod((input, cell) => sources);
            builder.ReceiveSystemObjectDrop().FilterByType<ISignalSourceRegistered>();
            builder.BuildAndAdd();

            // Column: tieline cost
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Tieline cost");
            builder.Width(70);
            builder.TextEditable(true);
            builder.UpdaterMethod((input, cell) => {
                cell.Value = input.TielineCost?.ToString() ?? "";
                cell.Style.BackColor = input.IsTieline ? CELL_COST_BACK_IS_TIELINE : CELL_COST_BACK_NOT_TIELINE;
                cell.ReadOnly = !input.IsTieline;
            });
            builder.AddChangeEvent(nameof(RouterInput.IsTieline));
            builder.AddChangeEvent(nameof(RouterInput.TielineCost));
            builder.CellEndEditHandlerMethod((input, cell, eventargs) => {
                if (!input.IsTieline)
                {
                    MessageBox.Show("This input is not used as tieline, cost cannot be specified.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cell.Value = "";
                    return;
                }
                if(!int.TryParse(cell.Value?.ToString(), out int tielineCost) || (tielineCost < 0))
                {
                    MessageBox.Show("Invalid input! Tieline cost must be a non-negative integer!", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cell.Value = "";
                    return;
                }
                input.TielineCost = tielineCost;
            });
            builder.BuildAndAdd();

            // Column: Reserve tieline
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.DisableButton);
            builder.Header("Reserve TL");
            builder.Width(70);
            builder.ButtonText("Reserve");
            builder.UpdaterMethod((input, cell) => {
                ((DataGridViewDisableButtonCell)cell).Enabled = (input.IsTieline && !(input.TielineIsReserved ?? false));
            });
            builder.AddChangeEvent(nameof(RouterInput.IsTieline));
            builder.AddChangeEvent(nameof(RouterInput.TielineIsReserved));
            builder.CellContentClickHandlerMethod((input, cell, eventargs) => {
                input.TielineIsReserved = true;
            });
            builder.BuildAndAdd();

            // Column: Free tieline
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.DisableButton);
            builder.Header("Free TL");
            builder.Width(70);
            builder.ButtonText("Free");
            builder.UpdaterMethod((input, cell) => {
                ((DataGridViewDisableButtonCell)cell).Enabled = (input.IsTieline && (input.TielineIsReserved ?? false));
            });
            builder.AddChangeEvent(nameof(RouterInput.IsTieline));
            builder.AddChangeEvent(nameof(RouterInput.TielineIsReserved));
            builder.CellContentClickHandlerMethod((input, cell, eventargs) => {
                input.TielineIsReserved = false;
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((input, cell, e) => {
                string msgBoxText = $"Do you really want to delete input [{input}]?";
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    router.RemoveInput(input);
            });
            builder.BuildAndAdd();

            // Bind collection
            inputsTableCDGV.BoundCollection = router.Inputs;

        }

        private CustomDataGridView<RouterOutput> outputsTableCDGV;

        private void initOutputsTable()
        {

            Router router = (Router)EditedModel;
            outputsTableCDGV = createTable<RouterOutput>(outputsTableContainerPanel, ref this.outputsTable);
            CustomDataGridViewColumnDescriptorBuilder<RouterOutput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Index; });
            builder.AddChangeEvent(nameof(RouterOutput.Index));
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
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(100);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Name; });
            builder.AddChangeEvent(nameof(RouterOutput.Name));
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

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((output, cell, e) => {
                string msgBoxText = $"Do you really want to delete output [{output}]?";
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    router.RemoveOutput(output);
            });
            builder.BuildAndAdd();

            // Bind collection
            outputsTableCDGV.BoundCollection = router.Outputs;

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

        private class SourceDropDownItem: CustomDataGridViewComboBoxItem<ISignalSourceRegistered>
        {
            public SourceDropDownItem(ISignalSourceRegistered value) : base(value)
            { }
            public override string ToString() => Value.SignalLabel;
        }

        private CustomDataGridViewComboBoxItem<ISignalSourceRegistered>[] getAllSources()
        {
            List<CustomDataGridViewComboBoxItem<ISignalSourceRegistered>> sourceList = new List<CustomDataGridViewComboBoxItem<ISignalSourceRegistered>>();
            sourceList.Add(new CustomDataGridViewComboBoxItem<ISignalSourceRegistered>.NullItem("(not connected)"));
            foreach (ISignalSourceRegistered signal in SignalRegister.Instance)
                sourceList.Add(new SourceDropDownItem(signal));
            return sourceList.ToArray();
        }

        private void addInputButton_Click(object sender, EventArgs e) => ((Router)EditedModel).AddInput();
        private void addOutputButton_Click(object sender, EventArgs e) => ((Router)EditedModel).AddOutput();

    }

}
