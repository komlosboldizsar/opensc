using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{

    public partial class MacroEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New macro";
        private const string TITLE_EDIT = "Edit macro: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New macro";
        private const string HEADER_TEXT_EDIT = "Edit macro";

        protected Macro macro;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), macro?.ID, macro?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), macro?.ID, macro?.Name);
            }
        }

        public MacroEditorForm()
        {
            InitializeComponent();
        }

        public MacroEditorForm(Macro macro)
        {
            InitializeComponent();
            AddingNew = (macro == null);
            if (macro != null)
                this.macro = macro;
        }

        protected override void loadData()
        {
            if (macro == null)
                return;
            idNumericField.Value = (addingNew ? MacroDatabase.Instance.NextValidId() : macro.ID);
            nameTextBox.Text = macro.Name;
            /*initCommandsEditor();
            initTriggersTable();*/
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

            macro.StartUpdate();
            writeFields();
            macro.EndUpdate();

            if (addingNew)
                MacroDatabase.Instance.Add(macro);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (macro == null)
                return;
            macro.ValidateId((int)idNumericField.Value);
            // TODO: validate name
        }

        protected virtual void writeFields()
        {
            if (macro == null)
                return;
            macro.ID = (int)idNumericField.Value;
            macro.Name = nameTextBox.Text;
        }
        
        private void initCommandsEditor()
        {
        }

        private CustomDataGridView<RouterOutput> triggersTableCDGV;

        private void initTriggersTable()
        {

            /*triggersTableCDGV = createTable<RouterOutput>(outputsTableContainerPanel, ref this.outputsTable);
            CustomDataGridViewColumnDescriptorBuilder<RouterOutput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(triggersTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Index + 1; });
            //builder.AddChangeEvent(nameof(RouterOutput.Index));
            builder.BuildAndAdd();

            // Column: name
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(triggersTableCDGV);
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
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(triggersTableCDGV);
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
            triggersTableCDGV.BoundCollection = router.Outputs;*/

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

        private void addCommandButton_Click(object sender, EventArgs e)
        {
            //router.AddInput();
        }

        private void addTriggerButton_Click(object sender, EventArgs e)
        {
            //router.AddOutput();
        }

        private void commandsEditorTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
