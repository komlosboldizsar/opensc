using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
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

        public LabelsetEditorForm() : base() => InitializeComponent();
        public LabelsetEditorForm(Labelset labelset) : base(labelset) => InitializeComponent();

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Labelset, Labelset>(this, LabelsetDatabase.Instance);

        protected ObservableProxyEnumerable<LabelProxy, ISystemObject> labelsetProxy;

        protected override void loadData()
        {
            base.loadData();
            Labelset labelset = (Labelset)EditedModel;
            if (labelset == null)
                return;
            labelsetProxy = new(LabelableObjectRegister.Instance, (sysObj => new LabelProxy(labelset, sysObj)));
            initLabelsTable();
        }

        protected override void validateFields()
        {
            base.validateFields();
            Labelset labelset = (Labelset)EditedModel;
            if (labelset == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Labelset labelset = (Labelset)EditedModel;
            if (labelset == null)
                return;
        }

        private CustomDataGridView<LabelProxy> labelsTableCDGV;

        private void initLabelsTable()
        {

            labelsTableCDGV = createTable<LabelProxy>(labelsTableContainerPanel, ref this.labelsTable);
            CustomDataGridViewColumnDescriptorBuilder<LabelProxy> builder;

            // Column: object
            builder = getColumnDescriptorBuilderForTable<LabelProxy>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Object ID");
            builder.Width(100);
            builder.UpdaterMethod((labelProxy, cell) => { cell.Value = labelProxy.Original.GlobalID; });
            builder.AddMultilevelChangeEvent(nameof(LabelProxy.Original), nameof(ISystemObject.GlobalID));
            builder.BuildAndAdd();

            // Column: text
            builder = getColumnDescriptorBuilderForTable<LabelProxy>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Text");
            builder.Width(100);
            builder.InitializerMethod((labelProxy, cell) => { cell.Value = labelProxy.Text; });
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
            => new CustomDataGridViewColumnDescriptorBuilder<T>(table);

        protected class LabelProxy : ObjectProxy<ISystemObject>
        {

            private Labelset labelset;

            public LabelProxy(Labelset labelset, ISystemObject systemObject) : base(systemObject) => this.labelset = labelset;

            public string Text
            {
                get => labelset.GetText(Original);
                set => labelset.SetText(Original, value);
            }

        }

    }

}
