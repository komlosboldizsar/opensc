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

        private const string TITLE_NEW = "New router";
        private const string TITLE_EDIT = "Edit router: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New router";
        private const string HEADER_TEXT_EDIT = "Edit router";

        protected Router router;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), router?.ID, router?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), router?.ID, router?.Name);
            }
        }

        public RouterEditorFormBase()
        {
            InitializeComponent();
        }

        public RouterEditorFormBase(Router router)
        {
            InitializeComponent();
            AddingNew = (router == null);
            if (router != null)
                this.router = router;
        }

        protected override void loadData()
        {
            if (router == null)
                return;
            idNumericField.Value = (addingNew ? RouterDatabase.Instance.NextValidId() : router.ID);
            nameTextBox.Text = router.Name;
            initInputsTable();
            initOutputsTable();
        }

        protected sealed override bool saveData()
        {

            try
            {
                validateFields();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            router.StartUpdate();
            writeFields();
            router.EndUpdate();

            if (addingNew)
                RouterDatabase.Instance.Add(router);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (router == null)
                return;
            router.ValidateId((int)idNumericField.Value);
            // TODO: validate name
        }

        protected virtual void writeFields()
        {
            if (router == null)
                return;
            router.ID = (int)idNumericField.Value;
            router.Name = nameTextBox.Text;
        }

        private static readonly Color CELL_COST_BACK_IS_TIELINE = Color.White;
        private static readonly Color CELL_COST_BACK_NOT_TIELINE = Color.Gray;

        private CustomDataGridView<RouterInput> inputsTableCDGV;

        private void initInputsTable()
        {

            inputsTableCDGV = createTable<RouterInput>(inputsTableContainerPanel, ref this.inputsTable);
            CustomDataGridViewColumnDescriptorBuilder<RouterInput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Index + 1; });
            //builder.AddChangeEvent(nameof(RouterInput.Index));
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
            builder.BuildAndAdd();

            // Column: source
            CustomDataGridViewComboBoxItem<ISignalSourceRegistered>[] sources = getAllSources();
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Source");
            builder.Width(300);
            builder.InitializerMethod((input, cell) => { });
            builder.UpdaterMethod((input, cell) => { cell.Value = input.CurrentSource; });
            builder.CellEndEditHandlerMethod((input, cell, eventargs) => { input.CurrentSource = cell.Value as ISignalSource; });
            builder.DropDownPopulatorMethod((input, cell) => sources);
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
                string msgBoxText = string.Format("Do you really want to delete input #{0}?", (input.Index + 1));
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

            outputsTableCDGV = createTable<RouterOutput>(outputsTableContainerPanel, ref this.outputsTable);
            CustomDataGridViewColumnDescriptorBuilder<RouterOutput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Index + 1; });
            //builder.AddChangeEvent(nameof(RouterOutput.Index));
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
            builder.BuildAndAdd();

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((output, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete output #{0}?", (output.Index + 1));
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
        {
             return new CustomDataGridViewColumnDescriptorBuilder<T>(table);
        }

        private class SourceDropDownItem: CustomDataGridViewComboBoxItem<ISignalSourceRegistered>
        {
            public SourceDropDownItem(ISignalSourceRegistered value) : base(value)
            { }

            public override string ToString()
                => Value.SignalLabel;

        }

        private CustomDataGridViewComboBoxItem<ISignalSourceRegistered>[] getAllSources()
        {
            List<CustomDataGridViewComboBoxItem<ISignalSourceRegistered>> sourceList = new List<CustomDataGridViewComboBoxItem<ISignalSourceRegistered>>();
            sourceList.Add(new CustomDataGridViewComboBoxItem<ISignalSourceRegistered>.NullItem("(not connected)"));
            foreach (ISignalSourceRegistered signal in SignalRegister.Instance)
                sourceList.Add(new SourceDropDownItem(signal));
            return sourceList.ToArray();
        }

        private void addInputButton_Click(object sender, EventArgs e)
        {
            router.AddInput();
        }

        private void addOutputButton_Click(object sender, EventArgs e)
        {
            router.AddOutput();
        }

    }

}
