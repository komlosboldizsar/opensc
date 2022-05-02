using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Variables;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Variables
{

    [WindowTypeName("variables.booleanlist")]
    public partial class BooleanList : ItemListFormBase
    {

        protected override string SubjectSingular { get; } = "boolean";
        protected override string SubjectPlural { get; } = "booleans";

        protected override IItemListFormBaseManager createManager()
            => new ItemListFormBaseManager<IBoolean>(this, BooleanRegister.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<IBoolean> table, ItemListFormBaseManager<IBoolean>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<IBoolean> builder;

            // Custom styles
            DataGridViewCellStyle stateColumnStyle = BOLD_TEXT_CELL_STYLE.Clone();
            stateColumnStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Column: name
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.Width(220);
            builder.UpdaterMethod((boolean, cell) => { cell.Value = boolean.Identifier; });
            builder.AddChangeEvent(nameof(IBoolean.Identifier));

            // Column: description
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Description");
            builder.Width(450);
            builder.UpdaterMethod((boolean, cell) => { cell.Value = boolean.Description; });
            builder.AddChangeEvent(nameof(IBoolean.Description));

            // Column: state
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.CellStyle(stateColumnStyle);
            builder.Width(80);
            builder.UpdaterMethod((boolean, cell) => {
                bool booleanCurrentState = boolean.CurrentState;
                cell.Value = booleanCurrentState ? "- on -" : "";
                cell.Style.BackColor = booleanCurrentState ? boolean.Color : OFF_COLOR;
            });
            builder.AddChangeEvent(nameof(IBoolean.CurrentState));

        }

        private static readonly Color OFF_COLOR = Color.White;

    }

}