using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Macros;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{

    [WindowTypeName("macros.macroslist")]
    public partial class MacroList : ChildWindowWithTable
    {

        public MacroList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<Macro> table;

        private void initializeTable()
        {

            table = CreateTable<Macro>();

            CustomDataGridViewColumnDescriptorBuilder<Macro> builder;

            // Custom cell styles
            DataGridViewCellStyle runButtonCellStyle = table.DefaultCellStyle.Clone();
            runButtonCellStyle.Font = new Font(runButtonCellStyle.Font, FontStyle.Bold);
            runButtonCellStyle.BackColor = Color.FromArgb(255, 145, 61);

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<Macro>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((macro, cell) => { cell.Value = string.Format("#{0}", macro.ID); });
            builder.AddChangeEvent(nameof(Macro.ID));
            builder.BuildAndAdd();

            // Column: label
            builder = GetColumnDescriptorBuilderForTable<Macro>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Label");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((macro, cell) => { cell.Value = macro.Name; });
            builder.AddChangeEvent(nameof(Macro.Name));
            builder.BuildAndAdd();

            // Column: command count
            builder = GetColumnDescriptorBuilderForTable<Macro>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Commands");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((macro, cell) => { cell.Value = macro.Commands.Count; });
            // TODO: builder.AddChangeEvent(nameof(Macro.Commands));
            builder.BuildAndAdd();

            // Column: run button
            builder = GetColumnDescriptorBuilderForTable<Macro>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Run");
            builder.Width(100);
            builder.ButtonText("Run");
            builder.CellStyle(runButtonCellStyle);
            builder.CellContentClickHandlerMethod((macro, cell, e) => { /* TODO: macro.Run(); */ });
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<Macro>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((macro, cell, e) => {
                /*var editWindow = new MacroEditorForm(macro);
                editWindow.ShowAsChild();*/
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<Macro>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((macro, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this macro?\n(#{0}) {1}", macro.ID, macro.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    MacroDatabase.Instance.Remove(macro);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = MacroDatabase.Instance;

        }

        private void addDynamicTextButton_Click(object sender, EventArgs e)
        {
            /*var editWindow = new MacroEditorForm(null);
            editWindow.ShowAsChild();*/
        }

    }

}