using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.Macros;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
            foreach (CommandArgumentControl argControl in argumentControls)
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
                string tooltip = string.Format("Arguments should have {0} arguments.", command.Arguments.Length);
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

        

        private void addTriggerButton_Click(object sender, EventArgs e)
        {
            //router.AddOutput();
        }

        

        private void MacroEditorForm_Load(object sender, EventArgs e)
        {
            initCommandsEditor();
            loadCommands();
        }

    }

}
