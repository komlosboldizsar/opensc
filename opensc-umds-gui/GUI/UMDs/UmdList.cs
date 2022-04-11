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
            => new ModelListFormBaseManager<Umd>(this, UmdDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Umd> table, ItemListFormBaseManager<Umd>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Umd> builder;

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
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.FullStaticText; });
            builder.AddChangeEvent(nameof(Umd.FullStaticText));

            // Column: use static text
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.CheckBox);
            builder.Header("Static");
            builder.Width(50);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.UseFullStaticText; });
            builder.CellContentClickHandlerMethod((umd, cell, e) => { umd.UseFullStaticText = !(bool)cell.Value; });
            builder.AddChangeEvent(nameof(Umd.UseFullStaticText));

            // Column: current text
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current text");
            builder.Width(200);
            builder.CellStyle(monospaceTextCellStyle);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.DisplayableRawText; });
            builder.AddChangeEvent(nameof(Umd.DisplayableRawText));

            // Column: tallies
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Tallies");
            builder.Width(100);
            builder.UpdaterMethod((umd, cell) => { cell.Value = "TODO"; });
            //builder.AddChangeEvent(nameof(UMD.TallyStates));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}