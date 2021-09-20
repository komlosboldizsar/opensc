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
    public partial class TallyCopyList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "tally copy";
        protected override string SubjectPlural { get; } = "tally copies";

        protected override IModelEditorForm ModelEditorForm { get; } = new TallyCopyEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<TallyCopy>(this, TallyCopyDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<TallyCopy> table, ItemListFormBaseManager<TallyCopy>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<TallyCopy> builder;

            // Column: ID, name
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: from signal
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("From signal");
            builder.Width(150);
            builder.UpdaterMethod((tallyCopy, cell) => { cell.Value = tallyCopy.FromSignal.SignalLabel; });
            builder.AddMultilevelChangeEvent(nameof(TallyCopy.FromSignal), nameof(ISignalSourceRegistered.SignalLabel));
            builder.BuildAndAdd();

            // Column: from color
            builder = builderGetterMethod();
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
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("To signal");
            builder.Width(150);
            builder.UpdaterMethod((tallyCopy, cell) => { cell.Value = tallyCopy.ToSignal.SignalLabel; });
            builder.AddMultilevelChangeEvent(nameof(TallyCopy.ToSignal), nameof(ISignalSourceRegistered.SignalLabel));
            builder.BuildAndAdd();

            // Column: to color
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("To color");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((tallyCopy, cell) => {
                cell.Value = signalTallyColorTranslations.Convert(tallyCopy.ToTallyColor);
                cell.Style.BackColor = tallyCopy.ToTallyColor.ConvertToLightColor();
            });
            builder.AddChangeEvent(nameof(TallyCopy.ToTallyColor));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private EnumToStringConverter<SignalTallyColor> signalTallyColorTranslations = new EnumToStringConverter<SignalTallyColor>()
        {
            { SignalTallyColor.Red, "red" },
            { SignalTallyColor.Yellow, "yellow" },
            { SignalTallyColor.Green, "green" },
        };

    }

}