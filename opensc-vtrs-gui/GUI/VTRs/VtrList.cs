using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.VTRs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.VTRs
{
    [WindowTypeName("vtrs.vtrlist")]
    public partial class VtrList : ChildWindowWithTable
    {
        public VtrList()
        {
            InitializeComponent();
            HeaderText = "List of VTRs";
            initializeTable();
            loadAddableVtrTypes();
        }

        private CustomDataGridView<Vtr> table;

        private void initializeTable()
        {

            table = CreateTable<Vtr>();

            CustomDataGridViewColumnDescriptorBuilder<Vtr> builder;

            // Column: ID
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = string.Format("#{0}", vtr.ID); });
            builder.AddChangeEvent(nameof(Vtr.ID));
            builder.BuildAndAdd();

            // Column: name
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.Name; });
            builder.AddChangeEvent(nameof(Vtr.Name));
            builder.BuildAndAdd();

            // Column: state image
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.Image);
            builder.Header("");
            builder.Width(30);
            builder.CellStyle(TWO_PIXELS_PADDING_CELL_STYLE);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = stateImageConverter.Convert(vtr.State); });
            builder.AddChangeEvent(nameof(Vtr.State));
            builder.BuildAndAdd();

            // Column: state label
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(60);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = stateLabelConverter.Convert(vtr.State); });
            builder.AddChangeEvent(nameof(Vtr.State));
            builder.BuildAndAdd();

            // Column: title
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Title");
            builder.Width(200);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.Title; });
            builder.AddChangeEvent(nameof(Vtr.Title));
            builder.BuildAndAdd();

            // Column: time (full)
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Time (full)");
            builder.Width(100);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.TimeFull.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Vtr.SecondsFull));
            builder.BuildAndAdd();

            // Column: time (elapsed)
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Time (elapsed)");
            builder.Width(100);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.TimeElapsed.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Vtr.SecondsElapsed));
            builder.BuildAndAdd();

            // Column: time (remaining)
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Time (remaining)");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((vtr, cell) => { cell.Value = vtr.TimeRemaining.ToString(@"hh\:mm\:ss"); });
            builder.AddChangeEvent(nameof(Vtr.SecondsRemaining));
            builder.BuildAndAdd();

            // Column: edit button
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((vtr, cell, e) => {
                var editWindow = VtrEditorFormTypeRegister.Instance.GetFormForModel(vtr) as ChildWindowBase;
                editWindow?.ShowAsChild();
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = GetColumnDescriptorBuilderForTable<Vtr>();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((vtr, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this VTR?\n(#{0}) {1}", vtr.ID, vtr.Title);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    VtrDatabase.Instance.Remove(vtr);
            });
            builder.BuildAndAdd();

            // Bind database
            table.BoundCollection = VtrDatabase.Instance;

        }

        private static readonly EnumToStringConverter<VtrState> stateLabelConverter = new EnumToStringConverter<VtrState>() {
            { VtrState.Stopped, "stopped" },
            { VtrState.Paused, "paused" },
            { VtrState.Playing, "playing" },
            { VtrState.Rewinding, "rewinding" },
            { VtrState.FastForwarding, "fast-forwarding" },
            { VtrState.Recording, "recording" },
        };

        private static readonly EnumToBitmapConverter<VtrState> stateImageConverter = new EnumToBitmapConverter<VtrState>() {
            { VtrState.Stopped, Icons._16_vtr_stopped },
            { VtrState.Paused, Icons._16_vtr_paused },
            { VtrState.Playing, Icons._16_vtr_playing },
            { VtrState.Rewinding, Icons._16_vtr_rewinding },
            { VtrState.FastForwarding, Icons._16_vtr_fastforwarding },
            { VtrState.Recording, Icons._16_vtr_recording },
        };

        private void loadAddableVtrTypes()
        {
            foreach (Type type in VtrEditorFormTypeRegister.Instance.RegisteredTypes)
            {
                string label = type.GetTypeLabel();
                ToolStripMenuItem contextMenuItem = new ToolStripMenuItem(label)
                {
                    Tag = type
                };
                contextMenuItem.Click += addVtrMenuItemClick;
                addableVtrTypesMenu.Items.Add(contextMenuItem);
            }
        }

        private static void addVtrMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem typedSender = sender as ToolStripMenuItem;
            Type newVtrType = typedSender?.Tag as Type;
            IModelEditorForm<Vtr> editorForm = VtrEditorFormTypeRegister.Instance.GetFormForType(newVtrType);
            (editorForm as ChildWindowBase)?.ShowAsChild();
        }

    }

}