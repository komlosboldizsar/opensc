namespace OpenSC.GUI.X32Faders
{
    partial class X32FaderEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(X32FaderEditorForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oscPathLabel = new System.Windows.Forms.Label();
            this.oscPathTextBox = new System.Windows.Forms.TextBox();
            this.targetLevelLabel = new System.Windows.Forms.Label();
            this.targetLevelNumericField = new System.Windows.Forms.NumericUpDown();
            this.referenceLevelNumericField = new System.Windows.Forms.NumericUpDown();
            this.referenceLevelLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.stepTimeLabel = new System.Windows.Forms.Label();
            this.timeNumericField = new System.Windows.Forms.NumericUpDown();
            this.stepTimeNumericField = new System.Windows.Forms.NumericUpDown();
            this.bindTimeToReferenceCheckBox = new System.Windows.Forms.CheckBox();
            this.timeUnitLabel = new System.Windows.Forms.Label();
            this.stepTimeUnitLabel = new System.Windows.Forms.Label();
            this.oscPathHint = new System.Windows.Forms.Label();
            this.targetLevelHint = new System.Windows.Forms.Label();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetLevelNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.referenceLevelNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepTimeNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.groupBox1);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 15, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(1082, 535);
            this.customElementsPanel.Controls.SetChildIndex(this.groupBox1, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Size = new System.Drawing.Size(1082, 621);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(10, 120);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.groupBox1.Size = new System.Drawing.Size(1062, 330);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fade data";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oscPathLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.oscPathTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.targetLevelLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.targetLevelNumericField, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.referenceLevelNumericField, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.referenceLevelLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.timeLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.stepTimeLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.timeNumericField, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.stepTimeNumericField, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.bindTimeToReferenceCheckBox, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.timeUnitLabel, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.stepTimeUnitLabel, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.oscPathHint, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.targetLevelHint, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1046, 295);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oscPathLabel
            // 
            this.oscPathLabel.AutoSize = true;
            this.oscPathLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.oscPathLabel.Location = new System.Drawing.Point(3, 0);
            this.oscPathLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.oscPathLabel.Name = "oscPathLabel";
            this.oscPathLabel.Size = new System.Drawing.Size(71, 33);
            this.oscPathLabel.TabIndex = 0;
            this.oscPathLabel.Text = "OSC path";
            this.oscPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // oscPathTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.oscPathTextBox, 2);
            this.oscPathTextBox.Location = new System.Drawing.Point(131, 3);
            this.oscPathTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.oscPathTextBox.Name = "oscPathTextBox";
            this.oscPathTextBox.Size = new System.Drawing.Size(347, 27);
            this.oscPathTextBox.TabIndex = 1;
            // 
            // targetLevelLabel
            // 
            this.targetLevelLabel.AutoSize = true;
            this.targetLevelLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.targetLevelLabel.Location = new System.Drawing.Point(3, 148);
            this.targetLevelLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.targetLevelLabel.Name = "targetLevelLabel";
            this.targetLevelLabel.Size = new System.Drawing.Size(85, 33);
            this.targetLevelLabel.TabIndex = 2;
            this.targetLevelLabel.Text = "Target level";
            this.targetLevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // targetLevelNumericField
            // 
            this.targetLevelNumericField.DecimalPlaces = 3;
            this.targetLevelNumericField.Dock = System.Windows.Forms.DockStyle.Left;
            this.targetLevelNumericField.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.targetLevelNumericField.Location = new System.Drawing.Point(131, 151);
            this.targetLevelNumericField.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.targetLevelNumericField.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.targetLevelNumericField.Name = "targetLevelNumericField";
            this.targetLevelNumericField.Size = new System.Drawing.Size(150, 27);
            this.targetLevelNumericField.TabIndex = 3;
            // 
            // referenceLevelNumericField
            // 
            this.referenceLevelNumericField.DecimalPlaces = 3;
            this.referenceLevelNumericField.Dock = System.Windows.Forms.DockStyle.Left;
            this.referenceLevelNumericField.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.referenceLevelNumericField.Location = new System.Drawing.Point(131, 199);
            this.referenceLevelNumericField.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.referenceLevelNumericField.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.referenceLevelNumericField.Name = "referenceLevelNumericField";
            this.referenceLevelNumericField.Size = new System.Drawing.Size(150, 27);
            this.referenceLevelNumericField.TabIndex = 4;
            // 
            // referenceLevelLabel
            // 
            this.referenceLevelLabel.AutoSize = true;
            this.referenceLevelLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.referenceLevelLabel.Location = new System.Drawing.Point(3, 196);
            this.referenceLevelLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.referenceLevelLabel.Name = "referenceLevelLabel";
            this.referenceLevelLabel.Size = new System.Drawing.Size(110, 33);
            this.referenceLevelLabel.TabIndex = 5;
            this.referenceLevelLabel.Text = "Reference level";
            this.referenceLevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.timeLabel.Location = new System.Drawing.Point(3, 229);
            this.timeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(42, 33);
            this.timeLabel.TabIndex = 6;
            this.timeLabel.Text = "Time";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stepTimeLabel
            // 
            this.stepTimeLabel.AutoSize = true;
            this.stepTimeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.stepTimeLabel.Location = new System.Drawing.Point(3, 262);
            this.stepTimeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.stepTimeLabel.Name = "stepTimeLabel";
            this.stepTimeLabel.Size = new System.Drawing.Size(73, 33);
            this.stepTimeLabel.TabIndex = 8;
            this.stepTimeLabel.Text = "Step time";
            this.stepTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeNumericField
            // 
            this.timeNumericField.Dock = System.Windows.Forms.DockStyle.Left;
            this.timeNumericField.Location = new System.Drawing.Point(131, 232);
            this.timeNumericField.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.timeNumericField.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.timeNumericField.Name = "timeNumericField";
            this.timeNumericField.Size = new System.Drawing.Size(150, 27);
            this.timeNumericField.TabIndex = 9;
            // 
            // stepTimeNumericField
            // 
            this.stepTimeNumericField.Dock = System.Windows.Forms.DockStyle.Left;
            this.stepTimeNumericField.Location = new System.Drawing.Point(131, 265);
            this.stepTimeNumericField.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.stepTimeNumericField.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.stepTimeNumericField.Name = "stepTimeNumericField";
            this.stepTimeNumericField.Size = new System.Drawing.Size(150, 27);
            this.stepTimeNumericField.TabIndex = 10;
            // 
            // bindTimeToReferenceCheckBox
            // 
            this.bindTimeToReferenceCheckBox.AutoSize = true;
            this.bindTimeToReferenceCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.bindTimeToReferenceCheckBox.Location = new System.Drawing.Point(299, 199);
            this.bindTimeToReferenceCheckBox.Name = "bindTimeToReferenceCheckBox";
            this.bindTimeToReferenceCheckBox.Size = new System.Drawing.Size(179, 27);
            this.bindTimeToReferenceCheckBox.TabIndex = 7;
            this.bindTimeToReferenceCheckBox.Text = "Bind time to reference";
            this.bindTimeToReferenceCheckBox.UseVisualStyleBackColor = true;
            this.bindTimeToReferenceCheckBox.CheckedChanged += new System.EventHandler(this.bindTimeToReferenceCheckBox_CheckedChanged);
            // 
            // timeUnitLabel
            // 
            this.timeUnitLabel.AutoSize = true;
            this.timeUnitLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.timeUnitLabel.Location = new System.Drawing.Point(296, 229);
            this.timeUnitLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.timeUnitLabel.Name = "timeUnitLabel";
            this.timeUnitLabel.Size = new System.Drawing.Size(38, 33);
            this.timeUnitLabel.TabIndex = 11;
            this.timeUnitLabel.Text = "(ms)";
            this.timeUnitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stepTimeUnitLabel
            // 
            this.stepTimeUnitLabel.AutoSize = true;
            this.stepTimeUnitLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.stepTimeUnitLabel.Location = new System.Drawing.Point(296, 262);
            this.stepTimeUnitLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.stepTimeUnitLabel.Name = "stepTimeUnitLabel";
            this.stepTimeUnitLabel.Size = new System.Drawing.Size(38, 33);
            this.stepTimeUnitLabel.TabIndex = 12;
            this.stepTimeUnitLabel.Text = "(ms)";
            this.stepTimeUnitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // oscPathHint
            // 
            this.oscPathHint.AutoSize = true;
            this.oscPathHint.Dock = System.Windows.Forms.DockStyle.Left;
            this.oscPathHint.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.oscPathHint.Location = new System.Drawing.Point(496, 0);
            this.oscPathHint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.oscPathHint.Name = "oscPathHint";
            this.tableLayoutPanel1.SetRowSpan(this.oscPathHint, 2);
            this.oscPathHint.Size = new System.Drawing.Size(281, 140);
            this.oscPathHint.TabIndex = 13;
            this.oscPathHint.Text = resources.GetString("oscPathHint.Text");
            this.oscPathHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // targetLevelHint
            // 
            this.targetLevelHint.AutoSize = true;
            this.targetLevelHint.Dock = System.Windows.Forms.DockStyle.Left;
            this.targetLevelHint.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.targetLevelHint.Location = new System.Drawing.Point(496, 148);
            this.targetLevelHint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.targetLevelHint.Name = "targetLevelHint";
            this.tableLayoutPanel1.SetRowSpan(this.targetLevelHint, 2);
            this.targetLevelHint.Size = new System.Drawing.Size(497, 40);
            this.targetLevelHint.TabIndex = 14;
            this.targetLevelHint.Text = "-inf dB: 0.0000, -60 dB: 0.0625, -50 dB: 0.1250, -40 dB: 0.1875\r\n-30 dB: 0.2500, " +
    "-20 dB: 0.3750, -10 dB: 0.5000, 0 dB: 0.7500, +10 dB: 1.0000";
            // 
            // X32FaderEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 691);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New X32 fader";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(1000, 738);
            this.Name = "X32FaderEditorForm";
            this.SubjectPlural = "X32 faders";
            this.SubjectSingular = "X32 fader";
            this.Text = "New X32 fader";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetLevelNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.referenceLevelNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepTimeNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label targetLevelLabel;
        private System.Windows.Forms.NumericUpDown targetLevelNumericField;
        private System.Windows.Forms.NumericUpDown referenceLevelNumericField;
        private System.Windows.Forms.Label referenceLevelLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.CheckBox bindTimeToReferenceCheckBox;
        private System.Windows.Forms.Label stepTimeLabel;
        private System.Windows.Forms.NumericUpDown timeNumericField;
        private System.Windows.Forms.NumericUpDown stepTimeNumericField;
        private System.Windows.Forms.Label timeUnitLabel;
        private System.Windows.Forms.Label stepTimeUnitLabel;
        private System.Windows.Forms.Label oscPathLabel;
        private System.Windows.Forms.TextBox oscPathTextBox;
        private System.Windows.Forms.Label oscPathHint;
        private System.Windows.Forms.Label targetLevelHint;
    }
}