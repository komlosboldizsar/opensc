using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.X32Faders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.X32Faders
{

    [WindowTypeName("audio.x32faderlist")]
    public partial class X32FaderList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "X32 fader";
        protected override string SubjectPlural { get; } = "X32 fader list";

        protected override IModelEditorForm ModelEditorForm { get; } = new X32FaderEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<X32Fader>(this, X32FaderDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<X32Fader> table, ItemListFormBaseManager<X32Fader>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<X32Fader> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: OSC path
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("OSC path");
            builder.Width(200);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((x32fader, cell) => { cell.Value = x32fader.OscPath; });
            builder.AddChangeEvent(nameof(X32Fader.OscPath));

            // Column: target level
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Target level");
            builder.Width(70);
            builder.UpdaterMethod((x32fader, cell) => { cell.Value = $"{x32fader.TargetLevel:0.0000}"; });
            builder.AddChangeEvent(nameof(X32Fader.TargetLevel));

            // Column: time
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Time");
            builder.Width(70);
            builder.UpdaterMethod((x32fader, cell) => { cell.Value = $"{x32fader.Time} ms"; });
            builder.AddChangeEvent(nameof(X32Fader.Time));

            // Do
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.DisableButton);
            builder.Header("Do");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Do");
            builder.CellContentClickHandlerMethod((x32fader, cell, e) => x32fader.Do());

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}