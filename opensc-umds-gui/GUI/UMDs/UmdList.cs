using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.UMDs;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{
    [WindowTypeName("umds.umdlist")]
    public partial class UmdList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "UMD";
        protected override string SubjectPlural { get; } = "UMDs";

        protected override IModelTypeRegister TypeRegister { get; } = UmdTypeRegister.Instance;
        protected override IModelEditorFormTypeRegister EditorFormTypeRegister { get; } = UmdEditorFormTypeRegister.Instance;

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<UMD>(this, UmdDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<UMD> table, ItemListFormBaseManager<UMD>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<UMD> builder;

            // Custom cell styles
            DataGridViewCellStyle monospaceTextCellStyle = table.DefaultCellStyle.Clone();
            monospaceTextCellStyle.Font = new Font(FontFamily.GenericMonospace, 9);

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: static text
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Static text");
            builder.Width(200);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.StaticText; });
            builder.AddChangeEvent(nameof(UMD.StaticText));

            // Column: use static text
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.CheckBox);
            builder.Header("Static");
            builder.Width(50);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.UseStaticText; });
            builder.CellContentClickHandlerMethod((umd, cell, e) => { umd.UseStaticText = !(bool)cell.Value; });
            builder.AddChangeEvent(nameof(UMD.UseStaticText));

            // Column: current text
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current text");
            builder.Width(200);
            builder.CellStyle(monospaceTextCellStyle);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.CurrentText; });
            builder.AddChangeEvent(nameof(UMD.CurrentText));

            // Columns: tallies
            for (int i = 0; i < MAX_TALLIES; i++)
            {
                builder = builderGetterMethod();
                builder.Type(DataGridViewColumnType.TextBox);
                builder.Header(string.Format("T{0}", i + 1));
                builder.Width(30);
                builder.UpdaterMethod((umd, cell) => {
                    cell.Style.BackColor = ((umd.Type.TallyCount > i) && umd.TallyStates[i]) ? umd.TallyColors[i] : Color.LightGray;
                });
                if (i == MAX_TALLIES - 1)
                    builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
                builder.AddChangeEvent(nameof(UMD.TallyStates));
            }

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private const int MAX_TALLIES = 2;

    }

}