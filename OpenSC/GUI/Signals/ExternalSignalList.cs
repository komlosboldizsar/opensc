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

    [WindowTypeName("signals.externalsignallist")]
    public partial class ExternalSignalList : ChildWindowWithTable
    {

        public ExternalSignalList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<ExternalSignal> table;

        private void initializeTable()
        {

            table = CreateTable<ExternalSignal>();

            CustomDataGridViewColumnDescriptorBuilder<ExternalSignal> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<ExternalSignal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((signal, cell) => { cell.Value = string.Format("#{0}", signal.ID); });
            builder.AddChangeEvent(nameof(ExternalSignal.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<ExternalSignal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((signal, cell) => { cell.Value = signal.Name; });
            builder.AddChangeEvent(nameof(ExternalSignal.Name));
            builder.BuildAndAdd();

            // Column: category
            builder = GetColumnDescriptorBuilderForTable<ExternalSignal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Category");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((signal, cell) => {
                cell.Value = (signal.Category != null) ? string.Format("(#{0}) {1}", signal.Category.ID, signal.Category.Name) : "(not associated)";
                cell.Style.BackColor = (signal.Category != null) ? signal.Category.Color : table.DefaultCellStyle.BackColor;
            });
            builder.ExternalUpdateEventSubscriberMethod((signal, updateInvoker) =>
            {
                ExternalSignalCategory.IdChangedDelegate signalCategoryIdChangedHandler = (_category, oldCategory, newCategory) => updateInvoker();
                ExternalSignalCategory.NameChangedDelegate signalCategoryNameChangedHandler = (_category, oldName, newName) => updateInvoker();
                ExternalSignalCategory.ColorChangedDelegate signalCategoryColorChangedHandler = (_category, oldColor, newColor) => updateInvoker();
                if (signal.Category != null)
                {
                    signal.Category.IdChanged += signalCategoryIdChangedHandler;
                    signal.Category.NameChanged += signalCategoryNameChangedHandler;
                    signal.Category.ColorChanged += signalCategoryColorChangedHandler;
                }
                signal.CategoryChanged += (_signal, oldCategory, newCategory) => {
                    if(oldCategory != null)
                    {
                        oldCategory.IdChanged -= signalCategoryIdChangedHandler;
                        oldCategory.NameChanged -= signalCategoryNameChangedHandler;
                        oldCategory.ColorChanged -= signalCategoryColorChangedHandler;
                    }
                    if(newCategory != null)
                    {
                        newCategory.IdChanged += signalCategoryIdChangedHandler;
                        newCategory.NameChanged += signalCategoryNameChangedHandler;
                        newCategory.ColorChanged += signalCategoryColorChangedHandler;
                    }
                };
            });
            builder.AddChangeEvent(nameof(ExternalSignal.Category));
            builder.BuildAndAdd();

            // Column: red tally
            builder = GetColumnDescriptorBuilderForTable<ExternalSignal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("TR");
            builder.Width(50);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.RedTally.State ? Color.Red : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.RedTally), nameof(IBidirectionalSignalTally.State));
            builder.BuildAndAdd();

            // Column: green tally
            builder = GetColumnDescriptorBuilderForTable<ExternalSignal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("TG");
            builder.Width(50);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.GreenTally.State ? Color.ForestGreen : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.GreenTally), nameof(IBidirectionalSignalTally.State));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<ExternalSignal>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((signal, cell, e) => {
                var editWindow = new ExternalSignalEditorForm(signal);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<ExternalSignal>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((signal, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this signal?\n(#{0}) {1}", signal.ID, signal.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    ExternalSignalDatabases.Signals.Remove(signal);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = ExternalSignalDatabases.Signals;

        }

        private void addSignalButton_Click(object sender, EventArgs e)
        {
            var editWindow = new ExternalSignalEditorForm(null);
            editWindow.ShowAsChild();
        }
    }

}