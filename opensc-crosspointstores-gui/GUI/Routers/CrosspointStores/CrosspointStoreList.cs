using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.CrosspointStores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.CrosspointStores
{

    [WindowTypeName("routers.crosspointstorelist")]
    public partial class CrosspointStoreList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "crosspoint store";
        protected override string SubjectPlural { get; } = "crosspoint stores";

        protected override IModelEditorForm ModelEditorForm { get; } = new CrosspointStoreEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<CrosspointStore>(this, CrosspointStoreDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<CrosspointStore> table, ItemListFormBaseManager<CrosspointStore>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<CrosspointStore> builder;

            // Column: ID, name
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: current output
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current output");
            builder.Width(150);
            builder.UpdaterMethod((crosspointStore, cell) => {
                if (crosspointStore.StoredOutput == null)
                {
                    cell.Value = "-";
                    return;
                }
                cell.Value = string.Format("(#{0}) {1} / (#{2}) {3}",
                    crosspointStore.StoredOutput?.Router?.ID, crosspointStore.StoredOutput?.Router?.Name,
                    crosspointStore.StoredOutput?.Index, crosspointStore.StoredOutput?.Name);
            });
            builder.AddMultilevelChangeEvent(nameof(CrosspointStore.StoredOutput), nameof(RouterOutput.Router), nameof(Router.ID));
            builder.AddMultilevelChangeEvent(nameof(CrosspointStore.StoredOutput), nameof(RouterOutput.Router), nameof(Router.Name));
            builder.AddMultilevelChangeEvent(nameof(CrosspointStore.StoredOutput), nameof(RouterOutput.Index));
            builder.AddMultilevelChangeEvent(nameof(CrosspointStore.StoredOutput), nameof(RouterOutput.Name));

            // Column: current input
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current input");
            builder.Width(150);
            builder.UpdaterMethod((crosspointStore, cell) => {
                if (crosspointStore.StoredInput == null)
                {
                    cell.Value = "-";
                    return;
                }
                cell.Value = string.Format("(#{0}) {1} / (#{2}) {3}",
                    crosspointStore.StoredInput?.Router?.ID, crosspointStore.StoredInput?.Router?.Name,
                    crosspointStore.StoredInput?.Index, crosspointStore.StoredInput?.Name);
                bool invalidCrosspoint = (crosspointStore.StoredInput?.Router != null) && (crosspointStore.StoredOutput?.Router != null)
                    && (crosspointStore.StoredInput.Router != crosspointStore.StoredOutput.Router);
                cell.Style.ForeColor = invalidCrosspoint ? TEXT_COLOR_INVALID_CROSSPOINT : TEXT_COLOR_VALID_CROSSPOINT;
            });
            builder.AddChangeEvent(nameof(CrosspointStore.StoredInput));
            builder.AddChangeEvent(nameof(CrosspointStore.StoredOutput));
            builder.AddMultilevelChangeEvent(nameof(CrosspointStore.StoredInput), nameof(RouterInput.Router), nameof(Router.ID));
            builder.AddMultilevelChangeEvent(nameof(CrosspointStore.StoredInput), nameof(RouterInput.Router), nameof(Router.Name));
            builder.AddMultilevelChangeEvent(nameof(CrosspointStore.StoredInput), nameof(RouterInput.Index));
            builder.AddMultilevelChangeEvent(nameof(CrosspointStore.StoredInput), nameof(RouterInput.Name));
     
            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private static readonly Color TEXT_COLOR_INVALID_CROSSPOINT = Color.Red;
        private static readonly Color TEXT_COLOR_VALID_CROSSPOINT = Color.Black;

    }

}