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
            this.basicDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.basicDataGroupBox = new System.Windows.Forms.GroupBox();
            this.modeGroupBox = new System.Windows.Forms.GroupBox();
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
            this.mainContainer.SuspendLayout();
            this.basicDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.basicDataGroupBox.SuspendLayout();
            this.modeGroupBox.SuspendLayout();
            this.modeTable.SuspendLayout();
            this.modeOptionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countdownStartNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.saveButton);
            this.mainContainer.Controls.Add(this.cancelButton);
            this.mainContainer.Controls.Add(this.saveAndCloseButton);
            this.mainContainer.Controls.Add(this.modeGroupBox);
            this.mainContainer.Controls.Add(this.basicDataGroupBox);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(493, 334);
            // 
            // basicDataTable
            // 
            this.basicDataTable.AutoSize = true;
            this.basicDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basicDataTable.ColumnCount = 2;
            this.basicDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.basicDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.basicDataTable.Controls.Add(this.idLabel, 0, 0);
            this.basicDataTable.Controls.Add(this.titleLabel, 0, 1);
            this.basicDataTable.Controls.Add(this.idNumericField, 1, 0);
            this.basicDataTable.Controls.Add(this.titleTextBox, 1, 1);
            this.basicDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basicDataTable.Location = new System.Drawing.Point(8, 27);
            this.basicDataTable.Name = "basicDataTable";
            this.basicDataTable.RowCount = 2;
            this.basicDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.basicDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.basicDataTable.Size = new System.Drawing.Size(457, 56);
            this.basicDataTable.TabIndex = 0;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(3, 0);
            this.idLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(21, 17);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "ID";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(3, 28);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(35, 17);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Title";
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(61, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 2;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleTextBox.Location = new System.Drawing.Point(61, 31);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(393, 22);
            this.titleTextBox.TabIndex = 3;
            // 
            // basicDataGroupBox
            // 
            this.basicDataGroupBox.AutoSize = true;
            this.basicDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basicDataGroupBox.Controls.Add(this.basicDataTable);
            this.basicDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.basicDataGroupBox.Location = new System.Drawing.Point(10, 10);
            this.basicDataGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.basicDataGroupBox.Name = "basicDataGroupBox";
            this.basicDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 12, 8, 8);
            this.basicDataGroupBox.Size = new System.Drawing.Size(473, 91);
            this.basicDataGroupBox.TabIndex = 1;
            this.basicDataGroupBox.TabStop = false;
            this.basicDataGroupBox.Text = "Basic data";
            // 
            // modeGroupBox
            // 
            this.modeGroupBox.AutoSize = true;
            this.modeGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modeGroupBox.Controls.Add(this.modeTable);
            this.modeGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.modeGroupBox.Location = new System.Drawing.Point(10, 101);
            this.modeGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.modeGroupBox.Name = "modeGroupBox";
            this.modeGroupBox.Padding = new System.Windows.Forms.Padding(8, 12, 8, 8);
            this.modeGroupBox.Size = new System.Drawing.Size(473, 150);
            this.modeGroupBox.TabIndex = 2;
            this.modeGroupBox.TabStop = false;
            this.modeGroupBox.Text = "Mode";
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
            this.modeTable.Location = new System.Drawing.Point(8, 27);
            this.modeTable.Name = "modeTable";
            this.modeTable.RowCount = 2;
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.Size = new System.Drawing.Size(457, 115);
            this.modeTable.TabIndex = 0;
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Location = new System.Drawing.Point(3, 0);
            this.modeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(43, 17);
            this.modeLabel.TabIndex = 0;
            this.modeLabel.Text = "Mode";
            // 
            // countdownStartLabel
            // 
            this.countdownStartLabel.AutoSize = true;
            this.countdownStartLabel.Location = new System.Drawing.Point(3, 87);
            this.countdownStartLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.countdownStartLabel.Name = "countdownStartLabel";
            this.countdownStartLabel.Size = new System.Drawing.Size(110, 17);
            this.countdownStartLabel.TabIndex = 1;
            this.countdownStartLabel.Text = "Countdown start";
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
            this.modeOptionTable.Size = new System.Drawing.Size(318, 81);
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
            this.saveAndCloseButton.Location = new System.Drawing.Point(343, 287);
            this.saveAndCloseButton.Name = "saveAndCloseButton";
            this.saveAndCloseButton.Size = new System.Drawing.Size(137, 35);
            this.saveAndCloseButton.TabIndex = 3;
            this.saveAndCloseButton.Text = "Save and close";
            this.saveAndCloseButton.UseVisualStyleBackColor = true;
            this.saveAndCloseButton.Click += new System.EventHandler(this.saveAndCloseButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(165, 286);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(83, 35);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(254, 287);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(83, 35);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // TimerEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 390);
            this.Name = "TimerEditWindow";
            this.Text = "TimerEditWindow";
            this.Load += new System.EventHandler(this.TimerEditWindow_Load);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.basicDataTable.ResumeLayout(false);
            this.basicDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.basicDataGroupBox.ResumeLayout(false);
            this.basicDataGroupBox.PerformLayout();
            this.modeGroupBox.ResumeLayout(false);
            this.modeGroupBox.PerformLayout();
            this.modeTable.ResumeLayout(false);
            this.modeTable.PerformLayout();
            this.modeOptionTable.ResumeLayout(false);
            this.modeOptionTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countdownStartNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox basicDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel basicDataTable;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.GroupBox modeGroupBox;
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
    }
}