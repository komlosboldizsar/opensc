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

        private const string TITLE_NEW = "New router";
        private const string TITLE_EDIT = "Edit router: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New router";
        private const string HEADER_TEXT_EDIT = "Edit router";

        protected Mixer mixer;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), mixer?.ID, mixer?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), mixer?.ID, mixer?.Name);
            }
        }

        public MixerEditorFormBase()
        {
            InitializeComponent();
        }

        public MixerEditorFormBase(Mixer mixer)
        {
            InitializeComponent();
            AddingNew = (mixer == null);
            if (mixer != null)
                this.mixer = mixer;
        }

        protected override void loadData()
        {
            if (mixer == null)
                return;
            idNumericField.Value = (addingNew ? MixerDatabase.Instance.NextValidId() : mixer.ID);
            nameTextBox.Text = mixer.Name;
            initInputsTable();
        }

        protected sealed override bool saveData()
        {

            try
            {
                validateFields();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            writeFields();
            if (addingNew)
                MixerDatabase.Instance.Add(mixer);

            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (mixer == null)
                return;
            mixer.ValidateId((int)idNumericField.Value);
            mixer.ValidateName(nameTextBox.Text);
            // TODO: validate name
        }

        protected virtual void writeFields()
        {
            if (mixer == null)
                return;
            mixer.ID = (int)idNumericField.Value;
            mixer.Name = nameTextBox.Text;
        }

        private CustomDataGridView<MixerInput> inputsTableCDGV;

        private void initInputsTable()
        {

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
            builder.BuildAndAdd();

            // Column: source
            CustomDataGridViewComboBoxItem<Signal>[] signals = getAllSignals();
            builder = getColumnDescriptorBuilderForTable<MixerInput>(inputsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Source");
            builder.Width(300);
            builder.InitializerMethod((input, cell) => { });
            builder.UpdaterMethod((input, cell) => { cell.Value = input.Source; });
            builder.CellEndEditHandlerMethod((input, cell, eventargs) => { input.Source = cell.Value as Signal; });
            builder.DropDownPopulatorMethod((input, cell) => signals);
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

        private CustomDataGridViewComboBoxItem<Signal>[] getAllSignals()
        {
            List<CustomDataGridViewComboBoxItem<Signal>> signalList = new List<CustomDataGridViewComboBoxItem<Signal>>();
            signalList.Add(new CustomDataGridViewComboBoxItem<Signal>.NullItem("(not connected)"));
            foreach (Signal signal in SignalDatabases.Signals.ItemsAsList)
                signalList.Add(new SourceDropDownItem(signal));
            return signalList.ToArray();
        }

        private class SourceDropDownItem: CustomDataGridViewComboBoxItem<Signal>
        {
            public SourceDropDownItem(Signal value) : base(value)
            { }

            public override string ToString()
                => string.Format("Signal: (#{0}) {1}", Value.ID, Value.Name);
        }


        private void addInputButton_Click(object sender, EventArgs e)
        {
            mixer.AddInput();
        }

    }

}
