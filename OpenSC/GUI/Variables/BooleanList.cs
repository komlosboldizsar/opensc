using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Variables;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Variables
{

    [WindowTypeName("variables.booleanlist")]
    public partial class BooleanList : ChildWindowWithTable
    {

        public BooleanList()
        {
            InitializeComponent();
            initializeTable();
        }

        private readonly Color OFF_COLOR = Color.White;

        private CustomDataGridView<IBoolean> table;

        private void initializeTable()
        {

            table = CreateTable<IBoolean>();

            CustomDataGridViewColumnDescriptorBuilder<IBoolean> builder;

            // Custom styles
            DataGridViewCellStyle stateColumnStyle = BOLD_TEXT_CELL_STYLE.Clone();
            stateColumnStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<IBoolean>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.Width(220);
            builder.UpdaterMethod((boolean, cell) => { cell.Value = boolean.Name; });
            builder.AddChangeEvent(nameof(IBoolean.NameChangedPCN));
            builder.BuildAndAdd();

            // Column: description
            builder = GetColumnDescriptorBuilderForTable<IBoolean>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Description");
            builder.Width(450);
            builder.UpdaterMethod((boolean, cell) => { cell.Value = boolean.Description; });
            builder.AddChangeEvent(nameof(IBoolean.DescriptionChangedPCN));
            builder.BuildAndAdd();

            // Column: state
            builder = GetColumnDescriptorBuilderForTable<IBoolean>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.CellStyle(stateColumnStyle);
            builder.Width(80);
            builder.UpdaterMethod((boolean, cell) => {
                bool booleanCurrentState = boolean.CurrentState;
                cell.Value = booleanCurrentState ? "- on -" : "";
                cell.Style.BackColor = booleanCurrentState ? boolean.Color : OFF_COLOR;
            });
            builder.AddChangeEvent(nameof(IBoolean.StateChangedPCN));
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = BooleanRegister.Instance;

        }

    }

}