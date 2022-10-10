namespace OpenSC.GUI.SerialPorts
{
    partial class SerialPortMonitorForm
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
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.simulateSendingEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.sendDataButton = new System.Windows.Forms.Button();
            this.simulateSendingEncodingLabel = new System.Windows.Forms.Label();
            this.simulateSendingDataLabel = new System.Windows.Forms.Label();
            this.simulateSendingLabel = new System.Windows.Forms.Label();
            this.bothTextBox = new System.Windows.Forms.TextBox();
            this.sentLabel = new System.Windows.Forms.Label();
            this.receivedLabel = new System.Windows.Forms.Label();
            this.sentTextBox = new System.Windows.Forms.TextBox();
            this.receivedTextBox = new System.Windows.Forms.TextBox();
            this.simulateSendingDataTextBox = new System.Windows.Forms.TextBox();
            this.simulateReceivingLabel = new System.Windows.Forms.Label();
            this.simulateReceivingDataLabel = new System.Windows.Forms.Label();
            this.simulateReceivingEncodingLabel = new System.Windows.Forms.Label();
            this.receiveDataButton = new System.Windows.Forms.Button();
            this.sentTextClearButton = new System.Windows.Forms.Button();
            this.receivedTextClearButton = new System.Windows.Forms.Button();
            this.sentTextBoxEncodingLabel = new System.Windows.Forms.Label();
            this.sentTextBoxEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.receivedTextBoxEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.receivedTextBoxEncodingLabel = new System.Windows.Forms.Label();
            this.simulateReceivingEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.simulateReceivingDataTextBox = new System.Windows.Forms.TextBox();
            this.monitoringSinceLabel = new System.Windows.Forms.Label();
            this.dialogTextBoxEncodingLabel = new System.Windows.Forms.Label();
            this.bothTextBoxEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.allTextClearButton = new System.Windows.Forms.Button();
            this.mainContainer.SuspendLayout();
            this.comPortDataPanel.SuspendLayout();
            this.comPortDataGroupBox.SuspendLayout();
            this.comPortDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataBitsNumericField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baudRateNumericField)).BeginInit();
            this.table.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.table);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(681, 698);
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
            this.comPortDataGroupBox.Size = new System.Drawing.Size(500, 204);
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
            this.comPortDataTable.Location = new System.Drawing.Point(8, 28);
            this.comPortDataTable.Name = "comPortDataTable";
            this.comPortDataTable.RowCount = 5;
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.Size = new System.Drawing.Size(484, 168);
            this.comPortDataTable.TabIndex = 0;
            // 
            // parityDropDowm
            // 
            this.parityDropDowm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityDropDowm.FormattingEnabled = true;
            this.parityDropDowm.Location = new System.Drawing.Point(97, 137);
            this.parityDropDowm.Name = "parityDropDowm";
            this.parityDropDowm.Size = new System.Drawing.Size(228, 28);
            this.parityDropDowm.TabIndex = 11;
            // 
            // parityLabel
            // 
            this.parityLabel.AutoSize = true;
            this.parityLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.parityLabel.Location = new System.Drawing.Point(3, 134);
            this.parityLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.parityLabel.Name = "parityLabel";
            this.parityLabel.Size = new System.Drawing.Size(45, 34);
            this.parityLabel.TabIndex = 10;
            this.parityLabel.Text = "Parity";
            this.parityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stopBitsDropDown
            // 
            this.stopBitsDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBitsDropDown.FormattingEnabled = true;
            this.stopBitsDropDown.Location = new System.Drawing.Point(97, 103);
            this.stopBitsDropDown.Name = "stopBitsDropDown";
            this.stopBitsDropDown.Size = new System.Drawing.Size(228, 28);
            this.stopBitsDropDown.TabIndex = 9;
            // 
            // stopBitsLabel
            // 
            this.stopBitsLabel.AutoSize = true;
            this.stopBitsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.stopBitsLabel.Location = new System.Drawing.Point(3, 100);
            this.stopBitsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.stopBitsLabel.Name = "stopBitsLabel";
            this.stopBitsLabel.Size = new System.Drawing.Size(68, 34);
            this.stopBitsLabel.TabIndex = 8;
            this.stopBitsLabel.Text = "Stop bits";
            this.stopBitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataBitsNumericField
            // 
            this.dataBitsNumericField.Location = new System.Drawing.Point(97, 70);
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
            this.dataBitsNumericField.Size = new System.Drawing.Size(120, 27);
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
            this.dataBitsLabel.Location = new System.Drawing.Point(3, 67);
            this.dataBitsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.dataBitsLabel.Name = "dataBitsLabel";
            this.dataBitsLabel.Size = new System.Drawing.Size(69, 33);
            this.dataBitsLabel.TabIndex = 6;
            this.dataBitsLabel.Text = "Data bits";
            this.dataBitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // baudRateNumericField
            // 
            this.baudRateNumericField.Location = new System.Drawing.Point(97, 37);
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
            this.baudRateNumericField.Size = new System.Drawing.Size(120, 27);
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
            this.portNameLabel.Size = new System.Drawing.Size(76, 34);
            this.portNameLabel.TabIndex = 0;
            this.portNameLabel.Text = "Port name";
            this.portNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // baudRateLabel
            // 
            this.baudRateLabel.AutoSize = true;
            this.baudRateLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.baudRateLabel.Location = new System.Drawing.Point(3, 34);
            this.baudRateLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.baudRateLabel.Name = "baudRateLabel";
            this.baudRateLabel.Size = new System.Drawing.Size(73, 33);
            this.baudRateLabel.TabIndex = 1;
            this.baudRateLabel.Text = "Baud rate";
            this.baudRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portNameDropDown
            // 
            this.portNameDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNameDropDown.FormattingEnabled = true;
            this.portNameDropDown.Location = new System.Drawing.Point(97, 3);
            this.portNameDropDown.Name = "portNameDropDown";
            this.portNameDropDown.Size = new System.Drawing.Size(228, 28);
            this.portNameDropDown.TabIndex = 4;
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.ColumnCount = 4;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.Controls.Add(this.simulateSendingEncodingDropDown, 1, 8);
            this.table.Controls.Add(this.sendDataButton, 0, 9);
            this.table.Controls.Add(this.simulateSendingEncodingLabel, 0, 8);
            this.table.Controls.Add(this.simulateSendingDataLabel, 0, 7);
            this.table.Controls.Add(this.simulateSendingLabel, 0, 6);
            this.table.Controls.Add(this.bothTextBox, 0, 0);
            this.table.Controls.Add(this.sentLabel, 0, 2);
            this.table.Controls.Add(this.receivedLabel, 2, 2);
            this.table.Controls.Add(this.sentTextBox, 0, 3);
            this.table.Controls.Add(this.receivedTextBox, 2, 3);
            this.table.Controls.Add(this.simulateSendingDataTextBox, 1, 7);
            this.table.Controls.Add(this.simulateReceivingLabel, 2, 6);
            this.table.Controls.Add(this.simulateReceivingDataLabel, 2, 7);
            this.table.Controls.Add(this.simulateReceivingEncodingLabel, 2, 8);
            this.table.Controls.Add(this.receiveDataButton, 2, 9);
            this.table.Controls.Add(this.sentTextClearButton, 0, 5);
            this.table.Controls.Add(this.receivedTextClearButton, 2, 5);
            this.table.Controls.Add(this.sentTextBoxEncodingLabel, 0, 4);
            this.table.Controls.Add(this.sentTextBoxEncodingDropDown, 1, 4);
            this.table.Controls.Add(this.receivedTextBoxEncodingDropDown, 3, 4);
            this.table.Controls.Add(this.receivedTextBoxEncodingLabel, 2, 4);
            this.table.Controls.Add(this.simulateReceivingEncodingDropDown, 3, 8);
            this.table.Controls.Add(this.simulateReceivingDataTextBox, 3, 7);
            this.table.Controls.Add(this.monitoringSinceLabel, 2, 10);
            this.table.Controls.Add(this.dialogTextBoxEncodingLabel, 0, 1);
            this.table.Controls.Add(this.bothTextBoxEncodingDropDown, 1, 1);
            this.table.Controls.Add(this.allTextClearButton, 2, 1);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(10, 10);
            this.table.Name = "table";
            this.table.RowCount = 11;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(661, 678);
            this.table.TabIndex = 0;
            // 
            // simulateSendingEncodingDropDown
            // 
            this.simulateSendingEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateSendingEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulateSendingEncodingDropDown.FormattingEnabled = true;
            this.simulateSendingEncodingDropDown.Location = new System.Drawing.Point(95, 565);
            this.simulateSendingEncodingDropDown.Name = "simulateSendingEncodingDropDown";
            this.simulateSendingEncodingDropDown.Size = new System.Drawing.Size(232, 28);
            this.simulateSendingEncodingDropDown.TabIndex = 12;
            // 
            // sendDataButton
            // 
            this.table.SetColumnSpan(this.sendDataButton, 2);
            this.sendDataButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.sendDataButton.Location = new System.Drawing.Point(3, 599);
            this.sendDataButton.Name = "sendDataButton";
            this.sendDataButton.Size = new System.Drawing.Size(324, 29);
            this.sendDataButton.TabIndex = 11;
            this.sendDataButton.Text = "Send data";
            this.sendDataButton.UseVisualStyleBackColor = true;
            this.sendDataButton.Click += new System.EventHandler(this.sendDataButton_Click);
            // 
            // simulateSendingEncodingLabel
            // 
            this.simulateSendingEncodingLabel.AutoSize = true;
            this.simulateSendingEncodingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.simulateSendingEncodingLabel.Location = new System.Drawing.Point(3, 562);
            this.simulateSendingEncodingLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.simulateSendingEncodingLabel.Name = "simulateSendingEncodingLabel";
            this.simulateSendingEncodingLabel.Size = new System.Drawing.Size(74, 34);
            this.simulateSendingEncodingLabel.TabIndex = 10;
            this.simulateSendingEncodingLabel.Text = "Encoding:";
            this.simulateSendingEncodingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // simulateSendingDataLabel
            // 
            this.simulateSendingDataLabel.AutoSize = true;
            this.simulateSendingDataLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.simulateSendingDataLabel.Location = new System.Drawing.Point(3, 531);
            this.simulateSendingDataLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.simulateSendingDataLabel.Name = "simulateSendingDataLabel";
            this.simulateSendingDataLabel.Size = new System.Drawing.Size(39, 31);
            this.simulateSendingDataLabel.TabIndex = 9;
            this.simulateSendingDataLabel.Text = "Text:";
            this.simulateSendingDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // simulateSendingLabel
            // 
            this.simulateSendingLabel.AutoSize = true;
            this.table.SetColumnSpan(this.simulateSendingLabel, 2);
            this.simulateSendingLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateSendingLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.simulateSendingLabel.Location = new System.Drawing.Point(3, 506);
            this.simulateSendingLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 5);
            this.simulateSendingLabel.Name = "simulateSendingLabel";
            this.simulateSendingLabel.Size = new System.Drawing.Size(324, 20);
            this.simulateSendingLabel.TabIndex = 8;
            this.simulateSendingLabel.Text = "Simulate sending";
            // 
            // bothTextBox
            // 
            this.bothTextBox.BackColor = System.Drawing.Color.White;
            this.table.SetColumnSpan(this.bothTextBox, 4);
            this.bothTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.bothTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bothTextBox.Location = new System.Drawing.Point(3, 3);
            this.bothTextBox.MinimumSize = new System.Drawing.Size(4, 200);
            this.bothTextBox.Multiline = true;
            this.bothTextBox.Name = "bothTextBox";
            this.bothTextBox.ReadOnly = true;
            this.bothTextBox.Size = new System.Drawing.Size(655, 200);
            this.bothTextBox.TabIndex = 0;
            // 
            // sentLabel
            // 
            this.sentLabel.AutoSize = true;
            this.table.SetColumnSpan(this.sentLabel, 2);
            this.sentLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.sentLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sentLabel.Location = new System.Drawing.Point(3, 251);
            this.sentLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.sentLabel.Name = "sentLabel";
            this.sentLabel.Size = new System.Drawing.Size(324, 20);
            this.sentLabel.TabIndex = 1;
            this.sentLabel.Text = "Sent";
            // 
            // receivedLabel
            // 
            this.receivedLabel.AutoSize = true;
            this.table.SetColumnSpan(this.receivedLabel, 2);
            this.receivedLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.receivedLabel.Location = new System.Drawing.Point(333, 251);
            this.receivedLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.receivedLabel.Name = "receivedLabel";
            this.receivedLabel.Size = new System.Drawing.Size(325, 20);
            this.receivedLabel.TabIndex = 2;
            this.receivedLabel.Text = "Received";
            // 
            // sentTextBox
            // 
            this.sentTextBox.BackColor = System.Drawing.Color.White;
            this.table.SetColumnSpan(this.sentTextBox, 2);
            this.sentTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.sentTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sentTextBox.Location = new System.Drawing.Point(3, 274);
            this.sentTextBox.MinimumSize = new System.Drawing.Size(4, 150);
            this.sentTextBox.Multiline = true;
            this.sentTextBox.Name = "sentTextBox";
            this.sentTextBox.ReadOnly = true;
            this.sentTextBox.Size = new System.Drawing.Size(324, 150);
            this.sentTextBox.TabIndex = 3;
            // 
            // receivedTextBox
            // 
            this.receivedTextBox.BackColor = System.Drawing.Color.White;
            this.table.SetColumnSpan(this.receivedTextBox, 2);
            this.receivedTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.receivedTextBox.Location = new System.Drawing.Point(333, 274);
            this.receivedTextBox.MinimumSize = new System.Drawing.Size(4, 150);
            this.receivedTextBox.Multiline = true;
            this.receivedTextBox.Name = "receivedTextBox";
            this.receivedTextBox.ReadOnly = true;
            this.receivedTextBox.Size = new System.Drawing.Size(325, 150);
            this.receivedTextBox.TabIndex = 4;
            // 
            // simulateSendingDataTextBox
            // 
            this.simulateSendingDataTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateSendingDataTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.simulateSendingDataTextBox.Location = new System.Drawing.Point(95, 534);
            this.simulateSendingDataTextBox.Name = "simulateSendingDataTextBox";
            this.simulateSendingDataTextBox.Size = new System.Drawing.Size(232, 25);
            this.simulateSendingDataTextBox.TabIndex = 13;
            // 
            // simulateReceivingLabel
            // 
            this.simulateReceivingLabel.AutoSize = true;
            this.table.SetColumnSpan(this.simulateReceivingLabel, 2);
            this.simulateReceivingLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateReceivingLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.simulateReceivingLabel.Location = new System.Drawing.Point(333, 506);
            this.simulateReceivingLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.simulateReceivingLabel.Name = "simulateReceivingLabel";
            this.simulateReceivingLabel.Size = new System.Drawing.Size(325, 20);
            this.simulateReceivingLabel.TabIndex = 14;
            this.simulateReceivingLabel.Text = "Simulate receiving";
            // 
            // simulateReceivingDataLabel
            // 
            this.simulateReceivingDataLabel.AutoSize = true;
            this.simulateReceivingDataLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.simulateReceivingDataLabel.Location = new System.Drawing.Point(333, 531);
            this.simulateReceivingDataLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.simulateReceivingDataLabel.Name = "simulateReceivingDataLabel";
            this.simulateReceivingDataLabel.Size = new System.Drawing.Size(39, 31);
            this.simulateReceivingDataLabel.TabIndex = 15;
            this.simulateReceivingDataLabel.Text = "Text:";
            this.simulateReceivingDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // simulateReceivingEncodingLabel
            // 
            this.simulateReceivingEncodingLabel.AutoSize = true;
            this.simulateReceivingEncodingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.simulateReceivingEncodingLabel.Location = new System.Drawing.Point(333, 562);
            this.simulateReceivingEncodingLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.simulateReceivingEncodingLabel.Name = "simulateReceivingEncodingLabel";
            this.simulateReceivingEncodingLabel.Size = new System.Drawing.Size(74, 34);
            this.simulateReceivingEncodingLabel.TabIndex = 16;
            this.simulateReceivingEncodingLabel.Text = "Encoding:";
            this.simulateReceivingEncodingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // receiveDataButton
            // 
            this.table.SetColumnSpan(this.receiveDataButton, 2);
            this.receiveDataButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.receiveDataButton.Location = new System.Drawing.Point(333, 599);
            this.receiveDataButton.Name = "receiveDataButton";
            this.receiveDataButton.Size = new System.Drawing.Size(325, 29);
            this.receiveDataButton.TabIndex = 17;
            this.receiveDataButton.Text = "Receive data";
            this.receiveDataButton.UseVisualStyleBackColor = true;
            this.receiveDataButton.Click += new System.EventHandler(this.receiveDataButton_Click);
            // 
            // sentTextClearButton
            // 
            this.table.SetColumnSpan(this.sentTextClearButton, 2);
            this.sentTextClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.sentTextClearButton.Location = new System.Drawing.Point(3, 464);
            this.sentTextClearButton.Name = "sentTextClearButton";
            this.sentTextClearButton.Size = new System.Drawing.Size(324, 29);
            this.sentTextClearButton.TabIndex = 18;
            this.sentTextClearButton.Text = "Clear";
            this.sentTextClearButton.UseVisualStyleBackColor = true;
            this.sentTextClearButton.Click += new System.EventHandler(this.sentTextClearButton_Click);
            // 
            // receivedTextClearButton
            // 
            this.table.SetColumnSpan(this.receivedTextClearButton, 2);
            this.receivedTextClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextClearButton.Location = new System.Drawing.Point(333, 464);
            this.receivedTextClearButton.Name = "receivedTextClearButton";
            this.receivedTextClearButton.Size = new System.Drawing.Size(325, 29);
            this.receivedTextClearButton.TabIndex = 19;
            this.receivedTextClearButton.Text = "Clear";
            this.receivedTextClearButton.UseVisualStyleBackColor = true;
            this.receivedTextClearButton.Click += new System.EventHandler(this.receivedTextClearButton_Click);
            // 
            // sentTextBoxEncodingLabel
            // 
            this.sentTextBoxEncodingLabel.AutoSize = true;
            this.sentTextBoxEncodingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sentTextBoxEncodingLabel.Location = new System.Drawing.Point(3, 427);
            this.sentTextBoxEncodingLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.sentTextBoxEncodingLabel.Name = "sentTextBoxEncodingLabel";
            this.sentTextBoxEncodingLabel.Size = new System.Drawing.Size(74, 34);
            this.sentTextBoxEncodingLabel.TabIndex = 20;
            this.sentTextBoxEncodingLabel.Text = "Encoding:";
            this.sentTextBoxEncodingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sentTextBoxEncodingDropDown
            // 
            this.sentTextBoxEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.sentTextBoxEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sentTextBoxEncodingDropDown.FormattingEnabled = true;
            this.sentTextBoxEncodingDropDown.Location = new System.Drawing.Point(95, 430);
            this.sentTextBoxEncodingDropDown.Name = "sentTextBoxEncodingDropDown";
            this.sentTextBoxEncodingDropDown.Size = new System.Drawing.Size(232, 28);
            this.sentTextBoxEncodingDropDown.TabIndex = 21;
            this.sentTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.sentTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // receivedTextBoxEncodingDropDown
            // 
            this.receivedTextBoxEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextBoxEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.receivedTextBoxEncodingDropDown.FormattingEnabled = true;
            this.receivedTextBoxEncodingDropDown.Location = new System.Drawing.Point(425, 430);
            this.receivedTextBoxEncodingDropDown.Name = "receivedTextBoxEncodingDropDown";
            this.receivedTextBoxEncodingDropDown.Size = new System.Drawing.Size(233, 28);
            this.receivedTextBoxEncodingDropDown.TabIndex = 22;
            this.receivedTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.receivedTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // receivedTextBoxEncodingLabel
            // 
            this.receivedTextBoxEncodingLabel.AutoSize = true;
            this.receivedTextBoxEncodingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.receivedTextBoxEncodingLabel.Location = new System.Drawing.Point(333, 427);
            this.receivedTextBoxEncodingLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.receivedTextBoxEncodingLabel.Name = "receivedTextBoxEncodingLabel";
            this.receivedTextBoxEncodingLabel.Size = new System.Drawing.Size(74, 34);
            this.receivedTextBoxEncodingLabel.TabIndex = 23;
            this.receivedTextBoxEncodingLabel.Text = "Encoding:";
            this.receivedTextBoxEncodingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // simulateReceivingEncodingDropDown
            // 
            this.simulateReceivingEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateReceivingEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulateReceivingEncodingDropDown.FormattingEnabled = true;
            this.simulateReceivingEncodingDropDown.Location = new System.Drawing.Point(425, 565);
            this.simulateReceivingEncodingDropDown.Name = "simulateReceivingEncodingDropDown";
            this.simulateReceivingEncodingDropDown.Size = new System.Drawing.Size(233, 28);
            this.simulateReceivingEncodingDropDown.TabIndex = 24;
            // 
            // simulateReceivingDataTextBox
            // 
            this.simulateReceivingDataTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateReceivingDataTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.simulateReceivingDataTextBox.Location = new System.Drawing.Point(425, 534);
            this.simulateReceivingDataTextBox.Name = "simulateReceivingDataTextBox";
            this.simulateReceivingDataTextBox.Size = new System.Drawing.Size(233, 25);
            this.simulateReceivingDataTextBox.TabIndex = 25;
            // 
            // monitoringSinceLabel
            // 
            this.monitoringSinceLabel.AutoSize = true;
            this.table.SetColumnSpan(this.monitoringSinceLabel, 2);
            this.monitoringSinceLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.monitoringSinceLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.monitoringSinceLabel.Location = new System.Drawing.Point(333, 658);
            this.monitoringSinceLabel.Name = "monitoringSinceLabel";
            this.monitoringSinceLabel.Size = new System.Drawing.Size(325, 20);
            this.monitoringSinceLabel.TabIndex = 26;
            this.monitoringSinceLabel.Text = "Monitoring since 2000. 01. 01. 12:00:00";
            this.monitoringSinceLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // dialogTextBoxEncodingLabel
            // 
            this.dialogTextBoxEncodingLabel.AutoSize = true;
            this.dialogTextBoxEncodingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.dialogTextBoxEncodingLabel.Location = new System.Drawing.Point(3, 206);
            this.dialogTextBoxEncodingLabel.Name = "dialogTextBoxEncodingLabel";
            this.dialogTextBoxEncodingLabel.Size = new System.Drawing.Size(74, 35);
            this.dialogTextBoxEncodingLabel.TabIndex = 27;
            this.dialogTextBoxEncodingLabel.Text = "Encoding:";
            this.dialogTextBoxEncodingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bothTextBoxEncodingDropDown
            // 
            this.bothTextBoxEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.bothTextBoxEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bothTextBoxEncodingDropDown.FormattingEnabled = true;
            this.bothTextBoxEncodingDropDown.Location = new System.Drawing.Point(95, 209);
            this.bothTextBoxEncodingDropDown.Name = "bothTextBoxEncodingDropDown";
            this.bothTextBoxEncodingDropDown.Size = new System.Drawing.Size(232, 28);
            this.bothTextBoxEncodingDropDown.TabIndex = 28;
            this.bothTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.bothTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // allTextClearButton
            // 
            this.table.SetColumnSpan(this.allTextClearButton, 2);
            this.allTextClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.allTextClearButton.Location = new System.Drawing.Point(333, 209);
            this.allTextClearButton.Name = "allTextClearButton";
            this.allTextClearButton.Size = new System.Drawing.Size(325, 29);
            this.allTextClearButton.TabIndex = 29;
            this.allTextClearButton.Text = "Clear all";
            this.allTextClearButton.UseVisualStyleBackColor = true;
            this.allTextClearButton.Click += new System.EventHandler(this.allTextClearButton_Click);
            // 
            // SerialPortMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 768);
            this.HeaderText = "Serial port monitor";
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.MinimumSize = new System.Drawing.Size(500, 538);
            this.Name = "SerialPortMonitorForm";
            this.Text = "Serial port monitor";
            this.Load += new System.EventHandler(this.SerialPortMonitorForm_Load);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.comPortDataPanel.ResumeLayout(false);
            this.comPortDataPanel.PerformLayout();
            this.comPortDataGroupBox.ResumeLayout(false);
            this.comPortDataGroupBox.PerformLayout();
            this.comPortDataTable.ResumeLayout(false);
            this.comPortDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataBitsNumericField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baudRateNumericField)).EndInit();
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.TextBox bothTextBox;
        private System.Windows.Forms.Label sentLabel;
        private System.Windows.Forms.Label receivedLabel;
        private System.Windows.Forms.TextBox sentTextBox;
        private System.Windows.Forms.TextBox receivedTextBox;
        private System.Windows.Forms.ComboBox simulateSendingEncodingDropDown;
        private System.Windows.Forms.Button sendDataButton;
        private System.Windows.Forms.Label simulateSendingEncodingLabel;
        private System.Windows.Forms.Label simulateSendingDataLabel;
        private System.Windows.Forms.Label simulateSendingLabel;
        private System.Windows.Forms.TextBox simulateSendingDataTextBox;
        private System.Windows.Forms.Label simulateReceivingLabel;
        private System.Windows.Forms.Label simulateReceivingDataLabel;
        private System.Windows.Forms.Label simulateReceivingEncodingLabel;
        private System.Windows.Forms.Button receiveDataButton;
        private System.Windows.Forms.Button sentTextClearButton;
        private System.Windows.Forms.Button receivedTextClearButton;
        private System.Windows.Forms.Label sentTextBoxEncodingLabel;
        private System.Windows.Forms.ComboBox sentTextBoxEncodingDropDown;
        private System.Windows.Forms.ComboBox receivedTextBoxEncodingDropDown;
        private System.Windows.Forms.Label receivedTextBoxEncodingLabel;
        private System.Windows.Forms.ComboBox simulateReceivingEncodingDropDown;
        private System.Windows.Forms.TextBox simulateReceivingDataTextBox;
        private System.Windows.Forms.Label monitoringSinceLabel;
        private System.Windows.Forms.Label dialogTextBoxEncodingLabel;
        private System.Windows.Forms.ComboBox bothTextBoxEncodingDropDown;
        private System.Windows.Forms.Button allTextClearButton;
    }
}