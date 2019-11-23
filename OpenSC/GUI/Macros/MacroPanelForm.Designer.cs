namespace OpenSC.GUI.Macros
{
    partial class MacroPanelForm
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
            this.elementsPanel = new System.Windows.Forms.Panel();
            this.editorPanel = new System.Windows.Forms.Panel();
            this.elementDataPanel = new System.Windows.Forms.Panel();
            this.elementDataGroupBox = new System.Windows.Forms.GroupBox();
            this.elementDataTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.elementPosXLabel = new System.Windows.Forms.Label();
            this.elementPosYLabel = new System.Windows.Forms.Label();
            this.elementSizeWLabel = new System.Windows.Forms.Label();
            this.elementSizeHLabel = new System.Windows.Forms.Label();
            this.deleteLabel = new System.Windows.Forms.Label();
            this.elementBackgroundLabel = new System.Windows.Forms.Label();
            this.elementForegroundLabel = new System.Windows.Forms.Label();
            this.showLabelLabel = new System.Windows.Forms.Label();
            this.elementMacroLabel = new System.Windows.Forms.Label();
            this.elementLabelLabel = new System.Windows.Forms.Label();
            this.elementLabelTextBox = new System.Windows.Forms.TextBox();
            this.elementMacroDropDown = new System.Windows.Forms.ComboBox();
            this.elementShowLabelCheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundControlsContainerPanel = new System.Windows.Forms.Panel();
            this.backgroundColorPanelContainerPanel = new System.Windows.Forms.Panel();
            this.backgroundColorPanel = new System.Windows.Forms.Panel();
            this.pickBackgroundColorButton = new System.Windows.Forms.Button();
            this.foregroundControlsContainerPanel = new System.Windows.Forms.Panel();
            this.foregroundColorPanelContainerPanel = new System.Windows.Forms.Panel();
            this.foregroundColorPanel = new System.Windows.Forms.Panel();
            this.pickForegroundColorButton = new System.Windows.Forms.Button();
            this.removeSelectedElementButton = new System.Windows.Forms.Button();
            this.elementPosXNumericField = new System.Windows.Forms.NumericUpDown();
            this.elementPosYNumericField = new System.Windows.Forms.NumericUpDown();
            this.elementSizeWNumericField = new System.Windows.Forms.NumericUpDown();
            this.elementSizeHNumericField = new System.Windows.Forms.NumericUpDown();
            this.elementOperationsPanel = new System.Windows.Forms.Panel();
            this.elementOperationsGroupBox = new System.Windows.Forms.GroupBox();
            this.addElementButton = new System.Windows.Forms.Button();
            this.baseDataPanel = new System.Windows.Forms.Panel();
            this.baseDataGroupBox = new System.Windows.Forms.GroupBox();
            this.baseDataTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.colorPickerDialog = new System.Windows.Forms.ColorDialog();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.editorPanel.SuspendLayout();
            this.elementDataPanel.SuspendLayout();
            this.elementDataGroupBox.SuspendLayout();
            this.elementDataTableLayout.SuspendLayout();
            this.backgroundControlsContainerPanel.SuspendLayout();
            this.backgroundColorPanelContainerPanel.SuspendLayout();
            this.foregroundControlsContainerPanel.SuspendLayout();
            this.foregroundColorPanelContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.elementPosXNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elementPosYNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elementSizeWNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elementSizeHNumericField)).BeginInit();
            this.elementOperationsPanel.SuspendLayout();
            this.elementOperationsGroupBox.SuspendLayout();
            this.baseDataPanel.SuspendLayout();
            this.baseDataGroupBox.SuspendLayout();
            this.baseDataTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.elementsPanel);
            this.customElementsPanel.Controls.Add(this.editorPanel);
            this.customElementsPanel.Size = new System.Drawing.Size(1186, 599);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(1186, 668);
            // 
            // elementsPanel
            // 
            this.elementsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.elementsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementsPanel.Location = new System.Drawing.Point(10, 10);
            this.elementsPanel.Name = "elementsPanel";
            this.elementsPanel.Size = new System.Drawing.Size(886, 589);
            this.elementsPanel.TabIndex = 0;
            // 
            // editorPanel
            // 
            this.editorPanel.Controls.Add(this.elementDataPanel);
            this.editorPanel.Controls.Add(this.elementOperationsPanel);
            this.editorPanel.Controls.Add(this.baseDataPanel);
            this.editorPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.editorPanel.Location = new System.Drawing.Point(896, 10);
            this.editorPanel.Name = "editorPanel";
            this.editorPanel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.editorPanel.Size = new System.Drawing.Size(280, 589);
            this.editorPanel.TabIndex = 1;
            // 
            // elementDataPanel
            // 
            this.elementDataPanel.AutoSize = true;
            this.elementDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.elementDataPanel.Controls.Add(this.elementDataGroupBox);
            this.elementDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.elementDataPanel.Location = new System.Drawing.Point(10, 179);
            this.elementDataPanel.Name = "elementDataPanel";
            this.elementDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.elementDataPanel.Size = new System.Drawing.Size(270, 336);
            this.elementDataPanel.TabIndex = 2;
            // 
            // elementDataGroupBox
            // 
            this.elementDataGroupBox.AutoSize = true;
            this.elementDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.elementDataGroupBox.Controls.Add(this.elementDataTableLayout);
            this.elementDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.elementDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.elementDataGroupBox.Name = "elementDataGroupBox";
            this.elementDataGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.elementDataGroupBox.Size = new System.Drawing.Size(270, 329);
            this.elementDataGroupBox.TabIndex = 0;
            this.elementDataGroupBox.TabStop = false;
            this.elementDataGroupBox.Text = "Button settings";
            // 
            // elementDataTableLayout
            // 
            this.elementDataTableLayout.AutoSize = true;
            this.elementDataTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.elementDataTableLayout.ColumnCount = 2;
            this.elementDataTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.elementDataTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.elementDataTableLayout.Controls.Add(this.elementPosXLabel, 0, 6);
            this.elementDataTableLayout.Controls.Add(this.elementPosYLabel, 0, 7);
            this.elementDataTableLayout.Controls.Add(this.elementSizeWLabel, 0, 8);
            this.elementDataTableLayout.Controls.Add(this.elementSizeHLabel, 0, 9);
            this.elementDataTableLayout.Controls.Add(this.deleteLabel, 0, 5);
            this.elementDataTableLayout.Controls.Add(this.elementBackgroundLabel, 0, 4);
            this.elementDataTableLayout.Controls.Add(this.elementForegroundLabel, 0, 3);
            this.elementDataTableLayout.Controls.Add(this.showLabelLabel, 0, 2);
            this.elementDataTableLayout.Controls.Add(this.elementMacroLabel, 0, 0);
            this.elementDataTableLayout.Controls.Add(this.elementLabelLabel, 0, 1);
            this.elementDataTableLayout.Controls.Add(this.elementLabelTextBox, 1, 1);
            this.elementDataTableLayout.Controls.Add(this.elementMacroDropDown, 1, 0);
            this.elementDataTableLayout.Controls.Add(this.elementShowLabelCheckBox, 1, 2);
            this.elementDataTableLayout.Controls.Add(this.backgroundControlsContainerPanel, 1, 4);
            this.elementDataTableLayout.Controls.Add(this.foregroundControlsContainerPanel, 1, 3);
            this.elementDataTableLayout.Controls.Add(this.removeSelectedElementButton, 1, 5);
            this.elementDataTableLayout.Controls.Add(this.elementPosXNumericField, 1, 6);
            this.elementDataTableLayout.Controls.Add(this.elementPosYNumericField, 1, 7);
            this.elementDataTableLayout.Controls.Add(this.elementSizeWNumericField, 1, 8);
            this.elementDataTableLayout.Controls.Add(this.elementSizeHNumericField, 1, 9);
            this.elementDataTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementDataTableLayout.Location = new System.Drawing.Point(8, 23);
            this.elementDataTableLayout.Name = "elementDataTableLayout";
            this.elementDataTableLayout.RowCount = 10;
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.elementDataTableLayout.Size = new System.Drawing.Size(254, 298);
            this.elementDataTableLayout.TabIndex = 0;
            // 
            // elementPosXLabel
            // 
            this.elementPosXLabel.AutoSize = true;
            this.elementPosXLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.elementPosXLabel.Location = new System.Drawing.Point(3, 186);
            this.elementPosXLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.elementPosXLabel.Name = "elementPosXLabel";
            this.elementPosXLabel.Size = new System.Drawing.Size(21, 28);
            this.elementPosXLabel.TabIndex = 19;
            this.elementPosXLabel.Text = "X:";
            this.elementPosXLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementPosYLabel
            // 
            this.elementPosYLabel.AutoSize = true;
            this.elementPosYLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.elementPosYLabel.Location = new System.Drawing.Point(3, 214);
            this.elementPosYLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.elementPosYLabel.Name = "elementPosYLabel";
            this.elementPosYLabel.Size = new System.Drawing.Size(21, 28);
            this.elementPosYLabel.TabIndex = 18;
            this.elementPosYLabel.Text = "Y:";
            this.elementPosYLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementSizeWLabel
            // 
            this.elementSizeWLabel.AutoSize = true;
            this.elementSizeWLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.elementSizeWLabel.Location = new System.Drawing.Point(3, 242);
            this.elementSizeWLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.elementSizeWLabel.Name = "elementSizeWLabel";
            this.elementSizeWLabel.Size = new System.Drawing.Size(48, 28);
            this.elementSizeWLabel.TabIndex = 17;
            this.elementSizeWLabel.Text = "Width:";
            this.elementSizeWLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementSizeHLabel
            // 
            this.elementSizeHLabel.AutoSize = true;
            this.elementSizeHLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.elementSizeHLabel.Location = new System.Drawing.Point(3, 270);
            this.elementSizeHLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.elementSizeHLabel.Name = "elementSizeHLabel";
            this.elementSizeHLabel.Size = new System.Drawing.Size(53, 28);
            this.elementSizeHLabel.TabIndex = 12;
            this.elementSizeHLabel.Text = "Height:";
            this.elementSizeHLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // deleteLabel
            // 
            this.deleteLabel.AutoSize = true;
            this.deleteLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.deleteLabel.Location = new System.Drawing.Point(3, 151);
            this.deleteLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.deleteLabel.Name = "deleteLabel";
            this.deleteLabel.Size = new System.Drawing.Size(53, 35);
            this.deleteLabel.TabIndex = 10;
            this.deleteLabel.Text = "Delete:";
            this.deleteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementBackgroundLabel
            // 
            this.elementBackgroundLabel.AutoSize = true;
            this.elementBackgroundLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.elementBackgroundLabel.Location = new System.Drawing.Point(3, 116);
            this.elementBackgroundLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.elementBackgroundLabel.Name = "elementBackgroundLabel";
            this.elementBackgroundLabel.Size = new System.Drawing.Size(88, 35);
            this.elementBackgroundLabel.TabIndex = 7;
            this.elementBackgroundLabel.Text = "Background:";
            this.elementBackgroundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementForegroundLabel
            // 
            this.elementForegroundLabel.AutoSize = true;
            this.elementForegroundLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.elementForegroundLabel.Location = new System.Drawing.Point(3, 81);
            this.elementForegroundLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.elementForegroundLabel.Name = "elementForegroundLabel";
            this.elementForegroundLabel.Size = new System.Drawing.Size(86, 35);
            this.elementForegroundLabel.TabIndex = 6;
            this.elementForegroundLabel.Text = "Foreground:";
            this.elementForegroundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // showLabelLabel
            // 
            this.showLabelLabel.AutoSize = true;
            this.showLabelLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.showLabelLabel.Location = new System.Drawing.Point(3, 58);
            this.showLabelLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.showLabelLabel.Name = "showLabelLabel";
            this.showLabelLabel.Size = new System.Drawing.Size(80, 23);
            this.showLabelLabel.TabIndex = 4;
            this.showLabelLabel.Text = "Show label:";
            this.showLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementMacroLabel
            // 
            this.elementMacroLabel.AutoSize = true;
            this.elementMacroLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.elementMacroLabel.Location = new System.Drawing.Point(3, 0);
            this.elementMacroLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.elementMacroLabel.Name = "elementMacroLabel";
            this.elementMacroLabel.Size = new System.Drawing.Size(51, 30);
            this.elementMacroLabel.TabIndex = 0;
            this.elementMacroLabel.Text = "Macro:";
            this.elementMacroLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementLabelLabel
            // 
            this.elementLabelLabel.AutoSize = true;
            this.elementLabelLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.elementLabelLabel.Location = new System.Drawing.Point(3, 30);
            this.elementLabelLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.elementLabelLabel.Name = "elementLabelLabel";
            this.elementLabelLabel.Size = new System.Drawing.Size(47, 28);
            this.elementLabelLabel.TabIndex = 1;
            this.elementLabelLabel.Text = "Label:";
            this.elementLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // elementLabelTextBox
            // 
            this.elementLabelTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.elementLabelTextBox.Location = new System.Drawing.Point(109, 33);
            this.elementLabelTextBox.Name = "elementLabelTextBox";
            this.elementLabelTextBox.Size = new System.Drawing.Size(142, 22);
            this.elementLabelTextBox.TabIndex = 2;
            this.elementLabelTextBox.TextChanged += new System.EventHandler(this.elementLabelTextBox_TextChanged);
            // 
            // elementMacroDropDown
            // 
            this.elementMacroDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.elementMacroDropDown.FormattingEnabled = true;
            this.elementMacroDropDown.Location = new System.Drawing.Point(109, 3);
            this.elementMacroDropDown.Name = "elementMacroDropDown";
            this.elementMacroDropDown.Size = new System.Drawing.Size(142, 24);
            this.elementMacroDropDown.TabIndex = 3;
            this.elementMacroDropDown.SelectedIndexChanged += new System.EventHandler(this.elementMacroDropDown_SelectedIndexChanged);
            // 
            // elementShowLabelCheckBox
            // 
            this.elementShowLabelCheckBox.AutoSize = true;
            this.elementShowLabelCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.elementShowLabelCheckBox.Location = new System.Drawing.Point(109, 61);
            this.elementShowLabelCheckBox.Name = "elementShowLabelCheckBox";
            this.elementShowLabelCheckBox.Size = new System.Drawing.Size(142, 17);
            this.elementShowLabelCheckBox.TabIndex = 5;
            this.elementShowLabelCheckBox.UseVisualStyleBackColor = true;
            this.elementShowLabelCheckBox.CheckedChanged += new System.EventHandler(this.showLabelCheckBox_CheckedChanged);
            // 
            // backgroundControlsContainerPanel
            // 
            this.backgroundControlsContainerPanel.Controls.Add(this.backgroundColorPanelContainerPanel);
            this.backgroundControlsContainerPanel.Controls.Add(this.pickBackgroundColorButton);
            this.backgroundControlsContainerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.backgroundControlsContainerPanel.Location = new System.Drawing.Point(109, 119);
            this.backgroundControlsContainerPanel.Name = "backgroundControlsContainerPanel";
            this.backgroundControlsContainerPanel.Size = new System.Drawing.Size(142, 29);
            this.backgroundControlsContainerPanel.TabIndex = 9;
            // 
            // backgroundColorPanelContainerPanel
            // 
            this.backgroundColorPanelContainerPanel.Controls.Add(this.backgroundColorPanel);
            this.backgroundColorPanelContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundColorPanelContainerPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundColorPanelContainerPanel.Name = "backgroundColorPanelContainerPanel";
            this.backgroundColorPanelContainerPanel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.backgroundColorPanelContainerPanel.Size = new System.Drawing.Size(84, 29);
            this.backgroundColorPanelContainerPanel.TabIndex = 1;
            // 
            // backgroundColorPanel
            // 
            this.backgroundColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backgroundColorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundColorPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundColorPanel.Name = "backgroundColorPanel";
            this.backgroundColorPanel.Size = new System.Drawing.Size(74, 29);
            this.backgroundColorPanel.TabIndex = 0;
            // 
            // pickBackgroundColorButton
            // 
            this.pickBackgroundColorButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pickBackgroundColorButton.Location = new System.Drawing.Point(84, 0);
            this.pickBackgroundColorButton.Margin = new System.Windows.Forms.Padding(0);
            this.pickBackgroundColorButton.Name = "pickBackgroundColorButton";
            this.pickBackgroundColorButton.Size = new System.Drawing.Size(58, 29);
            this.pickBackgroundColorButton.TabIndex = 0;
            this.pickBackgroundColorButton.Text = "Pick";
            this.pickBackgroundColorButton.UseVisualStyleBackColor = true;
            this.pickBackgroundColorButton.Click += new System.EventHandler(this.pickBackgroundColorButton_Click);
            // 
            // foregroundControlsContainerPanel
            // 
            this.foregroundControlsContainerPanel.Controls.Add(this.foregroundColorPanelContainerPanel);
            this.foregroundControlsContainerPanel.Controls.Add(this.pickForegroundColorButton);
            this.foregroundControlsContainerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.foregroundControlsContainerPanel.Location = new System.Drawing.Point(109, 84);
            this.foregroundControlsContainerPanel.Name = "foregroundControlsContainerPanel";
            this.foregroundControlsContainerPanel.Size = new System.Drawing.Size(142, 29);
            this.foregroundControlsContainerPanel.TabIndex = 8;
            // 
            // foregroundColorPanelContainerPanel
            // 
            this.foregroundColorPanelContainerPanel.Controls.Add(this.foregroundColorPanel);
            this.foregroundColorPanelContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foregroundColorPanelContainerPanel.Location = new System.Drawing.Point(0, 0);
            this.foregroundColorPanelContainerPanel.Name = "foregroundColorPanelContainerPanel";
            this.foregroundColorPanelContainerPanel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.foregroundColorPanelContainerPanel.Size = new System.Drawing.Size(84, 29);
            this.foregroundColorPanelContainerPanel.TabIndex = 1;
            // 
            // foregroundColorPanel
            // 
            this.foregroundColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.foregroundColorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foregroundColorPanel.Location = new System.Drawing.Point(0, 0);
            this.foregroundColorPanel.Name = "foregroundColorPanel";
            this.foregroundColorPanel.Size = new System.Drawing.Size(74, 29);
            this.foregroundColorPanel.TabIndex = 0;
            // 
            // pickForegroundColorButton
            // 
            this.pickForegroundColorButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pickForegroundColorButton.Location = new System.Drawing.Point(84, 0);
            this.pickForegroundColorButton.Margin = new System.Windows.Forms.Padding(0);
            this.pickForegroundColorButton.Name = "pickForegroundColorButton";
            this.pickForegroundColorButton.Size = new System.Drawing.Size(58, 29);
            this.pickForegroundColorButton.TabIndex = 0;
            this.pickForegroundColorButton.Text = "Pick";
            this.pickForegroundColorButton.UseVisualStyleBackColor = true;
            this.pickForegroundColorButton.Click += new System.EventHandler(this.pickForegroundColorButton_Click);
            // 
            // removeSelectedElementButton
            // 
            this.removeSelectedElementButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.removeSelectedElementButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.removeSelectedElementButton.Location = new System.Drawing.Point(109, 154);
            this.removeSelectedElementButton.Name = "removeSelectedElementButton";
            this.removeSelectedElementButton.Size = new System.Drawing.Size(142, 29);
            this.removeSelectedElementButton.TabIndex = 11;
            this.removeSelectedElementButton.Text = "Delete";
            this.removeSelectedElementButton.UseVisualStyleBackColor = true;
            this.removeSelectedElementButton.Click += new System.EventHandler(this.removeSelectedElementButton_Click);
            // 
            // elementPosXNumericField
            // 
            this.elementPosXNumericField.Location = new System.Drawing.Point(109, 189);
            this.elementPosXNumericField.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.elementPosXNumericField.Name = "elementPosXNumericField";
            this.elementPosXNumericField.Size = new System.Drawing.Size(120, 22);
            this.elementPosXNumericField.TabIndex = 13;
            this.elementPosXNumericField.ValueChanged += new System.EventHandler(this.elementPosXNumericField_ValueChanged);
            // 
            // elementPosYNumericField
            // 
            this.elementPosYNumericField.Location = new System.Drawing.Point(109, 217);
            this.elementPosYNumericField.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.elementPosYNumericField.Name = "elementPosYNumericField";
            this.elementPosYNumericField.Size = new System.Drawing.Size(120, 22);
            this.elementPosYNumericField.TabIndex = 14;
            this.elementPosYNumericField.ValueChanged += new System.EventHandler(this.elementPosYNumericField_ValueChanged);
            // 
            // elementSizeWNumericField
            // 
            this.elementSizeWNumericField.Location = new System.Drawing.Point(109, 245);
            this.elementSizeWNumericField.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.elementSizeWNumericField.Name = "elementSizeWNumericField";
            this.elementSizeWNumericField.Size = new System.Drawing.Size(120, 22);
            this.elementSizeWNumericField.TabIndex = 15;
            this.elementSizeWNumericField.ValueChanged += new System.EventHandler(this.elementSizeWNumericField_ValueChanged);
            // 
            // elementSizeHNumericField
            // 
            this.elementSizeHNumericField.Location = new System.Drawing.Point(109, 273);
            this.elementSizeHNumericField.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.elementSizeHNumericField.Name = "elementSizeHNumericField";
            this.elementSizeHNumericField.Size = new System.Drawing.Size(120, 22);
            this.elementSizeHNumericField.TabIndex = 16;
            this.elementSizeHNumericField.ValueChanged += new System.EventHandler(this.elementSizeHNumericField_ValueChanged);
            // 
            // elementOperationsPanel
            // 
            this.elementOperationsPanel.AutoSize = true;
            this.elementOperationsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.elementOperationsPanel.Controls.Add(this.elementOperationsGroupBox);
            this.elementOperationsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.elementOperationsPanel.Location = new System.Drawing.Point(10, 94);
            this.elementOperationsPanel.Name = "elementOperationsPanel";
            this.elementOperationsPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.elementOperationsPanel.Size = new System.Drawing.Size(270, 85);
            this.elementOperationsPanel.TabIndex = 2;
            // 
            // elementOperationsGroupBox
            // 
            this.elementOperationsGroupBox.AutoSize = true;
            this.elementOperationsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.elementOperationsGroupBox.Controls.Add(this.addElementButton);
            this.elementOperationsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.elementOperationsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.elementOperationsGroupBox.Name = "elementOperationsGroupBox";
            this.elementOperationsGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.elementOperationsGroupBox.Size = new System.Drawing.Size(270, 78);
            this.elementOperationsGroupBox.TabIndex = 0;
            this.elementOperationsGroupBox.TabStop = false;
            this.elementOperationsGroupBox.Text = "Buttons";
            // 
            // addElementButton
            // 
            this.addElementButton.Location = new System.Drawing.Point(8, 26);
            this.addElementButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.addElementButton.Name = "addElementButton";
            this.addElementButton.Size = new System.Drawing.Size(75, 29);
            this.addElementButton.TabIndex = 0;
            this.addElementButton.Text = "Add new";
            this.addElementButton.UseVisualStyleBackColor = true;
            this.addElementButton.Click += new System.EventHandler(this.addElementButton_Click);
            // 
            // baseDataPanel
            // 
            this.baseDataPanel.AutoSize = true;
            this.baseDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataPanel.Controls.Add(this.baseDataGroupBox);
            this.baseDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseDataPanel.Location = new System.Drawing.Point(10, 0);
            this.baseDataPanel.Name = "baseDataPanel";
            this.baseDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.baseDataPanel.Size = new System.Drawing.Size(270, 94);
            this.baseDataPanel.TabIndex = 1;
            // 
            // baseDataGroupBox
            // 
            this.baseDataGroupBox.AutoSize = true;
            this.baseDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataGroupBox.Controls.Add(this.baseDataTableLayout);
            this.baseDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.baseDataGroupBox.Name = "baseDataGroupBox";
            this.baseDataGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.baseDataGroupBox.Size = new System.Drawing.Size(270, 87);
            this.baseDataGroupBox.TabIndex = 0;
            this.baseDataGroupBox.TabStop = false;
            this.baseDataGroupBox.Text = "Base data";
            // 
            // baseDataTableLayout
            // 
            this.baseDataTableLayout.AutoSize = true;
            this.baseDataTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataTableLayout.ColumnCount = 2;
            this.baseDataTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.baseDataTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.baseDataTableLayout.Controls.Add(this.idLabel, 0, 0);
            this.baseDataTableLayout.Controls.Add(this.nameLabel, 0, 1);
            this.baseDataTableLayout.Controls.Add(this.nameTextBox, 1, 1);
            this.baseDataTableLayout.Controls.Add(this.idNumericField, 1, 0);
            this.baseDataTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseDataTableLayout.Location = new System.Drawing.Point(8, 23);
            this.baseDataTableLayout.Name = "baseDataTableLayout";
            this.baseDataTableLayout.RowCount = 2;
            this.baseDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.baseDataTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.baseDataTableLayout.Size = new System.Drawing.Size(254, 56);
            this.baseDataTableLayout.TabIndex = 0;
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
            this.nameTextBox.Size = new System.Drawing.Size(181, 22);
            this.nameTextBox.TabIndex = 2;
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(70, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 3;
            // 
            // MacroPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 724);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "Edit macro panel";
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MacroPanelForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Edit macro panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MacroPanelForm_FormClosing);
            this.Load += new System.EventHandler(this.MacroPanelForm_Load);
            this.customElementsPanel.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.editorPanel.ResumeLayout(false);
            this.editorPanel.PerformLayout();
            this.elementDataPanel.ResumeLayout(false);
            this.elementDataPanel.PerformLayout();
            this.elementDataGroupBox.ResumeLayout(false);
            this.elementDataGroupBox.PerformLayout();
            this.elementDataTableLayout.ResumeLayout(false);
            this.elementDataTableLayout.PerformLayout();
            this.backgroundControlsContainerPanel.ResumeLayout(false);
            this.backgroundColorPanelContainerPanel.ResumeLayout(false);
            this.foregroundControlsContainerPanel.ResumeLayout(false);
            this.foregroundColorPanelContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.elementPosXNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elementPosYNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elementSizeWNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elementSizeHNumericField)).EndInit();
            this.elementOperationsPanel.ResumeLayout(false);
            this.elementOperationsPanel.PerformLayout();
            this.elementOperationsGroupBox.ResumeLayout(false);
            this.baseDataPanel.ResumeLayout(false);
            this.baseDataPanel.PerformLayout();
            this.baseDataGroupBox.ResumeLayout(false);
            this.baseDataGroupBox.PerformLayout();
            this.baseDataTableLayout.ResumeLayout(false);
            this.baseDataTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel elementsPanel;
        private System.Windows.Forms.Panel editorPanel;
        private System.Windows.Forms.Panel elementDataPanel;
        private System.Windows.Forms.GroupBox elementDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel elementDataTableLayout;
        private System.Windows.Forms.Label elementMacroLabel;
        private System.Windows.Forms.Label elementLabelLabel;
        private System.Windows.Forms.Panel baseDataPanel;
        private System.Windows.Forms.GroupBox baseDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel baseDataTableLayout;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.ComboBox elementMacroDropDown;
        private System.Windows.Forms.Label elementBackgroundLabel;
        private System.Windows.Forms.Label elementForegroundLabel;
        private System.Windows.Forms.Label showLabelLabel;
        private System.Windows.Forms.TextBox elementLabelTextBox;
        private System.Windows.Forms.CheckBox elementShowLabelCheckBox;
        private System.Windows.Forms.Panel foregroundControlsContainerPanel;
        private System.Windows.Forms.Panel foregroundColorPanelContainerPanel;
        private System.Windows.Forms.Button pickForegroundColorButton;
        private System.Windows.Forms.Panel backgroundControlsContainerPanel;
        private System.Windows.Forms.Panel backgroundColorPanelContainerPanel;
        private System.Windows.Forms.Panel backgroundColorPanel;
        private System.Windows.Forms.Button pickBackgroundColorButton;
        private System.Windows.Forms.Panel foregroundColorPanel;
        private System.Windows.Forms.ColorDialog colorPickerDialog;
        private System.Windows.Forms.Label deleteLabel;
        private System.Windows.Forms.Button removeSelectedElementButton;
        private System.Windows.Forms.Panel elementOperationsPanel;
        private System.Windows.Forms.GroupBox elementOperationsGroupBox;
        private System.Windows.Forms.Button addElementButton;
        private System.Windows.Forms.Label elementSizeHLabel;
        private System.Windows.Forms.NumericUpDown elementPosXNumericField;
        private System.Windows.Forms.NumericUpDown elementPosYNumericField;
        private System.Windows.Forms.NumericUpDown elementSizeWNumericField;
        private System.Windows.Forms.NumericUpDown elementSizeHNumericField;
        private System.Windows.Forms.Label elementPosXLabel;
        private System.Windows.Forms.Label elementPosYLabel;
        private System.Windows.Forms.Label elementSizeWLabel;
    }
}