using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{

    public partial class MacroEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New macro";
        private const string TITLE_EDIT = "Edit macro: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New macro";
        private const string HEADER_TEXT_EDIT = "Edit macro";

        protected Macro macro;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), macro?.ID, macro?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), macro?.ID, macro?.Name);
            }
        }

        public MacroEditorForm()
        {
            InitializeComponent();
        }

        public MacroEditorForm(Macro macro)
        {
            InitializeComponent();
            AddingNew = (macro == null);
            if (macro != null)
                this.macro = macro;
        }

        protected override void loadData()
        {
            if (macro == null)
                return;
            idNumericField.Value = (addingNew ? MacroDatabase.Instance.NextValidId() : macro.ID);
            nameTextBox.Text = macro.Name;
            /*initCommandsEditor();
            initTriggersTable();*/
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

            macro.StartUpdate();
            writeFields();
            macro.EndUpdate();

            if (addingNew)
                MacroDatabase.Instance.Add(macro);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (macro == null)
                return;
            macro.ValidateId((int)idNumericField.Value);
            // TODO: validate name
        }

        protected virtual void writeFields()
        {
            if (macro == null)
                return;
            macro.ID = (int)idNumericField.Value;
            macro.Name = nameTextBox.Text;
        }
        
        private void initCommandsEditor()
        {
        }

        private CustomDataGridView<RouterOutput> triggersTableCDGV;

        private void initTriggersTable()
        {

            /*triggersTableCDGV = createTable<RouterOutput>(outputsTableContainerPanel, ref this.outputsTable);
            CustomDataGridViewColumnDescriptorBuilder<RouterOutput> builder;

            // Column: index
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(triggersTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("#");
            builder.Width(30);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Index + 1; });
            //builder.AddChangeEvent(nameof(RouterOutput.Index));
            builder.BuildAndAdd();

            // Column: name
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(triggersTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Name");
            builder.Width(100);
            builder.UpdaterMethod((output, cell) => { cell.Value = output.Name; });
            builder.AddChangeEvent(nameof(RouterOutput.Name));
            builder.TextEditable(true);
            builder.CellEndEditHandlerMethod((output, cell, eventargs) =>
            {
                try
                {
                    output.Name = cell.Value.ToString();
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cell.Value = output.Name;
                }
            });
            builder.BuildAndAdd();

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<RouterOutput>(triggersTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((output, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete output #{0}?", (output.Index + 1));
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    router.RemoveOutput(output);
            });
            builder.BuildAndAdd();

            // Bind collection
            triggersTableCDGV.BoundCollection = router.Outputs;*/

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
        {
             return new CustomDataGridViewColumnDescriptorBuilder<T>(table);
        }

        private void addCommandButton_Click(object sender, EventArgs e)
        {
            IMacroCommand command = selectCommandComboBox.SelectedValue as IMacroCommand;
            if (command == null)
            {
                // Show error message
                return;
            }
            List<object> arguments = new List<object>();
            foreach (CommandArgumentControl argControl in argumentControls)
                arguments.Add(argControl.ArgumentValue);
            string[] argumentKeys = command.GetArgumentKeys(arguments.ToArray());
            string serializedCommand = command.CommandCode + "(";
            for (int i = 0; i < serializedCommand.Length; i++)
                serializedCommand += string.Format("{1}{0}{1}{2}", argumentKeys[i], ((command.Arguments[i].Type == typeof(string)) ? "\"" : ""), ((i == (serializedCommand.Length - 1)) ? "" : ", "));
            serializedCommand += ")\r\n";
            commandsEditorTextBox.AppendText(serializedCommand);
        }

        private void addTriggerButton_Click(object sender, EventArgs e)
        {
            //router.AddOutput();
        }

        private void commandsEditorTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private List<CommandArgumentControl> argumentControls = new List<CommandArgumentControl>();

        private void selectCommandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IMacroCommand selectedCommand = selectCommandComboBox.SelectedValue as IMacroCommand;
            commandDescriptionTextBox.Text = "";
            commandArgumentsPanel.Controls.Clear();
            foreach (CommandArgumentControl argControl in argumentControls)
                argControl.ArgumentValueChanged -= ArgumentControl_ArgumentValueChanged;
            argumentControls.Clear();
            if (selectedCommand != null)
            {
                commandDescriptionTextBox.Text = selectedCommand.Description;
                int i = 0;
                int argCount = selectedCommand.Arguments.Length;
                foreach (IMacroCommandArgument arg in selectedCommand.Arguments)
                {
                    var argumentControl = new CommandArgumentControl(arg, i, (i == (argCount - 1)));
                    argumentControl.ArgumentValueChanged += ArgumentControl_ArgumentValueChanged;
                    argumentControls.Add(argumentControl);
                    i++;
                }
            }

            for (int i = argumentControls.Count - 1; i >= 0; i--)
            {
                CommandArgumentControl control = argumentControls[i];
                commandArgumentsPanel.Controls.Add(control);
                control.Dock = DockStyle.Top;
            }

        }

        private void ArgumentControl_ArgumentValueChanged(CommandArgumentControl control, IMacroCommandArgument argument, object newValue)
        {

            List<object> argumentValues = new List<object>();
            object[] argumentValuesArr = null;

            bool collecting = true;
            foreach (CommandArgumentControl argControl in argumentControls)
            {

                if (collecting)
                    argumentValues.Add(argControl.ArgumentValue);
                else
                    control.PreviousArgumentValues = argumentValuesArr;

                if (argControl == control)
                {
                    collecting = false;
                    argumentValuesArr = argumentValues.ToArray();
                }

            }

        }

        private void loadCommands()
        {
            selectCommandComboBox.CreateAdapterAsDataSource(MacroCommandRegister.Instance.RegisteredCommands, mc => string.Format("[{0}] {1}", mc.CommandCode, mc.CommandName), true, "-");
        }

        private void MacroEditorForm_Load(object sender, EventArgs e)
        {
            loadCommands();
        }

    }

}
