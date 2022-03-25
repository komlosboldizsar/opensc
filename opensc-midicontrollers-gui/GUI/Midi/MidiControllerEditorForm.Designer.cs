namespace OpenSC.GUI.MidiControllers
{
    partial class MidiControllerEditorForm
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
            this.comPortDataPanel = new System.Windows.Forms.Panel();
            this.hardwarePropertesGroupBox = new System.Windows.Forms.GroupBox();
            this.hardwarePropertiesTable = new System.Windows.Forms.TableLayoutPanel();
            this.deviceIndexNumericField = new System.Windows.Forms.NumericUpDown();
            this.deviceIndexLabel = new System.Windows.Forms.Label();
            this.lookupByLabel = new System.Windows.Forms.Label();
            this.lookupByRadiobuttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.indexRadioButton = new System.Windows.Forms.RadioButton();
            this.nameRadioButton = new System.Windows.Forms.RadioButton();
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.deviceNameDropDown = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.comPortDataPanel.SuspendLayout();
            this.hardwarePropertesGroupBox.SuspendLayout();
            this.hardwarePropertiesTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceIndexNumericField)).BeginInit();
            this.lookupByRadiobuttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.comPortDataPanel);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 12);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(500, 429);
            this.customElementsPanel.Controls.SetChildIndex(this.comPortDataPanel, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.mainContainer.Size = new System.Drawing.Size(520, 539);
            // 
            // comPortDataPanel
            // 
            this.comPortDataPanel.AutoSize = true;
            this.comPortDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comPortDataPanel.Controls.Add(this.hardwarePropertesGroupBox);
            this.comPortDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.comPortDataPanel.Location = new System.Drawing.Point(0, 105);
            this.comPortDataPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comPortDataPanel.Name = "comPortDataPanel";
            this.comPortDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.comPortDataPanel.Size = new System.Drawing.Size(500, 154);
            this.comPortDataPanel.TabIndex = 3;
            // 
            // hardwarePropertesGroupBox
            // 
            this.hardwarePropertesGroupBox.AutoSize = true;
            this.hardwarePropertesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hardwarePropertesGroupBox.Controls.Add(this.hardwarePropertiesTable);
            this.hardwarePropertesGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.hardwarePropertesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.hardwarePropertesGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.hardwarePropertesGroupBox.Name = "hardwarePropertesGroupBox";
            this.hardwarePropertesGroupBox.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.hardwarePropertesGroupBox.Size = new System.Drawing.Size(500, 145);
            this.hardwarePropertesGroupBox.TabIndex = 1;
            this.hardwarePropertesGroupBox.TabStop = false;
            this.hardwarePropertesGroupBox.Text = "Hardware properties";
            // 
            // hardwarePropertiesTable
            // 
            this.hardwarePropertiesTable.AutoSize = true;
            this.hardwarePropertiesTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hardwarePropertiesTable.ColumnCount = 2;
            this.hardwarePropertiesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.hardwarePropertiesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.hardwarePropertiesTable.Controls.Add(this.deviceIndexNumericField, 1, 1);
            this.hardwarePropertiesTable.Controls.Add(this.deviceIndexLabel, 0, 1);
            this.hardwarePropertiesTable.Controls.Add(this.lookupByLabel, 0, 0);
            this.hardwarePropertiesTable.Controls.Add(this.lookupByRadiobuttonPanel, 1, 0);
            this.hardwarePropertiesTable.Controls.Add(this.deviceNameLabel, 0, 2);
            this.hardwarePropertiesTable.Controls.Add(this.deviceNameDropDown, 1, 2);
            this.hardwarePropertiesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hardwarePropertiesTable.Location = new System.Drawing.Point(8, 30);
            this.hardwarePropertiesTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hardwarePropertiesTable.Name = "hardwarePropertiesTable";
            this.hardwarePropertiesTable.RowCount = 3;
            this.hardwarePropertiesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hardwarePropertiesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hardwarePropertiesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hardwarePropertiesTable.Size = new System.Drawing.Size(484, 105);
            this.hardwarePropertiesTable.TabIndex = 0;
            // 
            // deviceIndexNumericField
            // 
            this.deviceIndexNumericField.Location = new System.Drawing.Point(116, 40);
            this.deviceIndexNumericField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deviceIndexNumericField.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.deviceIndexNumericField.Name = "deviceIndexNumericField";
            this.deviceIndexNumericField.Size = new System.Drawing.Size(120, 27);
            this.deviceIndexNumericField.TabIndex = 5;
            // 
            // deviceIndexLabel
            // 
            this.deviceIndexLabel.AutoSize = true;
            this.deviceIndexLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.deviceIndexLabel.Location = new System.Drawing.Point(3, 36);
            this.deviceIndexLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.deviceIndexLabel.Name = "deviceIndexLabel";
            this.deviceIndexLabel.Size = new System.Drawing.Size(94, 35);
            this.deviceIndexLabel.TabIndex = 1;
            this.deviceIndexLabel.Text = "Device index";
            this.deviceIndexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lookupByLabel
            // 
            this.lookupByLabel.AutoSize = true;
            this.lookupByLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lookupByLabel.Location = new System.Drawing.Point(3, 0);
            this.lookupByLabel.Name = "lookupByLabel";
            this.lookupByLabel.Padding = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.lookupByLabel.Size = new System.Drawing.Size(96, 36);
            this.lookupByLabel.TabIndex = 6;
            this.lookupByLabel.Text = "Lookup by";
            this.lookupByLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lookupByRadiobuttonPanel
            // 
            this.lookupByRadiobuttonPanel.AutoSize = true;
            this.lookupByRadiobuttonPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lookupByRadiobuttonPanel.Controls.Add(this.indexRadioButton);
            this.lookupByRadiobuttonPanel.Controls.Add(this.nameRadioButton);
            this.lookupByRadiobuttonPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lookupByRadiobuttonPanel.Location = new System.Drawing.Point(116, 3);
            this.lookupByRadiobuttonPanel.Name = "lookupByRadiobuttonPanel";
            this.lookupByRadiobuttonPanel.Size = new System.Drawing.Size(148, 30);
            this.lookupByRadiobuttonPanel.TabIndex = 7;
            // 
            // indexRadioButton
            // 
            this.indexRadioButton.AutoSize = true;
            this.indexRadioButton.Location = new System.Drawing.Point(3, 3);
            this.indexRadioButton.Name = "indexRadioButton";
            this.indexRadioButton.Size = new System.Drawing.Size(66, 24);
            this.indexRadioButton.TabIndex = 0;
            this.indexRadioButton.TabStop = true;
            this.indexRadioButton.Text = "Index";
            this.indexRadioButton.UseVisualStyleBackColor = true;
            this.indexRadioButton.CheckedChanged += new System.EventHandler(this.indexRadioButton_CheckedChanged);
            // 
            // nameRadioButton
            // 
            this.nameRadioButton.AutoSize = true;
            this.nameRadioButton.Location = new System.Drawing.Point(75, 3);
            this.nameRadioButton.Name = "nameRadioButton";
            this.nameRadioButton.Size = new System.Drawing.Size(70, 24);
            this.nameRadioButton.TabIndex = 1;
            this.nameRadioButton.TabStop = true;
            this.nameRadioButton.Text = "Name";
            this.nameRadioButton.UseVisualStyleBackColor = true;
            this.nameRadioButton.CheckedChanged += new System.EventHandler(this.nameRadioButton_CheckedChanged);
            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.AutoSize = true;
            this.deviceNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.deviceNameLabel.Location = new System.Drawing.Point(3, 71);
            this.deviceNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(95, 34);
            this.deviceNameLabel.TabIndex = 8;
            this.deviceNameLabel.Text = "Device name";
            this.deviceNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // deviceNameDropDown
            // 
            this.deviceNameDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.deviceNameDropDown.FormattingEnabled = true;
            this.deviceNameDropDown.Location = new System.Drawing.Point(116, 74);
            this.deviceNameDropDown.Name = "deviceNameDropDown";
            this.deviceNameDropDown.Size = new System.Drawing.Size(263, 28);
            this.deviceNameDropDown.TabIndex = 9;
            // 
            // MidiControllerEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 609);
            this.DeleteButtonVisible = true;
            this.HeaderText = "Edit MIDI controller";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(500, 538);
            this.Name = "MidiControllerEditorForm";
            this.Text = "Edit MIDI controller";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.comPortDataPanel.ResumeLayout(false);
            this.comPortDataPanel.PerformLayout();
            this.hardwarePropertesGroupBox.ResumeLayout(false);
            this.hardwarePropertesGroupBox.PerformLayout();
            this.hardwarePropertiesTable.ResumeLayout(false);
            this.hardwarePropertiesTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceIndexNumericField)).EndInit();
            this.lookupByRadiobuttonPanel.ResumeLayout(false);
            this.lookupByRadiobuttonPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel comPortDataPanel;
        private System.Windows.Forms.GroupBox hardwarePropertesGroupBox;
        private System.Windows.Forms.TableLayoutPanel hardwarePropertiesTable;
        private System.Windows.Forms.Label deviceIndexLabel;
        private System.Windows.Forms.NumericUpDown deviceIndexNumericField;
        private System.Windows.Forms.Label lookupByLabel;
        private System.Windows.Forms.FlowLayoutPanel lookupByRadiobuttonPanel;
        private System.Windows.Forms.RadioButton indexRadioButton;
        private System.Windows.Forms.RadioButton nameRadioButton;
        private System.Windows.Forms.Label deviceNameLabel;
        private System.Windows.Forms.ComboBox deviceNameDropDown;
    }
}