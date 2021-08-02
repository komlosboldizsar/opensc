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

        private CustomDataGridView<ISignalSourceRegistered> table;

        private void initializeTable()
        {

            table = CreateTable<ISignalSourceRegistered>();

            CustomDataGridViewColumnDescriptorBuilder<ISignalSourceRegistered> builder;

            // Column: label
            builder = GetColumnDescriptorBuilderForTable<ISignalSourceRegistered>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Label");
            builder.Width(300);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((signal, cell) => { cell.Value = signal.SignalLabel; });
            builder.AddChangeEvent(nameof(ISignalSourceRegistered.SignalLabel));
            builder.BuildAndAdd();

            // Column: red tally
            builder = GetColumnDescriptorBuilderForTable<ISignalSourceRegistered>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("TR");
            builder.Width(50);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.RedTally.State ? Color.Red : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.RedTally), nameof(IBidirectionalSignalTally.State));
            builder.BuildAndAdd();

            // Column: green tally
            builder = GetColumnDescriptorBuilderForTable<ISignalSourceRegistered>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("TG");
            builder.Width(50);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.GreenTally.State ? Color.ForestGreen : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.GreenTally), nameof(IBidirectionalSignalTally.State));
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = SignalRegister.Instance;

        }
        
    }

}