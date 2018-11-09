using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Mixers;
using OpenSC.Model.Routers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Mixers
{

    [WindowTypeName("mixers.mixerlist")]
    public partial class MixerList : ChildWindowWithTable
    {

        public MixerList()
        {
            InitializeComponent();
            initializeTable();
            loadAddableMixerTypes();
        }

        private CustomDataGridView<Mixer> table;

        private void initializeTable()
        {

            table = CreateTable<Mixer>();

            CustomDataGridViewColumnDescriptorBuilder<Mixer> builder;

            // Custom cell styles
            DataGridViewCellStyle onProgramColumnCellStyle = table.DefaultCellStyle.Clone();
            onProgramColumnCellStyle.ForeColor = Color.Red;

            DataGridViewCellStyle onPreviewColumnCellStyle = table.DefaultCellStyle.Clone();
            onPreviewColumnCellStyle.ForeColor = Color.ForestGreen;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<Mixer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((mixer, cell) => { cell.Value = string.Format("#{0}", mixer.ID); });
            builder.AddChangeEvent(nameof(Mixer.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<Mixer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((mixer, cell) => { cell.Value = mixer.Name; });
            builder.AddChangeEvent(nameof(Mixer.Name));
            builder.BuildAndAdd();

            // Column: inputs
            builder = GetColumnDescriptorBuilderForTable<Mixer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Inputs");
            builder.Width(50);
            builder.UpdaterMethod((mixer, cell) => { cell.Value = mixer.Inputs.Count; });
            builder.AddChangeEvent(nameof(Mixer.Inputs));
            builder.BuildAndAdd();

            // Column: program
            builder = GetColumnDescriptorBuilderForTable<Mixer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Program");
            builder.Width(110);
            builder.CellStyle(onProgramColumnCellStyle);
            builder.UpdaterMethod((mixer, cell) => { cell.Value = (mixer.OnProgramInputName ?? "-"); });
            builder.AddChangeEvent(nameof(Mixer.OnProgramInputName));
            builder.BuildAndAdd();

            // Column: program
            builder = GetColumnDescriptorBuilderForTable<Mixer>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Preview");
            builder.Width(110);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(onPreviewColumnCellStyle);
            builder.UpdaterMethod((mixer, cell) => { cell.Value = (mixer.OnPreviewInputName ?? "-"); });
            builder.AddChangeEvent(nameof(Mixer.OnPreviewInputName));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<Mixer>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((mixer, cell, e) => {
                var editWindow = MixerEditorFormTypeRegister.Instance.GetFormForModel(mixer) as ChildWindowBase;
                editWindow?.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<Mixer>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((mixer, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this mixer?\n(#{0}) {1}", mixer.ID, mixer.Name);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    MixerDatabase.Instance.Remove(mixer);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = MixerDatabase.Instance;

        }
        
        private void loadAddableMixerTypes()
        {
            foreach (Type type in MixerEditorFormTypeRegister.Instance.RegisteredTypes)
            {
                string label = type.GetTypeLabel();
                ToolStripMenuItem contextMenuItem = new ToolStripMenuItem(label)
                {
                    Tag = type
                };
                contextMenuItem.Click += addMixerMenuItemClick;
                addableMixerTypesMenu.Items.Add(contextMenuItem);
            }
        }

        private static void addMixerMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem typedSender = sender as ToolStripMenuItem;
            Type newMixerType = typedSender?.Tag as Type;
            IModelEditorForm<Mixer> editorForm = MixerEditorFormTypeRegister.Instance.GetFormForType(newMixerType);
            (editorForm as ChildWindowBase)?.ShowAsChild();
        }

    }

}