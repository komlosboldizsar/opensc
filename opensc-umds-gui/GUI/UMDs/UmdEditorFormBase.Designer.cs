namespace OpenSC.GUI.UMDs
{
    partial class UmdEditorFormBase
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
            this.components = new System.ComponentModel.Container();
            this.textsTabPage = new System.Windows.Forms.TabPage();
            this.textsTabPageNoTextLabel = new System.Windows.Forms.Label();
            this.textsSourceAndAlignmentGroupBox = new System.Windows.Forms.GroupBox();
            this.textsSourceAndAlignmentTable = new System.Windows.Forms.TableLayoutPanel();
            this.textNameExampleLabel = new System.Windows.Forms.Label();
            this.textUsedExampleLabel = new System.Windows.Forms.Label();
            this.textUsedExampleCheckBox = new System.Windows.Forms.CheckBox();
            this.textDynamicSourceExampleLabel = new System.Windows.Forms.Label();
            this.textStaticSourceExampleLabel = new System.Windows.Forms.Label();
            this.textAlignmentExampleLabel = new System.Windows.Forms.Label();
            this.textDynamicSourceExampleDropDown = new System.Windows.Forms.ComboBox();
            this.textStaticSourceExampleTextBox = new System.Windows.Forms.TextBox();
            this.textAlignmentExampleDropDown = new System.Windows.Forms.ComboBox();
            this.textUseStaticSourceExampleCheckBox = new System.Windows.Forms.CheckBox();
            this.talliesTabPage = new System.Windows.Forms.TabPage();
            this.talliesTabPageNoTallyLabel = new System.Windows.Forms.Label();
            this.talliesSourceAndColorGroupBox = new System.Windows.Forms.GroupBox();
            this.talliesSourceAndColorTable = new System.Windows.Forms.TableLayoutPanel();
            this.tallyNameExampleLabel = new System.Windows.Forms.Label();
            this.tallySourceExampleComboBox = new System.Windows.Forms.ComboBox();
            this.tallyColorExampleButton = new System.Windows.Forms.Button();
            this.tallyColorButtonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fullStaticAlignmentLabel = new System.Windows.Forms.Label();
            this.fullStaticAlignmentDropDown = new System.Windows.Forms.ComboBox();
            this.fullStaticTextGroupBox = new System.Windows.Forms.GroupBox();
            this.fullStaticTextTable = new System.Windows.Forms.TableLayoutPanel();
            this.staticTextLabel = new System.Windows.Forms.Label();
            this.staticTextTextBox = new System.Windows.Forms.TextBox();
            this.useStaticTextLabel = new System.Windows.Forms.Label();
            this.useStaticTextCheckBox = new System.Windows.Forms.CheckBox();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.connectionTabPage = new System.Windows.Forms.TabPage();
            this.fullStaticTextTabPage = new System.Windows.Forms.TabPage();
            this.statusMonitorGroupBox = new System.Windows.Forms.GroupBox();
            this.statusMonitorTable = new System.Windows.Forms.TableLayoutPanel();
            this.statusMonitorCompactTextLabel = new System.Windows.Forms.Label();
            this.statusMonitorRawTextTextLabel = new System.Windows.Forms.Label();
            this.statusMonitorTalliesLabel = new System.Windows.Forms.Label();
            this.statusMonitorTalliesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.statusMonitorNoTalliesLabel = new System.Windows.Forms.Label();
            this.statusMonitorExampleTallyLabel = new System.Windows.Forms.Label();
            this.statusMonitorCompactTextTextBox = new System.Windows.Forms.TextBox();
            this.statusMonitorRawTextTextBox = new System.Windows.Forms.TextBox();
            this.tallyNameToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.textsTabPage.SuspendLayout();
            this.textsSourceAndAlignmentGroupBox.SuspendLayout();
            this.textsSourceAndAlignmentTable.SuspendLayout();
            this.talliesTabPage.SuspendLayout();
            this.talliesSourceAndColorGroupBox.SuspendLayout();
            this.talliesSourceAndColorTable.SuspendLayout();
            this.fullStaticTextGroupBox.SuspendLayout();
            this.fullStaticTextTable.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.fullStaticTextTabPage.SuspendLayout();
            this.statusMonitorGroupBox.SuspendLayout();
            this.statusMonitorTable.SuspendLayout();
            this.statusMonitorTalliesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.mainTabControl);
            this.customElementsPanel.Controls.Add(this.statusMonitorGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 12);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(695, 523);
            this.customElementsPanel.Controls.SetChildIndex(this.statusMonitorGroupBox, 0);
            this.customElementsPanel.Controls.SetChildIndex(this.mainTabControl, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.mainContainer.Size = new System.Drawing.Size(715, 633);
            // 
            // textsTabPage
            // 
            this.textsTabPage.AutoScroll = true;
            this.textsTabPage.Controls.Add(this.textsTabPageNoTextLabel);
            this.textsTabPage.Controls.Add(this.textsSourceAndAlignmentGroupBox);
            this.textsTabPage.Location = new System.Drawing.Point(4, 29);
            this.textsTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textsTabPage.Name = "textsTabPage";
            this.textsTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textsTabPage.Size = new System.Drawing.Size(490, 42);
            this.textsTabPage.TabIndex = 1;
            this.textsTabPage.Text = "Texts";
            // 
            // textsTabPageNoTextLabel
            // 
            this.textsTabPageNoTextLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textsTabPageNoTextLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textsTabPageNoTextLabel.Location = new System.Drawing.Point(3, 165);
            this.textsTabPageNoTextLabel.Name = "textsTabPageNoTextLabel";
            this.textsTabPageNoTextLabel.Size = new System.Drawing.Size(463, 0);
            this.textsTabPageNoTextLabel.TabIndex = 4;
            this.textsTabPageNoTextLabel.Text = "This UMD has no texts.";
            this.textsTabPageNoTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textsSourceAndAlignmentGroupBox
            // 
            this.textsSourceAndAlignmentGroupBox.AutoSize = true;
            this.textsSourceAndAlignmentGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textsSourceAndAlignmentGroupBox.Controls.Add(this.textsSourceAndAlignmentTable);
            this.textsSourceAndAlignmentGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.textsSourceAndAlignmentGroupBox.Location = new System.Drawing.Point(3, 4);
            this.textsSourceAndAlignmentGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.textsSourceAndAlignmentGroupBox.Name = "textsSourceAndAlignmentGroupBox";
            this.textsSourceAndAlignmentGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.textsSourceAndAlignmentGroupBox.Size = new System.Drawing.Size(463, 161);
            this.textsSourceAndAlignmentGroupBox.TabIndex = 3;
            this.textsSourceAndAlignmentGroupBox.TabStop = false;
            this.textsSourceAndAlignmentGroupBox.Text = "Sources and alignments";
            // 
            // textsSourceAndAlignmentTable
            // 
            this.textsSourceAndAlignmentTable.AutoSize = true;
            this.textsSourceAndAlignmentTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textsSourceAndAlignmentTable.ColumnCount = 5;
            this.textsSourceAndAlignmentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.textsSourceAndAlignmentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.textsSourceAndAlignmentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.textsSourceAndAlignmentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.textsSourceAndAlignmentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.textsSourceAndAlignmentTable.Controls.Add(this.textNameExampleLabel, 0, 0);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textUsedExampleLabel, 1, 0);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textUsedExampleCheckBox, 2, 0);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textDynamicSourceExampleLabel, 1, 1);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textStaticSourceExampleLabel, 1, 2);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textAlignmentExampleLabel, 1, 3);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textDynamicSourceExampleDropDown, 3, 1);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textStaticSourceExampleTextBox, 3, 2);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textAlignmentExampleDropDown, 3, 3);
            this.textsSourceAndAlignmentTable.Controls.Add(this.textUseStaticSourceExampleCheckBox, 2, 2);
            this.textsSourceAndAlignmentTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textsSourceAndAlignmentTable.Location = new System.Drawing.Point(8, 25);
            this.textsSourceAndAlignmentTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textsSourceAndAlignmentTable.Name = "textsSourceAndAlignmentTable";
            this.textsSourceAndAlignmentTable.RowCount = 4;
            this.textsSourceAndAlignmentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.textsSourceAndAlignmentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.textsSourceAndAlignmentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.textsSourceAndAlignmentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.textsSourceAndAlignmentTable.Size = new System.Drawing.Size(447, 126);
            this.textsSourceAndAlignmentTable.TabIndex = 0;
            // 
            // textNameExampleLabel
            // 
            this.textNameExampleLabel.AutoSize = true;
            this.textNameExampleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.textNameExampleLabel.Location = new System.Drawing.Point(3, 0);
            this.textNameExampleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.textNameExampleLabel.Name = "textNameExampleLabel";
            this.textNameExampleLabel.Size = new System.Drawing.Size(57, 20);
            this.textNameExampleLabel.TabIndex = 0;
            this.textNameExampleLabel.Text = "Text #1";
            this.textNameExampleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textUsedExampleLabel
            // 
            this.textUsedExampleLabel.AutoSize = true;
            this.textUsedExampleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.textUsedExampleLabel.Location = new System.Drawing.Point(78, 0);
            this.textUsedExampleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.textUsedExampleLabel.Name = "textUsedExampleLabel";
            this.textUsedExampleLabel.Size = new System.Drawing.Size(42, 20);
            this.textUsedExampleLabel.TabIndex = 1;
            this.textUsedExampleLabel.Text = "Used";
            this.textUsedExampleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textUsedExampleCheckBox
            // 
            this.textUsedExampleCheckBox.AutoSize = true;
            this.textUsedExampleCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.textUsedExampleCheckBox.Location = new System.Drawing.Point(192, 0);
            this.textUsedExampleCheckBox.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.textUsedExampleCheckBox.Name = "textUsedExampleCheckBox";
            this.textUsedExampleCheckBox.Size = new System.Drawing.Size(18, 20);
            this.textUsedExampleCheckBox.TabIndex = 2;
            this.textUsedExampleCheckBox.UseVisualStyleBackColor = true;
            // 
            // textDynamicSourceExampleLabel
            // 
            this.textDynamicSourceExampleLabel.AutoSize = true;
            this.textDynamicSourceExampleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.textDynamicSourceExampleLabel.Location = new System.Drawing.Point(78, 20);
            this.textDynamicSourceExampleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.textDynamicSourceExampleLabel.Name = "textDynamicSourceExampleLabel";
            this.textDynamicSourceExampleLabel.Size = new System.Drawing.Size(96, 34);
            this.textDynamicSourceExampleLabel.TabIndex = 3;
            this.textDynamicSourceExampleLabel.Text = "Dynamic text";
            this.textDynamicSourceExampleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textStaticSourceExampleLabel
            // 
            this.textStaticSourceExampleLabel.AutoSize = true;
            this.textStaticSourceExampleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.textStaticSourceExampleLabel.Location = new System.Drawing.Point(78, 54);
            this.textStaticSourceExampleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.textStaticSourceExampleLabel.Name = "textStaticSourceExampleLabel";
            this.textStaticSourceExampleLabel.Size = new System.Drawing.Size(75, 33);
            this.textStaticSourceExampleLabel.TabIndex = 5;
            this.textStaticSourceExampleLabel.Text = "Static text";
            this.textStaticSourceExampleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textAlignmentExampleLabel
            // 
            this.textAlignmentExampleLabel.AutoSize = true;
            this.textAlignmentExampleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.textAlignmentExampleLabel.Location = new System.Drawing.Point(78, 87);
            this.textAlignmentExampleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 8);
            this.textAlignmentExampleLabel.Name = "textAlignmentExampleLabel";
            this.textAlignmentExampleLabel.Size = new System.Drawing.Size(78, 31);
            this.textAlignmentExampleLabel.TabIndex = 7;
            this.textAlignmentExampleLabel.Text = "Alignment";
            this.textAlignmentExampleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textDynamicSourceExampleDropDown
            // 
            this.textsSourceAndAlignmentTable.SetColumnSpan(this.textDynamicSourceExampleDropDown, 2);
            this.textDynamicSourceExampleDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.textDynamicSourceExampleDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textDynamicSourceExampleDropDown.FormattingEnabled = true;
            this.textDynamicSourceExampleDropDown.Location = new System.Drawing.Point(228, 23);
            this.textDynamicSourceExampleDropDown.Name = "textDynamicSourceExampleDropDown";
            this.textDynamicSourceExampleDropDown.Size = new System.Drawing.Size(216, 28);
            this.textDynamicSourceExampleDropDown.TabIndex = 9;
            // 
            // textStaticSourceExampleTextBox
            // 
            this.textsSourceAndAlignmentTable.SetColumnSpan(this.textStaticSourceExampleTextBox, 2);
            this.textStaticSourceExampleTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.textStaticSourceExampleTextBox.Location = new System.Drawing.Point(228, 57);
            this.textStaticSourceExampleTextBox.Name = "textStaticSourceExampleTextBox";
            this.textStaticSourceExampleTextBox.Size = new System.Drawing.Size(216, 27);
            this.textStaticSourceExampleTextBox.TabIndex = 10;
            // 
            // textAlignmentExampleDropDown
            // 
            this.textAlignmentExampleDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.textAlignmentExampleDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textAlignmentExampleDropDown.FormattingEnabled = true;
            this.textAlignmentExampleDropDown.Location = new System.Drawing.Point(228, 90);
            this.textAlignmentExampleDropDown.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.textAlignmentExampleDropDown.Name = "textAlignmentExampleDropDown";
            this.textAlignmentExampleDropDown.Size = new System.Drawing.Size(200, 28);
            this.textAlignmentExampleDropDown.TabIndex = 11;
            // 
            // textUseStaticSourceExampleCheckBox
            // 
            this.textUseStaticSourceExampleCheckBox.AutoSize = true;
            this.textUseStaticSourceExampleCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.textUseStaticSourceExampleCheckBox.Location = new System.Drawing.Point(192, 57);
            this.textUseStaticSourceExampleCheckBox.Name = "textUseStaticSourceExampleCheckBox";
            this.textUseStaticSourceExampleCheckBox.Size = new System.Drawing.Size(18, 27);
            this.textUseStaticSourceExampleCheckBox.TabIndex = 12;
            this.textUseStaticSourceExampleCheckBox.UseVisualStyleBackColor = true;
            // 
            // talliesTabPage
            // 
            this.talliesTabPage.AutoScroll = true;
            this.talliesTabPage.Controls.Add(this.talliesTabPageNoTallyLabel);
            this.talliesTabPage.Controls.Add(this.talliesSourceAndColorGroupBox);
            this.talliesTabPage.Location = new System.Drawing.Point(4, 29);
            this.talliesTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesTabPage.Name = "talliesTabPage";
            this.talliesTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesTabPage.Size = new System.Drawing.Size(687, 254);
            this.talliesTabPage.TabIndex = 2;
            this.talliesTabPage.Text = "Tallies";
            // 
            // talliesTabPageNoTallyLabel
            // 
            this.talliesTabPageNoTallyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talliesTabPageNoTallyLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.talliesTabPageNoTallyLabel.Location = new System.Drawing.Point(3, 75);
            this.talliesTabPageNoTallyLabel.Name = "talliesTabPageNoTallyLabel";
            this.talliesTabPageNoTallyLabel.Size = new System.Drawing.Size(681, 175);
            this.talliesTabPageNoTallyLabel.TabIndex = 3;
            this.talliesTabPageNoTallyLabel.Text = "This UMD has no tallies.";
            this.talliesTabPageNoTallyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // talliesSourceAndColorGroupBox
            // 
            this.talliesSourceAndColorGroupBox.AutoSize = true;
            this.talliesSourceAndColorGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesSourceAndColorGroupBox.Controls.Add(this.talliesSourceAndColorTable);
            this.talliesSourceAndColorGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.talliesSourceAndColorGroupBox.Location = new System.Drawing.Point(3, 4);
            this.talliesSourceAndColorGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.talliesSourceAndColorGroupBox.Name = "talliesSourceAndColorGroupBox";
            this.talliesSourceAndColorGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.talliesSourceAndColorGroupBox.Size = new System.Drawing.Size(681, 71);
            this.talliesSourceAndColorGroupBox.TabIndex = 2;
            this.talliesSourceAndColorGroupBox.TabStop = false;
            this.talliesSourceAndColorGroupBox.Text = "Sources and colors";
            // 
            // talliesSourceAndColorTable
            // 
            this.talliesSourceAndColorTable.AutoSize = true;
            this.talliesSourceAndColorTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesSourceAndColorTable.ColumnCount = 3;
            this.talliesSourceAndColorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.talliesSourceAndColorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.talliesSourceAndColorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.talliesSourceAndColorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.talliesSourceAndColorTable.Controls.Add(this.tallyNameExampleLabel, 0, 0);
            this.talliesSourceAndColorTable.Controls.Add(this.tallySourceExampleComboBox, 1, 0);
            this.talliesSourceAndColorTable.Controls.Add(this.tallyColorExampleButton, 2, 0);
            this.talliesSourceAndColorTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talliesSourceAndColorTable.Location = new System.Drawing.Point(8, 25);
            this.talliesSourceAndColorTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesSourceAndColorTable.Name = "talliesSourceAndColorTable";
            this.talliesSourceAndColorTable.RowCount = 1;
            this.talliesSourceAndColorTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.talliesSourceAndColorTable.Size = new System.Drawing.Size(665, 36);
            this.talliesSourceAndColorTable.TabIndex = 0;
            this.talliesSourceAndColorTable.Paint += new System.Windows.Forms.PaintEventHandler(this.talliesSourceAndColorTable_Paint);
            // 
            // tallyNameExampleLabel
            // 
            this.tallyNameExampleLabel.AutoSize = true;
            this.tallyNameExampleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tallyNameExampleLabel.Location = new System.Drawing.Point(3, 0);
            this.tallyNameExampleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.tallyNameExampleLabel.Name = "tallyNameExampleLabel";
            this.tallyNameExampleLabel.Size = new System.Drawing.Size(59, 36);
            this.tallyNameExampleLabel.TabIndex = 0;
            this.tallyNameExampleLabel.Text = "Tally #1";
            this.tallyNameExampleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tallySourceExampleComboBox
            // 
            this.tallySourceExampleComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tallySourceExampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tallySourceExampleComboBox.FormattingEnabled = true;
            this.tallySourceExampleComboBox.Location = new System.Drawing.Point(80, 3);
            this.tallySourceExampleComboBox.Name = "tallySourceExampleComboBox";
            this.tallySourceExampleComboBox.Size = new System.Drawing.Size(438, 28);
            this.tallySourceExampleComboBox.TabIndex = 4;
            // 
            // tallyColorExampleButton
            // 
            this.tallyColorExampleButton.BackColor = System.Drawing.Color.White;
            this.tallyColorExampleButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tallyColorExampleButton.ForeColor = System.Drawing.Color.Black;
            this.tallyColorExampleButton.Location = new System.Drawing.Point(524, 3);
            this.tallyColorExampleButton.Name = "tallyColorExampleButton";
            this.tallyColorExampleButton.Size = new System.Drawing.Size(138, 30);
            this.tallyColorExampleButton.TabIndex = 5;
            this.tallyColorExampleButton.Text = "set color";
            this.tallyColorExampleButton.UseVisualStyleBackColor = false;
            // 
            // fullStaticAlignmentLabel
            // 
            this.fullStaticAlignmentLabel.AutoSize = true;
            this.fullStaticAlignmentLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.fullStaticAlignmentLabel.Location = new System.Drawing.Point(3, 60);
            this.fullStaticAlignmentLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.fullStaticAlignmentLabel.Name = "fullStaticAlignmentLabel";
            this.fullStaticAlignmentLabel.Size = new System.Drawing.Size(78, 34);
            this.fullStaticAlignmentLabel.TabIndex = 9;
            this.fullStaticAlignmentLabel.Text = "Alignment";
            this.fullStaticAlignmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fullStaticAlignmentDropDown
            // 
            this.fullStaticAlignmentDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.fullStaticAlignmentDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fullStaticAlignmentDropDown.FormattingEnabled = true;
            this.fullStaticAlignmentDropDown.Location = new System.Drawing.Point(99, 63);
            this.fullStaticAlignmentDropDown.Name = "fullStaticAlignmentDropDown";
            this.fullStaticAlignmentDropDown.Size = new System.Drawing.Size(246, 28);
            this.fullStaticAlignmentDropDown.TabIndex = 10;
            // 
            // fullStaticTextGroupBox
            // 
            this.fullStaticTextGroupBox.AutoSize = true;
            this.fullStaticTextGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fullStaticTextGroupBox.Controls.Add(this.fullStaticTextTable);
            this.fullStaticTextGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.fullStaticTextGroupBox.Location = new System.Drawing.Point(3, 4);
            this.fullStaticTextGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.fullStaticTextGroupBox.Name = "fullStaticTextGroupBox";
            this.fullStaticTextGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.fullStaticTextGroupBox.Size = new System.Drawing.Size(484, 129);
            this.fullStaticTextGroupBox.TabIndex = 2;
            this.fullStaticTextGroupBox.TabStop = false;
            this.fullStaticTextGroupBox.Text = "Value and alignment";
            // 
            // fullStaticTextTable
            // 
            this.fullStaticTextTable.AutoSize = true;
            this.fullStaticTextTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fullStaticTextTable.ColumnCount = 2;
            this.fullStaticTextTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.fullStaticTextTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.fullStaticTextTable.Controls.Add(this.staticTextLabel, 0, 0);
            this.fullStaticTextTable.Controls.Add(this.staticTextTextBox, 1, 0);
            this.fullStaticTextTable.Controls.Add(this.useStaticTextLabel, 0, 1);
            this.fullStaticTextTable.Controls.Add(this.useStaticTextCheckBox, 1, 1);
            this.fullStaticTextTable.Controls.Add(this.fullStaticAlignmentLabel, 0, 2);
            this.fullStaticTextTable.Controls.Add(this.fullStaticAlignmentDropDown, 1, 2);
            this.fullStaticTextTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fullStaticTextTable.Location = new System.Drawing.Point(8, 25);
            this.fullStaticTextTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fullStaticTextTable.Name = "fullStaticTextTable";
            this.fullStaticTextTable.RowCount = 3;
            this.fullStaticTextTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.fullStaticTextTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.fullStaticTextTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.fullStaticTextTable.Size = new System.Drawing.Size(468, 94);
            this.fullStaticTextTable.TabIndex = 0;
            // 
            // staticTextLabel
            // 
            this.staticTextLabel.AutoSize = true;
            this.staticTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.staticTextLabel.Location = new System.Drawing.Point(3, 0);
            this.staticTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.staticTextLabel.Name = "staticTextLabel";
            this.staticTextLabel.Size = new System.Drawing.Size(45, 35);
            this.staticTextLabel.TabIndex = 1;
            this.staticTextLabel.Text = "Value";
            this.staticTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // staticTextTextBox
            // 
            this.staticTextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.staticTextTextBox.Location = new System.Drawing.Point(99, 4);
            this.staticTextTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.staticTextTextBox.Name = "staticTextTextBox";
            this.staticTextTextBox.Size = new System.Drawing.Size(366, 27);
            this.staticTextTextBox.TabIndex = 6;
            // 
            // useStaticTextLabel
            // 
            this.useStaticTextLabel.AutoSize = true;
            this.useStaticTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.useStaticTextLabel.Location = new System.Drawing.Point(3, 35);
            this.useStaticTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.useStaticTextLabel.Name = "useStaticTextLabel";
            this.useStaticTextLabel.Size = new System.Drawing.Size(33, 25);
            this.useStaticTextLabel.TabIndex = 7;
            this.useStaticTextLabel.Text = "Use";
            this.useStaticTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // useStaticTextCheckBox
            // 
            this.useStaticTextCheckBox.AutoSize = true;
            this.useStaticTextCheckBox.Location = new System.Drawing.Point(99, 39);
            this.useStaticTextCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.useStaticTextCheckBox.Name = "useStaticTextCheckBox";
            this.useStaticTextCheckBox.Size = new System.Drawing.Size(18, 17);
            this.useStaticTextCheckBox.TabIndex = 8;
            this.useStaticTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.connectionTabPage);
            this.mainTabControl.Controls.Add(this.textsTabPage);
            this.mainTabControl.Controls.Add(this.talliesTabPage);
            this.mainTabControl.Controls.Add(this.fullStaticTextTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 236);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(695, 287);
            this.mainTabControl.TabIndex = 3;
            // 
            // connectionTabPage
            // 
            this.connectionTabPage.Location = new System.Drawing.Point(4, 29);
            this.connectionTabPage.Name = "connectionTabPage";
            this.connectionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.connectionTabPage.Size = new System.Drawing.Size(687, 254);
            this.connectionTabPage.TabIndex = 3;
            this.connectionTabPage.Text = "Connection";
            // 
            // fullStaticTextTabPage
            // 
            this.fullStaticTextTabPage.Controls.Add(this.fullStaticTextGroupBox);
            this.fullStaticTextTabPage.Location = new System.Drawing.Point(4, 29);
            this.fullStaticTextTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fullStaticTextTabPage.Name = "fullStaticTextTabPage";
            this.fullStaticTextTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fullStaticTextTabPage.Size = new System.Drawing.Size(490, 42);
            this.fullStaticTextTabPage.TabIndex = 0;
            this.fullStaticTextTabPage.Text = "Full static text";
            // 
            // statusMonitorGroupBox
            // 
            this.statusMonitorGroupBox.AutoSize = true;
            this.statusMonitorGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusMonitorGroupBox.Controls.Add(this.statusMonitorTable);
            this.statusMonitorGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusMonitorGroupBox.Location = new System.Drawing.Point(0, 105);
            this.statusMonitorGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.statusMonitorGroupBox.Name = "statusMonitorGroupBox";
            this.statusMonitorGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.statusMonitorGroupBox.Size = new System.Drawing.Size(695, 131);
            this.statusMonitorGroupBox.TabIndex = 10;
            this.statusMonitorGroupBox.TabStop = false;
            this.statusMonitorGroupBox.Text = "Status monitor";
            // 
            // statusMonitorTable
            // 
            this.statusMonitorTable.AutoSize = true;
            this.statusMonitorTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusMonitorTable.ColumnCount = 2;
            this.statusMonitorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.statusMonitorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statusMonitorTable.Controls.Add(this.statusMonitorCompactTextLabel, 0, 0);
            this.statusMonitorTable.Controls.Add(this.statusMonitorRawTextTextLabel, 0, 1);
            this.statusMonitorTable.Controls.Add(this.statusMonitorTalliesLabel, 0, 2);
            this.statusMonitorTable.Controls.Add(this.statusMonitorTalliesPanel, 1, 2);
            this.statusMonitorTable.Controls.Add(this.statusMonitorCompactTextTextBox, 1, 0);
            this.statusMonitorTable.Controls.Add(this.statusMonitorRawTextTextBox, 1, 1);
            this.statusMonitorTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusMonitorTable.Location = new System.Drawing.Point(8, 25);
            this.statusMonitorTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.statusMonitorTable.Name = "statusMonitorTable";
            this.statusMonitorTable.RowCount = 3;
            this.statusMonitorTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.statusMonitorTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.statusMonitorTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.statusMonitorTable.Size = new System.Drawing.Size(679, 96);
            this.statusMonitorTable.TabIndex = 0;
            // 
            // statusMonitorCompactTextLabel
            // 
            this.statusMonitorCompactTextLabel.AutoSize = true;
            this.statusMonitorCompactTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.statusMonitorCompactTextLabel.Location = new System.Drawing.Point(3, 0);
            this.statusMonitorCompactTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.statusMonitorCompactTextLabel.Name = "statusMonitorCompactTextLabel";
            this.statusMonitorCompactTextLabel.Size = new System.Drawing.Size(36, 31);
            this.statusMonitorCompactTextLabel.TabIndex = 0;
            this.statusMonitorCompactTextLabel.Text = "Text";
            this.statusMonitorCompactTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusMonitorRawTextTextLabel
            // 
            this.statusMonitorRawTextTextLabel.AutoSize = true;
            this.statusMonitorRawTextTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.statusMonitorRawTextTextLabel.Location = new System.Drawing.Point(3, 31);
            this.statusMonitorRawTextTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.statusMonitorRawTextTextLabel.Name = "statusMonitorRawTextTextLabel";
            this.statusMonitorRawTextTextLabel.Size = new System.Drawing.Size(130, 31);
            this.statusMonitorRawTextTextLabel.TabIndex = 1;
            this.statusMonitorRawTextTextLabel.Text = "Text (to hardware)";
            this.statusMonitorRawTextTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusMonitorTalliesLabel
            // 
            this.statusMonitorTalliesLabel.AutoSize = true;
            this.statusMonitorTalliesLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.statusMonitorTalliesLabel.Location = new System.Drawing.Point(3, 62);
            this.statusMonitorTalliesLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.statusMonitorTalliesLabel.Name = "statusMonitorTalliesLabel";
            this.statusMonitorTalliesLabel.Size = new System.Drawing.Size(49, 34);
            this.statusMonitorTalliesLabel.TabIndex = 2;
            this.statusMonitorTalliesLabel.Text = "Tallies";
            this.statusMonitorTalliesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusMonitorTalliesPanel
            // 
            this.statusMonitorTalliesPanel.AutoSize = true;
            this.statusMonitorTalliesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusMonitorTalliesPanel.Controls.Add(this.statusMonitorNoTalliesLabel);
            this.statusMonitorTalliesPanel.Controls.Add(this.statusMonitorExampleTallyLabel);
            this.statusMonitorTalliesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusMonitorTalliesPanel.Location = new System.Drawing.Point(148, 65);
            this.statusMonitorTalliesPanel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.statusMonitorTalliesPanel.Name = "statusMonitorTalliesPanel";
            this.statusMonitorTalliesPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.statusMonitorTalliesPanel.Size = new System.Drawing.Size(528, 28);
            this.statusMonitorTalliesPanel.TabIndex = 3;
            // 
            // statusMonitorNoTalliesLabel
            // 
            this.statusMonitorNoTalliesLabel.AutoSize = true;
            this.statusMonitorNoTalliesLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.statusMonitorNoTalliesLabel.Location = new System.Drawing.Point(0, 3);
            this.statusMonitorNoTalliesLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.statusMonitorNoTalliesLabel.Name = "statusMonitorNoTalliesLabel";
            this.statusMonitorNoTalliesLabel.Size = new System.Drawing.Size(166, 20);
            this.statusMonitorNoTalliesLabel.TabIndex = 1;
            this.statusMonitorNoTalliesLabel.Text = "This UMD has no tallies.";
            // 
            // statusMonitorExampleTallyLabel
            // 
            this.statusMonitorExampleTallyLabel.AutoSize = true;
            this.statusMonitorExampleTallyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusMonitorExampleTallyLabel.Location = new System.Drawing.Point(172, 3);
            this.statusMonitorExampleTallyLabel.Name = "statusMonitorExampleTallyLabel";
            this.statusMonitorExampleTallyLabel.Size = new System.Drawing.Size(19, 22);
            this.statusMonitorExampleTallyLabel.TabIndex = 0;
            this.statusMonitorExampleTallyLabel.Text = "1";
            // 
            // statusMonitorCompactTextTextBox
            // 
            this.statusMonitorCompactTextTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusMonitorCompactTextTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusMonitorCompactTextTextBox.Location = new System.Drawing.Point(151, 3);
            this.statusMonitorCompactTextTextBox.Name = "statusMonitorCompactTextTextBox";
            this.statusMonitorCompactTextTextBox.ReadOnly = true;
            this.statusMonitorCompactTextTextBox.Size = new System.Drawing.Size(525, 25);
            this.statusMonitorCompactTextTextBox.TabIndex = 4;
            // 
            // statusMonitorRawTextTextBox
            // 
            this.statusMonitorRawTextTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusMonitorRawTextTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusMonitorRawTextTextBox.Location = new System.Drawing.Point(151, 34);
            this.statusMonitorRawTextTextBox.Name = "statusMonitorRawTextTextBox";
            this.statusMonitorRawTextTextBox.ReadOnly = true;
            this.statusMonitorRawTextTextBox.Size = new System.Drawing.Size(525, 25);
            this.statusMonitorRawTextTextBox.TabIndex = 5;
            // 
            // UmdEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 703);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New UMD";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(500, 538);
            this.Name = "UmdEditorFormBase";
            this.SubjectPlural = "UMDs";
            this.SubjectSingular = "UMD";
            this.Text = "New UMD";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.textsTabPage.ResumeLayout(false);
            this.textsTabPage.PerformLayout();
            this.textsSourceAndAlignmentGroupBox.ResumeLayout(false);
            this.textsSourceAndAlignmentGroupBox.PerformLayout();
            this.textsSourceAndAlignmentTable.ResumeLayout(false);
            this.textsSourceAndAlignmentTable.PerformLayout();
            this.talliesTabPage.ResumeLayout(false);
            this.talliesTabPage.PerformLayout();
            this.talliesSourceAndColorGroupBox.ResumeLayout(false);
            this.talliesSourceAndColorGroupBox.PerformLayout();
            this.talliesSourceAndColorTable.ResumeLayout(false);
            this.talliesSourceAndColorTable.PerformLayout();
            this.fullStaticTextGroupBox.ResumeLayout(false);
            this.fullStaticTextGroupBox.PerformLayout();
            this.fullStaticTextTable.ResumeLayout(false);
            this.fullStaticTextTable.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.fullStaticTextTabPage.ResumeLayout(false);
            this.fullStaticTextTabPage.PerformLayout();
            this.statusMonitorGroupBox.ResumeLayout(false);
            this.statusMonitorGroupBox.PerformLayout();
            this.statusMonitorTable.ResumeLayout(false);
            this.statusMonitorTable.PerformLayout();
            this.statusMonitorTalliesPanel.ResumeLayout(false);
            this.statusMonitorTalliesPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox fullStaticTextGroupBox;
        private System.Windows.Forms.TableLayoutPanel fullStaticTextTable;
        private System.Windows.Forms.Label staticTextLabel;
        private System.Windows.Forms.TextBox staticTextTextBox;
        private System.Windows.Forms.Label useStaticTextLabel;
        private System.Windows.Forms.CheckBox useStaticTextCheckBox;
        protected System.Windows.Forms.TabControl mainTabControl;
        protected System.Windows.Forms.TabPage textsTabPage;
        protected System.Windows.Forms.TabPage talliesTabPage;
        protected System.Windows.Forms.TabPage fullStaticTextTabPage;
        private System.Windows.Forms.GroupBox talliesSourceAndColorGroupBox;
        private System.Windows.Forms.TableLayoutPanel talliesSourceAndColorTable;
        private System.Windows.Forms.Label tallyNameExampleLabel;
        private System.Windows.Forms.ComboBox tallySourceExampleComboBox;
        private System.Windows.Forms.ToolTip tallyColorButtonToolTip;
        private System.Windows.Forms.Button tallyColorExampleButton;
        private System.Windows.Forms.GroupBox textsSourceAndAlignmentGroupBox;
        private System.Windows.Forms.TableLayoutPanel textsSourceAndAlignmentTable;
        private System.Windows.Forms.Label textNameExampleLabel;
        private System.Windows.Forms.Label textUsedExampleLabel;
        private System.Windows.Forms.CheckBox textUsedExampleCheckBox;
        private System.Windows.Forms.Label textDynamicSourceExampleLabel;
        private System.Windows.Forms.Label textStaticSourceExampleLabel;
        private System.Windows.Forms.Label textAlignmentExampleLabel;
        private System.Windows.Forms.ComboBox textDynamicSourceExampleDropDown;
        private System.Windows.Forms.TextBox textStaticSourceExampleTextBox;
        private System.Windows.Forms.ComboBox textAlignmentExampleDropDown;
        private System.Windows.Forms.CheckBox textUseStaticSourceExampleCheckBox;
        private System.Windows.Forms.Label fullStaticAlignmentLabel;
        private System.Windows.Forms.ComboBox fullStaticAlignmentDropDown;
        private System.Windows.Forms.GroupBox statusMonitorGroupBox;
        private System.Windows.Forms.TableLayoutPanel statusMonitorTable;
        private System.Windows.Forms.Label statusMonitorCompactTextLabel;
        private System.Windows.Forms.Label statusMonitorRawTextTextLabel;
        private System.Windows.Forms.Label statusMonitorTalliesLabel;
        private System.Windows.Forms.FlowLayoutPanel statusMonitorTalliesPanel;
        private System.Windows.Forms.Label statusMonitorExampleTallyLabel;
        private System.Windows.Forms.Label statusMonitorNoTalliesLabel;
        private System.Windows.Forms.ToolTip tallyNameToolTip;
        private System.Windows.Forms.TextBox statusMonitorCompactTextTextBox;
        private System.Windows.Forms.TextBox statusMonitorRawTextTextBox;
        private System.Windows.Forms.Label talliesTabPageNoTallyLabel;
        private System.Windows.Forms.Label textsTabPageNoTextLabel;
        protected System.Windows.Forms.TabPage connectionTabPage;
    }
}