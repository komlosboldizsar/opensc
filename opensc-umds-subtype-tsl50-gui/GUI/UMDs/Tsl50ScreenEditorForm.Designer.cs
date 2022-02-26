namespace OpenSC.GUI.UMDs
{
    partial class Tsl50ScreenEditorForm
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
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.connectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.portLabel = new System.Windows.Forms.Label();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.indexLabel = new System.Windows.Forms.Label();
            this.indexNumericInput = new System.Windows.Forms.NumericUpDown();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.portNumericInput = new System.Windows.Forms.NumericUpDown();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.connectionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indexNumericInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericInput)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.connectionGroupBox);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 15, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(482, 406);
            this.customElementsPanel.Controls.SetChildIndex(this.connectionGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Size = new System.Drawing.Size(482, 492);
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.AutoSize = true;
            this.connectionGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionGroupBox.Controls.Add(this.connectionTable);
            this.connectionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionGroupBox.Location = new System.Drawing.Point(10, 120);
            this.connectionGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.connectionGroupBox.Size = new System.Drawing.Size(462, 134);
            this.connectionGroupBox.TabIndex = 4;
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
            this.connectionTable.Controls.Add(this.portNumericInput, 1, 1);
            this.connectionTable.Controls.Add(this.portLabel, 0, 1);
            this.connectionTable.Controls.Add(this.ipAddressLabel, 0, 0);
            this.connectionTable.Controls.Add(this.indexLabel, 0, 2);
            this.connectionTable.Controls.Add(this.indexNumericInput, 1, 2);
            this.connectionTable.Controls.Add(this.ipAddressTextBox, 1, 0);
            this.connectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTable.Location = new System.Drawing.Point(8, 25);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 3;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(446, 99);
            this.connectionTable.TabIndex = 0;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portLabel.Location = new System.Drawing.Point(3, 33);
            this.portLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(35, 33);
            this.portLabel.TabIndex = 5;
            this.portLabel.Text = "Port";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ipAddressLabel.Location = new System.Drawing.Point(3, 0);
            this.ipAddressLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(76, 33);
            this.ipAddressLabel.TabIndex = 0;
            this.ipAddressLabel.Text = "IP address";
            this.ipAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // indexLabel
            // 
            this.indexLabel.AutoSize = true;
            this.indexLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.indexLabel.Location = new System.Drawing.Point(3, 66);
            this.indexLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.indexLabel.Name = "indexLabel";
            this.indexLabel.Size = new System.Drawing.Size(45, 33);
            this.indexLabel.TabIndex = 2;
            this.indexLabel.Text = "Index";
            this.indexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // indexNumericInput
            // 
            this.indexNumericInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.indexNumericInput.Location = new System.Drawing.Point(97, 69);
            this.indexNumericInput.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.indexNumericInput.Name = "indexNumericInput";
            this.indexNumericInput.Size = new System.Drawing.Size(151, 27);
            this.indexNumericInput.TabIndex = 3;
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(97, 3);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(256, 27);
            this.ipAddressTextBox.TabIndex = 4;
            // 
            // portNumericInput
            // 
            this.portNumericInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.portNumericInput.Location = new System.Drawing.Point(97, 36);
            this.portNumericInput.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumericInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.portNumericInput.Name = "portNumericInput";
            this.portNumericInput.Size = new System.Drawing.Size(151, 27);
            this.portNumericInput.TabIndex = 6;
            this.portNumericInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Tsl50ScreenEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 562);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New TSL 5.0 screen";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(500, 286);
            this.Name = "Tsl50ScreenEditorForm";
            this.SubjectPlural = "TSL 5.0 screens";
            this.SubjectSingular = "TSL 5.0 screen";
            this.Text = "New TSL 5.0 screen";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indexNumericInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.Label indexLabel;
        private System.Windows.Forms.NumericUpDown indexNumericInput;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.NumericUpDown portNumericInput;
    }
}