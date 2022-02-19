using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.VTRs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.VTRs
{
    [WindowTypeName("vtrs.vtrlist")]
    public partial class VtrList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "VTR";
        protected override string SubjectPlural { get; } = "VTRs";

        protected override IModelTypeRegister TypeRegister { get; } = VtrTypeRegister.Instance;
        protected override IModelEditorFormTypeRegister EditorFormTypeRegister { get; } = VtrEditorFormTypeRegister.Instance;

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<Vtr>(this, VtrDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Vtr> table, ItemListFormBaseManager<Vtr>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Vtr> builder;

            // Column: ID, name
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: state image
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(30);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = stateImageConverter.Convert(vtr.State); });
            builder.AddChangeEvent(nameof(Vtr.State));

            // Column: state label
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(60);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = stateLabelConverter.Convert(vtr.State); });
            builder.AddChangeEvent(nameof(Vtr.State));

            // Column: title
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Title");
            builder.Width(200);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.Title; });
            builder.AddChangeEvent(nameof(Vtr.Title));

            // Column: time (full)
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Time (full)");
            builder.Width(100);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.TimeFull.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Vtr.SecondsFull));

            // Column: time (elapsed)
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Time (elapsed)");
            builder.Width(100);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.TimeElapsed.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Vtr.SecondsElapsed));

            // Column: time (remaining)
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Time (remaining)");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.TimeRemaining.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Vtr.SecondsRemaining));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private static readonly EnumToStringConverter<VtrState> stateLabelConverter = new EnumToStringConverter<VtrState>() {
            { VtrState.Stopped, "stopped" },
            { VtrState.Paused, "paused" },
            { VtrState.Cued, "cued" },
            { VtrState.Playing, "playing" },
            { VtrState.Rewinding, "rewinding" },
            { VtrState.FastForwarding, "fast-forwarding" },
            { VtrState.Recording, "recording" },
        };

        private static readonly EnumToBitmapConverter<VtrState> stateImageConverter = new EnumToBitmapConverter<VtrState>() {
            { VtrState.Stopped, Icons._16_vtr_stopped },
            { VtrState.Paused, Icons._16_vtr_paused },
            { VtrState.Cued, Icons._16_vtr_cued },
            { VtrState.Playing, Icons._16_vtr_playing },
            { VtrState.Rewinding, Icons._16_vtr_rewinding },
            { VtrState.FastForwarding, Icons._16_vtr_fastforwarding },
            { VtrState.Recording, Icons._16_vtr_recording },
        };

    }

}