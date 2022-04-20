using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Macros;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{

    [WindowTypeName("macros.macroslist")]
    public partial class MacroList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "macro";
        protected override string SubjectPlural { get; } = "macros";

        protected override IModelEditorForm ModelEditorForm { get; } = new MacroEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<Macro>(this, MacroDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Macro> table, ItemListFormBaseManager<Macro>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Macro> builder;

            // Custom cell styles
            DataGridViewCellStyle runButtonCellStyle = table.DefaultCellStyle.Clone();
            runButtonCellStyle.Font = new Font(runButtonCellStyle.Font, FontStyle.Bold);
            runButtonCellStyle.BackColor = Color.FromArgb(255, 145, 61);

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: command count
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Commands");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((macro, cell) => { cell.Value = macro.Commands.Count; });
            // TODO: builder.AddChangeEvent(nameof(Macro.Commands));

            // Column: run button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Run");
            builder.Width(100);
            builder.ButtonText("Run");
            builder.CellStyle(runButtonCellStyle);
            builder.CellContentClickHandlerMethod((macro, cell, e) => { macro.Run(); });

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}