using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.Signals.TallyCopying.Helpers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Signals;
using OpenSC.Model.Signals.BooleanTallies;
using OpenSC.Model.Signals.TallyCopying;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals.BooleanTallies
{

    [WindowTypeName("signals.booleantallylist")]
    public partial class BooleanTallyList : ChildWindowWithTable
    {

        public BooleanTallyList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<BooleanTally> table;

        private void initializeTable()
        {

            table = CreateTable<BooleanTally>();

            CustomDataGridViewColumnDescriptorBuilder<BooleanTally> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<BooleanTally>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((booleanTally, cell) => { cell.Value = string.Format("#{0}", booleanTally.ID); });
            builder.AddChangeEvent(nameof(BooleanTally.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<BooleanTally>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((booleanTally, cell) => { cell.Value = booleanTally.Name; });
            builder.AddChangeEvent(nameof(BooleanTally.Name));
            builder.BuildAndAdd();

            // Column: from boolean
            builder = GetColumnDescriptorBuilderForTable<BooleanTally>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("From boolean");
            builder.Width(150);
            builder.UpdaterMethod((booleanTally, cell) => { cell.Value = booleanTally.FromBoolean.Name; });
            builder.AddMultilevelChangeEvent(nameof(BooleanTally.FromBoolean), nameof(IBoolean.Name));
            builder.BuildAndAdd();

            // Column: to signal
            builder = GetColumnDescriptorBuilderForTable<BooleanTally>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("To signal");
            builder.Width(150);
            builder.UpdaterMethod((booleanTally, cell) => { cell.Value = booleanTally.ToSignal.SignalLabel; });
            builder.AddMultilevelChangeEvent(nameof(BooleanTally.ToSignal), nameof(ISignalSourceRegistered.SignalLabel));
            builder.BuildAndAdd();

            // Column: to color
            builder = GetColumnDescriptorBuilderForTable<BooleanTally>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("To color");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((booleanTally, cell) => {
                cell.Value = signalTallyColorTranslations.Convert(booleanTally.ToTallyColor);
                cell.Style.BackColor = booleanTally.ToTallyColor.ConvertToLightColor();
            });
            builder.AddChangeEvent(nameof(BooleanTally.ToTallyColor));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<BooleanTally>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((booleanTally, cell, e) => {
                var editWindow = new BooleanTallyEditorForm(booleanTally);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<BooleanTally>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((booleanTally, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this boolean tally?\n(#{0}) {1}", booleanTally.ID, booleanTally.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    BooleanTallyDatabase.Instance.Remove(booleanTally);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = BooleanTallyDatabase.Instance;

        }

        private void addBooleanTallyButton_Click(object sender, EventArgs e)
        {
            var editWindow = new BooleanTallyEditorForm(null);
            editWindow.ShowAsChild();
        }

        private EnumToStringConverter<SignalTallyColor> signalTallyColorTranslations = new EnumToStringConverter<SignalTallyColor>()
        {
            { SignalTallyColor.Red, "red" },
            { SignalTallyColor.Yellow, "yellow" },
            { SignalTallyColor.Green, "green" },
        };

    }

}