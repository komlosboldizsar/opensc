namespace OpenSC.GUI.Macros
{
    partial class CommandArgumentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.argumentIndexLabel = new GrowLabel();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.argumentDescriptionLabel = new GrowLabel();
            this.argumentNameLabel = new GrowLabel();
            this.valueComboBox = new System.Windows.Forms.ComboBox();
            this.valueLabel = new System.Windows.Forms.Label();
            this.tableContainerPanel = new System.Windows.Forms.Panel();
            this.borderPanel = new System.Windows.Forms.Panel();
            this.table.SuspendLayout();
            this.tableContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // argumentIndexLabel
            // 
            this.argumentIndexLabel.AutoSize = true;
            this.argumentIndexLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.argumentIndexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.argumentIndexLabel.Location = new System.Drawing.Point(3, 3);
            this.argumentIndexLabel.Margin = new System.Windows.Forms.Padding(3);
            this.argumentIndexLabel.Name = "argumentIndexLabel";
            this.argumentIndexLabel.Size = new System.Drawing.Size(110, 17);
            this.argumentIndexLabel.TabIndex = 2;
            this.argumentIndexLabel.Text = "(argN)";
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.ColumnCount = 2;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.table.Controls.Add(this.argumentNameLabel, 1, 0);
            this.table.Controls.Add(this.argumentIndexLabel, 0, 0);
            this.table.Controls.Add(this.argumentDescriptionLabel, 0, 1);
            this.table.Controls.Add(this.valueComboBox, 1, 2);
            this.table.Controls.Add(this.valueLabel, 0, 2);
            this.table.Dock = System.Windows.Forms.DockStyle.Left;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 4;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.Size = new System.Drawing.Size(582, 84);
            this.table.TabIndex = 3;
            // 
            // argumentDescriptionLabel
            // 
            this.table.SetColumnSpan(this.argumentDescriptionLabel, 2);
            this.argumentDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.argumentDescriptionLabel.Location = new System.Drawing.Point(5, 28);
            this.argumentDescriptionLabel.Margin = new System.Windows.Forms.Padding(5);
            this.argumentDescriptionLabel.Name = "argumentDescriptionLabel";
            this.argumentDescriptionLabel.Size = new System.Drawing.Size(572, 17);
            this.argumentDescriptionLabel.TabIndex = 3;
            this.argumentDescriptionLabel.Text = "Egy soros leírás.";
            // 
            // argumentNameLabel
            // 
            this.argumentNameLabel.AutoSize = true;
            this.argumentNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.argumentNameLabel.Location = new System.Drawing.Point(119, 3);
            this.argumentNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.argumentNameLabel.Name = "argumentNameLabel";
            this.argumentNameLabel.Size = new System.Drawing.Size(460, 17);
            this.argumentNameLabel.TabIndex = 5;
            this.argumentNameLabel.Text = "Argument name";
            // 
            // valueComboBox
            // 
            this.valueComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.valueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueComboBox.FormattingEnabled = true;
            this.valueComboBox.Location = new System.Drawing.Point(119, 53);
            this.valueComboBox.Name = "valueComboBox";
            this.valueComboBox.Size = new System.Drawing.Size(460, 24);
            this.valueComboBox.TabIndex = 6;
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.valueLabel.Location = new System.Drawing.Point(3, 50);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(48, 30);
            this.valueLabel.TabIndex = 7;
            this.valueLabel.Text = "Value:";
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableContainerPanel
            // 
            this.tableContainerPanel.AutoSize = true;
            this.tableContainerPanel.Controls.Add(this.table);
            this.tableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableContainerPanel.Location = new System.Drawing.Point(0, 0);
            this.tableContainerPanel.Name = "tableContainerPanel";
            this.tableContainerPanel.Size = new System.Drawing.Size(582, 84);
            this.tableContainerPanel.TabIndex = 8;
            // 
            // borderPanel
            // 
            this.borderPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.borderPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderPanel.Location = new System.Drawing.Point(0, 84);
            this.borderPanel.Name = "borderPanel";
            this.borderPanel.Size = new System.Drawing.Size(582, 2);
            this.borderPanel.TabIndex = 8;
            // 
            // CommandArgumentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableContainerPanel);
            this.Controls.Add(this.borderPanel);
            this.Name = "CommandArgumentControl";
            this.Size = new System.Drawing.Size(582, 86);
            this.Load += new System.EventHandler(this.CommandArgumentControl_Load);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.tableContainerPanel.ResumeLayout(false);
            this.tableContainerPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GrowLabel argumentIndexLabel;
        private System.Windows.Forms.TableLayoutPanel table;
        private GrowLabel argumentNameLabel;
        private GrowLabel argumentDescriptionLabel;
        private System.Windows.Forms.ComboBox valueComboBox;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Panel tableContainerPanel;
        private System.Windows.Forms.Panel borderPanel;
    }
}
