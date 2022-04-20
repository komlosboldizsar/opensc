using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Mixers;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Mixers
{

    [WindowTypeName("mixers.mixerlist")]
    public partial class MixerList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "mixer";
        protected override string SubjectPlural { get; } = "mixers";

        protected override IModelTypeRegister TypeRegister { get; } = MixerTypeRegister.Instance;
        protected override IModelEditorFormTypeRegister EditorFormTypeRegister { get; } = MixerEditorFormTypeRegister.Instance;

        protected override IItemListFormBaseManager createManager()
            => Manager = new ModelListFormBaseManager<Mixer>(this, MixerDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Mixer> table, ItemListFormBaseManager<Mixer>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Mixer> builder;

            // Custom cell styles
            DataGridViewCellStyle onProgramColumnCellStyle = table.DefaultCellStyle.Clone();
            onProgramColumnCellStyle.ForeColor = Color.Red;

            DataGridViewCellStyle onPreviewColumnCellStyle = table.DefaultCellStyle.Clone();
            onPreviewColumnCellStyle.ForeColor = Color.ForestGreen;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: state
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((mixer, cell) => {
                cell.Style.BackColor = stateColorConverter.Convert(mixer.State);
                cell.Value = mixer.StateString;
            });
            builder.AddChangeEvent(nameof(Mixer.State));
            builder.AddChangeEvent(nameof(Mixer.StateString));

            // Column: inputs
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Inputs");
            builder.Width(50);
            builder.UpdaterMethod((mixer, cell) => { cell.Value = mixer.Inputs.Count; });
            builder.AddChangeEvent(nameof(Mixer.Inputs));

            // Column: program
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Program");
            builder.Width(110);
            builder.CellStyle(onProgramColumnCellStyle);
            builder.UpdaterMethod((mixer, cell) => { cell.Value = (mixer.OnProgramInputName ?? "-"); });
            builder.AddChangeEvent(nameof(Mixer.OnProgramInputName));

            // Column: preview
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Preview");
            builder.Width(110);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(onPreviewColumnCellStyle);
            builder.UpdaterMethod((mixer, cell) => { cell.Value = (mixer.OnPreviewInputName ?? "-"); });
            builder.AddChangeEvent(nameof(Mixer.OnPreviewInputName));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private static readonly EnumConverter<MixerState, Color> stateColorConverter = new EnumConverter<MixerState, Color>(null)
        {
            { MixerState.Ok, Color.LightGreen },
            { MixerState.Warning, Color.FromArgb(255, 255, 244, 104) },
            { MixerState.Error, Color.LightPink },
            { MixerState.Unknown, Color.LightSlateGray }
        };

    }

}