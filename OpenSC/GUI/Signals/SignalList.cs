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

        private CustomDataGridView<ISignal> table;

        private void initializeTable()
        {

            table = CreateTable<ISignal>();

            CustomDataGridViewColumnDescriptorBuilder<ISignal> builder;

            // Column: label
            builder = GetColumnDescriptorBuilderForTable<ISignal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Label");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((signal, cell) => { cell.Value = signal.SignalLabel; });
            builder.AddChangeEvent(nameof(ISignal.SignalLabel));
            builder.BuildAndAdd();

            // Column: red tally
            builder = GetColumnDescriptorBuilderForTable<ISignal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("TR");
            builder.Width(50);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.RedTally ? Color.Red : Color.LightGray); });
            builder.AddChangeEvent(nameof(ExternalSignal.RedTally));
            builder.BuildAndAdd();

            // Column: green tally
            builder = GetColumnDescriptorBuilderForTable<ISignal>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("TG");
            builder.Width(50);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.GreenTally ? Color.ForestGreen : Color.LightGray); });
            builder.AddChangeEvent(nameof(ExternalSignal.GreenTally));
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = SignalRegister.Instance;

        }
        
    }

}