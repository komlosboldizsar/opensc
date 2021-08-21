using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Mirrors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.Mirrors
{

    [WindowTypeName("routers.mirrorlist")]
    public partial class RouterMirrorList : ChildWindowWithTable
    {

        public RouterMirrorList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<RouterMirror> table;

        private void initializeTable()
        {

            table = CreateTable<RouterMirror>();

            CustomDataGridViewColumnDescriptorBuilder<RouterMirror> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<RouterMirror>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((routerMirror, cell) => { cell.Value = string.Format("#{0}", routerMirror.ID); });
            builder.AddChangeEvent(nameof(RouterMirror.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<RouterMirror>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((routerMirror, cell) => { cell.Value = routerMirror.Name; });
            builder.AddChangeEvent(nameof(RouterMirror.Name));
            builder.BuildAndAdd();

            // Column: router A
            builder = GetColumnDescriptorBuilderForTable<RouterMirror>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Router A");
            builder.Width(150);
            builder.UpdaterMethod((routerMirror, cell) => routerRefCellUpdaterMethod(routerMirror, cell, RouterMirrorSide.SideA));
            builder.AddChangeEvent(nameof(RouterMirror.RouterA));
            builder.BuildAndAdd();

            // Column: router B
            builder = GetColumnDescriptorBuilderForTable<RouterMirror>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Router B");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((routerMirror, cell) => routerRefCellUpdaterMethod(routerMirror, cell, RouterMirrorSide.SideB));
            builder.AddChangeEvent(nameof(RouterMirror.RouterB));
            builder.BuildAndAdd();

            // Column: sync A->B button
            builder = GetColumnDescriptorBuilderForTable<RouterMirror>();
            builder.Type(DataGridViewColumnType.DisableButton);
            builder.Header("Sync A→B");
            builder.Width(70);
            builder.ButtonText("Sync A→B");
            builder.UpdaterMethod(syncButtonUpdaterMethod);
            builder.CellContentClickHandlerMethod((routerMirror, cell, e) => syncButtonClickMethod(routerMirror, RouterMirrorSide.SideA));
            builder.BuildAndAdd();

            // Column: sync B->A button
            builder = GetColumnDescriptorBuilderForTable<RouterMirror>();
            builder.Type(DataGridViewColumnType.DisableButton);
            builder.Header("Sync B→A");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Sync B→A");
            builder.UpdaterMethod(syncButtonUpdaterMethod);
            builder.CellContentClickHandlerMethod((routerMirror, cell, e) => syncButtonClickMethod(routerMirror, RouterMirrorSide.SideB));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<RouterMirror>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((routerMirror, cell, e) => {
                var editWindow = new RouterMirrorEditorForm(routerMirror);
                editWindow?.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<RouterMirror>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((routerMirror, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this router mirror?\n(#{0}) {1}", routerMirror.ID, routerMirror.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    RouterMirrorDatabase.Instance.Remove(routerMirror);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = RouterMirrorDatabase.Instance;

        }

        private void routerRefCellUpdaterMethod(RouterMirror routerMirror, DataGridViewCell cell, RouterMirrorSide side)
        {
            Router router = side.Choose(routerMirror.RouterA, routerMirror.RouterB);
            if (router == null)
            {
                cell.Value = "-";
                return;
            }
            cell.Value = string.Format("(#{0}) {1}", router.ID, router.Name);
        }

        private void syncButtonUpdaterMethod(RouterMirror routerMirror, DataGridViewCell cell)
            => ((DataGridViewDisableButtonCell)cell).Enabled = ((routerMirror.RouterA != null) && (routerMirror.RouterA != null));

        private void syncButtonClickMethod(RouterMirror routerMirror, RouterMirrorSide sourceSide)
        {
            if ((routerMirror.RouterA == null) || (routerMirror.RouterB == null))
                return;
            Router sourceRouter = sourceSide.Choose(routerMirror.RouterA, routerMirror.RouterB);
            Router destinationRouter = sourceSide.Choose(routerMirror.RouterA, routerMirror.RouterB, true);
            string messageBoxText = string.Format("Are sure you want to synchronize crosspoints from router [(#{0}) {1}] to router [(#{2}) {3}]?",
                sourceRouter.ID, sourceRouter.Name, destinationRouter.ID, destinationRouter.Name);
            if (MessageBox.Show(messageBoxText, "Router synchronization", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                routerMirror.Synchronize(sourceSide);
        }

        private void addRouterMirrorButton_Click(object sender, EventArgs e)
        {
            var editWindow = new RouterMirrorEditorForm(null);
            editWindow.ShowAsChild();
        }

    }

}