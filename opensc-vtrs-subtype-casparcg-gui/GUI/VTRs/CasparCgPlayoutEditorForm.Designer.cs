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
            this.ipAddressInput = new OpenSC.GUI.GeneralComponents.IPAddressControl.IPv4AddressControl();
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
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 19, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(505, 406);
            this.customElementsPanel.Controls.SetChildIndex(this.casparCgPanel, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.mainContainer.Size = new System.Drawing.Size(505, 492);
            // 
            // casparCgPanel
            // 
            this.casparCgPanel.AutoSize = true;
            this.casparCgPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.casparCgPanel.Controls.Add(this.casparCgGroupBox);
            this.casparCgPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.casparCgPanel.Location = new System.Drawing.Point(10, 128);
            this.casparCgPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.casparCgPanel.Name = "casparCgPanel";
            this.casparCgPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.casparCgPanel.Size = new System.Drawing.Size(485, 152);
            this.casparCgPanel.TabIndex = 1;
            // 
            // casparCgGroupBox
            // 
            this.casparCgGroupBox.AutoSize = true;
            this.casparCgGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.casparCgGroupBox.Controls.Add(this.casparCgTable);
            this.casparCgGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.casparCgGroupBox.Location = new System.Drawing.Point(0, 0);
            this.casparCgGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.casparCgGroupBox.Name = "casparCgGroupBox";
            this.casparCgGroupBox.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.casparCgGroupBox.Size = new System.Drawing.Size(485, 143);
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
            this.casparCgTable.Controls.Add(this.channelNumericField, 1, 1);
            this.casparCgTable.Controls.Add(this.layerNumericField, 1, 2);
            this.casparCgTable.Controls.Add(this.ipAddressInput, 1, 0);
            this.casparCgTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.casparCgTable.Location = new System.Drawing.Point(8, 30);
            this.casparCgTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.casparCgTable.Name = "casparCgTable";
            this.casparCgTable.RowCount = 3;
            this.casparCgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.casparCgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.casparCgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.casparCgTable.Size = new System.Drawing.Size(469, 103);
            this.casparCgTable.TabIndex = 0;
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ipLabel.Location = new System.Drawing.Point(3, 0);
            this.ipLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(87, 33);
            this.ipLabel.TabIndex = 0;
            this.ipLabel.Text = "IP of Server:";
            this.ipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // channelLabel
            // 
            this.channelLabel.AutoSize = true;
            this.channelLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.channelLabel.Location = new System.Drawing.Point(3, 33);
            this.channelLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.channelLabel.Name = "channelLabel";
            this.channelLabel.Size = new System.Drawing.Size(65, 35);
            this.channelLabel.TabIndex = 1;
            this.channelLabel.Text = "Channel:";
            this.channelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // layerLabel
            // 
            this.layerLabel.AutoSize = true;
            this.layerLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.layerLabel.Location = new System.Drawing.Point(3, 68);
            this.layerLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.layerLabel.Name = "layerLabel";
            this.layerLabel.Size = new System.Drawing.Size(47, 35);
            this.layerLabel.TabIndex = 2;
            this.layerLabel.Text = "Layer:";
            this.layerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // channelNumericField
            // 
            this.channelNumericField.Location = new System.Drawing.Point(108, 37);
            this.channelNumericField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.channelNumericField.Size = new System.Drawing.Size(100, 27);
            this.channelNumericField.TabIndex = 4;
            this.channelNumericField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // layerNumericField
            // 
            this.layerNumericField.Location = new System.Drawing.Point(108, 72);
            this.layerNumericField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layerNumericField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.layerNumericField.Name = "layerNumericField";
            this.layerNumericField.Size = new System.Drawing.Size(100, 27);
            this.layerNumericField.TabIndex = 5;
            this.layerNumericField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ipAddressInput
            // 
            this.ipAddressInput.AllowInternalTab = false;
            this.ipAddressInput.AutoHeight = true;
            this.ipAddressInput.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipAddressInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.ipAddressInput.Location = new System.Drawing.Point(108, 3);
            this.ipAddressInput.Name = "ipAddressInput";
            this.ipAddressInput.ReadOnly = false;
            this.ipAddressInput.Size = new System.Drawing.Size(212, 27);
            this.ipAddressInput.TabIndex = 6;
            this.ipAddressInput.Text = "...";
            // 
            // CasparCgPlayoutEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 562);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(500, 346);
            this.Name = "CasparCgPlayoutEditorForm";
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
        private System.Windows.Forms.NumericUpDown channelNumericField;
        private System.Windows.Forms.NumericUpDown layerNumericField;
        private GeneralComponents.IPAddressControl.IPv4AddressControl ipAddressInput;
    }
}