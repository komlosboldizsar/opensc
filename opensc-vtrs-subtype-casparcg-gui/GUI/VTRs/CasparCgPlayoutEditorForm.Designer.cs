namespace OpenSC.GUI.VTRs
{
    partial class CasparCgPlayoutEditorForm
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
            this.casparCgPanel = new System.Windows.Forms.Panel();
            this.casparCgGroupBox = new System.Windows.Forms.GroupBox();
            this.casparCgTable = new System.Windows.Forms.TableLayoutPanel();
            this.ipLabel = new System.Windows.Forms.Label();
            this.channelLabel = new System.Windows.Forms.Label();
            this.layerLabel = new System.Windows.Forms.Label();
            this.channelNumericField = new System.Windows.Forms.NumericUpDown();
            this.layerNumericField = new System.Windows.Forms.NumericUpDown();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.casparCgPanel.SuspendLayout();
            this.casparCgGroupBox.SuspendLayout();
            this.casparCgTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.channelNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.casparCgPanel);
            this.customElementsPanel.Size = new System.Drawing.Size(505, 325);
            this.customElementsPanel.Controls.SetChildIndex(this.casparCgPanel, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(505, 394);
            // 
            // casparCgPanel
            // 
            this.casparCgPanel.AutoSize = true;
            this.casparCgPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.casparCgPanel.Controls.Add(this.casparCgGroupBox);
            this.casparCgPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.casparCgPanel.Location = new System.Drawing.Point(10, 104);
            this.casparCgPanel.Name = "casparCgPanel";
            this.casparCgPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.casparCgPanel.Size = new System.Drawing.Size(485, 122);
            this.casparCgPanel.TabIndex = 1;
            // 
            // casparCgGroupBox
            // 
            this.casparCgGroupBox.AutoSize = true;
            this.casparCgGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.casparCgGroupBox.Controls.Add(this.casparCgTable);
            this.casparCgGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.casparCgGroupBox.Location = new System.Drawing.Point(0, 0);
            this.casparCgGroupBox.Name = "casparCgGroupBox";
            this.casparCgGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.casparCgGroupBox.Size = new System.Drawing.Size(485, 115);
            this.casparCgGroupBox.TabIndex = 0;
            this.casparCgGroupBox.TabStop = false;
            this.casparCgGroupBox.Text = "CasparCG";
            // 
            // casparCgTable
            // 
            this.casparCgTable.AutoSize = true;
            this.casparCgTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.casparCgTable.ColumnCount = 2;
            this.casparCgTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.casparCgTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.casparCgTable.Controls.Add(this.ipLabel, 0, 0);
            this.casparCgTable.Controls.Add(this.channelLabel, 0, 1);
            this.casparCgTable.Controls.Add(this.layerLabel, 0, 2);
            this.casparCgTable.Controls.Add(this.ipTextBox, 1, 0);
            this.casparCgTable.Controls.Add(this.channelNumericField, 1, 1);
            this.casparCgTable.Controls.Add(this.layerNumericField, 1, 2);
            this.casparCgTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.casparCgTable.Location = new System.Drawing.Point(8, 23);
            this.casparCgTable.Name = "casparCgTable";
            this.casparCgTable.RowCount = 3;
            this.casparCgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.casparCgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.casparCgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.casparCgTable.Size = new System.Drawing.Size(469, 84);
            this.casparCgTable.TabIndex = 0;
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ipLabel.Location = new System.Drawing.Point(3, 0);
            this.ipLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(86, 28);
            this.ipLabel.TabIndex = 0;
            this.ipLabel.Text = "IP of Server:";
            this.ipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // channelLabel
            // 
            this.channelLabel.AutoSize = true;
            this.channelLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.channelLabel.Location = new System.Drawing.Point(3, 28);
            this.channelLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.channelLabel.Name = "channelLabel";
            this.channelLabel.Size = new System.Drawing.Size(64, 28);
            this.channelLabel.TabIndex = 1;
            this.channelLabel.Text = "Channel:";
            this.channelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // layerLabel
            // 
            this.layerLabel.AutoSize = true;
            this.layerLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.layerLabel.Location = new System.Drawing.Point(3, 56);
            this.layerLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.layerLabel.Name = "layerLabel";
            this.layerLabel.Size = new System.Drawing.Size(48, 28);
            this.layerLabel.TabIndex = 2;
            this.layerLabel.Text = "Layer:";
            this.layerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // channelNumericField
            // 
            this.channelNumericField.Location = new System.Drawing.Point(107, 31);
            this.channelNumericField.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.channelNumericField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.channelNumericField.Name = "channelNumericField";
            this.channelNumericField.Size = new System.Drawing.Size(100, 22);
            this.channelNumericField.TabIndex = 4;
            this.channelNumericField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // layerNumericField
            // 
            this.layerNumericField.Location = new System.Drawing.Point(107, 59);
            this.layerNumericField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.layerNumericField.Name = "layerNumericField";
            this.layerNumericField.Size = new System.Drawing.Size(100, 22);
            this.layerNumericField.TabIndex = 5;
            this.layerNumericField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ipTextBox
            // 
            this.ipTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ipTextBox.Location = new System.Drawing.Point(107, 3);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(359, 22);
            this.ipTextBox.TabIndex = 3;
            // 
            // CasparCgPlayoutEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 450);
            this.DeleteButtonVisible = true;
            this.Name = "CasparCgPlayoutEditorForm";
            this.Text = "CasparCgPlayoutEditorForm";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.casparCgPanel.ResumeLayout(false);
            this.casparCgPanel.PerformLayout();
            this.casparCgGroupBox.ResumeLayout(false);
            this.casparCgGroupBox.PerformLayout();
            this.casparCgTable.ResumeLayout(false);
            this.casparCgTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.channelNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel casparCgPanel;
        private System.Windows.Forms.GroupBox casparCgGroupBox;
        private System.Windows.Forms.TableLayoutPanel casparCgTable;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label channelLabel;
        private System.Windows.Forms.Label layerLabel;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.NumericUpDown channelNumericField;
        private System.Windows.Forms.NumericUpDown layerNumericField;
    }
}