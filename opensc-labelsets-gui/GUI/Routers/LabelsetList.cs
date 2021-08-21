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
    public partial class LabelsetList : ChildWindowWithTable
    {

        public LabelsetList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<Labelset> table;

        private void initializeTable()
        {

            table = CreateTable<Labelset>();

            CustomDataGridViewColumnDescriptorBuilder<Labelset> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<Labelset>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((router, cell) => { cell.Value = string.Format("#{0}", router.ID); });
            builder.AddChangeEvent(nameof(Router.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<Labelset>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((router, cell) => { cell.Value = router.Name; });
            builder.AddChangeEvent(nameof(Router.Name));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<Labelset>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((labelset, cell, e) => {
                var editWindow = new LabelsetEditorForm(labelset);
                editWindow?.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<Labelset>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((labelset, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this labelset?\n(#{0}) {1}", labelset.ID, labelset.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    LabelsetDatabase.Instance.Remove(labelset);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = LabelsetDatabase.Instance;

        }

        private static void addLabelsetMenuItemClick(object sender, EventArgs e)
        {
            var editWindow = new LabelsetEditorForm(null);
            editWindow.ShowAsChild();
        }

    }

}