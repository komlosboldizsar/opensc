using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.Signals.TallyCopying.Helpers;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Signals;
using OpenSC.Model.Signals.BooleanTallies;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals.BooleanTallies
{

    [WindowTypeName("signals.booleantallylist")]
    public partial class BooleanTallyList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "boolean tally";
        protected override string SubjectPlural { get; } = "boolean tallies";

        protected override IModelEditorForm ModelEditorForm { get; } = new BooleanTallyEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<BooleanTally>(this, BooleanTallyDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<BooleanTally> table, ItemListFormBaseManager<BooleanTally>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<BooleanTally> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: from boolean
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("From boolean");
            builder.Width(150);
            builder.UpdaterMethod((booleanTally, cell) => { cell.Value = booleanTally.FromBoolean.Identifier; });
            builder.AddMultilevelChangeEvent(nameof(BooleanTally.FromBoolean), nameof(IBoolean.Identifier));

            // Column: to signal
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("To signal");
            builder.Width(150);
            builder.UpdaterMethod((booleanTally, cell) => { cell.Value = booleanTally.ToSignal.SignalLabel; });
            builder.AddMultilevelChangeEvent(nameof(BooleanTally.ToSignal), nameof(ISignalSourceRegistered.SignalLabel));

            // Column: to color
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("To color");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((booleanTally, cell) => {
                cell.Value = signalTallyColorTranslations.Convert(booleanTally.ToTallyColor);
                cell.Style.BackColor = booleanTally.ToTallyColor.ConvertToLightColor();
            });
            builder.AddChangeEvent(nameof(BooleanTally.ToTallyColor));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private static readonly EnumToStringConverter<SignalTallyColor> signalTallyColorTranslations = new EnumToStringConverter<SignalTallyColor>()
        {
            { SignalTallyColor.Red, "red" },
            { SignalTallyColor.Yellow, "yellow" },
            { SignalTallyColor.Green, "green" },
        };

    }

}