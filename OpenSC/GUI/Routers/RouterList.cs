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

    [WindowTypeName("routers.routerlist")]
    public partial class RouterList : ChildWindowWithTable
    {

        public RouterList()
        {
            InitializeComponent();
            initializeTable();
            loadAddableRouterTypes();
        }

        private CustomDataGridView<Router> table;

        private void initializeTable()
        {

            table = CreateTable<Router>();

            CustomDataGridViewColumnDescriptorBuilder<Router> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<Router>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((router, cell) => { cell.Value = string.Format("#{0}", router.ID); });
            builder.AddChangeEvent(nameof(Router.IdChangedPCN));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<Router>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((router, cell) => { cell.Value = router.Name; });
            builder.AddChangeEvent(nameof(Router.NameChangedPCN));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<Router>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((router, cell, e) => {
                var editWindow = RouterEditorFormTypeRegister.Instance.GetFormForModel(router) as ChildWindowBase;
                editWindow?.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<Router>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((router, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this router?\n(#{0}) {1}", router.ID, router.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    RouterDatabase.Instance.Remove(router);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = RouterDatabase.Instance;

        }
        
        private void loadAddableRouterTypes()
        {
            foreach (Type type in RouterEditorFormTypeRegister.Instance.RegisteredTypes)
            {
                string label = type.GetTypeLabel();
                ToolStripMenuItem contextMenuItem = new ToolStripMenuItem(label)
                {
                    Tag = type
                };
                contextMenuItem.Click += addRouterMenuItemClick;
                addableRouterTypesMenu.Items.Add(contextMenuItem);
            }
        }

        private static void addRouterMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem typedSender = sender as ToolStripMenuItem;
            Type newRouterType = typedSender?.Tag as Type;
            IModelEditorForm<Router> editorForm = RouterEditorFormTypeRegister.Instance.GetFormForType(newRouterType);
            (editorForm as ChildWindowBase)?.ShowAsChild();
        }

    }

}