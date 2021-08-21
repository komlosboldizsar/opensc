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
    public partial class CrosspointStoreList : ChildWindowWithTable
    {

        public CrosspointStoreList()
        {
            InitializeComponent();
            initializeTable();
        }

        private static readonly Color TEXT_COLOR_INVALID_CROSSPOINT = Color.Red;
        private static readonly Color TEXT_COLOR_VALID_CROSSPOINT = Color.Black;

        private CustomDataGridView<CrosspointStore> table;

        private void initializeTable()
        {

            table = CreateTable<CrosspointStore>();

            CustomDataGridViewColumnDescriptorBuilder<CrosspointStore> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<CrosspointStore>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((crosspointStore, cell) => { cell.Value = string.Format("#{0}", crosspointStore.ID); });
            builder.AddChangeEvent(nameof(CrosspointStore.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<CrosspointStore>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((crosspointStore, cell) => { cell.Value = crosspointStore.Name; });
            builder.AddChangeEvent(nameof(CrosspointStore.Name));
            builder.BuildAndAdd();

            // Column: current output
            builder = GetColumnDescriptorBuilderForTable<CrosspointStore>();
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
            builder.BuildAndAdd();

            // Column: current input
            builder = GetColumnDescriptorBuilderForTable<CrosspointStore>();
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
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<CrosspointStore>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((crosspointStore, cell, e) => {
                var editWindow = new CrosspointStoreEditorForm(crosspointStore);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<CrosspointStore>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((crosspointStore, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this crosspoint store?\n(#{0}) {1}", crosspointStore.ID, crosspointStore.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    CrosspointStoreDatabase.Instance.Remove(crosspointStore);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = CrosspointStoreDatabase.Instance;

        }

        private void addCrosspointStoreButton_Click(object sender, EventArgs e)
        {
            var editWindow = new CrosspointStoreEditorForm(null);
            editWindow.ShowAsChild();
        }

    }

}