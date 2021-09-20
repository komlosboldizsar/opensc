using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals
{

    [WindowTypeName("signals.signalcategorylist")]
    public partial class ExternalSignalCategoryList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "external signal category";
        protected override string SubjectPlural { get; } = "external signal categories";

        protected override IModelEditorForm ModelEditorForm { get; } = new ExternalSignalCategoryEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<ExternalSignalCategory>(this, ExternalSignalDatabases.Categories, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<ExternalSignalCategory> table, ItemListFormBaseManager<ExternalSignalCategory>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<ExternalSignalCategory> builder;

            // Column: ID, name
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: color
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("");
            builder.Width(30);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((category, cell) => { cell.Style.BackColor = category.Color; });
            builder.AddChangeEvent(nameof(ExternalSignalCategory.Color));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}