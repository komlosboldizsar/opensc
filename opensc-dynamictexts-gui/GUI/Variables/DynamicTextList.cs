using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Variables
{

    [WindowTypeName("variables.dynamictextlist")]
    public partial class DynamicTextList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "dynamic text";
        protected override string SubjectPlural { get; } = "dynamic texts";

        protected override IModelEditorForm ModelEditorForm { get; } = new DynamicTextEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<DynamicText>(this, DynamicTextDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<DynamicText> table, ItemListFormBaseManager<DynamicText>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<DynamicText> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: current text
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current text");
            builder.Width(200);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((dyntext, cell) => { cell.Value = dyntext.CurrentText; });
            builder.AddChangeEvent(nameof(DynamicText.CurrentText));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}