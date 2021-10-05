namespace OpenSC.GUI.SerialPorts
{
    partial class SerialPortEditorForm
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
            this.comPortDataGroupBox = new System.Windows.Forms.GroupBox();
            this.comPortDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.parityDropDowm = new System.Windows.Forms.ComboBox();
            this.parityLabel = new System.Windows.Forms.Label();
            this.stopBitsDropDown = new System.Windows.Forms.ComboBox();
            this.stopBitsLabel = new System.Windows.Forms.Label();
            this.dataBitsNumericField = new System.Windows.Forms.NumericUpDown();
            this.dataBitsLabel = new System.Windows.Forms.Label();
            this.baudRateNumericField = new System.Windows.Forms.NumericUpDown();
            this.portNameLabel = new System.Windows.Forms.Label();
            this.baudRateLabel = new System.Windows.Forms.Label();
            this.portNameDropDown = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.comPortDataPanel.SuspendLayout();
            this.comPortDataGroupBox.SuspendLayout();
            this.comPortDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataBitsNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baudRateNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.comPortDataPanel);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(500, 342);
            this.customElementsPanel.Controls.SetChildIndex(this.comPortDataPanel, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(520, 431);
            // 
            // comPortDataPanel
            // 
            this.comPortDataPanel.AutoSize = true;
            this.comPortDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comPortDataPanel.Controls.Add(this.comPortDataGroupBox);
            this.comPortDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.comPortDataPanel.Location = new System.Drawing.Point(0, 27);
            this.comPortDataPanel.Name = "comPortDataPanel";
            this.comPortDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.comPortDataPanel.Size = new System.Drawing.Size(500, 184);
            this.comPortDataPanel.TabIndex = 3;
            // 
            // comPortDataGroupBox
            // 
            this.comPortDataGroupBox.AutoSize = true;
            this.comPortDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comPortDataGroupBox.Controls.Add(this.comPortDataTable);
            this.comPortDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.comPortDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.comPortDataGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.comPortDataGroupBox.Name = "comPortDataGroupBox";
            this.comPortDataGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.comPortDataGroupBox.Size = new System.Drawing.Size(500, 177);
            this.comPortDataGroupBox.TabIndex = 1;
            this.comPortDataGroupBox.TabStop = false;
            this.comPortDataGroupBox.Text = "COM port properties";
            // 
            // comPortDataTable
            // 
            this.comPortDataTable.AutoSize = true;
            this.comPortDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comPortDataTable.ColumnCount = 2;
            this.comPortDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.comPortDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.comPortDataTable.Controls.Add(this.parityDropDowm, 1, 4);
            this.comPortDataTable.Controls.Add(this.parityLabel, 0, 4);
            this.comPortDataTable.Controls.Add(this.stopBitsDropDown, 1, 3);
            this.comPortDataTable.Controls.Add(this.stopBitsLabel, 0, 3);
            this.comPortDataTable.Controls.Add(this.dataBitsNumericField, 1, 2);
            this.comPortDataTable.Controls.Add(this.dataBitsLabel, 0, 2);
            this.comPortDataTable.Controls.Add(this.baudRateNumericField, 1, 1);
            this.comPortDataTable.Controls.Add(this.portNameLabel, 0, 0);
            this.comPortDataTable.Controls.Add(this.baudRateLabel, 0, 1);
            this.comPortDataTable.Controls.Add(this.portNameDropDown, 1, 0);
            this.comPortDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comPortDataTable.Location = new System.Drawing.Point(8, 23);
            this.comPortDataTable.Name = "comPortDataTable";
            this.comPortDataTable.RowCount = 5;
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.Size = new System.Drawing.Size(484, 146);
            this.comPortDataTable.TabIndex = 0;
            // 
            // parityDropDowm
            // 
            this.parityDropDowm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityDropDowm.FormattingEnabled = true;
            this.parityDropDowm.Location = new System.Drawing.Point(94, 119);
            this.parityDropDowm.Name = "parityDropDowm";
            this.parityDropDowm.Size = new System.Drawing.Size(228, 24);
            this.parityDropDowm.TabIndex = 11;
            // 
            // parityLabel
            // 
            this.parityLabel.AutoSize = true;
            this.parityLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.parityLabel.Location = new System.Drawing.Point(3, 116);
            this.parityLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.parityLabel.Name = "parityLabel";
            this.parityLabel.Size = new System.Drawing.Size(44, 30);
            this.parityLabel.TabIndex = 10;
            this.parityLabel.Text = "Parity";
            this.parityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stopBitsDropDown
            // 
            this.stopBitsDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBitsDropDown.FormattingEnabled = true;
            this.stopBitsDropDown.Location = new System.Drawing.Point(94, 89);
            this.stopBitsDropDown.Name = "stopBitsDropDown";
            this.stopBitsDropDown.Size = new System.Drawing.Size(228, 24);
            this.stopBitsDropDown.TabIndex = 9;
            // 
            // stopBitsLabel
            // 
            this.stopBitsLabel.AutoSize = true;
            this.stopBitsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.stopBitsLabel.Location = new System.Drawing.Point(3, 86);
            this.stopBitsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.stopBitsLabel.Name = "stopBitsLabel";
            this.stopBitsLabel.Size = new System.Drawing.Size(63, 30);
            this.stopBitsLabel.TabIndex = 8;
            this.stopBitsLabel.Text = "Stop bits";
            this.stopBitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataBitsNumericField
            // 
            this.dataBitsNumericField.Location = new System.Drawing.Point(94, 61);
            this.dataBitsNumericField.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.dataBitsNumericField.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.dataBitsNumericField.Name = "dataBitsNumericField";
            this.dataBitsNumericField.Size = new System.Drawing.Size(120, 22);
            this.dataBitsNumericField.TabIndex = 7;
            this.dataBitsNumericField.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // dataBitsLabel
            // 
            this.dataBitsLabel.AutoSize = true;
            this.dataBitsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataBitsLabel.Location = new System.Drawing.Point(3, 58);
            this.dataBitsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.dataBitsLabel.Name = "dataBitsLabel";
            this.dataBitsLabel.Size = new System.Drawing.Size(64, 28);
            this.dataBitsLabel.TabIndex = 6;
            this.dataBitsLabel.Text = "Data bits";
            this.dataBitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // baudRateNumericField
            // 
            this.baudRateNumericField.Location = new System.Drawing.Point(94, 33);
            this.baudRateNumericField.Maximum = new decimal(new int[] {
            115200,
            0,
            0,
            0});
            this.baudRateNumericField.Minimum = new decimal(new int[] {
            9600,
            0,
            0,
            0});
            this.baudRateNumericField.Name = "baudRateNumericField";
            this.baudRateNumericField.Size = new System.Drawing.Size(120, 22);
            this.baudRateNumericField.TabIndex = 5;
            this.baudRateNumericField.Value = new decimal(new int[] {
            9600,
            0,
            0,
            0});
            // 
            // portNameLabel
            // 
            this.portNameLabel.AutoSize = true;
            this.portNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portNameLabel.Location = new System.Drawing.Point(3, 0);
            this.portNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portNameLabel.Name = "portNameLabel";
            this.portNameLabel.Size = new System.Drawing.Size(73, 30);
            this.portNameLabel.TabIndex = 0;
            this.portNameLabel.Text = "Port name";
            this.portNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // baudRateLabel
            // 
            this.baudRateLabel.AutoSize = true;
            this.baudRateLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.baudRateLabel.Location = new System.Drawing.Point(3, 30);
            this.baudRateLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.baudRateLabel.Name = "baudRateLabel";
            this.baudRateLabel.Size = new System.Drawing.Size(70, 28);
            this.baudRateLabel.TabIndex = 1;
            this.baudRateLabel.Text = "Baud rate";
            this.baudRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portNameDropDown
            // 
            this.portNameDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNameDropDown.FormattingEnabled = true;
            this.portNameDropDown.Location = new System.Drawing.Point(94, 3);
            this.portNameDropDown.Name = "portNameDropDown";
            this.portNameDropDown.Size = new System.Drawing.Size(228, 24);
            this.portNameDropDown.TabIndex = 4;
            // 
            // SerialPortEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 487);
            this.DeleteButtonVisible = true;
            this.HeaderText = "Edit serial port";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "SerialPortEditorForm";
            this.Text = "Edit serial port";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.comPortDataPanel.ResumeLayout(false);
            this.comPortDataPanel.PerformLayout();
            this.comPortDataGroupBox.ResumeLayout(false);
            this.comPortDataGroupBox.PerformLayout();
            this.comPortDataTable.ResumeLayout(false);
            this.comPortDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataBitsNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baudRateNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel comPortDataPanel;
        private System.Windows.Forms.GroupBox comPortDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel comPortDataTable;
        private System.Windows.Forms.Label portNameLabel;
        private System.Windows.Forms.Label baudRateLabel;
        private System.Windows.Forms.ComboBox portNameDropDown;
        private System.Windows.Forms.Label dataBitsLabel;
        private System.Windows.Forms.NumericUpDown baudRateNumericField;
        private System.Windows.Forms.ComboBox parityDropDowm;
        private System.Windows.Forms.Label parityLabel;
        private System.Windows.Forms.ComboBox stopBitsDropDown;
        private System.Windows.Forms.Label stopBitsLabel;
        private System.Windows.Forms.NumericUpDown dataBitsNumericField;
    }
}