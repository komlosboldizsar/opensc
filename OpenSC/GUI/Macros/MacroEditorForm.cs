﻿using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.General;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
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
            this.macro = (macro != null) ? macro : new Macro();
            AddingNew = (macro == null);
        }

        protected override void loadData()
        {
            if (macro == null)
                return;
            idNumericField.Value = (addingNew ? MacroDatabase.Instance.NextValidId() : macro.ID);
            nameTextBox.Text = macro.Name;
            loadCode();
            initTriggerCollectionProxies();
            macroTriggersCollectionProxy.Clear();
            macroTriggersCollectionProxy.AddRange(macro.Triggers);
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
            macro.ValidateName(nameTextBox.Text);
            validateCode();
        }

        protected virtual void writeFields()
        {
            if (macro == null)
                return;
            macro.ID = (int)idNumericField.Value;
            macro.Name = nameTextBox.Text;
            macro.Commands.Clear();
            foreach (MacroCommandWithArguments commandWithArgument in getCommandsWAFromCode())
                macro.Commands.Add(commandWithArgument);
            macro.RemoveAllTriggers();
            macro.AddTriggerRange(macroTriggersCollectionProxy);
        }

        private void MacroEditorForm_Load(object sender, EventArgs e)
        {
            initCommandsEditor();
            loadCommands();
            initTriggersTable();
            loadTriggers();
            editTrigger(null);
        }

        #region Commands
        private void initCommandsEditor()
        {
            commandsEditorTextBox.TextBox.TextChanged += commandsEditorTextBox_TextChanged;
        }

        private bool noInterpretOnTextChange = false;

        private void addCommandButton_Click(object sender, EventArgs e)
        {

            IMacroCommand command = selectCommandComboBox.SelectedValue as IMacroCommand;
            if (command == null)
            {
                // Show error message
                return;
            }

            List<object> argumentValues = new List<object>();
            foreach (CommandArgumentControl argControl in commandArgumentControls)
                argumentValues.Add(argControl.ArgumentValue);

            noInterpretOnTextChange = true;
            RichTextBox rtfBox = commandsEditorTextBox.TextBox;
            if ((rtfBox.Text.Length > 0) && (rtfBox.Text[rtfBox.Text.Length - 1] != '\n'))
                commandsEditorTextBox.TextBox.AppendText("\n");
            commandsEditorTextBox.TextBox.AppendText(command.GetCode(argumentValues.ToArray()) + "\n");
            interpretCurrentLine(-1);
            noInterpretOnTextChange = false;

        }

        private static readonly GUI.Helpers.Converters.EnumConverter<MacroCodeTokenizer.TokenType, Color> TOKEN_COLOR_CONVERTER = new Helpers.Converters.EnumConverter<MacroCodeTokenizer.TokenType, Color>(null) {
            { MacroCodeTokenizer.TokenType.CommandCode, Color.Red },
            { MacroCodeTokenizer.TokenType.StringArgument, Color.Green },
            { MacroCodeTokenizer.TokenType.IntArgument, Color.DarkBlue },
            { MacroCodeTokenizer.TokenType.FloatArgument, Color.Purple },
        };

        private void interpretLine(int lineIndex)
        {

            if (lineIndex < 0)
                return;

            string line = commandsEditorTextBox.TextBox.Lines[lineIndex];
            MacroCodeInterpreter interpreter = new MacroCodeInterpreter();
            interpreter.Formula = line;

            syntaxHighlightLine(lineIndex, interpreter.Tokens, interpreter.HasSyntaxError, interpreter.SyntaxErrorPosition);
            showPointForLine(lineIndex, interpreter);

        }

        private void syntaxHighlightLine(int lineIndex, IReadOnlyList<MacroCodeTokenizer.Token> tokens, bool hasSyntaxError, int syntaxErrorPosition)
        {

            RichTextBox rtfBox = commandsEditorTextBox.TextBox;

            int lineCount = rtfBox.Lines.Length;
            int lineStart = rtfBox.GetFirstCharIndexFromLine(lineIndex);
            int lineEnd = (lineIndex == (lineCount - 1)) ? rtfBox.TextLength : rtfBox.GetFirstCharIndexFromLine(lineIndex + 1);

            rtfBox.Select(lineStart, lineEnd - lineStart);
            rtfBox.SelectionColor = Color.Black;
            rtfBox.SelectionBackColor = Color.White;
            foreach (var token in tokens)
            {
                rtfBox.Select(lineStart + token.StartPosition, token.Length);
                rtfBox.SelectionColor = TOKEN_COLOR_CONVERTER.Convert(token.Type);
            }

            if (hasSyntaxError)
            {
                rtfBox.Select(lineStart + syntaxErrorPosition, 1);
                rtfBox.SelectionBackColor = Color.Yellow;
            }

        }

        private static readonly Color POINTCOLOR_SYNTAX_ERROR = Color.DarkRed;
        private static readonly Color POINTCOLOR_INCOMPLETE = Color.Cyan;
        private static readonly Color POINTCOLOR_NOTEXISTS = Color.Red;
        private static readonly Color POINTCOLOR_ARGUMENT_COUNT_MISMATCH = Color.Orange;
        private static readonly Color POINTCOLOR_ARGUMENT_TYPE_MISMATCH = Color.Yellow;
        private static readonly Color POINTCOLOR_OK = Color.DarkGreen;
        private static readonly Color POINTCOLOR_UNKNOWN = Color.Black;

        private void showPointForLine(int lineIndex, MacroCodeInterpreter interpreter)
        {

            RichTextBox rtfBox = commandsEditorTextBox.TextBox;

            if (interpreter.HasSyntaxError)
            {
                string tooltip = string.Format("Syntax error at position {0}.", interpreter.SyntaxErrorPosition);
                commandsEditorTextBox.SetPointColor(lineIndex, POINTCOLOR_SYNTAX_ERROR, tooltip);
                return;
            }

            if (interpreter.IsEmpty)
            {
                commandsEditorTextBox.RemovePoint(lineIndex);
                return;
            }

            if (!interpreter.IsComplete)
            {
                commandsEditorTextBox.SetPointColor(lineIndex, POINTCOLOR_INCOMPLETE, "Line incomplete.");
                return;
            }

            if (!interpreter.CommandExists)
            {
                commandsEditorTextBox.SetPointColor(lineIndex, POINTCOLOR_NOTEXISTS, "Command does not exist.");
                return;
            }

            IMacroCommand command = interpreter.GetCommand();
            if (interpreter.ArgumentCountMismatch)
            {
                string tooltip = string.Format("Command should have {0} arguments.", command.Arguments.Length);
                commandsEditorTextBox.SetPointColor(lineIndex, POINTCOLOR_ARGUMENT_COUNT_MISMATCH, tooltip);
                return;
            }

            List<bool> argumentTypeMatches = new List<bool>(interpreter.ArgumentTypeMatches);
            if (!argumentTypeMatches.TrueForAll(atm => atm))
            {
                string tooltip = "";
                for (int i = 0; i < argumentTypeMatches.Count; i++)
                {
                    if (!argumentTypeMatches[i])
                        tooltip += string.Format("Argument #{0} should be a {1}.\n", i+1, command.Arguments[i].KeyType.ToString().ToLower());
                }
                tooltip = tooltip.Substring(0, tooltip.Length - 1);
                commandsEditorTextBox.SetPointColor(lineIndex, POINTCOLOR_ARGUMENT_TYPE_MISMATCH, tooltip);
                return;
            }

            if (interpreter.IsComplete)
            {
                commandsEditorTextBox.SetPointColor(lineIndex, POINTCOLOR_OK, "OK.");
                return;
            }

            commandsEditorTextBox.SetPointColor(lineIndex, POINTCOLOR_UNKNOWN, "???");

        }

        private int currentLine()
        {
            RichTextBox rtfBox = commandsEditorTextBox.TextBox;
            int currentLineStart = rtfBox.GetFirstCharIndexOfCurrentLine();
            int currentLine = rtfBox.GetLineFromCharIndex(currentLineStart);
            if (currentLine >= rtfBox.Lines.Length)
                currentLine -= 1;
            return currentLine;
        }

        private void interpretCurrentLine(int offset = 0)
            => interpretLine(currentLine() + offset);

        private void interpretAllLines()
        {
            for (int i = 0; i < commandsEditorTextBox.TextBox.Lines.Length; i++)
                interpretLine(i);
        }

        private void commandsEditorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!noInterpretOnTextChange)
            {
                RichTextBox rtfBox = commandsEditorTextBox.TextBox;
                int selectionStart = rtfBox.SelectionStart;
                if ((selectionStart == rtfBox.Text.Length) && (rtfBox.SelectionLength == 0))
                    interpretCurrentLine();
                else
                    interpretAllLines();
                rtfBox.SelectionStart = selectionStart;
                rtfBox.SelectionLength = 0;
            }
        }

        private List<CommandArgumentControl> commandArgumentControls = new List<CommandArgumentControl>();

        private void selectCommandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            IMacroCommand selectedCommand = selectCommandComboBox.SelectedValue as IMacroCommand;
            commandDescriptionTextBox.Text = "";
            commandArgumentsPanel.Controls.Clear();
            foreach (CommandArgumentControl argControl in commandArgumentControls)
                argControl.ArgumentValueChanged -= CommandArgumentControl_ArgumentValueChanged;
            commandArgumentControls.Clear();

            addCommandButton.Enabled = (selectedCommand != null);

            if (selectedCommand != null)
            {
                commandDescriptionTextBox.Text = selectedCommand.Description;
                int i = 0;
                int argCount = selectedCommand.Arguments.Length;
                foreach (IMacroCommandArgument arg in selectedCommand.Arguments)
                {
                    var argumentControl = new CommandArgumentControl(arg, i, (i == (argCount - 1)));
                    argumentControl.ArgumentValueChanged += CommandArgumentControl_ArgumentValueChanged;
                    commandArgumentControls.Add(argumentControl);
                    i++;
                }
            }

            for (int i = commandArgumentControls.Count - 1; i >= 0; i--)
            {
                CommandArgumentControl control = commandArgumentControls[i];
                commandArgumentsPanel.Controls.Add(control);
                control.Dock = DockStyle.Top;
            }

        }

        private void CommandArgumentControl_ArgumentValueChanged(CommandArgumentControl control, IMacroCommandArgument argument, object newValue)
        {

            List<object> argumentValues = new List<object>();
            object[] argumentValuesArr = null;

            bool collecting = true;
            foreach (CommandArgumentControl argControl in commandArgumentControls)
            {

                if (collecting)
                    argumentValues.Add(argControl.ArgumentValue);
                else
                    argControl.PreviousArgumentValues = argumentValuesArr;

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

        private List<MacroCommandWithArguments> getCommandsWAFromCode()
        {
            MacroCodeInterpreter interpreter = new MacroCodeInterpreter();
            List<MacroCommandWithArguments> commandsWA = new List<MacroCommandWithArguments>();
            int lineIndex = 0;
            foreach (string line in commandsEditorTextBox.TextBox.Lines)
            {
                interpreter.Formula = line;
                if (interpreter.HasSyntaxError)
                    throw new Exception(string.Format("Line #{0} has syntax error!", (lineIndex + 1)));
                if (!interpreter.IsComplete && !interpreter.IsEmpty)
                    throw new Exception(string.Format("Line #{0} is incomplete!", (lineIndex + 1)));
                if (!interpreter.IsEmpty)
                    commandsWA.Add(interpreter.GetCommandWithArguments());
                lineIndex++;
            }
            return commandsWA;
        }
        private void validateCode()
        {
            MacroCodeInterpreter interpreter = new MacroCodeInterpreter();
            int lineIndex = 0;
            foreach (string line in commandsEditorTextBox.TextBox.Lines)
            {
                interpreter.Formula = line;
                if (interpreter.HasSyntaxError)
                    throw new Exception(string.Format("Line #{0} has syntax error!", (lineIndex + 1)));
                if (!interpreter.IsComplete && !interpreter.IsEmpty)
                    throw new Exception(string.Format("Line #{0} is incomplete!", (lineIndex + 1)));
                lineIndex++;
            }
        }

        private static string getCodeFromCommandsWA(IEnumerable<MacroCommandWithArguments> commandsWA)
        {
            StringBuilder code = new StringBuilder();
            foreach (MacroCommandWithArguments commandWA in commandsWA)
                code.AppendLine(commandWA.GetCode());
            return code.ToString();
        }

        private void loadCode()
        {
            noInterpretOnTextChange = true;
            commandsEditorTextBox.TextBox.Text = getCodeFromCommandsWA(macro.Commands);
            noInterpretOnTextChange = false;
            interpretAllLines();
        }
        #endregion

        #region Triggers
        private CustomDataGridView<MacroTriggerWithArgumentsProxy> triggersTableCDGV;

        private static readonly Color CELL_BACKGROUND_EDITING = Color.Yellow;
        private static readonly Color CELL_BACKGROUND_NOT_EDITING = Color.White;
        private void initTriggersTable()
        {

            triggersTableCDGV = createTable<MacroTriggerWithArgumentsProxy>(triggersTableContainerPanel, ref this.triggersTable);
            CustomDataGridViewColumnDescriptorBuilder<MacroTriggerWithArgumentsProxy> builder;

            // Column: code
            builder = getColumnDescriptorBuilderForTable<MacroTriggerWithArgumentsProxy>(triggersTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Code");
            builder.Width(150);
            builder.UpdaterMethod((triggerProxy, cell) => {
                cell.Value = triggerProxy.TriggerCode;
                cell.Style.BackColor = triggerProxy.Editing ? CELL_BACKGROUND_EDITING : CELL_BACKGROUND_NOT_EDITING;
            });
            builder.AddChangeEvent(nameof(MacroTriggerWithArgumentsProxy.TriggerCode));
            builder.AddChangeEvent(nameof(MacroTriggerWithArgumentsProxy.Editing));
            builder.BuildAndAdd();

            // Column: action description
            builder = getColumnDescriptorBuilderForTable<MacroTriggerWithArgumentsProxy>(triggersTableCDGV);
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Action description");
            builder.Width(350);
            builder.UpdaterMethod((triggerProxy, cell) => {
                cell.Value = triggerProxy.HumanReadable;
                cell.Style.BackColor = triggerProxy.Editing ? CELL_BACKGROUND_EDITING : CELL_BACKGROUND_NOT_EDITING;
            });
            builder.AddChangeEvent(nameof(MacroTriggerWithArgumentsProxy.HumanReadable));
            builder.AddChangeEvent(nameof(MacroTriggerWithArgumentsProxy.Editing));
            builder.BuildAndAdd();

            // Column: edit button
            builder = getColumnDescriptorBuilderForTable<MacroTriggerWithArgumentsProxy>(triggersTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.UpdaterMethod((triggerProxy, cell) => {
                cell.Style.BackColor = triggerProxy.Editing ? CELL_BACKGROUND_EDITING : CELL_BACKGROUND_NOT_EDITING;
            });
            builder.AddChangeEvent(nameof(MacroTriggerWithArgumentsProxy.Editing));
            builder.CellContentClickHandlerMethod((triggerProxy, cell, e) => { editTrigger(triggerProxy.Trigger); });
            builder.BuildAndAdd();

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<MacroTriggerWithArgumentsProxy>(triggersTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.UpdaterMethod((triggerProxy, cell) => {
                cell.Style.BackColor = triggerProxy.Editing ? CELL_BACKGROUND_EDITING : CELL_BACKGROUND_NOT_EDITING;
            });
            builder.AddChangeEvent(nameof(MacroTriggerWithArgumentsProxy.Editing));
            builder.CellContentClickHandlerMethod((triggerProxy, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this trigger #{0}?", cell.RowIndex);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    macroTriggersCollectionProxy.Remove(triggerProxy.Trigger);
            });
            builder.BuildAndAdd();

            // Bind collection
            triggersTableCDGV.BoundCollection = macroTriggersProxyCollection;

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

        private ObservableList<MacroTriggerWithArguments> macroTriggersCollectionProxy;
        private IObservableList<MacroTriggerWithArgumentsProxy> macroTriggersProxyCollection;

        private void initTriggerCollectionProxies()
        {
            macroTriggersCollectionProxy = new ObservableList<MacroTriggerWithArguments>();
            macroTriggersProxyCollection = new ObservableProxyList<MacroTriggerWithArgumentsProxy, MacroTriggerWithArguments>(macroTriggersCollectionProxy, mtwa => new MacroTriggerWithArgumentsProxy(mtwa));
        }

        private class MacroTriggerWithArgumentsProxy : Model.General.INotifyPropertyChanged
        {

            public MacroTriggerWithArguments Trigger { get; private set; }

            public string TriggerCode => Trigger.TriggerCode;

            public string HumanReadable => Trigger.HumanReadable;

            public MacroTriggerWithArgumentsProxy(MacroTriggerWithArguments original)
            {
                Trigger = original;
            }

            private bool editing = false;
            public bool Editing
            {
                get => editing;
                set
                {
                    editing = value;
                    PropertyChanged?.Invoke(nameof(Editing));
                    if (value == false)
                    {
                        PropertyChanged?.Invoke(nameof(TriggerCode));
                        PropertyChanged?.Invoke(nameof(HumanReadable));
                    }
                }
            }

            public event PropertyChangedDelegate PropertyChanged;

        }

        private void addOrSaveTriggerButton_Click(object sender, EventArgs e)
        {

            List<object> argumentValues = new List<object>();
            foreach (TriggerArgumentControl argControl in triggerArgumentControls)
                argumentValues.Add(argControl.ArgumentValue);
            object[] argumentValuesArr = argumentValues.ToArray();

            if (editedTriggerWA == null)
            {

                IMacroTrigger trigger = selectTriggerComboBox.SelectedValue as IMacroTrigger;
                if (trigger == null)
                {
                    // Show error message
                    return;
                }

                MacroTriggerWithArguments triggerWA = trigger.GetWithArguments(argumentValuesArr);
                macroTriggersCollectionProxy.Add(triggerWA);

            }
            else
            {
                editedTriggerWA.ArgumentValues = argumentValuesArr;
            }

            editTrigger(null);

        }

        private void addNewTriggerButton_Click(object sender, EventArgs e)
            => editTrigger(null);

        private const string BUTTON_TEXT_ADD_TRIGGER = "Add trigger";

        private const string BUTTON_TEXT_SAVE_TRIGGER = "Save trigger";

        private void editTrigger(MacroTriggerWithArguments triggerWA)
        {

            foreach (MacroTriggerWithArgumentsProxy proxy in macroTriggersProxyCollection)
                proxy.Editing = (proxy.Trigger == triggerWA);

            editedTriggerWA = triggerWA;

            if (triggerWA == null)
            {
                selectTriggerComboBox.SelectByValue(null);
                selectTriggerComboBox.Enabled = true;
                addOrSaveTriggerButton.Text = BUTTON_TEXT_ADD_TRIGGER;
                addNewTriggerButton.Enabled = false;
                return;
            }

            selectTriggerComboBox.SelectByValue(triggerWA.Trigger);
            selectTriggerComboBox.Enabled = false;
            addOrSaveTriggerButton.Text = BUTTON_TEXT_SAVE_TRIGGER;
            addNewTriggerButton.Enabled = true;
            int i = 0;
            foreach (TriggerArgumentControl argControl in triggerArgumentControls)
                argControl.ArgumentValue = triggerWA.ArgumentValues[i++];

        }

        private MacroTriggerWithArguments editedTriggerWA = null;

        private List<TriggerArgumentControl> triggerArgumentControls = new List<TriggerArgumentControl>();

        private void selectTriggerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            IMacroTrigger selectedTrigger = selectTriggerComboBox.SelectedValue as IMacroTrigger;
            triggerDescriptionTextBox.Text = "";
            triggerArgumentsPanel.Controls.Clear();
            foreach (TriggerArgumentControl argControl in triggerArgumentControls)
                argControl.ArgumentValueChanged -= TriggerArgumentControl_ArgumentValueChanged;
            triggerArgumentControls.Clear();

            addOrSaveTriggerButton.Enabled = (selectedTrigger != null);

            if (selectedTrigger != null)
            {
                triggerDescriptionTextBox.Text = selectedTrigger.Description;
                int i = 0;
                int argCount = selectedTrigger.Arguments.Length;
                foreach (IMacroTriggerArgument arg in selectedTrigger.Arguments)
                {
                    var argumentControl = new TriggerArgumentControl(arg, i, (i == (argCount - 1)));
                    argumentControl.ArgumentValueChanged += TriggerArgumentControl_ArgumentValueChanged;
                    triggerArgumentControls.Add(argumentControl);
                    i++;
                }
            }

            for (int i = triggerArgumentControls.Count - 1; i >= 0; i--)
            {
                TriggerArgumentControl control = triggerArgumentControls[i];
                triggerArgumentsPanel.Controls.Add(control);
                control.Dock = DockStyle.Top;
            }

        }

        private void TriggerArgumentControl_ArgumentValueChanged(TriggerArgumentControl control, IMacroTriggerArgument argument, object newValue)
        {

            List<object> argumentValues = new List<object>();
            object[] argumentValuesArr = null;

            bool collecting = true;
            foreach (TriggerArgumentControl argControl in triggerArgumentControls)
            {

                if (collecting)
                    argumentValues.Add(argControl.ArgumentValue);
                else
                    argControl.PreviousArgumentValues = argumentValuesArr;

                if (argControl == control)
                {
                    collecting = false;
                    argumentValuesArr = argumentValues.ToArray();
                }

            }

        }

        private void loadTriggers()
        {
            selectTriggerComboBox.CreateAdapterAsDataSource(MacroTriggerRegister.Instance.RegisteredTriggers, mt => string.Format("[{0}] {1}", mt.Code, mt.Name), true, "-");
        }

        #endregion

    }

}
