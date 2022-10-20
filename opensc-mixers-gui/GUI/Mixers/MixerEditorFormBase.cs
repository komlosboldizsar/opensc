using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Mixers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace OpenSC.GUI.Mixers
{

    public partial class MixerEditorFormBase : ModelEditorFormBase
    {

        public MixerEditorFormBase() => InitializeComponent();
        public MixerEditorFormBase(Mixer mixer) : base(mixer) => InitializeComponent();

        protected override void loadData()
        {
            base.loadData();
            Mixer mixer = (Mixer)EditedModel;
            if (mixer == null)
                return;
            initInputsTable();
            userMixersRedTallyCheckbox.Checked = mixer.GivesRedTallyToSources;
            userMixersGreenTallyCheckbox.Checked = mixer.GivesGreenTallyToSources;
        }

        protected override void validateFields()
        {
            base.validateFields();
            Mixer mixer = (Mixer)EditedModel;
            if (mixer == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Mixer mixer = (Mixer)EditedModel;
            if (mixer == null)
                return;
            mixer.GivesRedTallyToSources = userMixersRedTallyCheckbox.Checked;
            mixer.GivesGreenTallyToSources = userMixersGreenTallyCheckbox.Checked;
        }

        private CustomDataGridView<MixerInput> inputsTableCDGV;

        private void initInputsTable()
        {

            Mixer mixer = (Mixer)EditedModel;
            inputsTableCDGV = createTable<MixerInput>(inputsTableContainerPanel, ref this.inputsTable);
            CustomDataGridViewColumnDescriptorBuilder<MixerInput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<MixerInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Index; });
            builder.TextEditable(true);
            builder.CellEndEditHandlerMethod((input, cell, eventargs) =>
            {
                try
                {
                    input.Index = int.Parse(cell.Value.ToString());
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cell.Value = input.Index;
                }
            });
            //builder.AddChangeEvent(nameof(MixerInput.IndexChangedPCN));
            //builder.AllowSystemObjectDrag();
            builder.BuildAndAdd();

            // Column: name
            builder = getColumnDescriptorBuilderForTable<MixerInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(100);
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Name; });
            builder.AddChangeEvent(nameof(MixerInput.Name));
            builder.TextEditable(true);
            builder.CellEndEditHandlerMethod((input, cell, eventargs) =>
            {
                try
                {
                    input.Name = cell.Value.ToString();
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cell.Value = input.Name;
                }
            });
            //builder.AllowSystemObjectDrag();
            builder.BuildAndAdd();

            // Column: source
            CustomDataGridViewComboBoxItem<ISignalSourceRegistered>[] signals = getAllSignals();
            builder = getColumnDescriptorBuilderForTable<MixerInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Source");
            builder.Width(300);
            builder.InitializerMethod((input, cell) => { });
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Source; });
            builder.CellValueChangedHandlerMethod((input, cell, eventargs) => { input.Source = cell.Value as ISignalSourceRegistered; });
            builder.DropDownPopulatorMethod((input, cell) => signals);
            builder.ReceiveObjectDrop().FilterByType<ISignalSourceRegistered>();
            builder.BuildAndAdd();

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<MixerInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((input, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete input #{0}?", (input.Index + 1));
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    mixer.RemoveInput(input);
            });
            builder.BuildAndAdd();

            // Bind collection
            inputsTableCDGV.BoundCollection = mixer.Inputs;

        }

        public CustomDataGridView<T> createTable<T>(Control container, ref DataGridView originalTableMember)
            where T : class
        {
            var customTable = new CustomDataGridView<T>();
            container.Controls.Clear();
            container.Controls.Add(customTable);
            customTable.Dock = DockStyle.Fill;
            originalTableMember = customTable;
            return customTable;
        }

        private CustomDataGridViewColumnDescriptorBuilder<T> getColumnDescriptorBuilderForTable<T>(CustomDataGridView<T> table)
            where T : class
            => new CustomDataGridViewColumnDescriptorBuilder<T>(table);

        private CustomDataGridViewComboBoxItem<ISignalSourceRegistered>[] getAllSignals()
        {
            List<CustomDataGridViewComboBoxItem<ISignalSourceRegistered>> signalList = new List<CustomDataGridViewComboBoxItem<ISignalSourceRegistered>>();
            signalList.Add(new CustomDataGridViewComboBoxItem<ISignalSourceRegistered>.NullItem("(not connected)"));
            foreach (ISignalSourceRegistered signal in SignalRegister.Instance)
                signalList.Add(new SourceDropDownItem(signal));
            return signalList.ToArray();
        }

        private class SourceDropDownItem: CustomDataGridViewComboBoxItem<ISignalSourceRegistered>
        {
            public SourceDropDownItem(ISignalSourceRegistered value) : base(value)
            { }
            public override string Label => Value.SignalLabel;
        }

        private void addInputButton_Click(object sender, EventArgs e) => ((Mixer)EditedModel).AddInput();

    }

}
