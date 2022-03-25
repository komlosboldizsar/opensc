namespace OpenSC.GUI.Timers
{
    partial class TimerEditorForm
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
            this.operationGroupBox = new System.Windows.Forms.GroupBox();
            this.modeTable = new System.Windows.Forms.TableLayoutPanel();
            this.modeLabel = new System.Windows.Forms.Label();
            this.countdownStartLabel = new System.Windows.Forms.Label();
            this.modeOptionTable = new System.Windows.Forms.TableLayoutPanel();
            this.modeForwardsRadio = new System.Windows.Forms.RadioButton();
            this.modeBackwardsRadio = new System.Windows.Forms.RadioButton();
            this.modeClockRadio = new System.Windows.Forms.RadioButton();
            this.countdownStartNumericField = new System.Windows.Forms.NumericUpDown();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.operationGroupBox.SuspendLayout();
            this.modeTable.SuspendLayout();
            this.modeOptionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countdownStartNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.operationGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 12);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 15, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(462, 311);
            this.customElementsPanel.Controls.SetChildIndex(this.operationGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.mainContainer.Size = new System.Drawing.Size(482, 421);
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.AutoSize = true;
            this.operationGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operationGroupBox.Controls.Add(this.modeTable);
            this.operationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.operationGroupBox.Location = new System.Drawing.Point(10, 122);
            this.operationGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.operationGroupBox.Name = "operationGroupBox";
            this.operationGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.operationGroupBox.Size = new System.Drawing.Size(442, 174);
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
            this.modeTable.Location = new System.Drawing.Point(8, 25);
            this.modeTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modeTable.Name = "modeTable";
            this.modeTable.RowCount = 2;
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.Size = new System.Drawing.Size(426, 139);
            this.modeTable.TabIndex = 0;
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.modeLabel.Location = new System.Drawing.Point(3, 0);
            this.modeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(48, 104);
            this.modeLabel.TabIndex = 0;
            this.modeLabel.Text = "Mode";
            this.modeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // countdownStartLabel
            // 
            this.countdownStartLabel.AutoSize = true;
            this.countdownStartLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.countdownStartLabel.Location = new System.Drawing.Point(3, 104);
            this.countdownStartLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.countdownStartLabel.Name = "countdownStartLabel";
            this.countdownStartLabel.Size = new System.Drawing.Size(118, 35);
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
            this.modeOptionTable.Location = new System.Drawing.Point(144, 4);
            this.modeOptionTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modeOptionTable.Name = "modeOptionTable";
            this.modeOptionTable.RowCount = 3;
            this.modeOptionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeOptionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeOptionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeOptionTable.Size = new System.Drawing.Size(279, 96);
            this.modeOptionTable.TabIndex = 2;
            // 
            // modeForwardsRadio
            // 
            this.modeForwardsRadio.AutoSize = true;
            this.modeForwardsRadio.Location = new System.Drawing.Point(3, 4);
            this.modeForwardsRadio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modeForwardsRadio.Name = "modeForwardsRadio";
            this.modeForwardsRadio.Size = new System.Drawing.Size(155, 24);
            this.modeForwardsRadio.TabIndex = 0;
            this.modeForwardsRadio.TabStop = true;
            this.modeForwardsRadio.Text = "Stopper (forwards)";
            this.modeForwardsRadio.UseVisualStyleBackColor = true;
            // 
            // modeBackwardsRadio
            // 
            this.modeBackwardsRadio.AutoSize = true;
            this.modeBackwardsRadio.Location = new System.Drawing.Point(3, 36);
            this.modeBackwardsRadio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modeBackwardsRadio.Name = "modeBackwardsRadio";
            this.modeBackwardsRadio.Size = new System.Drawing.Size(190, 24);
            this.modeBackwardsRadio.TabIndex = 1;
            this.modeBackwardsRadio.TabStop = true;
            this.modeBackwardsRadio.Text = "Countdown (backwards)";
            this.modeBackwardsRadio.UseVisualStyleBackColor = true;
            // 
            // modeClockRadio
            // 
            this.modeClockRadio.AutoSize = true;
            this.modeClockRadio.Location = new System.Drawing.Point(3, 68);
            this.modeClockRadio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modeClockRadio.Name = "modeClockRadio";
            this.modeClockRadio.Size = new System.Drawing.Size(160, 24);
            this.modeClockRadio.TabIndex = 2;
            this.modeClockRadio.TabStop = true;
            this.modeClockRadio.Text = "Clock (current time)";
            this.modeClockRadio.UseVisualStyleBackColor = true;
            // 
            // countdownStartNumericField
            // 
            this.countdownStartNumericField.Location = new System.Drawing.Point(144, 108);
            this.countdownStartNumericField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.countdownStartNumericField.Name = "countdownStartNumericField";
            this.countdownStartNumericField.Size = new System.Drawing.Size(118, 27);
            this.countdownStartNumericField.TabIndex = 3;
            // 
            // TimerEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 491);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New timer";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(500, 538);
            this.Name = "TimerEditWindow";
            this.SubjectPlural = "timers";
            this.SubjectSingular = "timer";
            this.Text = "New timer";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
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
    }
}