using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.CrosspointBooleans;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.CrosspointBooleans
{

    [WindowTypeName("routers.crosspointbooleanlist")]
    public partial class CrosspointBooleanList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "crosspoint boolean";
        protected override string SubjectPlural { get; } = "crosspoint booleans";

        protected override IModelEditorForm ModelEditorForm { get; } = new CrosspointBooleanEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<CrosspointBoolean>(this, CrosspointBooleanDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<CrosspointBoolean> table, ItemListFormBaseManager<CrosspointBoolean>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<CrosspointBoolean> builder;

            // Column: ID, name
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: router
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Router");
            builder.Width(150);
            builder.UpdaterMethod((crosspointBoolean, cell) => {
                if (crosspointBoolean.WatchedRouter == null)
                {
                    cell.Value = "-";
                    return;
                }
                cell.Value = string.Format("(#{0}) {1}", crosspointBoolean.WatchedRouter?.ID, crosspointBoolean.WatchedRouter?.Name);
            });
            builder.AddMultilevelChangeEvent(nameof(CrosspointBoolean.WatchedRouter), nameof(Router.ID));
            builder.AddMultilevelChangeEvent(nameof(CrosspointBoolean.WatchedRouter), nameof(Router.Name));

            // Column: output
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Output");
            builder.Width(150);
            builder.UpdaterMethod((crosspointBoolean, cell) => {
                if (crosspointBoolean.WatchedOutput == null)
                {
                    cell.Value = "-";
                    return;
                }
                cell.Value = string.Format("(#{0}) {1}", crosspointBoolean.WatchedOutput?.Index, crosspointBoolean.WatchedOutput?.Name);
            });
            builder.AddMultilevelChangeEvent(nameof(CrosspointBoolean.WatchedOutput), nameof(RouterOutput.Index));
            builder.AddMultilevelChangeEvent(nameof(CrosspointBoolean.WatchedOutput), nameof(RouterOutput.Name));

            // Column: input
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Input");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((crosspointBoolean, cell) => {
                if (crosspointBoolean.WatchedInput == null)
                {
                    cell.Value = "-";
                    return;
                }
                cell.Value = string.Format("(#{0}) {1}", crosspointBoolean.WatchedInput?.Index, crosspointBoolean.WatchedInput?.Name);
            });
            builder.AddMultilevelChangeEvent(nameof(CrosspointBoolean.WatchedInput), nameof(RouterInput.Index));
            builder.AddMultilevelChangeEvent(nameof(CrosspointBoolean.WatchedInput), nameof(RouterInput.Name));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}