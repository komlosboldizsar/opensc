using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Variables
{

    [WindowTypeName("variables.dynamictextlist")]
    public partial class DynamicTextList : ChildWindowWithTable
    {

        public DynamicTextList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<DynamicText> table;

        private void initializeTable()
        {

            table = CreateTable<DynamicText>();

            CustomDataGridViewColumnDescriptorBuilder<DynamicText> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<DynamicText>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((dyntext, cell) => { cell.Value = string.Format("#{0}", dyntext.ID); });
            builder.AddChangeEvent(nameof(DynamicText.ID));
            builder.BuildAndAdd();

            // Column: label
            builder = GetColumnDescriptorBuilderForTable<DynamicText>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Label");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((dyntext, cell) => { cell.Value = dyntext.Name; });
            builder.AddChangeEvent(nameof(DynamicText.Name));
            builder.BuildAndAdd();

            // Column: current text
            builder = GetColumnDescriptorBuilderForTable<DynamicText>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current text");
            builder.Width(200);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((dyntext, cell) => { cell.Value = dyntext.CurrentText; });
            builder.AddChangeEvent(nameof(DynamicText.CurrentText));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<DynamicText>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((dyntext, cell, e) => {
                var editWindow = new DynamicTextEditorForm(dyntext);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<DynamicText>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((dyntext, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this dynamic text?\n(#{0}) {1}", dyntext.ID, dyntext.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    DynamicTextDatabase.Instance.Remove(dyntext);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = DynamicTextDatabase.Instance;

        }

        private void addDynamicTextButton_Click(object sender, EventArgs e)
        {
            var editWindow = new DynamicTextEditorForm(null);
            editWindow.ShowAsChild();
        }

    }

}