using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Macros;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{

    [WindowTypeName("macros.macropanelslist")]
    public partial class MacroPanelList : ChildWindowWithTable
    {

        public MacroPanelList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<MacroPanel> table;

        private void initializeTable()
        {

            table = CreateTable<MacroPanel>();

            CustomDataGridViewColumnDescriptorBuilder<MacroPanel> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<MacroPanel>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((macroPanel, cell) => { cell.Value = string.Format("#{0}", macroPanel.ID); });
            builder.AddChangeEvent(nameof(Macro.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<MacroPanel>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Label");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((macroPanel, cell) => { cell.Value = macroPanel.Name; });
            builder.AddChangeEvent(nameof(Macro.Name));
            builder.BuildAndAdd();

            // Column: command count
            builder = GetColumnDescriptorBuilderForTable<MacroPanel>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Elements");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((macroPanel, cell) => { cell.Value = macroPanel.Elements.Count; });
            // TODO: builder.AddChangeEvent(nameof(MacroPanel.Elements));
            builder.BuildAndAdd();

            // Column: open button
            builder = GetColumnDescriptorBuilderForTable<MacroPanel>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Open");
            builder.Width(70);
            builder.ButtonText("Open");
            builder.CellContentClickHandlerMethod((macroPanel, cell, e) => {
                var window = new MacroPanelForm(macroPanel);
                window.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<MacroPanel>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((macroPanel, cell, e) => {
                var editWindow = new MacroPanelEditorForm(macroPanel);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<MacroPanel>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((macroPanel, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this macro panel?\n(#{0}) {1}", macroPanel.ID, macroPanel.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    MacroPanelDatabase.Instance.Remove(macroPanel);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = MacroPanelDatabase.Instance;

        }

        private void addMacroPanelButton_Click(object sender, EventArgs e)
        {
            var editWindow = new MacroPanelEditorForm(null);
            editWindow.ShowAsChild();
        }

    }

}