namespace OpenSC.GUI.Timers
{
    partial class TimerEditWindow
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
            this.idLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.operationGroupBox = new System.Windows.Forms.GroupBox();
            this.modeTable = new System.Windows.Forms.TableLayoutPanel();
            this.modeLabel = new System.Windows.Forms.Label();
            this.countdownStartLabel = new System.Windows.Forms.Label();
            this.modeOptionTable = new System.Windows.Forms.TableLayoutPanel();
            this.modeForwardsRadio = new System.Windows.Forms.RadioButton();
            this.modeBackwardsRadio = new System.Windows.Forms.RadioButton();
            this.modeClockRadio = new System.Windows.Forms.RadioButton();
            this.countdownStartNumericField = new System.Windows.Forms.NumericUpDown();
            this.saveAndCloseButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.operationGroupBox.SuspendLayout();
            this.modeTable.SuspendLayout();
            this.modeOptionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countdownStartNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.operationGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Size = new System.Drawing.Size(462, 248);
            this.customElementsPanel.Controls.SetChildIndex(this.operationGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.saveButton);
            this.mainContainer.Controls.Add(this.cancelButton);
            this.mainContainer.Controls.Add(this.saveAndCloseButton);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(482, 337);
            this.mainContainer.Controls.SetChildIndex(this.saveAndCloseButton, 0);
            this.mainContainer.Controls.SetChildIndex(this.cancelButton, 0);
            this.mainContainer.Controls.SetChildIndex(this.saveButton, 0);
            this.mainContainer.Controls.SetChildIndex(this.customElementsPanel, 0);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.idLabel.Location = new System.Drawing.Point(3, 0);
            this.idLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(21, 28);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "ID";
            this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(61, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 2;
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.AutoSize = true;
            this.operationGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operationGroupBox.Controls.Add(this.modeTable);
            this.operationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.operationGroupBox.Location = new System.Drawing.Point(10, 65);
            this.operationGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.operationGroupBox.Name = "operationGroupBox";
            this.operationGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.operationGroupBox.Size = new System.Drawing.Size(442, 142);
            this.operationGroupBox.TabIndex = 2;
            this.operationGroupBox.TabStop = false;
            this.operationGroupBox.Text = "Operation";
            // 
            // modeTable
            // 
            this.modeTable.AutoSize = true;
            this.modeTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modeTable.ColumnCount = 2;
            this.modeTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.modeTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modeTable.Controls.Add(this.modeLabel, 0, 0);
            this.modeTable.Controls.Add(this.countdownStartLabel, 0, 1);
            this.modeTable.Controls.Add(this.modeOptionTable, 1, 0);
            this.modeTable.Controls.Add(this.countdownStartNumericField, 1, 1);
            this.modeTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeTable.Location = new System.Drawing.Point(8, 19);
            this.modeTable.Name = "modeTable";
            this.modeTable.RowCount = 2;
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.Size = new System.Drawing.Size(426, 115);
            this.modeTable.TabIndex = 0;
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.modeLabel.Location = new System.Drawing.Point(3, 0);
            this.modeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(43, 87);
            this.modeLabel.TabIndex = 0;
            this.modeLabel.Text = "Mode";
            this.modeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // countdownStartLabel
            // 
            this.countdownStartLabel.AutoSize = true;
            this.countdownStartLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.countdownStartLabel.Location = new System.Drawing.Point(3, 87);
            this.countdownStartLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.countdownStartLabel.Name = "countdownStartLabel";
            this.countdownStartLabel.Size = new System.Drawing.Size(110, 28);
            this.countdownStartLabel.TabIndex = 1;
            this.countdownStartLabel.Text = "Countdown start";
            this.countdownStartLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modeOptionTable
            // 
            this.modeOptionTable.AutoSize = true;
            this.modeOptionTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modeOptionTable.ColumnCount = 1;
            this.modeOptionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modeOptionTable.Controls.Add(this.modeForwardsRadio, 0, 0);
            this.modeOptionTable.Controls.Add(this.modeBackwardsRadio, 0, 1);
            this.modeOptionTable.Controls.Add(this.modeClockRadio, 0, 2);
            this.modeOptionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeOptionTable.Location = new System.Drawing.Point(136, 3);
            this.modeOptionTable.Name = "modeOptionTable";
            this.modeOptionTable.RowCount = 3;
            this.modeOptionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeOptionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeOptionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeOptionTable.Size = new System.Drawing.Size(287, 81);
            this.modeOptionTable.TabIndex = 2;
            // 
            // modeForwardsRadio
            // 
            this.modeForwardsRadio.AutoSize = true;
            this.modeForwardsRadio.Location = new System.Drawing.Point(3, 3);
            this.modeForwardsRadio.Name = "modeForwardsRadio";
            this.modeForwardsRadio.Size = new System.Drawing.Size(147, 21);
            this.modeForwardsRadio.TabIndex = 0;
            this.modeForwardsRadio.TabStop = true;
            this.modeForwardsRadio.Text = "Stopper (forwards)";
            this.modeForwardsRadio.UseVisualStyleBackColor = true;
            // 
            // modeBackwardsRadio
            // 
            this.modeBackwardsRadio.AutoSize = true;
            this.modeBackwardsRadio.Location = new System.Drawing.Point(3, 30);
            this.modeBackwardsRadio.Name = "modeBackwardsRadio";
            this.modeBackwardsRadio.Size = new System.Drawing.Size(180, 21);
            this.modeBackwardsRadio.TabIndex = 1;
            this.modeBackwardsRadio.TabStop = true;
            this.modeBackwardsRadio.Text = "Countdown (backwards)";
            this.modeBackwardsRadio.UseVisualStyleBackColor = true;
            // 
            // modeClockRadio
            // 
            this.modeClockRadio.AutoSize = true;
            this.modeClockRadio.Location = new System.Drawing.Point(3, 57);
            this.modeClockRadio.Name = "modeClockRadio";
            this.modeClockRadio.Size = new System.Drawing.Size(152, 21);
            this.modeClockRadio.TabIndex = 2;
            this.modeClockRadio.TabStop = true;
            this.modeClockRadio.Text = "Clock (current time)";
            this.modeClockRadio.UseVisualStyleBackColor = true;
            // 
            // countdownStartNumericField
            // 
            this.countdownStartNumericField.Location = new System.Drawing.Point(136, 90);
            this.countdownStartNumericField.Name = "countdownStartNumericField";
            this.countdownStartNumericField.Size = new System.Drawing.Size(118, 22);
            this.countdownStartNumericField.TabIndex = 3;
            // 
            // saveAndCloseButton
            // 
            this.saveAndCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAndCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveAndCloseButton.Location = new System.Drawing.Point(332, 290);
            this.saveAndCloseButton.Name = "saveAndCloseButton";
            this.saveAndCloseButton.Size = new System.Drawing.Size(137, 35);
            this.saveAndCloseButton.TabIndex = 3;
            this.saveAndCloseButton.Text = "Save and close";
            this.saveAndCloseButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(154, 289);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(83, 35);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(243, 290);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(83, 35);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // TimerEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 393);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New timer";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "TimerEditWindow";
            this.SubjectPlural = "timers";
            this.SubjectSingular = "timer";
            this.Text = "New timer";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.operationGroupBox.ResumeLayout(false);
            this.operationGroupBox.PerformLayout();
            this.modeTable.ResumeLayout(false);
            this.modeTable.PerformLayout();
            this.modeOptionTable.ResumeLayout(false);
            this.modeOptionTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countdownStartNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox operationGroupBox;
        private System.Windows.Forms.TableLayoutPanel modeTable;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.Label countdownStartLabel;
        private System.Windows.Forms.TableLayoutPanel modeOptionTable;
        private System.Windows.Forms.RadioButton modeForwardsRadio;
        private System.Windows.Forms.RadioButton modeBackwardsRadio;
        private System.Windows.Forms.RadioButton modeClockRadio;
        private System.Windows.Forms.NumericUpDown countdownStartNumericField;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveAndCloseButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
    }
}