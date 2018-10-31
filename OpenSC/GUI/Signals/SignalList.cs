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

    [WindowTypeName("signals.signallist")]
    public partial class SignalList : ChildWindowWithTable
    {

        public SignalList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<Signal> table;

        private void initializeTable()
        {

            table = CreateTable<Signal>();

            CustomDataGridViewColumnDescriptorBuilder<Signal> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<Signal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((signal, cell) => { cell.Value = string.Format("#{0}", signal.ID); });
            builder.AddChangeEvent(nameof(Signal.IdChangedPCN));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<Signal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((signal, cell) => { cell.Value = signal.Name; });
            builder.AddChangeEvent(nameof(Signal.NameChangedPCN));
            builder.BuildAndAdd();

            // Column: category
            builder = GetColumnDescriptorBuilderForTable<Signal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Category");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((signal, cell) => { cell.Value = string.Format("(#{0}) {1}", signal.Category.ID, signal.Category.Name); });
            builder.AddChangeEvent(nameof(Signal.CategoryChangedPCN));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<Signal>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((signal, cell, e) => {
                var editWindow = new SignalEditorForm(signal);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<Signal>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((signal, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this signal?\n(#{0}) {1}", signal.ID, signal.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    SignalDatabases.Signals.Remove(signal);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = SignalDatabases.Signals;

        }

        private void addSignalButton_Click(object sender, EventArgs e)
        {
            var editWindow = new SignalEditorForm(null);
            editWindow.ShowAsChild();
        }
    }

}