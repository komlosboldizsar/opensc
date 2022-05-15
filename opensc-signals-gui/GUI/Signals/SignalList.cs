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
    public partial class SignalList : ItemListFormBase
    {

        protected override string SubjectSingular { get; } = "signal";
        protected override string SubjectPlural { get; } = "signals";

        protected override IItemListFormBaseManager createManager()
            => new ItemListFormBaseManager<ISignalSourceRegistered>(this, SignalRegister.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<ISignalSourceRegistered> table, ItemListFormBaseManager<ISignalSourceRegistered>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<ISignalSourceRegistered> builder;

            // Column: label
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Label");
            builder.Width(300);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((signal, cell) => { cell.Value = signal.SignalLabel; });
            builder.AddChangeEvent(nameof(ISignalSourceRegistered.SignalLabel));
            builder.AllowObjectDrag();

            // Column: red tally
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("T(R)");
            builder.Width(50);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.RedTally.State ? Color.Red : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.RedTally), nameof(IBidirectionalSignalTally.State));
            builder.AllowObjectDrag((signal, cell) => signal.RedTally);

            // Column: yellow tally
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("T(Y)");
            builder.Width(50);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.YellowTally.State ? Color.Gold : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.YellowTally), nameof(IBidirectionalSignalTally.State));;
            builder.AllowObjectDrag((signal, cell) => signal.YellowTally);

            // Column: green tally
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("T(G)");
            builder.Width(50);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.GreenTally.State ? Color.ForestGreen : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.GreenTally), nameof(IBidirectionalSignalTally.State));
            builder.AllowObjectDrag((signal, cell) => signal.GreenTally);

        }

    }

}