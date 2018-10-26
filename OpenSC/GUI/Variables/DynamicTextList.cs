using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Variables;
using System;

namespace OpenSC.GUI.Variables
{

    [WindowTypeName("variables.dynamictextlist")]
    public partial class DynamicTextList : ChildWindowWithTable
    {

        public DynamicTextList()
        {
            InitializeComponent();
            initializeTable();
        }

        private CustomDataGridView<DynamicText> table;

        private void initializeTable()
        {

            table = CreateTable<DynamicText>();

            CustomDataGridViewColumnDescriptorBuilder<DynamicText> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<DynamicText>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((dyntext, cell) => { cell.Value = string.Format("#{0}", dyntext.ID); });
            builder.AddChangeEvent(nameof(DynamicText.IdChangedPCN));
            builder.BuildAndAdd();

            // Bind database
            table.BoundDatabase = DynamicTextDatabase.Instance;

        }

        private void addDynamicTextButton_Click(object sender, EventArgs e)
        {
            // TODO
        }

    }

}