using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.UMDs.Tsl50;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    [WindowTypeName("umds.tsl50screenlist")]
    public partial class Tsl50ScreenList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "TSL 5.0 screen";
        protected override string SubjectPlural { get; } = "TSL 5.0 screens";

        protected override IModelEditorForm ModelEditorForm => new Tsl50ScreenEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<Tsl50Screen>(this, Tsl50ScreenDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Tsl50Screen> table, ItemListFormBaseManager<Tsl50Screen>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Tsl50Screen> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: state image
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("IP:port");
            builder.Width(100);
            builder.UpdaterMethod((tsl50screen, cell) => { cell.Value = $"{tsl50screen.IpAddress}:{tsl50screen.Port}"; });
            builder.AddChangeEvent(nameof(Tsl50Screen.IpAddress));

            // Column: state label
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Index");
            builder.Width(60);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((tsl50screen, cell) => { cell.Value = tsl50screen.Index; });
            builder.AddChangeEvent(nameof(Tsl50Screen.Index));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}