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
    [WindowTypeName("umds.umdportlist")]
    public partial class UmdPortList : ChildWindowWithTable
    {

        public UmdPortList()
        {
            InitializeComponent();
            initializeTable();
            loadAddableUmdPortTypes();
        }

        private CustomDataGridView<UmdPort> table;

        private void initializeTable()
        {

            table = CreateTable<UmdPort>();

            CustomDataGridViewColumnDescriptorBuilder<UmdPort> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<UmdPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(50);
            builder.UpdaterMethod((port, cell) => { cell.Value = string.Format("#{0}", port.ID); });
            builder.AddChangeEvent(nameof(UmdPort.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<UmdPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(200);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((port, cell) => { cell.Value = port.Name; });
            builder.AddChangeEvent(nameof(UmdPort.Name));
            builder.BuildAndAdd();

            // Column: state (is initialized?)
            builder = GetColumnDescriptorBuilderForTable<UmdPort>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(80);
            builder.UpdaterMethod((port, cell) => {
                cell.Style.BackColor = port.Initialized ? Color.LightGreen : Color.LightPink;
                cell.Value = port.Initialized ? "initialized" : "not initialized";
            });
            builder.AddChangeEvent(nameof(UmdPort.Initialized));
            builder.BuildAndAdd();

            // Column: initialize button
            builder = GetColumnDescriptorBuilderForTable<UmdPort>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Initialize");
            builder.Width(70);
            builder.ButtonText("Initialize");
            builder.CellContentClickHandlerMethod((port, cell, e) => port.Init());
            builder.BuildAndAdd();

            // Column: deinitialize button
            builder = GetColumnDescriptorBuilderForTable<UmdPort>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Deinitialize");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Deinitialize");
            builder.CellContentClickHandlerMethod((port, cell, e) => port.DeInit());
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<UmdPort>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((port, cell, e) => {
                var editWindow = UmdPortEditorFormTypeRegister.Instance.GetFormForModel(port) as ChildWindowBase;
                editWindow?.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<UmdPort>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((port, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this UMD port?\n(#{0}) {1}", port.ID, port.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    UmdPortDatabase.Instance.Remove(port);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = UmdPortDatabase.Instance;

        }

        private void loadAddableUmdPortTypes()
        {
            foreach (Type type in UmdPortEditorFormTypeRegister.Instance.RegisteredTypes)
            {
                string label = type.GetTypeLabel();
                ToolStripMenuItem contextMenuItem = new ToolStripMenuItem(label)
                {
                    Tag = type
                };
                contextMenuItem.Click += addUmdPortMenuItemClick;
                addableUmdPortTypesMenu.Items.Add(contextMenuItem);
            }
        }

        private static void addUmdPortMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem typedSender = sender as ToolStripMenuItem;
            Type newUmdPortType = typedSender?.Tag as Type;
            IModelEditorForm<UmdPort> editorForm = UmdPortEditorFormTypeRegister.Instance.GetFormForType(newUmdPortType);
            (editorForm as ChildWindowBase)?.ShowAsChild();
        }

    }
}