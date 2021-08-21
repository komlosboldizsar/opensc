using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.Signals.TallyCopying.Helpers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Signals;
using OpenSC.Model.Signals.TallyCopying;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals.TallyCopying
{

    [WindowTypeName("signals.tallycopylist")]
    public partial class TallyCopyList : ChildWindowWithTable
    {

        public TallyCopyList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<TallyCopy> table;

        private void initializeTable()
        {

            table = CreateTable<TallyCopy>();

            CustomDataGridViewColumnDescriptorBuilder<TallyCopy> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<TallyCopy>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((tallyCopy, cell) => { cell.Value = string.Format("#{0}", tallyCopy.ID); });
            builder.AddChangeEvent(nameof(TallyCopy.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<TallyCopy>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((tallyCopy, cell) => { cell.Value = tallyCopy.Name; });
            builder.AddChangeEvent(nameof(TallyCopy.Name));
            builder.BuildAndAdd();

            // Column: from signal
            builder = GetColumnDescriptorBuilderForTable<TallyCopy>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("From signal");
            builder.Width(150);
            builder.UpdaterMethod((tallyCopy, cell) => { cell.Value = tallyCopy.FromSignal.SignalLabel; });
            builder.AddMultilevelChangeEvent(nameof(TallyCopy.FromSignal), nameof(ISignalSourceRegistered.SignalLabel));
            builder.BuildAndAdd();

            // Column: from color
            builder = GetColumnDescriptorBuilderForTable<TallyCopy>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("From color");
            builder.Width(70);
            builder.UpdaterMethod((tallyCopy, cell) => {
                cell.Value = signalTallyColorTranslations.Convert(tallyCopy.FromTallyColor);
                cell.Style.BackColor = tallyCopy.FromTallyColor.ConvertToLightColor();
            });
            builder.AddChangeEvent(nameof(TallyCopy.FromTallyColor));
            builder.BuildAndAdd();

            // Column: to signal
            builder = GetColumnDescriptorBuilderForTable<TallyCopy>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("To signal");
            builder.Width(150);
            builder.UpdaterMethod((tallyCopy, cell) => { cell.Value = tallyCopy.ToSignal.SignalLabel; });
            builder.AddMultilevelChangeEvent(nameof(TallyCopy.ToSignal), nameof(ISignalSourceRegistered.SignalLabel));
            builder.BuildAndAdd();

            // Column: to color
            builder = GetColumnDescriptorBuilderForTable<TallyCopy>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("To color");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((tallyCopy, cell) => {
                cell.Value = signalTallyColorTranslations.Convert(tallyCopy.ToTallyColor);
                cell.Style.BackColor = tallyCopy.ToTallyColor.ConvertToLightColor();
            });
            builder.AddChangeEvent(nameof(TallyCopy.ToTallyColor));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<TallyCopy>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((tallyCopy, cell, e) => {
                var editWindow = new TallyCopyEditorForm(tallyCopy);
                editWindow.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<TallyCopy>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((tallyCopy, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this tally copy?\n(#{0}) {1}", tallyCopy.ID, tallyCopy.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    TallyCopyDatabase.Instance.Remove(tallyCopy);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = TallyCopyDatabase.Instance;

        }

        private void addSignalButton_Click(object sender, EventArgs e)
        {
            var editWindow = new TallyCopyEditorForm(null);
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