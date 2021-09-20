using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.General;
using OpenSC.Model.Routers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{

    public partial class LabelsetEditorForm : ModelEditorFormBase, IModelEditorForm<Labelset>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Labelset);
        public IModelEditorForm<Labelset> GetInstanceT(Labelset modelInstance) => new LabelsetEditorForm(modelInstance);


        private const string TITLE_NEW = "New labelset";
        private const string TITLE_EDIT = "Edit labelset: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New labelset";
        private const string HEADER_TEXT_EDIT = "Edit labelset";

        protected Labelset labelset;

        protected LabelsetProxy labelsetProxy;

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
            labelsetProxy = new LabelsetProxy(labelset);
            idNumericField.Value = (addingNew ? LabelsetDatabase.Instance.NextValidId() : labelset.ID);
            nameTextBox.Text = labelset.Name;
            initLabelsTable();
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

        private CustomDataGridView<LabelProxy> labelsTableCDGV;

        private void initLabelsTable()
        {

            labelsTableCDGV = createTable<LabelProxy>(labelsTableContainerPanel, ref this.labelsTable);
            CustomDataGridViewColumnDescriptorBuilder<LabelProxy> builder;

            // Column: router name
            builder = getColumnDescriptorBuilderForTable<LabelProxy>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Router");
            builder.Width(100);
            builder.UpdaterMethod((label, cell) => { cell.Value = label.RouterInput.Router.Name; });
            //builder.AddChangeEvent(nameof(Model.Routers.Label.RouterInput.Router.Name));
            builder.BuildAndAdd();

            // Column: router input index
            builder = getColumnDescriptorBuilderForTable<LabelProxy>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Input");
            builder.Width(50);
            builder.UpdaterMethod((label, cell) => { cell.Value = label.RouterInput.Index + 1; });
            //builder.AddChangeEvent(nameof(Model.Routers.Label.RouterInput.Index));
            builder.BuildAndAdd();

            // Column: text
            builder = getColumnDescriptorBuilderForTable<LabelProxy>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Text");
            builder.Width(100);
            builder.UpdaterMethod((label, cell) => { cell.Value = label.Text; });
            builder.AddChangeEvent(nameof(LabelProxy.Text));
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
            labelsTableCDGV.BoundCollection = labelsetProxy;

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

        protected class LabelProxy : Model.General.INotifyPropertyChanged
        {

            private Labelset labelset;

            public RouterInput RouterInput { get; private set; }
            
            public LabelProxy(Labelset labelset, RouterInput routerInput)
            {
                this.labelset = labelset;
                this.RouterInput = routerInput;
            }
            public string Text
            {
                get { return labelset.GetText(RouterInput); }
                set { labelset.SetText(RouterInput, value); }
            }

            public void TextChanged() => PropertyChanged?.Invoke(nameof(LabelProxy.Text));

            public event PropertyChangedDelegate PropertyChanged;

        }

        protected class LabelsetProxy : ObservableList<LabelProxy>
        {

            public Labelset Labelset { get; private set; }
            public LabelsetProxy(Labelset labelset)
            {
                this.Labelset = labelset;
                this.Labelset.LabelTextChanged += labelTextChanged;
                foreach (Router router in RouterDatabase.Instance)
                    foreach (RouterInput routerInput in router.Inputs)
                        Add(new LabelProxy(Labelset, routerInput));
            }

            private void labelTextChanged(Labelset labelset, RouterInput routerInput, string oldText, string newText)
            {
                foreach (LabelProxy labelProxy in this)
                    if (labelProxy.RouterInput == routerInput)
                        labelProxy.TextChanged();
            }

        }

    }

}
