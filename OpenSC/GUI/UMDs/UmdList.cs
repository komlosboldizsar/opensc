using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Timers;
using OpenSC.Model.UMDs;
using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = OpenSC.Model.Timers.Timer;

namespace OpenSC.GUI.UMDs
{
    [WindowTypeName("umds.umdlist")]
    public partial class UmdList : ChildWindowWithTitleAndTable<UMD>
    {

        private const int MAX_TALLIES = 2;

        public UmdList()
        {
            InitializeComponent();
            HeaderText = "List of UMDs";
            initializeTable();
        }

        private void initializeTable()
        {

            CustomDataGridViewColumnDescriptorBuilder<UMD> builder;

            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((umd, cell) => { cell.Value = string.Format("#{0}", umd.ID); });
            builder.AddChangeEvent(nameof(UMD.IdChangedPCN));
            builder.BuildAndAdd();

            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(200);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.Name; });
            builder.AddChangeEvent(nameof(UMD.NameChangedPCN));
            builder.BuildAndAdd();

            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Static text");
            builder.Width(200);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.StaticText; });
            builder.AddChangeEvent(nameof(UMD.StaticTextChangedPCN));
            builder.BuildAndAdd();

            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.CheckBox);
            builder.Header("Static");
            builder.Width(50);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.UseStaticText; });
            builder.CellContentClickHandlerMethod((umd, cell, e) => { umd.UseStaticText = !(bool)cell.Value; });
            builder.AddChangeEvent(nameof(UMD.UseStaticTextChangedPCN));
            builder.BuildAndAdd();

            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Current text");
            builder.Width(200);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((umd, cell) => { cell.Value = umd.CurrentText; });
            builder.AddChangeEvent(nameof(UMD.CurrentTextChangedPCN));
            builder.BuildAndAdd();

            for(int i = 0; i < MAX_TALLIES; i++)
            {
                builder = GetColumnDescriptorBuilderForTable();
                builder.Type(DataGridViewColumnType.TextBox);
                builder.Header(string.Format("T{0}", i+1));
                builder.Width(30);
                builder.UpdaterMethod((umd, cell) => {
                    cell.Style.BackColor = ((umd.Type.TallyCount > i) && umd.TallyStates[i]) ? umd.TallyColors[i] : Color.LightGray;
                });
                if(i == MAX_TALLIES - 1)
                    builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
                builder.AddChangeEvent(nameof(UMD.TallyChangedPCN));
                builder.BuildAndAdd();
            }

            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((umd, cell, e) => {
                var editWindow = new UmdEditWindow(umd);
                editWindow.ShowAsChild();
                return;
            });
            builder.BuildAndAdd();

            builder = GetColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((umd, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this UMD?\n(#{0}) {1}", umd.ID, umd.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    UmdDatabase.Instance.Remove(umd);
            });
            builder.BuildAndAdd();

            table.BoundDatabase = UmdDatabase.Instance;

        }

    }
}