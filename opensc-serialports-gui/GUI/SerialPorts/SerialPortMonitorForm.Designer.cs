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
            this.dialogTextBoxEncodingLabel = new System.Windows.Forms.Label();
            this.bothTextBoxEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.allTextClearButton = new System.Windows.Forms.Button();
            this.portSettingsLabel = new System.Windows.Forms.Label();
            this.portNameLabel = new System.Windows.Forms.Label();
            this.portBaudrateLabel = new System.Windows.Forms.Label();
            this.portDatabitsLabel = new System.Windows.Forms.Label();
            this.portNameValueLabel = new System.Windows.Forms.Label();
            this.portBaudrateValueLabel = new System.Windows.Forms.Label();
            this.portDatabitsValueLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.portStatusLabel = new System.Windows.Forms.Label();
            this.portStatusValueLabel = new System.Windows.Forms.Label();
            this.portInitDeinitButton = new System.Windows.Forms.Button();
            this.portStopbitsLabel = new System.Windows.Forms.Label();
            this.portParityLabel = new System.Windows.Forms.Label();
            this.portStopbitsValueLabel = new System.Windows.Forms.Label();
            this.portParityValueLabel = new System.Windows.Forms.Label();
            this.monitoringStatusLabel = new System.Windows.Forms.Label();
            this.startStopMonitoringButton = new System.Windows.Forms.Button();
            this.monitoringStatusValueLabel = new System.Windows.Forms.Label();
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
            this.mainContainer.Size = new System.Drawing.Size(727, 829);
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
            this.comPortDataGroupBox.Size = new System.Drawing.Size(500, 170);
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
            this.comPortDataTable.Controls.Add(this.baudRateLabel, 0, 1);
            this.comPortDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comPortDataTable.Location = new System.Drawing.Point(8, 28);
            this.comPortDataTable.Name = "comPortDataTable";
            this.comPortDataTable.RowCount = 5;
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.Size = new System.Drawing.Size(484, 134);
            this.comPortDataTable.TabIndex = 0;
            // 
            // parityDropDowm
            // 
            this.parityDropDowm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityDropDowm.FormattingEnabled = true;
            this.parityDropDowm.Location = new System.Drawing.Point(94, 103);
            this.parityDropDowm.Name = "parityDropDowm";
            this.parityDropDowm.Size = new System.Drawing.Size(228, 28);
            this.parityDropDowm.TabIndex = 11;
            // 
            // parityLabel
            // 
            this.parityLabel.AutoSize = true;
            this.parityLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.parityLabel.Location = new System.Drawing.Point(3, 100);
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
            this.stopBitsDropDown.Location = new System.Drawing.Point(94, 69);
            this.stopBitsDropDown.Name = "stopBitsDropDown";
            this.stopBitsDropDown.Size = new System.Drawing.Size(228, 28);
            this.stopBitsDropDown.TabIndex = 9;
            // 
            // stopBitsLabel
            // 
            this.stopBitsLabel.AutoSize = true;
            this.stopBitsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.stopBitsLabel.Location = new System.Drawing.Point(3, 66);
            this.stopBitsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.stopBitsLabel.Name = "stopBitsLabel";
            this.stopBitsLabel.Size = new System.Drawing.Size(68, 34);
            this.stopBitsLabel.TabIndex = 8;
            this.stopBitsLabel.Text = "Stop bits";
            this.stopBitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataBitsNumericField
            // 
            this.dataBitsNumericField.Location = new System.Drawing.Point(94, 36);
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
            this.dataBitsLabel.Location = new System.Drawing.Point(3, 33);
            this.dataBitsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.dataBitsLabel.Name = "dataBitsLabel";
            this.dataBitsLabel.Size = new System.Drawing.Size(69, 33);
            this.dataBitsLabel.TabIndex = 6;
            this.dataBitsLabel.Text = "Data bits";
            this.dataBitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // baudRateNumericField
            // 
            this.baudRateNumericField.Location = new System.Drawing.Point(94, 3);
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
            // baudRateLabel
            // 
            this.baudRateLabel.AutoSize = true;
            this.baudRateLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.baudRateLabel.Location = new System.Drawing.Point(3, 0);
            this.baudRateLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.baudRateLabel.Name = "baudRateLabel";
            this.baudRateLabel.Size = new System.Drawing.Size(73, 33);
            this.baudRateLabel.TabIndex = 1;
            this.baudRateLabel.Text = "Baud rate";
            this.baudRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portNameDropDown
            // 
            this.portNameDropDown.Location = new System.Drawing.Point(0, 0);
            this.portNameDropDown.Name = "portNameDropDown";
            this.portNameDropDown.Size = new System.Drawing.Size(121, 28);
            this.portNameDropDown.TabIndex = 0;
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.ColumnCount = 4;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
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
            this.table.Controls.Add(this.dialogTextBoxEncodingLabel, 0, 1);
            this.table.Controls.Add(this.bothTextBoxEncodingDropDown, 1, 1);
            this.table.Controls.Add(this.allTextClearButton, 2, 1);
            this.table.Controls.Add(this.portSettingsLabel, 0, 10);
            this.table.Controls.Add(this.portNameLabel, 0, 11);
            this.table.Controls.Add(this.portBaudrateLabel, 0, 12);
            this.table.Controls.Add(this.portDatabitsLabel, 0, 13);
            this.table.Controls.Add(this.portNameValueLabel, 1, 11);
            this.table.Controls.Add(this.portBaudrateValueLabel, 1, 12);
            this.table.Controls.Add(this.portDatabitsValueLabel, 1, 13);
            this.table.Controls.Add(this.statusLabel, 2, 10);
            this.table.Controls.Add(this.portStatusLabel, 2, 11);
            this.table.Controls.Add(this.portStatusValueLabel, 3, 11);
            this.table.Controls.Add(this.portInitDeinitButton, 2, 12);
            this.table.Controls.Add(this.portStopbitsLabel, 0, 15);
            this.table.Controls.Add(this.portParityLabel, 0, 16);
            this.table.Controls.Add(this.portStopbitsValueLabel, 1, 15);
            this.table.Controls.Add(this.portParityValueLabel, 1, 16);
            this.table.Controls.Add(this.monitoringStatusLabel, 2, 15);
            this.table.Controls.Add(this.startStopMonitoringButton, 2, 16);
            this.table.Controls.Add(this.monitoringStatusValueLabel, 3, 15);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(10, 10);
            this.table.Name = "table";
            this.table.RowCount = 18;
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
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(707, 809);
            this.table.TabIndex = 0;
            // 
            // simulateSendingEncodingDropDown
            // 
            this.simulateSendingEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateSendingEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulateSendingEncodingDropDown.FormattingEnabled = true;
            this.simulateSendingEncodingDropDown.Location = new System.Drawing.Point(109, 565);
            this.simulateSendingEncodingDropDown.Name = "simulateSendingEncodingDropDown";
            this.simulateSendingEncodingDropDown.Size = new System.Drawing.Size(241, 28);
            this.simulateSendingEncodingDropDown.TabIndex = 12;
            // 
            // sendDataButton
            // 
            this.table.SetColumnSpan(this.sendDataButton, 2);
            this.sendDataButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.sendDataButton.Location = new System.Drawing.Point(3, 599);
            this.sendDataButton.Name = "sendDataButton";
            this.sendDataButton.Size = new System.Drawing.Size(347, 29);
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
            this.simulateSendingLabel.Size = new System.Drawing.Size(347, 20);
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
            this.bothTextBox.Size = new System.Drawing.Size(701, 200);
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
            this.sentLabel.Size = new System.Drawing.Size(347, 20);
            this.sentLabel.TabIndex = 1;
            this.sentLabel.Text = "Sent";
            // 
            // receivedLabel
            // 
            this.receivedLabel.AutoSize = true;
            this.table.SetColumnSpan(this.receivedLabel, 2);
            this.receivedLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.receivedLabel.Location = new System.Drawing.Point(356, 251);
            this.receivedLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.receivedLabel.Name = "receivedLabel";
            this.receivedLabel.Size = new System.Drawing.Size(348, 20);
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
            this.sentTextBox.Size = new System.Drawing.Size(347, 150);
            this.sentTextBox.TabIndex = 3;
            // 
            // receivedTextBox
            // 
            this.receivedTextBox.BackColor = System.Drawing.Color.White;
            this.table.SetColumnSpan(this.receivedTextBox, 2);
            this.receivedTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.receivedTextBox.Location = new System.Drawing.Point(356, 274);
            this.receivedTextBox.MinimumSize = new System.Drawing.Size(4, 150);
            this.receivedTextBox.Multiline = true;
            this.receivedTextBox.Name = "receivedTextBox";
            this.receivedTextBox.ReadOnly = true;
            this.receivedTextBox.Size = new System.Drawing.Size(348, 150);
            this.receivedTextBox.TabIndex = 4;
            // 
            // simulateSendingDataTextBox
            // 
            this.simulateSendingDataTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateSendingDataTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.simulateSendingDataTextBox.Location = new System.Drawing.Point(109, 534);
            this.simulateSendingDataTextBox.Name = "simulateSendingDataTextBox";
            this.simulateSendingDataTextBox.Size = new System.Drawing.Size(241, 25);
            this.simulateSendingDataTextBox.TabIndex = 13;
            // 
            // simulateReceivingLabel
            // 
            this.simulateReceivingLabel.AutoSize = true;
            this.table.SetColumnSpan(this.simulateReceivingLabel, 2);
            this.simulateReceivingLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateReceivingLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.simulateReceivingLabel.Location = new System.Drawing.Point(356, 506);
            this.simulateReceivingLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.simulateReceivingLabel.Name = "simulateReceivingLabel";
            this.simulateReceivingLabel.Size = new System.Drawing.Size(348, 20);
            this.simulateReceivingLabel.TabIndex = 14;
            this.simulateReceivingLabel.Text = "Simulate receiving";
            // 
            // simulateReceivingDataLabel
            // 
            this.simulateReceivingDataLabel.AutoSize = true;
            this.simulateReceivingDataLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.simulateReceivingDataLabel.Location = new System.Drawing.Point(356, 531);
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
            this.simulateReceivingEncodingLabel.Location = new System.Drawing.Point(356, 562);
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
            this.receiveDataButton.Location = new System.Drawing.Point(356, 599);
            this.receiveDataButton.Name = "receiveDataButton";
            this.receiveDataButton.Size = new System.Drawing.Size(348, 29);
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
            this.sentTextClearButton.Size = new System.Drawing.Size(347, 29);
            this.sentTextClearButton.TabIndex = 18;
            this.sentTextClearButton.Text = "Clear";
            this.sentTextClearButton.UseVisualStyleBackColor = true;
            this.sentTextClearButton.Click += new System.EventHandler(this.sentTextClearButton_Click);
            // 
            // receivedTextClearButton
            // 
            this.table.SetColumnSpan(this.receivedTextClearButton, 2);
            this.receivedTextClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextClearButton.Location = new System.Drawing.Point(356, 464);
            this.receivedTextClearButton.Name = "receivedTextClearButton";
            this.receivedTextClearButton.Size = new System.Drawing.Size(348, 29);
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
            this.sentTextBoxEncodingDropDown.Location = new System.Drawing.Point(109, 430);
            this.sentTextBoxEncodingDropDown.Name = "sentTextBoxEncodingDropDown";
            this.sentTextBoxEncodingDropDown.Size = new System.Drawing.Size(241, 28);
            this.sentTextBoxEncodingDropDown.TabIndex = 21;
            this.sentTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.sentTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // receivedTextBoxEncodingDropDown
            // 
            this.receivedTextBoxEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextBoxEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.receivedTextBoxEncodingDropDown.FormattingEnabled = true;
            this.receivedTextBoxEncodingDropDown.Location = new System.Drawing.Point(462, 430);
            this.receivedTextBoxEncodingDropDown.Name = "receivedTextBoxEncodingDropDown";
            this.receivedTextBoxEncodingDropDown.Size = new System.Drawing.Size(242, 28);
            this.receivedTextBoxEncodingDropDown.TabIndex = 22;
            this.receivedTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.receivedTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // receivedTextBoxEncodingLabel
            // 
            this.receivedTextBoxEncodingLabel.AutoSize = true;
            this.receivedTextBoxEncodingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.receivedTextBoxEncodingLabel.Location = new System.Drawing.Point(356, 427);
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
            this.simulateReceivingEncodingDropDown.Location = new System.Drawing.Point(462, 565);
            this.simulateReceivingEncodingDropDown.Name = "simulateReceivingEncodingDropDown";
            this.simulateReceivingEncodingDropDown.Size = new System.Drawing.Size(242, 28);
            this.simulateReceivingEncodingDropDown.TabIndex = 24;
            // 
            // simulateReceivingDataTextBox
            // 
            this.simulateReceivingDataTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateReceivingDataTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.simulateReceivingDataTextBox.Location = new System.Drawing.Point(462, 534);
            this.simulateReceivingDataTextBox.Name = "simulateReceivingDataTextBox";
            this.simulateReceivingDataTextBox.Size = new System.Drawing.Size(242, 25);
            this.simulateReceivingDataTextBox.TabIndex = 25;
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
            this.bothTextBoxEncodingDropDown.Location = new System.Drawing.Point(109, 209);
            this.bothTextBoxEncodingDropDown.Name = "bothTextBoxEncodingDropDown";
            this.bothTextBoxEncodingDropDown.Size = new System.Drawing.Size(241, 28);
            this.bothTextBoxEncodingDropDown.TabIndex = 28;
            this.bothTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.bothTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // allTextClearButton
            // 
            this.table.SetColumnSpan(this.allTextClearButton, 2);
            this.allTextClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.allTextClearButton.Location = new System.Drawing.Point(356, 209);
            this.allTextClearButton.Name = "allTextClearButton";
            this.allTextClearButton.Size = new System.Drawing.Size(348, 29);
            this.allTextClearButton.TabIndex = 29;
            this.allTextClearButton.Text = "Clear all";
            this.allTextClearButton.UseVisualStyleBackColor = true;
            this.allTextClearButton.Click += new System.EventHandler(this.allTextClearButton_Click);
            // 
            // portSettingsLabel
            // 
            this.portSettingsLabel.AutoSize = true;
            this.table.SetColumnSpan(this.portSettingsLabel, 2);
            this.portSettingsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.portSettingsLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.portSettingsLabel.Location = new System.Drawing.Point(3, 641);
            this.portSettingsLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.portSettingsLabel.Name = "portSettingsLabel";
            this.portSettingsLabel.Size = new System.Drawing.Size(347, 20);
            this.portSettingsLabel.TabIndex = 30;
            this.portSettingsLabel.Text = "Port settings";
            // 
            // portNameLabel
            // 
            this.portNameLabel.AutoSize = true;
            this.portNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portNameLabel.Location = new System.Drawing.Point(3, 661);
            this.portNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portNameLabel.Name = "portNameLabel";
            this.portNameLabel.Size = new System.Drawing.Size(52, 20);
            this.portNameLabel.TabIndex = 31;
            this.portNameLabel.Text = "Name:";
            this.portNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portBaudrateLabel
            // 
            this.portBaudrateLabel.AutoSize = true;
            this.portBaudrateLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portBaudrateLabel.Location = new System.Drawing.Point(3, 681);
            this.portBaudrateLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portBaudrateLabel.Name = "portBaudrateLabel";
            this.portBaudrateLabel.Size = new System.Drawing.Size(72, 20);
            this.portBaudrateLabel.TabIndex = 32;
            this.portBaudrateLabel.Text = "Baudrate:";
            this.portBaudrateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portDatabitsLabel
            // 
            this.portDatabitsLabel.AutoSize = true;
            this.portDatabitsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portDatabitsLabel.Location = new System.Drawing.Point(3, 701);
            this.portDatabitsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portDatabitsLabel.Name = "portDatabitsLabel";
            this.portDatabitsLabel.Size = new System.Drawing.Size(72, 20);
            this.portDatabitsLabel.TabIndex = 33;
            this.portDatabitsLabel.Text = "Data bits:";
            this.portDatabitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portNameValueLabel
            // 
            this.portNameValueLabel.AutoSize = true;
            this.portNameValueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portNameValueLabel.Location = new System.Drawing.Point(109, 661);
            this.portNameValueLabel.Name = "portNameValueLabel";
            this.portNameValueLabel.Size = new System.Drawing.Size(50, 20);
            this.portNameValueLabel.TabIndex = 35;
            this.portNameValueLabel.Text = "COM0";
            this.portNameValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portBaudrateValueLabel
            // 
            this.portBaudrateValueLabel.AutoSize = true;
            this.portBaudrateValueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portBaudrateValueLabel.Location = new System.Drawing.Point(109, 681);
            this.portBaudrateValueLabel.Name = "portBaudrateValueLabel";
            this.portBaudrateValueLabel.Size = new System.Drawing.Size(41, 20);
            this.portBaudrateValueLabel.TabIndex = 36;
            this.portBaudrateValueLabel.Text = "9600";
            this.portBaudrateValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portDatabitsValueLabel
            // 
            this.portDatabitsValueLabel.AutoSize = true;
            this.portDatabitsValueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portDatabitsValueLabel.Location = new System.Drawing.Point(109, 701);
            this.portDatabitsValueLabel.Name = "portDatabitsValueLabel";
            this.portDatabitsValueLabel.Size = new System.Drawing.Size(17, 20);
            this.portDatabitsValueLabel.TabIndex = 37;
            this.portDatabitsValueLabel.Text = "8";
            this.portDatabitsValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.table.SetColumnSpan(this.statusLabel, 2);
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusLabel.Location = new System.Drawing.Point(356, 641);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(348, 20);
            this.statusLabel.TabIndex = 39;
            this.statusLabel.Text = "Status";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portStatusLabel
            // 
            this.portStatusLabel.AutoSize = true;
            this.portStatusLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portStatusLabel.Location = new System.Drawing.Point(356, 661);
            this.portStatusLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portStatusLabel.Name = "portStatusLabel";
            this.portStatusLabel.Size = new System.Drawing.Size(38, 20);
            this.portStatusLabel.TabIndex = 40;
            this.portStatusLabel.Text = "Port:";
            this.portStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portStatusValueLabel
            // 
            this.portStatusValueLabel.AutoSize = true;
            this.portStatusValueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portStatusValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.portStatusValueLabel.Location = new System.Drawing.Point(462, 661);
            this.portStatusValueLabel.Name = "portStatusValueLabel";
            this.portStatusValueLabel.Size = new System.Drawing.Size(91, 20);
            this.portStatusValueLabel.TabIndex = 41;
            this.portStatusValueLabel.Text = "deinitialized";
            this.portStatusValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portInitDeinitButton
            // 
            this.table.SetColumnSpan(this.portInitDeinitButton, 2);
            this.portInitDeinitButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.portInitDeinitButton.Location = new System.Drawing.Point(356, 684);
            this.portInitDeinitButton.Name = "portInitDeinitButton";
            this.table.SetRowSpan(this.portInitDeinitButton, 2);
            this.portInitDeinitButton.Size = new System.Drawing.Size(348, 29);
            this.portInitDeinitButton.TabIndex = 42;
            this.portInitDeinitButton.Text = "Initialize port";
            this.portInitDeinitButton.UseVisualStyleBackColor = true;
            this.portInitDeinitButton.Click += new System.EventHandler(this.initDeinitPortButton_Click);
            // 
            // portStopbitsLabel
            // 
            this.portStopbitsLabel.AutoSize = true;
            this.portStopbitsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portStopbitsLabel.Location = new System.Drawing.Point(3, 721);
            this.portStopbitsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portStopbitsLabel.Name = "portStopbitsLabel";
            this.portStopbitsLabel.Size = new System.Drawing.Size(71, 20);
            this.portStopbitsLabel.TabIndex = 43;
            this.portStopbitsLabel.Text = "Stop bits:";
            this.portStopbitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portParityLabel
            // 
            this.portParityLabel.AutoSize = true;
            this.portParityLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portParityLabel.Location = new System.Drawing.Point(3, 741);
            this.portParityLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portParityLabel.Name = "portParityLabel";
            this.portParityLabel.Size = new System.Drawing.Size(48, 20);
            this.portParityLabel.TabIndex = 44;
            this.portParityLabel.Text = "Parity:";
            this.portParityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portStopbitsValueLabel
            // 
            this.portStopbitsValueLabel.AutoSize = true;
            this.portStopbitsValueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portStopbitsValueLabel.Location = new System.Drawing.Point(109, 721);
            this.portStopbitsValueLabel.Name = "portStopbitsValueLabel";
            this.portStopbitsValueLabel.Size = new System.Drawing.Size(17, 20);
            this.portStopbitsValueLabel.TabIndex = 45;
            this.portStopbitsValueLabel.Text = "1";
            this.portStopbitsValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portParityValueLabel
            // 
            this.portParityValueLabel.AutoSize = true;
            this.portParityValueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portParityValueLabel.Location = new System.Drawing.Point(109, 741);
            this.portParityValueLabel.Name = "portParityValueLabel";
            this.portParityValueLabel.Size = new System.Drawing.Size(28, 20);
            this.portParityValueLabel.TabIndex = 46;
            this.portParityValueLabel.Text = "off";
            this.portParityValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // monitoringStatusLabel
            // 
            this.monitoringStatusLabel.AutoSize = true;
            this.monitoringStatusLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.monitoringStatusLabel.Location = new System.Drawing.Point(356, 721);
            this.monitoringStatusLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.monitoringStatusLabel.Name = "monitoringStatusLabel";
            this.monitoringStatusLabel.Size = new System.Drawing.Size(86, 20);
            this.monitoringStatusLabel.TabIndex = 47;
            this.monitoringStatusLabel.Text = "Monitoring:";
            this.monitoringStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startStopMonitoringButton
            // 
            this.table.SetColumnSpan(this.startStopMonitoringButton, 2);
            this.startStopMonitoringButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.startStopMonitoringButton.Location = new System.Drawing.Point(356, 744);
            this.startStopMonitoringButton.Name = "startStopMonitoringButton";
            this.table.SetRowSpan(this.startStopMonitoringButton, 2);
            this.startStopMonitoringButton.Size = new System.Drawing.Size(348, 29);
            this.startStopMonitoringButton.TabIndex = 48;
            this.startStopMonitoringButton.Text = "Start monitoring";
            this.startStopMonitoringButton.UseVisualStyleBackColor = true;
            this.startStopMonitoringButton.Click += new System.EventHandler(this.startStopMonitoringButton_Click);
            // 
            // monitoringStatusValueLabel
            // 
            this.monitoringStatusValueLabel.AutoSize = true;
            this.monitoringStatusValueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.monitoringStatusValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.monitoringStatusValueLabel.Location = new System.Drawing.Point(462, 721);
            this.monitoringStatusValueLabel.Name = "monitoringStatusValueLabel";
            this.monitoringStatusValueLabel.Size = new System.Drawing.Size(28, 20);
            this.monitoringStatusValueLabel.TabIndex = 49;
            this.monitoringStatusValueLabel.Text = "off";
            this.monitoringStatusValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SerialPortMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 899);
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
        private System.Windows.Forms.Label dialogTextBoxEncodingLabel;
        private System.Windows.Forms.ComboBox bothTextBoxEncodingDropDown;
        private System.Windows.Forms.Button allTextClearButton;
        private System.Windows.Forms.Label portSettingsLabel;
        private System.Windows.Forms.Label portNameLabel;
        private System.Windows.Forms.Label portBaudrateLabel;
        private System.Windows.Forms.Label portDatabitsLabel;
        private System.Windows.Forms.Label portNameValueLabel;
        private System.Windows.Forms.Label portBaudrateValueLabel;
        private System.Windows.Forms.Label portDatabitsValueLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label portStatusLabel;
        private System.Windows.Forms.Label portStatusValueLabel;
        private System.Windows.Forms.Button portInitDeinitButton;
        private System.Windows.Forms.Label portStopbitsLabel;
        private System.Windows.Forms.Label portParityLabel;
        private System.Windows.Forms.Label portStopbitsValueLabel;
        private System.Windows.Forms.Label portParityValueLabel;
        private System.Windows.Forms.Label monitoringStatusLabel;
        private System.Windows.Forms.Button startStopMonitoringButton;
        private System.Windows.Forms.Label monitoringStatusValueLabel;
    }
}