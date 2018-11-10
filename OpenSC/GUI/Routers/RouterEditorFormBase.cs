using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            CustomDataGridViewComboBoxItem<IRouterInputSource>[] sources = getAllSources();
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Source");
            builder.Width(300);
            builder.InitializerMethod((input, cell) => { });
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Source; });
            builder.CellEndEditHandlerMethod((input, cell, eventargs) => { input.Source = cell.Value as IRouterInputSource; });
            builder.DropDownPopulatorMethod((input, cell) => sources);
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

        private class SourceDropDownItem: CustomDataGridViewComboBoxItem<IRouterInputSource>
        {
            public SourceDropDownItem(IRouterInputSource value) : base(value)
            { }

            public override string ToString()
            {

                InputSourceSignal inputSourceSignal = Value as InputSourceSignal;
                if(inputSourceSignal != null)
                    return string.Format("Signal: (#{0}) {1}", inputSourceSignal.Signal.ID, inputSourceSignal.Signal.Name);

                RouterOutput routerOutput = Value as RouterOutput;
                if (routerOutput != null)
                    return string.Format("Router: (#{0}) {1}, output: ({2}.) {3}",
                        routerOutput.Router.ID, routerOutput.Router.Name,
                        (routerOutput.Index + 1), routerOutput.Name);

                return "unknown source type";

            }

        }

        private CustomDataGridViewComboBoxItem<IRouterInputSource>[] getAllSources()
        {

            List<CustomDataGridViewComboBoxItem<IRouterInputSource>> sourceList = new List<CustomDataGridViewComboBoxItem<IRouterInputSource>>();

            sourceList.Add(new CustomDataGridViewComboBoxItem<IRouterInputSource>.NullItem("(not connected)"));

            foreach (Signal signal in SignalDatabases.Signals.ItemsAsList)
                sourceList.Add(new SourceDropDownItem(new InputSourceSignal(signal)));

            foreach (Router router in RouterDatabase.Instance.ItemsAsList)
                foreach (RouterOutput output in router.Outputs)
                    sourceList.Add(new SourceDropDownItem(output));

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
