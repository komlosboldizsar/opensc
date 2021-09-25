namespace OpenSC.GUI.UMDs
{
    partial class McCurdyUmd1EditorForm
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
            this.layoutGroupBox = new System.Windows.Forms.GroupBox();
            this.layoutTable = new System.Windows.Forms.TableLayoutPanel();
            this.columnCountLabel = new System.Windows.Forms.Label();
            this.column1WidthLabel = new System.Windows.Forms.Label();
            this.column2WidthLabel = new System.Windows.Forms.Label();
            this.column3WidthLabel = new System.Windows.Forms.Label();
            this.columnCountRadioButtonContainer = new System.Windows.Forms.Panel();
            this.columnCountThreeRadioButton = new System.Windows.Forms.RadioButton();
            this.columnCountTwoRadioButton = new System.Windows.Forms.RadioButton();
            this.columnCountOneRadioButton = new System.Windows.Forms.RadioButton();
            this.column1WidthNumericField = new System.Windows.Forms.NumericUpDown();
            this.column2WidthNumericField = new System.Windows.Forms.NumericUpDown();
            this.column3WidthNumericField = new System.Windows.Forms.NumericUpDown();
            this.useSeparatorBarLabel = new System.Windows.Forms.Label();
            this.useSeparatorBarCheckBox = new System.Windows.Forms.CheckBox();
            this.dynamicSourcesGroupBox = new System.Windows.Forms.GroupBox();
            this.dynamicSourcesTable = new System.Windows.Forms.TableLayoutPanel();
            this.column3DynamicTextLabel = new System.Windows.Forms.Label();
            this.column2DynamicTextLabel = new System.Windows.Forms.Label();
            this.column1DynamicTextLabel = new System.Windows.Forms.Label();
            this.column1DynamicDataPanel = new System.Windows.Forms.Panel();
            this.column1DynamicTextSourceDropDown = new System.Windows.Forms.ComboBox();
            this.column1AlignmentDropDown = new System.Windows.Forms.ComboBox();
            this.column2DynamicDataPanel = new System.Windows.Forms.Panel();
            this.column2DynamicTextSourceDropDown = new System.Windows.Forms.ComboBox();
            this.column2AlignmentDropDown = new System.Windows.Forms.ComboBox();
            this.column3DynamicDataPanel = new System.Windows.Forms.Panel();
            this.column3DynamicTextSourceDropDown = new System.Windows.Forms.ComboBox();
            this.column3AlignmentDropDown = new System.Windows.Forms.ComboBox();
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.connectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.portLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.portDropDown = new System.Windows.Forms.ComboBox();
            this.addressNumericField = new System.Windows.Forms.NumericUpDown();
            this.mainTabControl.SuspendLayout();
            this.dynamicDataTabPage.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.layoutGroupBox.SuspendLayout();
            this.layoutTable.SuspendLayout();
            this.columnCountRadioButtonContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.column1WidthNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.column2WidthNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.column3WidthNumericField)).BeginInit();
            this.dynamicSourcesGroupBox.SuspendLayout();
            this.dynamicSourcesTable.SuspendLayout();
            this.column1DynamicDataPanel.SuspendLayout();
            this.column2DynamicDataPanel.SuspendLayout();
            this.column3DynamicDataPanel.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.connectionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addressNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Size = new System.Drawing.Size(775, 509);
            // 
            // tabPage1
            // 
            this.baseDataTabPage.Size = new System.Drawing.Size(767, 480);
            // 
            // dynamicDataTabPage
            // 
            this.dynamicDataTabPage.Controls.Add(this.dynamicSourcesGroupBox);
            this.dynamicDataTabPage.Controls.Add(this.layoutGroupBox);
            this.dynamicDataTabPage.Size = new System.Drawing.Size(481, 260);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(775, 509);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(795, 598);
            // 
            // layoutGroupBox
            // 
            this.layoutGroupBox.AutoSize = true;
            this.layoutGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutGroupBox.Controls.Add(this.layoutTable);
            this.layoutGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutGroupBox.Location = new System.Drawing.Point(3, 3);
            this.layoutGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.layoutGroupBox.Name = "layoutGroupBox";
            this.layoutGroupBox.Padding = new System.Windows.Forms.Padding(8, 8, 4, 8);
            this.layoutGroupBox.Size = new System.Drawing.Size(475, 211);
            this.layoutGroupBox.TabIndex = 0;
            this.layoutGroupBox.TabStop = false;
            this.layoutGroupBox.Text = "Layout";
            // 
            // layoutTable
            // 
            this.layoutTable.AutoSize = true;
            this.layoutTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutTable.ColumnCount = 2;
            this.layoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTable.Controls.Add(this.columnCountLabel, 0, 0);
            this.layoutTable.Controls.Add(this.column1WidthLabel, 0, 2);
            this.layoutTable.Controls.Add(this.column2WidthLabel, 0, 3);
            this.layoutTable.Controls.Add(this.column3WidthLabel, 0, 4);
            this.layoutTable.Controls.Add(this.columnCountRadioButtonContainer, 1, 0);
            this.layoutTable.Controls.Add(this.column1WidthNumericField, 1, 2);
            this.layoutTable.Controls.Add(this.column2WidthNumericField, 1, 3);
            this.layoutTable.Controls.Add(this.column3WidthNumericField, 1, 4);
            this.layoutTable.Controls.Add(this.useSeparatorBarLabel, 0, 5);
            this.layoutTable.Controls.Add(this.useSeparatorBarCheckBox, 1, 5);
            this.layoutTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTable.Location = new System.Drawing.Point(8, 23);
            this.layoutTable.Name = "layoutTable";
            this.layoutTable.RowCount = 6;
            this.layoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutTable.Size = new System.Drawing.Size(463, 180);
            this.layoutTable.TabIndex = 0;
            // 
            // columnCountLabel
            // 
            this.columnCountLabel.AutoSize = true;
            this.columnCountLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.columnCountLabel.Location = new System.Drawing.Point(3, 0);
            this.columnCountLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.columnCountLabel.Name = "columnCountLabel";
            this.columnCountLabel.Size = new System.Drawing.Size(94, 69);
            this.columnCountLabel.TabIndex = 1;
            this.columnCountLabel.Text = "Column count";
            this.columnCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // column1WidthLabel
            // 
            this.column1WidthLabel.AutoSize = true;
            this.column1WidthLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.column1WidthLabel.Location = new System.Drawing.Point(3, 69);
            this.column1WidthLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.column1WidthLabel.Name = "column1WidthLabel";
            this.column1WidthLabel.Size = new System.Drawing.Size(129, 28);
            this.column1WidthLabel.TabIndex = 3;
            this.column1WidthLabel.Text = "Width of column #1";
            this.column1WidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // column2WidthLabel
            // 
            this.column2WidthLabel.AutoSize = true;
            this.column2WidthLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.column2WidthLabel.Location = new System.Drawing.Point(3, 97);
            this.column2WidthLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.column2WidthLabel.Name = "column2WidthLabel";
            this.column2WidthLabel.Size = new System.Drawing.Size(129, 28);
            this.column2WidthLabel.TabIndex = 4;
            this.column2WidthLabel.Text = "Width of column #2";
            this.column2WidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // column3WidthLabel
            // 
            this.column3WidthLabel.AutoSize = true;
            this.column3WidthLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.column3WidthLabel.Location = new System.Drawing.Point(3, 125);
            this.column3WidthLabel.Name = "column3WidthLabel";
            this.column3WidthLabel.Size = new System.Drawing.Size(129, 28);
            this.column3WidthLabel.TabIndex = 7;
            this.column3WidthLabel.Text = "Width of column #3";
            this.column3WidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // columnCountRadioButtonContainer
            // 
            this.columnCountRadioButtonContainer.AutoSize = true;
            this.columnCountRadioButtonContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.columnCountRadioButtonContainer.Controls.Add(this.columnCountThreeRadioButton);
            this.columnCountRadioButtonContainer.Controls.Add(this.columnCountTwoRadioButton);
            this.columnCountRadioButtonContainer.Controls.Add(this.columnCountOneRadioButton);
            this.columnCountRadioButtonContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.columnCountRadioButtonContainer.Location = new System.Drawing.Point(150, 3);
            this.columnCountRadioButtonContainer.Name = "columnCountRadioButtonContainer";
            this.columnCountRadioButtonContainer.Size = new System.Drawing.Size(310, 63);
            this.columnCountRadioButtonContainer.TabIndex = 5;
            // 
            // columnCountThreeRadioButton
            // 
            this.columnCountThreeRadioButton.AutoSize = true;
            this.columnCountThreeRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.columnCountThreeRadioButton.Location = new System.Drawing.Point(0, 42);
            this.columnCountThreeRadioButton.Name = "columnCountThreeRadioButton";
            this.columnCountThreeRadioButton.Size = new System.Drawing.Size(310, 21);
            this.columnCountThreeRadioButton.TabIndex = 2;
            this.columnCountThreeRadioButton.TabStop = true;
            this.columnCountThreeRadioButton.Tag = "3";
            this.columnCountThreeRadioButton.Text = "three";
            this.columnCountThreeRadioButton.UseVisualStyleBackColor = true;
            this.columnCountThreeRadioButton.CheckedChanged += new System.EventHandler(this.columnCountRadioButtonCheckedStateChange);
            // 
            // columnCountTwoRadioButton
            // 
            this.columnCountTwoRadioButton.AutoSize = true;
            this.columnCountTwoRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.columnCountTwoRadioButton.Location = new System.Drawing.Point(0, 21);
            this.columnCountTwoRadioButton.Name = "columnCountTwoRadioButton";
            this.columnCountTwoRadioButton.Size = new System.Drawing.Size(310, 21);
            this.columnCountTwoRadioButton.TabIndex = 1;
            this.columnCountTwoRadioButton.TabStop = true;
            this.columnCountTwoRadioButton.Tag = "2";
            this.columnCountTwoRadioButton.Text = "two";
            this.columnCountTwoRadioButton.UseVisualStyleBackColor = true;
            this.columnCountTwoRadioButton.CheckedChanged += new System.EventHandler(this.columnCountRadioButtonCheckedStateChange);
            // 
            // columnCountOneRadioButton
            // 
            this.columnCountOneRadioButton.AutoSize = true;
            this.columnCountOneRadioButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.columnCountOneRadioButton.Location = new System.Drawing.Point(0, 0);
            this.columnCountOneRadioButton.Name = "columnCountOneRadioButton";
            this.columnCountOneRadioButton.Size = new System.Drawing.Size(310, 21);
            this.columnCountOneRadioButton.TabIndex = 0;
            this.columnCountOneRadioButton.TabStop = true;
            this.columnCountOneRadioButton.Tag = "1";
            this.columnCountOneRadioButton.Text = "one";
            this.columnCountOneRadioButton.UseVisualStyleBackColor = true;
            this.columnCountOneRadioButton.CheckedChanged += new System.EventHandler(this.columnCountRadioButtonCheckedStateChange);
            // 
            // column1WidthNumericField
            // 
            this.column1WidthNumericField.Location = new System.Drawing.Point(150, 72);
            this.column1WidthNumericField.Maximum = new decimal(new int[] {
            160,
            0,
            0,
            0});
            this.column1WidthNumericField.Name = "column1WidthNumericField";
            this.column1WidthNumericField.Size = new System.Drawing.Size(120, 22);
            this.column1WidthNumericField.TabIndex = 8;
            this.column1WidthNumericField.ValueChanged += new System.EventHandler(this.columnWidthChangedHandler);
            // 
            // column2WidthNumericField
            // 
            this.column2WidthNumericField.Location = new System.Drawing.Point(150, 100);
            this.column2WidthNumericField.Maximum = new decimal(new int[] {
            160,
            0,
            0,
            0});
            this.column2WidthNumericField.Name = "column2WidthNumericField";
            this.column2WidthNumericField.Size = new System.Drawing.Size(120, 22);
            this.column2WidthNumericField.TabIndex = 9;
            this.column2WidthNumericField.ValueChanged += new System.EventHandler(this.columnWidthChangedHandler);
            // 
            // column3WidthNumericField
            // 
            this.column3WidthNumericField.Location = new System.Drawing.Point(150, 128);
            this.column3WidthNumericField.Maximum = new decimal(new int[] {
            160,
            0,
            0,
            0});
            this.column3WidthNumericField.Name = "column3WidthNumericField";
            this.column3WidthNumericField.Size = new System.Drawing.Size(120, 22);
            this.column3WidthNumericField.TabIndex = 10;
            this.column3WidthNumericField.ValueChanged += new System.EventHandler(this.columnWidthChangedHandler);
            // 
            // useSeparatorBarLabel
            // 
            this.useSeparatorBarLabel.AutoSize = true;
            this.useSeparatorBarLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.useSeparatorBarLabel.Location = new System.Drawing.Point(3, 153);
            this.useSeparatorBarLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.useSeparatorBarLabel.Name = "useSeparatorBarLabel";
            this.useSeparatorBarLabel.Size = new System.Drawing.Size(96, 27);
            this.useSeparatorBarLabel.TabIndex = 11;
            this.useSeparatorBarLabel.Text = "Separator bar";
            this.useSeparatorBarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // useSeparatorBarCheckBox
            // 
            this.useSeparatorBarCheckBox.AutoSize = true;
            this.useSeparatorBarCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.useSeparatorBarCheckBox.Location = new System.Drawing.Point(150, 156);
            this.useSeparatorBarCheckBox.Name = "useSeparatorBarCheckBox";
            this.useSeparatorBarCheckBox.Size = new System.Drawing.Size(62, 21);
            this.useSeparatorBarCheckBox.TabIndex = 12;
            this.useSeparatorBarCheckBox.Text = "show";
            this.useSeparatorBarCheckBox.UseVisualStyleBackColor = true;
            this.useSeparatorBarCheckBox.CheckedChanged += new System.EventHandler(this.useSeparatorBarCheckBox_CheckedChanged);
            // 
            // dynamicSourcesGroupBox
            // 
            this.dynamicSourcesGroupBox.AutoSize = true;
            this.dynamicSourcesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dynamicSourcesGroupBox.Controls.Add(this.dynamicSourcesTable);
            this.dynamicSourcesGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.dynamicSourcesGroupBox.Location = new System.Drawing.Point(3, 214);
            this.dynamicSourcesGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.dynamicSourcesGroupBox.Name = "dynamicSourcesGroupBox";
            this.dynamicSourcesGroupBox.Padding = new System.Windows.Forms.Padding(8, 8, 4, 8);
            this.dynamicSourcesGroupBox.Size = new System.Drawing.Size(475, 143);
            this.dynamicSourcesGroupBox.TabIndex = 1;
            this.dynamicSourcesGroupBox.TabStop = false;
            this.dynamicSourcesGroupBox.Text = "Text sources";
            // 
            // dynamicSourcesTable
            // 
            this.dynamicSourcesTable.AutoSize = true;
            this.dynamicSourcesTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dynamicSourcesTable.ColumnCount = 2;
            this.dynamicSourcesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dynamicSourcesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dynamicSourcesTable.Controls.Add(this.column3DynamicTextLabel, 0, 2);
            this.dynamicSourcesTable.Controls.Add(this.column2DynamicTextLabel, 0, 1);
            this.dynamicSourcesTable.Controls.Add(this.column1DynamicTextLabel, 0, 0);
            this.dynamicSourcesTable.Controls.Add(this.column1DynamicDataPanel, 1, 0);
            this.dynamicSourcesTable.Controls.Add(this.column2DynamicDataPanel, 1, 1);
            this.dynamicSourcesTable.Controls.Add(this.column3DynamicDataPanel, 1, 2);
            this.dynamicSourcesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dynamicSourcesTable.Location = new System.Drawing.Point(8, 23);
            this.dynamicSourcesTable.Name = "dynamicSourcesTable";
            this.dynamicSourcesTable.RowCount = 3;
            this.dynamicSourcesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dynamicSourcesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dynamicSourcesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dynamicSourcesTable.Size = new System.Drawing.Size(463, 112);
            this.dynamicSourcesTable.TabIndex = 0;
            // 
            // column3DynamicTextLabel
            // 
            this.column3DynamicTextLabel.AutoSize = true;
            this.column3DynamicTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.column3DynamicTextLabel.Location = new System.Drawing.Point(3, 75);
            this.column3DynamicTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.column3DynamicTextLabel.Name = "column3DynamicTextLabel";
            this.column3DynamicTextLabel.Size = new System.Drawing.Size(75, 37);
            this.column3DynamicTextLabel.TabIndex = 6;
            this.column3DynamicTextLabel.Text = "Column #3";
            this.column3DynamicTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // column2DynamicTextLabel
            // 
            this.column2DynamicTextLabel.AutoSize = true;
            this.column2DynamicTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.column2DynamicTextLabel.Location = new System.Drawing.Point(3, 38);
            this.column2DynamicTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.column2DynamicTextLabel.Name = "column2DynamicTextLabel";
            this.column2DynamicTextLabel.Size = new System.Drawing.Size(75, 37);
            this.column2DynamicTextLabel.TabIndex = 5;
            this.column2DynamicTextLabel.Text = "Column #2";
            this.column2DynamicTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // column1DynamicTextLabel
            // 
            this.column1DynamicTextLabel.AutoSize = true;
            this.column1DynamicTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.column1DynamicTextLabel.Location = new System.Drawing.Point(3, 0);
            this.column1DynamicTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.column1DynamicTextLabel.Name = "column1DynamicTextLabel";
            this.column1DynamicTextLabel.Size = new System.Drawing.Size(75, 37);
            this.column1DynamicTextLabel.TabIndex = 4;
            this.column1DynamicTextLabel.Text = "Column #1";
            this.column1DynamicTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // column1DynamicDataPanel
            // 
            this.column1DynamicDataPanel.AutoSize = true;
            this.column1DynamicDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.column1DynamicDataPanel.Controls.Add(this.column1DynamicTextSourceDropDown);
            this.column1DynamicDataPanel.Controls.Add(this.column1AlignmentDropDown);
            this.column1DynamicDataPanel.Location = new System.Drawing.Point(96, 3);
            this.column1DynamicDataPanel.Name = "column1DynamicDataPanel";
            this.column1DynamicDataPanel.Size = new System.Drawing.Size(364, 32);
            this.column1DynamicDataPanel.TabIndex = 3;
            // 
            // column1DynamicTextSourceDropDown
            // 
            this.column1DynamicTextSourceDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.column1DynamicTextSourceDropDown.FormattingEnabled = true;
            this.column1DynamicTextSourceDropDown.Location = new System.Drawing.Point(3, 4);
            this.column1DynamicTextSourceDropDown.Name = "column1DynamicTextSourceDropDown";
            this.column1DynamicTextSourceDropDown.Size = new System.Drawing.Size(274, 24);
            this.column1DynamicTextSourceDropDown.TabIndex = 9;
            // 
            // column1AlignmentDropDown
            // 
            this.column1AlignmentDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.column1AlignmentDropDown.FormattingEnabled = true;
            this.column1AlignmentDropDown.Location = new System.Drawing.Point(283, 4);
            this.column1AlignmentDropDown.Name = "column1AlignmentDropDown";
            this.column1AlignmentDropDown.Size = new System.Drawing.Size(121, 24);
            this.column1AlignmentDropDown.TabIndex = 10;
            // 
            // column2DynamicDataPanel
            // 
            this.column2DynamicDataPanel.AutoSize = true;
            this.column2DynamicDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.column2DynamicDataPanel.Controls.Add(this.column2DynamicTextSourceDropDown);
            this.column2DynamicDataPanel.Controls.Add(this.column2AlignmentDropDown);
            this.column2DynamicDataPanel.Location = new System.Drawing.Point(96, 41);
            this.column2DynamicDataPanel.Name = "column2DynamicDataPanel";
            this.column2DynamicDataPanel.Size = new System.Drawing.Size(364, 31);
            this.column2DynamicDataPanel.TabIndex = 2;
            // 
            // column2DynamicTextSourceDropDown
            // 
            this.column2DynamicTextSourceDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.column2DynamicTextSourceDropDown.FormattingEnabled = true;
            this.column2DynamicTextSourceDropDown.Location = new System.Drawing.Point(3, 3);
            this.column2DynamicTextSourceDropDown.Name = "column2DynamicTextSourceDropDown";
            this.column2DynamicTextSourceDropDown.Size = new System.Drawing.Size(274, 25);
            this.column2DynamicTextSourceDropDown.TabIndex = 8;
            // 
            // column2AlignmentDropDown
            // 
            this.column2AlignmentDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.column2AlignmentDropDown.FormattingEnabled = true;
            this.column2AlignmentDropDown.Location = new System.Drawing.Point(283, 3);
            this.column2AlignmentDropDown.Name = "column2AlignmentDropDown";
            this.column2AlignmentDropDown.Size = new System.Drawing.Size(121, 25);
            this.column2AlignmentDropDown.TabIndex = 9;
            // 
            // column3DynamicDataPanel
            // 
            this.column3DynamicDataPanel.AutoSize = true;
            this.column3DynamicDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.column3DynamicDataPanel.Controls.Add(this.column3DynamicTextSourceDropDown);
            this.column3DynamicDataPanel.Controls.Add(this.column3AlignmentDropDown);
            this.column3DynamicDataPanel.Location = new System.Drawing.Point(96, 78);
            this.column3DynamicDataPanel.Name = "column3DynamicDataPanel";
            this.column3DynamicDataPanel.Size = new System.Drawing.Size(364, 31);
            this.column3DynamicDataPanel.TabIndex = 11;
            // 
            // column3DynamicTextSourceDropDown
            // 
            this.column3DynamicTextSourceDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.column3DynamicTextSourceDropDown.FormattingEnabled = true;
            this.column3DynamicTextSourceDropDown.Location = new System.Drawing.Point(3, 3);
            this.column3DynamicTextSourceDropDown.Name = "column3DynamicTextSourceDropDown";
            this.column3DynamicTextSourceDropDown.Size = new System.Drawing.Size(274, 25);
            this.column3DynamicTextSourceDropDown.TabIndex = 7;
            // 
            // column3AlignmentDropDown
            // 
            this.column3AlignmentDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.column3AlignmentDropDown.FormattingEnabled = true;
            this.column3AlignmentDropDown.Location = new System.Drawing.Point(283, 3);
            this.column3AlignmentDropDown.Name = "column3AlignmentDropDown";
            this.column3AlignmentDropDown.Size = new System.Drawing.Size(121, 25);
            this.column3AlignmentDropDown.TabIndex = 8;
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.AutoSize = true;
            this.connectionGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionGroupBox.Controls.Add(this.connectionTable);
            this.connectionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionGroupBox.Location = new System.Drawing.Point(3, 192);
            this.connectionGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Padding = new System.Windows.Forms.Padding(8, 8, 4, 8);
            this.connectionGroupBox.Size = new System.Drawing.Size(761, 89);
            this.connectionGroupBox.TabIndex = 3;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Connection";
            // 
            // connectionTable
            // 
            this.connectionTable.AutoSize = true;
            this.connectionTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionTable.ColumnCount = 2;
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.connectionTable.Controls.Add(this.portLabel, 0, 0);
            this.connectionTable.Controls.Add(this.addressLabel, 0, 1);
            this.connectionTable.Controls.Add(this.portDropDown, 1, 0);
            this.connectionTable.Controls.Add(this.addressNumericField, 1, 1);
            this.connectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTable.Location = new System.Drawing.Point(8, 23);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 2;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(749, 58);
            this.connectionTable.TabIndex = 0;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portLabel.Location = new System.Drawing.Point(3, 0);
            this.portLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(34, 30);
            this.portLabel.TabIndex = 0;
            this.portLabel.Text = "Port";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.addressLabel.Location = new System.Drawing.Point(3, 30);
            this.addressLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(60, 28);
            this.addressLabel.TabIndex = 1;
            this.addressLabel.Text = "Address";
            this.addressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portDropDown
            // 
            this.portDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portDropDown.FormattingEnabled = true;
            this.portDropDown.Location = new System.Drawing.Point(81, 3);
            this.portDropDown.Name = "portDropDown";
            this.portDropDown.Size = new System.Drawing.Size(212, 24);
            this.portDropDown.TabIndex = 2;
            // 
            // addressNumericField
            // 
            this.addressNumericField.Location = new System.Drawing.Point(81, 33);
            this.addressNumericField.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.addressNumericField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.addressNumericField.Name = "addressNumericField";
            this.addressNumericField.Size = new System.Drawing.Size(212, 22);
            this.addressNumericField.TabIndex = 3;
            this.addressNumericField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // McCurdyUmd1EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 654);
            this.DeleteButtonVisible = true;
            this.Name = "McCurdyUmd1EditorForm";
            this.mainTabControl.ResumeLayout(false);
            this.dynamicDataTabPage.ResumeLayout(false);
            this.dynamicDataTabPage.PerformLayout();
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.layoutGroupBox.ResumeLayout(false);
            this.layoutGroupBox.PerformLayout();
            this.layoutTable.ResumeLayout(false);
            this.layoutTable.PerformLayout();
            this.columnCountRadioButtonContainer.ResumeLayout(false);
            this.columnCountRadioButtonContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.column1WidthNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.column2WidthNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.column3WidthNumericField)).EndInit();
            this.dynamicSourcesGroupBox.ResumeLayout(false);
            this.dynamicSourcesGroupBox.PerformLayout();
            this.dynamicSourcesTable.ResumeLayout(false);
            this.dynamicSourcesTable.PerformLayout();
            this.column1DynamicDataPanel.ResumeLayout(false);
            this.column2DynamicDataPanel.ResumeLayout(false);
            this.column3DynamicDataPanel.ResumeLayout(false);
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addressNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox layoutGroupBox;
        private System.Windows.Forms.TableLayoutPanel layoutTable;
        private System.Windows.Forms.Label columnCountLabel;
        private System.Windows.Forms.Label column1WidthLabel;
        private System.Windows.Forms.Label column2WidthLabel;
        private System.Windows.Forms.Panel columnCountRadioButtonContainer;
        private System.Windows.Forms.RadioButton columnCountThreeRadioButton;
        private System.Windows.Forms.RadioButton columnCountTwoRadioButton;
        private System.Windows.Forms.RadioButton columnCountOneRadioButton;
        private System.Windows.Forms.Label column3WidthLabel;
        private System.Windows.Forms.GroupBox dynamicSourcesGroupBox;
        private System.Windows.Forms.TableLayoutPanel dynamicSourcesTable;
        private System.Windows.Forms.Label column3DynamicTextLabel;
        private System.Windows.Forms.Label column2DynamicTextLabel;
        private System.Windows.Forms.Label column1DynamicTextLabel;
        private System.Windows.Forms.ComboBox column3DynamicTextSourceDropDown;
        private System.Windows.Forms.ComboBox column2DynamicTextSourceDropDown;
        private System.Windows.Forms.ComboBox column1DynamicTextSourceDropDown;
        private System.Windows.Forms.NumericUpDown column1WidthNumericField;
        private System.Windows.Forms.NumericUpDown column2WidthNumericField;
        private System.Windows.Forms.NumericUpDown column3WidthNumericField;
        private System.Windows.Forms.Label useSeparatorBarLabel;
        private System.Windows.Forms.CheckBox useSeparatorBarCheckBox;
        private System.Windows.Forms.ComboBox column3AlignmentDropDown;
        private System.Windows.Forms.ComboBox column2AlignmentDropDown;
        private System.Windows.Forms.ComboBox column1AlignmentDropDown;
        private System.Windows.Forms.Panel column3DynamicDataPanel;
        private System.Windows.Forms.Panel column1DynamicDataPanel;
        private System.Windows.Forms.Panel column2DynamicDataPanel;
        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.ComboBox portDropDown;
        private System.Windows.Forms.NumericUpDown addressNumericField;
    }
}