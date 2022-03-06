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
    public partial class RouterMirrorList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "router mirror";
        protected override string SubjectPlural { get; } = "router mirrors";

        protected override IModelEditorForm ModelEditorForm { get; } = new RouterMirrorEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<RouterMirror>(this, RouterMirrorDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<RouterMirror> table, ItemListFormBaseManager<RouterMirror>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<RouterMirror> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: router A
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Router A");
            builder.Width(150);
            builder.UpdaterMethod((routerMirror, cell) => routerRefCellUpdaterMethod(routerMirror, cell, RouterMirrorSide.SideA));
            builder.AddChangeEvent(nameof(RouterMirror.RouterA));

            // Column: router B
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Router B");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((routerMirror, cell) => routerRefCellUpdaterMethod(routerMirror, cell, RouterMirrorSide.SideB));
            builder.AddChangeEvent(nameof(RouterMirror.RouterB));

            // Column: sync A->B button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.DisableButton);
            builder.Header("Sync A→B");
            builder.Width(70);
            builder.ButtonText("Sync A→B");
            builder.UpdaterMethod(syncButtonUpdaterMethod);
            builder.CellContentClickHandlerMethod((routerMirror, cell, e) => syncButtonClickMethod(routerMirror, RouterMirrorSide.SideA));

            // Column: sync B->A button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.DisableButton);
            builder.Header("Sync B→A");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Sync B→A");
            builder.UpdaterMethod(syncButtonUpdaterMethod);
            builder.CellContentClickHandlerMethod((routerMirror, cell, e) => syncButtonClickMethod(routerMirror, RouterMirrorSide.SideB));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

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

    }

}