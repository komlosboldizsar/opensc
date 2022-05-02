using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.UMDs.BmdSmartView;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    [WindowTypeName("umds.bmdsmartviewunitlist")]
    public partial class BmdSmartViewUnitList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "BMD SmartView unit";
        protected override string SubjectPlural { get; } = "BMD SmartView units";

        protected override IModelEditorForm ModelEditorForm => new BmdSmartViewUnitEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<BmdSmartViewUnit>(this, BmdSmartViewUnitDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<BmdSmartViewUnit> table, ItemListFormBaseManager<BmdSmartViewUnit>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<BmdSmartViewUnit> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: connection data
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("IP:port");
            builder.Width(100);
            builder.UpdaterMethod((bmdSmartViewUnit, cell) => { cell.Value = $"{bmdSmartViewUnit.IpAddress}:{bmdSmartViewUnit.Port}"; });
            builder.AddChangeEvent(nameof(BmdSmartViewUnit.IpAddress));

            // Column: connection state
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((bmdSmartViewUnit, cell) => {
                if (bmdSmartViewUnit.Connected)
                {
                    cell.Style.BackColor = CELL_BG_CONNECTED;
                    cell.Value = "connected";
                }
                else
                {
                    cell.Style.BackColor = CELL_BG_DISCONNECTED;
                    cell.Value = "disconnected";
                }
            });
            builder.AddChangeEvent(nameof(BmdSmartViewUnit.Connected));

            // Column: connect button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Connect");
            builder.Width(70);
            builder.ButtonText("Connect");
            builder.CellContentClickHandlerMethod(async (bmdSmartViewUnit, cell, e) => await bmdSmartViewUnit.Connect());

            // Column: disconnect button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Disconnect");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Disconnect");
            builder.CellContentClickHandlerMethod((bmdSmartViewUnit, cell, e) => bmdSmartViewUnit.Disconnect());

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private static readonly Color CELL_BG_CONNECTED = Color.LightPink;
        private static readonly Color CELL_BG_DISCONNECTED = Color.LightPink;

    }

}