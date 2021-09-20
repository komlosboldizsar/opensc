using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Routers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{

    [WindowTypeName("routers.labelsetlist")]
    public partial class LabelsetList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "labelset";
        protected override string SubjectPlural { get; } = "labelsets";

        protected override IModelEditorForm ModelEditorForm { get; } = new LabelsetEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<Labelset>(this, LabelsetDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Labelset> table, ItemListFormBaseManager<Labelset>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {
            // Column: ID, name
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);
            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);
        }

    }

}