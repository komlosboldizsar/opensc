using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{

    public partial class LabelsetEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New labelset";
        private const string TITLE_EDIT = "Edit labelset: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New labelset";
        private const string HEADER_TEXT_EDIT = "Edit labelset";

        protected Labelset labelset;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), labelset?.ID, labelset?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), labelset?.ID, labelset?.Name);
            }
        }

        public LabelsetEditorForm()
        {
            InitializeComponent();
        }

        public LabelsetEditorForm(Labelset labelset)
        {
            InitializeComponent();
            this.labelset = (labelset != null) ? labelset : new Labelset();
            AddingNew = (this.labelset == null);
        }

        protected override void loadData()
        {
            if (labelset == null)
                return;
            idNumericField.Value = (addingNew ? LabelsetDatabase.Instance.NextValidId() : labelset.ID);
            nameTextBox.Text = labelset.Name;
            //initInputsTable();
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

            labelset.StartUpdate();
            writeFields();
            labelset.EndUpdate();

            if (addingNew)
                LabelsetDatabase.Instance.Add(labelset);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (labelset == null)
                return;
            labelset.ValidateId((int)idNumericField.Value);
            // TODO: validate name
        }

        protected virtual void writeFields()
        {
            if (labelset == null)
                return;
            labelset.ID = (int)idNumericField.Value;
            labelset.Name = nameTextBox.Text;
        }

        private CustomDataGridView<Model.Routers.Label> labelsTableCDGV;

        private void initInputsTable()
        {

            labelsTableCDGV = createTable<Model.Routers.Label>(labelsTableContainerPanel, ref this.labelsTable);
            CustomDataGridViewColumnDescriptorBuilder<Model.Routers.Label> builder;

            // Column: router name
            builder = getColumnDescriptorBuilderForTable<Model.Routers.Label>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Router");
            builder.Width(100);
            builder.UpdaterMethod((label, cell) => { cell.Value = label.RouterInput.Router.Name; });
            //builder.AddChangeEvent(nameof(Model.Routers.Label.RouterInput.Router.Name));
            builder.BuildAndAdd();

            // Column: router input index
            builder = getColumnDescriptorBuilderForTable<Model.Routers.Label>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Input");
            builder.Width(50);
            builder.UpdaterMethod((label, cell) => { cell.Value = label.RouterInput.Index + 1; });
            //builder.AddChangeEvent(nameof(Model.Routers.Label.RouterInput.Index));
            builder.BuildAndAdd();

            // Column: text
            builder = getColumnDescriptorBuilderForTable<Model.Routers.Label>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Input");
            builder.Width(100);
            builder.UpdaterMethod((label, cell) => { cell.Value = label.Text; });
            builder.AddChangeEvent(nameof(Model.Routers.Label.Text));
            builder.TextEditable(true);
            builder.CellEndEditHandlerMethod((label, cell, eventargs) =>
            {
                try
                {
                    label.Text = cell.Value.ToString();
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cell.Value = label.Text;
                }
            });
            builder.BuildAndAdd();

            // Bind collection
            labelsTableCDGV.BoundCollection = labelset.Labels;

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
