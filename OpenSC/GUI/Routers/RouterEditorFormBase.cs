using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Routers;
using System;
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

            writeFields();
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
            //builder.AddChangeEvent(nameof(RouterInput.IndexChangedPCN));
            builder.BuildAndAdd();

            // Column: name
            builder = getColumnDescriptorBuilderForTable<RouterInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(100);
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Name; });
            builder.AddChangeEvent(nameof(RouterInput.NameChangedPCN));
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
            //builder.AddChangeEvent(nameof(RouterOutput.IndexChangedPCN));
            builder.BuildAndAdd();

            // Column: name
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(outputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(100);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Name; });
            builder.AddChangeEvent(nameof(RouterOutput.NameChangedPCN));
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


    }

}
