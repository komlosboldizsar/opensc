namespace OpenSC.GUI.Macros
{
    partial class MacroEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.baseDataPanel = new System.Windows.Forms.Panel();
            this.baseDataGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.baseDataTabPage = new System.Windows.Forms.TabPage();
            this.commandsTabPage = new System.Windows.Forms.TabPage();
            this.commandsEditorContainerPanel = new System.Windows.Forms.Panel();
            this.commandsEditorTextBox = new OpenSC.GUI.GeneralComponents.RichTextBoxWithBar();
            this.addCommandPanel = new System.Windows.Forms.Panel();
            this.commandArgumentsContainer = new System.Windows.Forms.Panel();
            this.commandArgumentsGroupBox = new System.Windows.Forms.GroupBox();
            this.commandArgumentsPanel = new System.Windows.Forms.Panel();
            this.commandArgumentControl3 = new OpenSC.GUI.Macros.CommandArgumentControl();
            this.commandArgumentControl2 = new OpenSC.GUI.Macros.CommandArgumentControl();
            this.commandArgumentControl1 = new OpenSC.GUI.Macros.CommandArgumentControl();
            this.addCommandButtonsPanel = new System.Windows.Forms.Panel();
            this.addCommandButton = new System.Windows.Forms.Button();
            this.selectCommandPanel = new System.Windows.Forms.Panel();
            this.selectCommandGroupBox = new System.Windows.Forms.GroupBox();
            this.commandDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.distanceHolder1 = new System.Windows.Forms.Panel();
            this.selectCommandComboBox = new System.Windows.Forms.ComboBox();
            this.triggersTabPage = new System.Windows.Forms.TabPage();
            this.triggersTableContainerPanel = new System.Windows.Forms.Panel();
            this.triggersTable = new System.Windows.Forms.DataGridView();
            this.addTriggerPanel = new System.Windows.Forms.Panel();
            this.triggerArgumentsContainer = new System.Windows.Forms.Panel();
            this.triggerArgumentsGroupBox = new System.Windows.Forms.GroupBox();
            this.triggerArgumentsPanel = new System.Windows.Forms.Panel();
            this.triggerArgumentControl3 = new OpenSC.GUI.Macros.TriggerArgumentControl();
            this.triggerArgumentControl2 = new OpenSC.GUI.Macros.TriggerArgumentControl();
            this.triggerArgumentControl1 = new OpenSC.GUI.Macros.TriggerArgumentControl();
            this.addTriggerButtonsPanel = new System.Windows.Forms.Panel();
            this.addOrSaveTriggerButton = new System.Windows.Forms.Button();
            this.selectTriggerPanel = new System.Windows.Forms.Panel();
            this.selectTriggerGroupBox = new System.Windows.Forms.GroupBox();
            this.triggerDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.distanceHolder2 = new System.Windows.Forms.Panel();
            this.selectTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.baseDataPanel.SuspendLayout();
            this.baseDataGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.tabControl.SuspendLayout();
            this.baseDataTabPage.SuspendLayout();
            this.commandsTabPage.SuspendLayout();
            this.commandsEditorContainerPanel.SuspendLayout();
            this.addCommandPanel.SuspendLayout();
            this.commandArgumentsContainer.SuspendLayout();
            this.commandArgumentsGroupBox.SuspendLayout();
            this.commandArgumentsPanel.SuspendLayout();
            this.addCommandButtonsPanel.SuspendLayout();
            this.selectCommandPanel.SuspendLayout();
            this.selectCommandGroupBox.SuspendLayout();
            this.triggersTabPage.SuspendLayout();
            this.triggersTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.triggersTable)).BeginInit();
            this.addTriggerPanel.SuspendLayout();
            this.triggerArgumentsContainer.SuspendLayout();
            this.triggerArgumentsGroupBox.SuspendLayout();
            this.triggerArgumentsPanel.SuspendLayout();
            this.addTriggerButtonsPanel.SuspendLayout();
            this.selectTriggerPanel.SuspendLayout();
            this.selectTriggerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.tabControl);
            this.customElementsPanel.Size = new System.Drawing.Size(1482, 504);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(1482, 573);
            // 
            // baseDataPanel
            // 
            this.baseDataPanel.AutoSize = true;
            this.baseDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataPanel.Controls.Add(this.baseDataGroupBox);
            this.baseDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseDataPanel.Location = new System.Drawing.Point(3, 3);
            this.baseDataPanel.Name = "baseDataPanel";
            this.baseDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.baseDataPanel.Size = new System.Drawing.Size(1448, 94);
            this.baseDataPanel.TabIndex = 0;
            // 
            // baseDataGroupBox
            // 
            this.baseDataGroupBox.AutoSize = true;
            this.baseDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.baseDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.baseDataGroupBox.Name = "baseDataGroupBox";
            this.baseDataGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.baseDataGroupBox.Size = new System.Drawing.Size(1448, 87);
            this.baseDataGroupBox.TabIndex = 0;
            this.baseDataGroupBox.TabStop = false;
            this.baseDataGroupBox.Text = "Base data";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.idLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.idNumericField, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1432, 56);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.idLabel.Location = new System.Drawing.Point(3, 0);
            this.idLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(25, 28);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "ID:";
            this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.nameLabel.Location = new System.Drawing.Point(3, 28);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(49, 28);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name:";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameTextBox.Location = new System.Drawing.Point(70, 31);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(1359, 22);
            this.nameTextBox.TabIndex = 2;
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(70, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.baseDataTabPage);
            this.tabControl.Controls.Add(this.commandsTabPage);
            this.tabControl.Controls.Add(this.triggersTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1462, 494);
            this.tabControl.TabIndex = 1;
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.baseDataTabPage.Controls.Add(this.baseDataPanel);
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 25);
            this.baseDataTabPage.Name = "baseDataTabPage";
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.baseDataTabPage.Size = new System.Drawing.Size(1454, 465);
            this.baseDataTabPage.TabIndex = 0;
            this.baseDataTabPage.Text = "Base data";
            // 
            // commandsTabPage
            // 
            this.commandsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.commandsTabPage.Controls.Add(this.commandsEditorContainerPanel);
            this.commandsTabPage.Controls.Add(this.addCommandPanel);
            this.commandsTabPage.Location = new System.Drawing.Point(4, 25);
            this.commandsTabPage.Name = "commandsTabPage";
            this.commandsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.commandsTabPage.Size = new System.Drawing.Size(1454, 465);
            this.commandsTabPage.TabIndex = 1;
            this.commandsTabPage.Text = "Commands";
            // 
            // commandsEditorContainerPanel
            // 
            this.commandsEditorContainerPanel.Controls.Add(this.commandsEditorTextBox);
            this.commandsEditorContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandsEditorContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.commandsEditorContainerPanel.Name = "commandsEditorContainerPanel";
            this.commandsEditorContainerPanel.Size = new System.Drawing.Size(940, 459);
            this.commandsEditorContainerPanel.TabIndex = 2;
            // 
            // commandsEditorTextBox
            // 
            this.commandsEditorTextBox.AutoSize = true;
            this.commandsEditorTextBox.BarWidth = 22;
            this.commandsEditorTextBox.CircleSize = 16;
            this.commandsEditorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandsEditorTextBox.Font = new System.Drawing.Font("Consolas", 10.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.commandsEditorTextBox.Location = new System.Drawing.Point(0, 0);
            this.commandsEditorTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.commandsEditorTextBox.Name = "commandsEditorTextBox";
            this.commandsEditorTextBox.Size = new System.Drawing.Size(940, 459);
            this.commandsEditorTextBox.TabIndex = 0;
            this.commandsEditorTextBox.TextChanged += new System.EventHandler(this.commandsEditorTextBox_TextChanged);
            // 
            // addCommandPanel
            // 
            this.addCommandPanel.Controls.Add(this.commandArgumentsContainer);
            this.addCommandPanel.Controls.Add(this.addCommandButtonsPanel);
            this.addCommandPanel.Controls.Add(this.selectCommandPanel);
            this.addCommandPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.addCommandPanel.Location = new System.Drawing.Point(943, 3);
            this.addCommandPanel.Name = "addCommandPanel";
            this.addCommandPanel.Padding = new System.Windows.Forms.Padding(10);
            this.addCommandPanel.Size = new System.Drawing.Size(508, 459);
            this.addCommandPanel.TabIndex = 1;
            // 
            // commandArgumentsContainer
            // 
            this.commandArgumentsContainer.AutoSize = true;
            this.commandArgumentsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.commandArgumentsContainer.Controls.Add(this.commandArgumentsGroupBox);
            this.commandArgumentsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandArgumentsContainer.Location = new System.Drawing.Point(10, 152);
            this.commandArgumentsContainer.Name = "commandArgumentsContainer";
            this.commandArgumentsContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.commandArgumentsContainer.Size = new System.Drawing.Size(488, 256);
            this.commandArgumentsContainer.TabIndex = 7;
            // 
            // commandArgumentsGroupBox
            // 
            this.commandArgumentsGroupBox.AutoSize = true;
            this.commandArgumentsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.commandArgumentsGroupBox.Controls.Add(this.commandArgumentsPanel);
            this.commandArgumentsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandArgumentsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.commandArgumentsGroupBox.Name = "commandArgumentsGroupBox";
            this.commandArgumentsGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.commandArgumentsGroupBox.Size = new System.Drawing.Size(488, 249);
            this.commandArgumentsGroupBox.TabIndex = 0;
            this.commandArgumentsGroupBox.TabStop = false;
            this.commandArgumentsGroupBox.Text = "Arguments";
            // 
            // commandArgumentsPanel
            // 
            this.commandArgumentsPanel.AutoScroll = true;
            this.commandArgumentsPanel.Controls.Add(this.commandArgumentControl3);
            this.commandArgumentsPanel.Controls.Add(this.commandArgumentControl2);
            this.commandArgumentsPanel.Controls.Add(this.commandArgumentControl1);
            this.commandArgumentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandArgumentsPanel.Location = new System.Drawing.Point(8, 23);
            this.commandArgumentsPanel.Name = "commandArgumentsPanel";
            this.commandArgumentsPanel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.commandArgumentsPanel.Size = new System.Drawing.Size(472, 218);
            this.commandArgumentsPanel.TabIndex = 0;
            // 
            // commandArgumentControl3
            // 
            this.commandArgumentControl3.AutoSize = true;
            this.commandArgumentControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandArgumentControl3.Location = new System.Drawing.Point(0, 208);
            this.commandArgumentControl3.Name = "commandArgumentControl3";
            this.commandArgumentControl3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.commandArgumentControl3.Size = new System.Drawing.Size(446, 104);
            this.commandArgumentControl3.TabIndex = 2;
            // 
            // commandArgumentControl2
            // 
            this.commandArgumentControl2.AutoSize = true;
            this.commandArgumentControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandArgumentControl2.Location = new System.Drawing.Point(0, 104);
            this.commandArgumentControl2.Name = "commandArgumentControl2";
            this.commandArgumentControl2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.commandArgumentControl2.Size = new System.Drawing.Size(446, 104);
            this.commandArgumentControl2.TabIndex = 1;
            // 
            // commandArgumentControl1
            // 
            this.commandArgumentControl1.AutoSize = true;
            this.commandArgumentControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandArgumentControl1.Location = new System.Drawing.Point(0, 0);
            this.commandArgumentControl1.Name = "commandArgumentControl1";
            this.commandArgumentControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.commandArgumentControl1.Size = new System.Drawing.Size(446, 104);
            this.commandArgumentControl1.TabIndex = 0;
            // 
            // addCommandButtonsPanel
            // 
            this.addCommandButtonsPanel.Controls.Add(this.addCommandButton);
            this.addCommandButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addCommandButtonsPanel.Location = new System.Drawing.Point(10, 408);
            this.addCommandButtonsPanel.Name = "addCommandButtonsPanel";
            this.addCommandButtonsPanel.Size = new System.Drawing.Size(488, 41);
            this.addCommandButtonsPanel.TabIndex = 8;
            // 
            // addCommandButton
            // 
            this.addCommandButton.Enabled = false;
            this.addCommandButton.Location = new System.Drawing.Point(8, 9);
            this.addCommandButton.Margin = new System.Windows.Forms.Padding(6);
            this.addCommandButton.Name = "addCommandButton";
            this.addCommandButton.Size = new System.Drawing.Size(126, 26);
            this.addCommandButton.TabIndex = 0;
            this.addCommandButton.Text = "Add command";
            this.addCommandButton.UseVisualStyleBackColor = true;
            this.addCommandButton.Click += new System.EventHandler(this.addCommandButton_Click);
            // 
            // selectCommandPanel
            // 
            this.selectCommandPanel.AutoSize = true;
            this.selectCommandPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectCommandPanel.Controls.Add(this.selectCommandGroupBox);
            this.selectCommandPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectCommandPanel.Location = new System.Drawing.Point(10, 10);
            this.selectCommandPanel.Name = "selectCommandPanel";
            this.selectCommandPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.selectCommandPanel.Size = new System.Drawing.Size(488, 142);
            this.selectCommandPanel.TabIndex = 6;
            // 
            // selectCommandGroupBox
            // 
            this.selectCommandGroupBox.AutoSize = true;
            this.selectCommandGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectCommandGroupBox.Controls.Add(this.commandDescriptionTextBox);
            this.selectCommandGroupBox.Controls.Add(this.distanceHolder1);
            this.selectCommandGroupBox.Controls.Add(this.selectCommandComboBox);
            this.selectCommandGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectCommandGroupBox.Location = new System.Drawing.Point(0, 0);
            this.selectCommandGroupBox.Name = "selectCommandGroupBox";
            this.selectCommandGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.selectCommandGroupBox.Size = new System.Drawing.Size(488, 135);
            this.selectCommandGroupBox.TabIndex = 0;
            this.selectCommandGroupBox.TabStop = false;
            this.selectCommandGroupBox.Text = "Macro";
            // 
            // commandDescriptionTextBox
            // 
            this.commandDescriptionTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.commandDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandDescriptionTextBox.Enabled = false;
            this.commandDescriptionTextBox.Location = new System.Drawing.Point(8, 57);
            this.commandDescriptionTextBox.Multiline = true;
            this.commandDescriptionTextBox.Name = "commandDescriptionTextBox";
            this.commandDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commandDescriptionTextBox.Size = new System.Drawing.Size(472, 70);
            this.commandDescriptionTextBox.TabIndex = 3;
            // 
            // distanceHolder1
            // 
            this.distanceHolder1.Dock = System.Windows.Forms.DockStyle.Top;
            this.distanceHolder1.Location = new System.Drawing.Point(8, 47);
            this.distanceHolder1.Name = "distanceHolder1";
            this.distanceHolder1.Size = new System.Drawing.Size(472, 10);
            this.distanceHolder1.TabIndex = 4;
            // 
            // selectCommandComboBox
            // 
            this.selectCommandComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectCommandComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectCommandComboBox.FormattingEnabled = true;
            this.selectCommandComboBox.Location = new System.Drawing.Point(8, 23);
            this.selectCommandComboBox.Name = "selectCommandComboBox";
            this.selectCommandComboBox.Size = new System.Drawing.Size(472, 24);
            this.selectCommandComboBox.TabIndex = 1;
            this.selectCommandComboBox.SelectedIndexChanged += new System.EventHandler(this.selectCommandComboBox_SelectedIndexChanged);
            // 
            // triggersTabPage
            // 
            this.triggersTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.triggersTabPage.Controls.Add(this.triggersTableContainerPanel);
            this.triggersTabPage.Controls.Add(this.addTriggerPanel);
            this.triggersTabPage.Location = new System.Drawing.Point(4, 25);
            this.triggersTabPage.Name = "triggersTabPage";
            this.triggersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.triggersTabPage.Size = new System.Drawing.Size(1454, 465);
            this.triggersTabPage.TabIndex = 2;
            this.triggersTabPage.Text = "Triggers";
            // 
            // triggersTableContainerPanel
            // 
            this.triggersTableContainerPanel.Controls.Add(this.triggersTable);
            this.triggersTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersTableContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.triggersTableContainerPanel.Name = "triggersTableContainerPanel";
            this.triggersTableContainerPanel.Size = new System.Drawing.Size(940, 459);
            this.triggersTableContainerPanel.TabIndex = 2;
            // 
            // triggersTable
            // 
            this.triggersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.triggersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersTable.Location = new System.Drawing.Point(0, 0);
            this.triggersTable.Name = "triggersTable";
            this.triggersTable.RowHeadersWidth = 51;
            this.triggersTable.RowTemplate.Height = 24;
            this.triggersTable.Size = new System.Drawing.Size(940, 459);
            this.triggersTable.TabIndex = 0;
            // 
            // addTriggerPanel
            // 
            this.addTriggerPanel.Controls.Add(this.triggerArgumentsContainer);
            this.addTriggerPanel.Controls.Add(this.addTriggerButtonsPanel);
            this.addTriggerPanel.Controls.Add(this.selectTriggerPanel);
            this.addTriggerPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.addTriggerPanel.Location = new System.Drawing.Point(943, 3);
            this.addTriggerPanel.Name = "addTriggerPanel";
            this.addTriggerPanel.Padding = new System.Windows.Forms.Padding(10);
            this.addTriggerPanel.Size = new System.Drawing.Size(508, 459);
            this.addTriggerPanel.TabIndex = 1;
            // 
            // triggerArgumentsContainer
            // 
            this.triggerArgumentsContainer.AutoSize = true;
            this.triggerArgumentsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.triggerArgumentsContainer.Controls.Add(this.triggerArgumentsGroupBox);
            this.triggerArgumentsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggerArgumentsContainer.Location = new System.Drawing.Point(10, 152);
            this.triggerArgumentsContainer.Name = "triggerArgumentsContainer";
            this.triggerArgumentsContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.triggerArgumentsContainer.Size = new System.Drawing.Size(488, 256);
            this.triggerArgumentsContainer.TabIndex = 7;
            // 
            // triggerArgumentsGroupBox
            // 
            this.triggerArgumentsGroupBox.AutoSize = true;
            this.triggerArgumentsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.triggerArgumentsGroupBox.Controls.Add(this.triggerArgumentsPanel);
            this.triggerArgumentsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggerArgumentsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.triggerArgumentsGroupBox.Name = "triggerArgumentsGroupBox";
            this.triggerArgumentsGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.triggerArgumentsGroupBox.Size = new System.Drawing.Size(488, 249);
            this.triggerArgumentsGroupBox.TabIndex = 0;
            this.triggerArgumentsGroupBox.TabStop = false;
            this.triggerArgumentsGroupBox.Text = "Arguments";
            // 
            // triggerArgumentsPanel
            // 
            this.triggerArgumentsPanel.AutoScroll = true;
            this.triggerArgumentsPanel.Controls.Add(this.triggerArgumentControl3);
            this.triggerArgumentsPanel.Controls.Add(this.triggerArgumentControl2);
            this.triggerArgumentsPanel.Controls.Add(this.triggerArgumentControl1);
            this.triggerArgumentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggerArgumentsPanel.Location = new System.Drawing.Point(8, 23);
            this.triggerArgumentsPanel.Name = "triggerArgumentsPanel";
            this.triggerArgumentsPanel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.triggerArgumentsPanel.Size = new System.Drawing.Size(472, 218);
            this.triggerArgumentsPanel.TabIndex = 0;
            // 
            // triggerArgumentControl3
            // 
            this.triggerArgumentControl3.AutoSize = true;
            this.triggerArgumentControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.triggerArgumentControl3.Location = new System.Drawing.Point(0, 208);
            this.triggerArgumentControl3.Name = "triggerArgumentControl3";
            this.triggerArgumentControl3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.triggerArgumentControl3.Size = new System.Drawing.Size(446, 104);
            this.triggerArgumentControl3.TabIndex = 2;
            // 
            // triggerArgumentControl2
            // 
            this.triggerArgumentControl2.AutoSize = true;
            this.triggerArgumentControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.triggerArgumentControl2.Location = new System.Drawing.Point(0, 104);
            this.triggerArgumentControl2.Name = "triggerArgumentControl2";
            this.triggerArgumentControl2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.triggerArgumentControl2.Size = new System.Drawing.Size(446, 104);
            this.triggerArgumentControl2.TabIndex = 1;
            // 
            // triggerArgumentControl1
            // 
            this.triggerArgumentControl1.AutoSize = true;
            this.triggerArgumentControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.triggerArgumentControl1.Location = new System.Drawing.Point(0, 0);
            this.triggerArgumentControl1.Name = "triggerArgumentControl1";
            this.triggerArgumentControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.triggerArgumentControl1.Size = new System.Drawing.Size(446, 104);
            this.triggerArgumentControl1.TabIndex = 0;
            // 
            // addTriggerButtonsPanel
            // 
            this.addTriggerButtonsPanel.Controls.Add(this.addOrSaveTriggerButton);
            this.addTriggerButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addTriggerButtonsPanel.Location = new System.Drawing.Point(10, 408);
            this.addTriggerButtonsPanel.Name = "addTriggerButtonsPanel";
            this.addTriggerButtonsPanel.Size = new System.Drawing.Size(488, 41);
            this.addTriggerButtonsPanel.TabIndex = 8;
            // 
            // addOrSaveTriggerButton
            // 
            this.addOrSaveTriggerButton.Enabled = false;
            this.addOrSaveTriggerButton.Location = new System.Drawing.Point(8, 9);
            this.addOrSaveTriggerButton.Margin = new System.Windows.Forms.Padding(6);
            this.addOrSaveTriggerButton.Name = "addOrSaveTriggerButton";
            this.addOrSaveTriggerButton.Size = new System.Drawing.Size(126, 26);
            this.addOrSaveTriggerButton.TabIndex = 0;
            this.addOrSaveTriggerButton.Text = "Add trigger";
            this.addOrSaveTriggerButton.UseVisualStyleBackColor = true;
            this.addOrSaveTriggerButton.Click += new System.EventHandler(this.addOrSaveTriggerButton_Click);
            // 
            // selectTriggerPanel
            // 
            this.selectTriggerPanel.AutoSize = true;
            this.selectTriggerPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectTriggerPanel.Controls.Add(this.selectTriggerGroupBox);
            this.selectTriggerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectTriggerPanel.Location = new System.Drawing.Point(10, 10);
            this.selectTriggerPanel.Name = "selectTriggerPanel";
            this.selectTriggerPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.selectTriggerPanel.Size = new System.Drawing.Size(488, 142);
            this.selectTriggerPanel.TabIndex = 6;
            // 
            // selectTriggerGroupBox
            // 
            this.selectTriggerGroupBox.AutoSize = true;
            this.selectTriggerGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectTriggerGroupBox.Controls.Add(this.triggerDescriptionTextBox);
            this.selectTriggerGroupBox.Controls.Add(this.distanceHolder2);
            this.selectTriggerGroupBox.Controls.Add(this.selectTriggerComboBox);
            this.selectTriggerGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectTriggerGroupBox.Location = new System.Drawing.Point(0, 0);
            this.selectTriggerGroupBox.Name = "selectTriggerGroupBox";
            this.selectTriggerGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.selectTriggerGroupBox.Size = new System.Drawing.Size(488, 135);
            this.selectTriggerGroupBox.TabIndex = 0;
            this.selectTriggerGroupBox.TabStop = false;
            this.selectTriggerGroupBox.Text = "Trigger";
            // 
            // triggerDescriptionTextBox
            // 
            this.triggerDescriptionTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.triggerDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.triggerDescriptionTextBox.Enabled = false;
            this.triggerDescriptionTextBox.Location = new System.Drawing.Point(8, 57);
            this.triggerDescriptionTextBox.Multiline = true;
            this.triggerDescriptionTextBox.Name = "triggerDescriptionTextBox";
            this.triggerDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.triggerDescriptionTextBox.Size = new System.Drawing.Size(472, 70);
            this.triggerDescriptionTextBox.TabIndex = 3;
            // 
            // distanceHolder2
            // 
            this.distanceHolder2.Dock = System.Windows.Forms.DockStyle.Top;
            this.distanceHolder2.Location = new System.Drawing.Point(8, 47);
            this.distanceHolder2.Name = "distanceHolder2";
            this.distanceHolder2.Size = new System.Drawing.Size(472, 10);
            this.distanceHolder2.TabIndex = 4;
            // 
            // selectTriggerComboBox
            // 
            this.selectTriggerComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectTriggerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectTriggerComboBox.FormattingEnabled = true;
            this.selectTriggerComboBox.Location = new System.Drawing.Point(8, 23);
            this.selectTriggerComboBox.Name = "selectTriggerComboBox";
            this.selectTriggerComboBox.Size = new System.Drawing.Size(472, 24);
            this.selectTriggerComboBox.TabIndex = 1;
            this.selectTriggerComboBox.SelectedIndexChanged += new System.EventHandler(this.selectTriggerComboBox_SelectedIndexChanged);
            // 
            // MacroEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 629);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "Edit macro";
            this.MinimumSize = new System.Drawing.Size(1500, 600);
            this.Name = "MacroEditorForm";
            this.Text = "Edit macro";
            this.Load += new System.EventHandler(this.MacroEditorForm_Load);
            this.customElementsPanel.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.baseDataPanel.ResumeLayout(false);
            this.baseDataPanel.PerformLayout();
            this.baseDataGroupBox.ResumeLayout(false);
            this.baseDataGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.baseDataTabPage.ResumeLayout(false);
            this.baseDataTabPage.PerformLayout();
            this.commandsTabPage.ResumeLayout(false);
            this.commandsEditorContainerPanel.ResumeLayout(false);
            this.commandsEditorContainerPanel.PerformLayout();
            this.addCommandPanel.ResumeLayout(false);
            this.addCommandPanel.PerformLayout();
            this.commandArgumentsContainer.ResumeLayout(false);
            this.commandArgumentsContainer.PerformLayout();
            this.commandArgumentsGroupBox.ResumeLayout(false);
            this.commandArgumentsPanel.ResumeLayout(false);
            this.commandArgumentsPanel.PerformLayout();
            this.addCommandButtonsPanel.ResumeLayout(false);
            this.selectCommandPanel.ResumeLayout(false);
            this.selectCommandPanel.PerformLayout();
            this.selectCommandGroupBox.ResumeLayout(false);
            this.selectCommandGroupBox.PerformLayout();
            this.triggersTabPage.ResumeLayout(false);
            this.triggersTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.triggersTable)).EndInit();
            this.addTriggerPanel.ResumeLayout(false);
            this.addTriggerPanel.PerformLayout();
            this.triggerArgumentsContainer.ResumeLayout(false);
            this.triggerArgumentsContainer.PerformLayout();
            this.triggerArgumentsGroupBox.ResumeLayout(false);
            this.triggerArgumentsPanel.ResumeLayout(false);
            this.triggerArgumentsPanel.PerformLayout();
            this.addTriggerButtonsPanel.ResumeLayout(false);
            this.selectTriggerPanel.ResumeLayout(false);
            this.selectTriggerPanel.PerformLayout();
            this.selectTriggerGroupBox.ResumeLayout(false);
            this.selectTriggerGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel baseDataPanel;
        private System.Windows.Forms.GroupBox baseDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage commandsTabPage;
        private System.Windows.Forms.Button addCommandButton;
        private System.Windows.Forms.Panel commandsEditorContainerPanel;
        protected System.Windows.Forms.TabPage baseDataTabPage;
        protected System.Windows.Forms.Panel addCommandPanel;
        private GeneralComponents.RichTextBoxWithBar commandsEditorTextBox;
        private System.Windows.Forms.Panel commandArgumentsContainer;
        private System.Windows.Forms.GroupBox commandArgumentsGroupBox;
        private System.Windows.Forms.Panel selectCommandPanel;
        private System.Windows.Forms.GroupBox selectCommandGroupBox;
        private System.Windows.Forms.TextBox commandDescriptionTextBox;
        private System.Windows.Forms.ComboBox selectCommandComboBox;
        private System.Windows.Forms.Panel addCommandButtonsPanel;
        private System.Windows.Forms.Panel distanceHolder1;
        private System.Windows.Forms.Panel commandArgumentsPanel;
        private CommandArgumentControl commandArgumentControl3;
        private CommandArgumentControl commandArgumentControl2;
        private CommandArgumentControl commandArgumentControl1;
        private System.Windows.Forms.TabPage triggersTabPage;
        private System.Windows.Forms.Panel triggersTableContainerPanel;
        protected System.Windows.Forms.Panel addTriggerPanel;
        private System.Windows.Forms.Panel triggerArgumentsContainer;
        private System.Windows.Forms.GroupBox triggerArgumentsGroupBox;
        private System.Windows.Forms.Panel triggerArgumentsPanel;
        private System.Windows.Forms.Panel addTriggerButtonsPanel;
        private System.Windows.Forms.Button addOrSaveTriggerButton;
        private System.Windows.Forms.Panel selectTriggerPanel;
        private System.Windows.Forms.GroupBox selectTriggerGroupBox;
        private System.Windows.Forms.TextBox triggerDescriptionTextBox;
        private System.Windows.Forms.Panel distanceHolder2;
        private System.Windows.Forms.ComboBox selectTriggerComboBox;
        private System.Windows.Forms.DataGridView triggersTable;
        private TriggerArgumentControl triggerArgumentControl3;
        private TriggerArgumentControl triggerArgumentControl2;
        private TriggerArgumentControl triggerArgumentControl1;
    }
}