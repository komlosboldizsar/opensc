using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
using OpenSC.Model.General;
using OpenSC.Model.Labelsets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSC.GUI.Labelsets
{

    public partial class AllLabelsetEditorForm : ChildWindowWithTitle
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Labelset);
        public IModelEditorForm<Labelset> GetInstanceT(Labelset modelInstance) => new LabelsetEditorForm(modelInstance);

        public AllLabelsetEditorForm() : base() => InitializeComponent();

        protected ObservableProxyEnumerable<ItemProxy, ISystemObject> itemProxies;

        private void AllLabelsetEditorForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        protected void loadData()
        {
            itemProxies = new(LabelableObjectRegister.Instance, (sysObj => new ItemProxy(sysObj)));
            initLabelsTable();
        }

        /*protected override void validateFields()
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
        }*/

        private CustomDataGridView<ItemProxy> labelsTableCDGV;

        private void initLabelsTable()
        {

            labelsTableCDGV = createTable<ItemProxy>(labelsTableContainerPanel, ref this.labelsTable);
            CustomDataGridViewColumnDescriptorBuilder<ItemProxy> builder;

            // Column: object
            builder = getColumnDescriptorBuilderForTable<ItemProxy>(labelsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Object ID");
            builder.Width(100);
            builder.UpdaterMethod((labelProxy, cell) => { cell.Value = labelProxy.Original.GlobalID; });
            builder.AddMultilevelChangeEvent(nameof(ItemProxy.Original), nameof(ISystemObject.GlobalID));
            builder.BuildAndAdd();

            foreach (Labelset labelset in LabelsetDatabase.Instance)
            {
                // Column: text
                builder = getColumnDescriptorBuilderForTable<ItemProxy>(labelsTableCDGV);
                builder.Type(DataGridViewColumnType.TextBox);
                builder.Header(labelset.ToString());
                builder.Width(100);
                builder.InitializerMethod((itemProxy, cell) => { cell.Value = itemProxy.TextFromLabelset(labelset); });
                builder.TextEditable(true);
                builder.CellEndEditHandlerMethod((itemProxy, cell, eventargs) =>
                {
                    try
                    {
                        itemProxy.TextToLabelset(labelset, cell.Value.ToString());
                    }
                    catch (ArgumentException e)
                    {
                        MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cell.Value = itemProxy.TextFromLabelset(labelset);
                    }
                });
                builder.BuildAndAdd();
            }

            // Bind collection
            labelsTableCDGV.BoundCollection = itemProxies;

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

        protected class ItemProxy : ObjectProxy<ISystemObject>
        {
            public ItemProxy(ISystemObject systemObject) : base(systemObject) { }
            public string TextFromLabelset(Labelset labelset) => labelset.GetText(Original);
            public void TextToLabelset(Labelset labelset, string text) => labelset.SetText(Original, text);
        }

    }

}
