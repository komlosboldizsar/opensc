using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.CrosspointBooleans;
using OpenSC.Model.Routers.CrosspointStores;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.CrosspointBooleans
{

    [WindowTypeName("routers.crosspointbooleanlist")]
    public partial class CrosspointBooleanList : ChildWindowWithTable
    {

        public CrosspointBooleanList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<CrosspointBoolean> table;

        private void initializeTable()
        {

            table = CreateTable<CrosspointBoolean>();

            CustomDataGridViewColumnDescriptorBuilder<CrosspointBoolean> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<CrosspointBoolean>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((crosspointBoolean, cell) => { cell.Value = string.Format("#{0}", crosspointBoolean.ID); });
            builder.AddChangeEvent(nameof(CrosspointBoolean.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<CrosspointBoolean>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((crosspointBoolean, cell) => { cell.Value = crosspointBoolean.Name; });
            builder.AddChangeEvent(nameof(CrosspointBoolean.Name));
            builder.BuildAndAdd();

            // Column: router
            builder = GetColumnDescriptorBuilderForTable<CrosspointBoolean>();
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
            builder.BuildAndAdd();

            // Column: output
            builder = GetColumnDescriptorBuilderForTable<CrosspointBoolean>();
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
            builder.BuildAndAdd();

            // Column: input
            builder = GetColumnDescriptorBuilderForTable<CrosspointBoolean>();
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
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<CrosspointBoolean>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((crosspointBoolean, cell, e) => {
                var editWindow = new CrosspointBooleanEditorForm(crosspointBoolean);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<CrosspointBoolean>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((crosspointBoolean, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this crosspoint boolean?\n(#{0}) {1}", crosspointBoolean.ID, crosspointBoolean.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    CrosspointBooleanDatabase.Instance.Remove(crosspointBoolean);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = CrosspointBooleanDatabase.Instance;

        }

        private void addCrosspointBooleanButton_Click(object sender, EventArgs e)
        {
            var editWindow = new CrosspointBooleanEditorForm(null);
            editWindow.ShowAsChild();
        }

    }

}