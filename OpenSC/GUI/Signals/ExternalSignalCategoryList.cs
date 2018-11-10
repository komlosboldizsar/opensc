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
    public partial class ExternalSignalCategoryList : ChildWindowWithTable
    {

        public ExternalSignalCategoryList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<ExternalSignalCategory> table;

        private void initializeTable()
        {

            table = CreateTable<ExternalSignalCategory>();

            CustomDataGridViewColumnDescriptorBuilder<ExternalSignalCategory> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<ExternalSignalCategory>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((category, cell) => { cell.Value = string.Format("#{0}", category.ID); });
            builder.AddChangeEvent(nameof(ExternalSignalCategory.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<ExternalSignalCategory>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((category, cell) => { cell.Value = category.Name; });
            builder.AddChangeEvent(nameof(ExternalSignalCategory.Name));
            builder.BuildAndAdd();

            // Column: color
            builder = GetColumnDescriptorBuilderForTable<ExternalSignalCategory>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("");
            builder.Width(30);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((category, cell) => { cell.Style.BackColor = category.Color; });
            builder.AddChangeEvent(nameof(ExternalSignalCategory.Color));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<ExternalSignalCategory>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((category, cell, e) => {
                var editWindow = new ExternalSignalCategoryEditorForm(category);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<ExternalSignalCategory>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((category, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this signal category?\n(#{0}) {1}", category.ID, category.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    ExternalSignalDatabases.Categories.Remove(category);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = ExternalSignalDatabases.Categories;

        }

        private void addSignalCategoryButton_Click(object sender, EventArgs e)
        {
            var editWindow = new ExternalSignalCategoryEditorForm(null);
            editWindow.ShowAsChild();
        }

    }

}