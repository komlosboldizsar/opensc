using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Labelsets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Labelsets
{

    [WindowTypeName("labelsets.labelsetlist")]
    public partial class LabelsetList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "labelset";
        protected override string SubjectPlural { get; } = "labelsets";

        protected override IModelEditorForm ModelEditorForm { get; } = new LabelsetEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<Labelset>(this, LabelsetDatabase.Instance, baseColumnCreator);

        public LabelsetList() => InitializeComponent();

        private void baseColumnCreator(CustomDataGridView<Labelset> table, ItemListFormBaseManager<Labelset>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {
            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);
            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);
        }

        private void editAllButton_Click(object sender, EventArgs e)
            => (new AllLabelsetEditorForm()).ShowAsChild();

    }

}