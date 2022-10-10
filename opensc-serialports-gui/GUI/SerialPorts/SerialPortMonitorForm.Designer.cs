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
            this.receiveDataButton = new System.Windows.Forms.Button();
            this.sentTextClearButton = new System.Windows.Forms.Button();
            this.receivedTextClearButton = new System.Windows.Forms.Button();
            this.sentTextBoxEncodingLabel = new System.Windows.Forms.Label();
            this.sentTextBoxEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.receivedTextBoxEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.simulateReceivingEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.simulateReceivingDataTextBox = new System.Windows.Forms.TextBox();
            this.bothTextBoxEncodingDropDown = new System.Windows.Forms.ComboBox();
            this.bothTextClearButton = new System.Windows.Forms.Button();
            this.portSettingsLabel = new System.Windows.Forms.Label();
            this.portNameLabel = new System.Windows.Forms.Label();
            this.portBaudrateLabel = new System.Windows.Forms.Label();
            this.portDatabitsLabel = new System.Windows.Forms.Label();
            this.portNameValueLabel = new System.Windows.Forms.Label();
            this.portBaudrateValueLabel = new System.Windows.Forms.Label();
            this.portDatabitsValueLabel = new System.Windows.Forms.Label();
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
            this.sentTextClearOnPortInitCheckBox = new System.Windows.Forms.CheckBox();
            this.sentTextClearOnMonitoringStartCheckBox = new System.Windows.Forms.CheckBox();
            this.receivedTextClearOnPortInitCheckBox = new System.Windows.Forms.CheckBox();
            this.receivedTextClearOnMonitoringStartCheckBox = new System.Windows.Forms.CheckBox();
            this.sentTextAutoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.receivedTextAutoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.sentTextScrollLabel = new System.Windows.Forms.Label();
            this.sentTextClearLabel = new System.Windows.Forms.Label();
            this.bothLabel = new System.Windows.Forms.Label();
            this.bothTextClearOnPortInitCheckBox = new System.Windows.Forms.CheckBox();
            this.bothTextClearOnMonitoringStartCheckBox = new System.Windows.Forms.CheckBox();
            this.bothTextAutoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.mainContainer.SuspendLayout();
            this.table.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.table);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(990, 683);
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.ColumnCount = 4;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.Controls.Add(this.simulateSendingEncodingDropDown, 1, 9);
            this.table.Controls.Add(this.sendDataButton, 1, 10);
            this.table.Controls.Add(this.simulateSendingEncodingLabel, 0, 9);
            this.table.Controls.Add(this.simulateSendingDataLabel, 0, 8);
            this.table.Controls.Add(this.simulateSendingLabel, 0, 7);
            this.table.Controls.Add(this.bothTextBox, 3, 1);
            this.table.Controls.Add(this.sentLabel, 1, 0);
            this.table.Controls.Add(this.receivedLabel, 2, 0);
            this.table.Controls.Add(this.sentTextBox, 1, 1);
            this.table.Controls.Add(this.receivedTextBox, 2, 1);
            this.table.Controls.Add(this.simulateSendingDataTextBox, 1, 8);
            this.table.Controls.Add(this.receiveDataButton, 2, 10);
            this.table.Controls.Add(this.sentTextClearButton, 1, 3);
            this.table.Controls.Add(this.receivedTextClearButton, 2, 3);
            this.table.Controls.Add(this.sentTextBoxEncodingLabel, 0, 2);
            this.table.Controls.Add(this.sentTextBoxEncodingDropDown, 1, 2);
            this.table.Controls.Add(this.receivedTextBoxEncodingDropDown, 2, 2);
            this.table.Controls.Add(this.simulateReceivingEncodingDropDown, 2, 9);
            this.table.Controls.Add(this.simulateReceivingDataTextBox, 2, 8);
            this.table.Controls.Add(this.bothTextBoxEncodingDropDown, 3, 2);
            this.table.Controls.Add(this.bothTextClearButton, 3, 3);
            this.table.Controls.Add(this.portSettingsLabel, 0, 11);
            this.table.Controls.Add(this.portNameLabel, 0, 12);
            this.table.Controls.Add(this.portBaudrateLabel, 0, 13);
            this.table.Controls.Add(this.portDatabitsLabel, 0, 14);
            this.table.Controls.Add(this.portNameValueLabel, 1, 12);
            this.table.Controls.Add(this.portBaudrateValueLabel, 1, 13);
            this.table.Controls.Add(this.portDatabitsValueLabel, 1, 14);
            this.table.Controls.Add(this.portStatusLabel, 2, 11);
            this.table.Controls.Add(this.portStatusValueLabel, 2, 12);
            this.table.Controls.Add(this.portInitDeinitButton, 2, 13);
            this.table.Controls.Add(this.portStopbitsLabel, 0, 16);
            this.table.Controls.Add(this.portParityLabel, 0, 17);
            this.table.Controls.Add(this.portStopbitsValueLabel, 1, 16);
            this.table.Controls.Add(this.portParityValueLabel, 1, 17);
            this.table.Controls.Add(this.monitoringStatusLabel, 3, 11);
            this.table.Controls.Add(this.startStopMonitoringButton, 3, 13);
            this.table.Controls.Add(this.monitoringStatusValueLabel, 3, 12);
            this.table.Controls.Add(this.sentTextClearOnPortInitCheckBox, 1, 4);
            this.table.Controls.Add(this.sentTextClearOnMonitoringStartCheckBox, 1, 5);
            this.table.Controls.Add(this.receivedTextClearOnPortInitCheckBox, 2, 4);
            this.table.Controls.Add(this.receivedTextClearOnMonitoringStartCheckBox, 2, 5);
            this.table.Controls.Add(this.sentTextAutoScrollCheckBox, 1, 6);
            this.table.Controls.Add(this.receivedTextAutoScrollCheckBox, 2, 6);
            this.table.Controls.Add(this.sentTextScrollLabel, 0, 6);
            this.table.Controls.Add(this.sentTextClearLabel, 0, 3);
            this.table.Controls.Add(this.bothLabel, 3, 0);
            this.table.Controls.Add(this.bothTextClearOnPortInitCheckBox, 3, 4);
            this.table.Controls.Add(this.bothTextClearOnMonitoringStartCheckBox, 3, 5);
            this.table.Controls.Add(this.bothTextAutoScrollCheckBox, 3, 6);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(10, 10);
            this.table.Name = "table";
            this.table.RowCount = 19;
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
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(970, 663);
            this.table.TabIndex = 0;
            // 
            // simulateSendingEncodingDropDown
            // 
            this.simulateSendingEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateSendingEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulateSendingEncodingDropDown.FormattingEnabled = true;
            this.simulateSendingEncodingDropDown.Location = new System.Drawing.Point(108, 454);
            this.simulateSendingEncodingDropDown.Name = "simulateSendingEncodingDropDown";
            this.simulateSendingEncodingDropDown.Size = new System.Drawing.Size(282, 28);
            this.simulateSendingEncodingDropDown.TabIndex = 12;
            // 
            // sendDataButton
            // 
            this.sendDataButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.sendDataButton.Location = new System.Drawing.Point(108, 488);
            this.sendDataButton.Name = "sendDataButton";
            this.sendDataButton.Size = new System.Drawing.Size(282, 29);
            this.sendDataButton.TabIndex = 11;
            this.sendDataButton.Text = "Send data";
            this.sendDataButton.UseVisualStyleBackColor = true;
            this.sendDataButton.Click += new System.EventHandler(this.sendDataButton_Click);
            // 
            // simulateSendingEncodingLabel
            // 
            this.simulateSendingEncodingLabel.AutoSize = true;
            this.simulateSendingEncodingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.simulateSendingEncodingLabel.Location = new System.Drawing.Point(3, 451);
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
            this.simulateSendingDataLabel.Location = new System.Drawing.Point(3, 420);
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
            this.table.SetColumnSpan(this.simulateSendingLabel, 4);
            this.simulateSendingLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateSendingLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.simulateSendingLabel.Location = new System.Drawing.Point(3, 395);
            this.simulateSendingLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 5);
            this.simulateSendingLabel.Name = "simulateSendingLabel";
            this.simulateSendingLabel.Size = new System.Drawing.Size(964, 20);
            this.simulateSendingLabel.TabIndex = 8;
            this.simulateSendingLabel.Text = "Simulate sending/receiving";
            // 
            // bothTextBox
            // 
            this.bothTextBox.BackColor = System.Drawing.Color.White;
            this.bothTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.bothTextBox.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bothTextBox.Location = new System.Drawing.Point(684, 23);
            this.bothTextBox.MinimumSize = new System.Drawing.Size(4, 200);
            this.bothTextBox.Multiline = true;
            this.bothTextBox.Name = "bothTextBox";
            this.bothTextBox.ReadOnly = true;
            this.bothTextBox.Size = new System.Drawing.Size(283, 200);
            this.bothTextBox.TabIndex = 0;
            // 
            // sentLabel
            // 
            this.sentLabel.AutoSize = true;
            this.sentLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.sentLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sentLabel.Location = new System.Drawing.Point(108, 0);
            this.sentLabel.Name = "sentLabel";
            this.sentLabel.Size = new System.Drawing.Size(282, 20);
            this.sentLabel.TabIndex = 1;
            this.sentLabel.Text = "Sent";
            // 
            // receivedLabel
            // 
            this.receivedLabel.AutoSize = true;
            this.receivedLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.receivedLabel.Location = new System.Drawing.Point(396, 0);
            this.receivedLabel.Name = "receivedLabel";
            this.receivedLabel.Size = new System.Drawing.Size(282, 20);
            this.receivedLabel.TabIndex = 2;
            this.receivedLabel.Text = "Received";
            // 
            // sentTextBox
            // 
            this.sentTextBox.BackColor = System.Drawing.Color.White;
            this.sentTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.sentTextBox.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sentTextBox.Location = new System.Drawing.Point(108, 23);
            this.sentTextBox.MinimumSize = new System.Drawing.Size(4, 200);
            this.sentTextBox.Multiline = true;
            this.sentTextBox.Name = "sentTextBox";
            this.sentTextBox.ReadOnly = true;
            this.sentTextBox.Size = new System.Drawing.Size(282, 200);
            this.sentTextBox.TabIndex = 3;
            // 
            // receivedTextBox
            // 
            this.receivedTextBox.BackColor = System.Drawing.Color.White;
            this.receivedTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextBox.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.receivedTextBox.Location = new System.Drawing.Point(396, 23);
            this.receivedTextBox.MinimumSize = new System.Drawing.Size(4, 200);
            this.receivedTextBox.Multiline = true;
            this.receivedTextBox.Name = "receivedTextBox";
            this.receivedTextBox.ReadOnly = true;
            this.receivedTextBox.Size = new System.Drawing.Size(282, 200);
            this.receivedTextBox.TabIndex = 4;
            // 
            // simulateSendingDataTextBox
            // 
            this.simulateSendingDataTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateSendingDataTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.simulateSendingDataTextBox.Location = new System.Drawing.Point(108, 423);
            this.simulateSendingDataTextBox.Name = "simulateSendingDataTextBox";
            this.simulateSendingDataTextBox.Size = new System.Drawing.Size(282, 25);
            this.simulateSendingDataTextBox.TabIndex = 13;
            // 
            // receiveDataButton
            // 
            this.receiveDataButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.receiveDataButton.Location = new System.Drawing.Point(396, 488);
            this.receiveDataButton.Name = "receiveDataButton";
            this.receiveDataButton.Size = new System.Drawing.Size(282, 29);
            this.receiveDataButton.TabIndex = 17;
            this.receiveDataButton.Text = "Receive data";
            this.receiveDataButton.UseVisualStyleBackColor = true;
            this.receiveDataButton.Click += new System.EventHandler(this.receiveDataButton_Click);
            // 
            // sentTextClearButton
            // 
            this.sentTextClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.sentTextClearButton.Location = new System.Drawing.Point(108, 263);
            this.sentTextClearButton.Name = "sentTextClearButton";
            this.sentTextClearButton.Size = new System.Drawing.Size(282, 29);
            this.sentTextClearButton.TabIndex = 18;
            this.sentTextClearButton.Text = "Now";
            this.sentTextClearButton.UseVisualStyleBackColor = true;
            this.sentTextClearButton.Click += new System.EventHandler(this.sentTextClearButton_Click);
            // 
            // receivedTextClearButton
            // 
            this.receivedTextClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextClearButton.Location = new System.Drawing.Point(396, 263);
            this.receivedTextClearButton.Name = "receivedTextClearButton";
            this.receivedTextClearButton.Size = new System.Drawing.Size(282, 29);
            this.receivedTextClearButton.TabIndex = 19;
            this.receivedTextClearButton.Text = "Now";
            this.receivedTextClearButton.UseVisualStyleBackColor = true;
            this.receivedTextClearButton.Click += new System.EventHandler(this.receivedTextClearButton_Click);
            // 
            // sentTextBoxEncodingLabel
            // 
            this.sentTextBoxEncodingLabel.AutoSize = true;
            this.sentTextBoxEncodingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sentTextBoxEncodingLabel.Location = new System.Drawing.Point(3, 226);
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
            this.sentTextBoxEncodingDropDown.Location = new System.Drawing.Point(108, 229);
            this.sentTextBoxEncodingDropDown.Name = "sentTextBoxEncodingDropDown";
            this.sentTextBoxEncodingDropDown.Size = new System.Drawing.Size(282, 28);
            this.sentTextBoxEncodingDropDown.TabIndex = 21;
            this.sentTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.sentTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // receivedTextBoxEncodingDropDown
            // 
            this.receivedTextBoxEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedTextBoxEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.receivedTextBoxEncodingDropDown.FormattingEnabled = true;
            this.receivedTextBoxEncodingDropDown.Location = new System.Drawing.Point(396, 229);
            this.receivedTextBoxEncodingDropDown.Name = "receivedTextBoxEncodingDropDown";
            this.receivedTextBoxEncodingDropDown.Size = new System.Drawing.Size(282, 28);
            this.receivedTextBoxEncodingDropDown.TabIndex = 22;
            this.receivedTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.receivedTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // simulateReceivingEncodingDropDown
            // 
            this.simulateReceivingEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateReceivingEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulateReceivingEncodingDropDown.FormattingEnabled = true;
            this.simulateReceivingEncodingDropDown.Location = new System.Drawing.Point(396, 454);
            this.simulateReceivingEncodingDropDown.Name = "simulateReceivingEncodingDropDown";
            this.simulateReceivingEncodingDropDown.Size = new System.Drawing.Size(282, 28);
            this.simulateReceivingEncodingDropDown.TabIndex = 24;
            // 
            // simulateReceivingDataTextBox
            // 
            this.simulateReceivingDataTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.simulateReceivingDataTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.simulateReceivingDataTextBox.Location = new System.Drawing.Point(396, 423);
            this.simulateReceivingDataTextBox.Name = "simulateReceivingDataTextBox";
            this.simulateReceivingDataTextBox.Size = new System.Drawing.Size(282, 25);
            this.simulateReceivingDataTextBox.TabIndex = 25;
            // 
            // bothTextBoxEncodingDropDown
            // 
            this.bothTextBoxEncodingDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.bothTextBoxEncodingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bothTextBoxEncodingDropDown.FormattingEnabled = true;
            this.bothTextBoxEncodingDropDown.Location = new System.Drawing.Point(684, 229);
            this.bothTextBoxEncodingDropDown.Name = "bothTextBoxEncodingDropDown";
            this.bothTextBoxEncodingDropDown.Size = new System.Drawing.Size(283, 28);
            this.bothTextBoxEncodingDropDown.TabIndex = 28;
            this.bothTextBoxEncodingDropDown.SelectedIndexChanged += new System.EventHandler(this.bothTextBoxEncodingDropDown_SelectedIndexChanged);
            // 
            // bothTextClearButton
            // 
            this.bothTextClearButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.bothTextClearButton.Location = new System.Drawing.Point(684, 263);
            this.bothTextClearButton.Name = "bothTextClearButton";
            this.bothTextClearButton.Size = new System.Drawing.Size(283, 29);
            this.bothTextClearButton.TabIndex = 29;
            this.bothTextClearButton.Text = "Now";
            this.bothTextClearButton.UseVisualStyleBackColor = true;
            this.bothTextClearButton.Click += new System.EventHandler(this.bothTextClearButton_Click);
            // 
            // portSettingsLabel
            // 
            this.portSettingsLabel.AutoSize = true;
            this.portSettingsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.portSettingsLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.portSettingsLabel.Location = new System.Drawing.Point(3, 530);
            this.portSettingsLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.portSettingsLabel.Name = "portSettingsLabel";
            this.portSettingsLabel.Size = new System.Drawing.Size(99, 20);
            this.portSettingsLabel.TabIndex = 30;
            this.portSettingsLabel.Text = "Port settings";
            // 
            // portNameLabel
            // 
            this.portNameLabel.AutoSize = true;
            this.portNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portNameLabel.Location = new System.Drawing.Point(3, 550);
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
            this.portBaudrateLabel.Location = new System.Drawing.Point(3, 570);
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
            this.portDatabitsLabel.Location = new System.Drawing.Point(3, 590);
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
            this.portNameValueLabel.Location = new System.Drawing.Point(108, 550);
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
            this.portBaudrateValueLabel.Location = new System.Drawing.Point(108, 570);
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
            this.portDatabitsValueLabel.Location = new System.Drawing.Point(108, 590);
            this.portDatabitsValueLabel.Name = "portDatabitsValueLabel";
            this.portDatabitsValueLabel.Size = new System.Drawing.Size(17, 20);
            this.portDatabitsValueLabel.TabIndex = 37;
            this.portDatabitsValueLabel.Text = "8";
            this.portDatabitsValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portStatusLabel
            // 
            this.portStatusLabel.AutoSize = true;
            this.portStatusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.portStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.portStatusLabel.Location = new System.Drawing.Point(396, 530);
            this.portStatusLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.portStatusLabel.Name = "portStatusLabel";
            this.portStatusLabel.Size = new System.Drawing.Size(282, 20);
            this.portStatusLabel.TabIndex = 39;
            this.portStatusLabel.Text = "Port status";
            this.portStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portStatusValueLabel
            // 
            this.portStatusValueLabel.AutoSize = true;
            this.portStatusValueLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portStatusValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.portStatusValueLabel.Location = new System.Drawing.Point(396, 550);
            this.portStatusValueLabel.Name = "portStatusValueLabel";
            this.portStatusValueLabel.Size = new System.Drawing.Size(91, 20);
            this.portStatusValueLabel.TabIndex = 41;
            this.portStatusValueLabel.Text = "deinitialized";
            this.portStatusValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portInitDeinitButton
            // 
            this.portInitDeinitButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.portInitDeinitButton.Location = new System.Drawing.Point(396, 573);
            this.portInitDeinitButton.Name = "portInitDeinitButton";
            this.table.SetRowSpan(this.portInitDeinitButton, 2);
            this.portInitDeinitButton.Size = new System.Drawing.Size(282, 29);
            this.portInitDeinitButton.TabIndex = 42;
            this.portInitDeinitButton.Text = "Initialize port";
            this.portInitDeinitButton.UseVisualStyleBackColor = true;
            this.portInitDeinitButton.Click += new System.EventHandler(this.initDeinitPortButton_Click);
            // 
            // portStopbitsLabel
            // 
            this.portStopbitsLabel.AutoSize = true;
            this.portStopbitsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portStopbitsLabel.Location = new System.Drawing.Point(3, 610);
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
            this.portParityLabel.Location = new System.Drawing.Point(3, 630);
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
            this.portStopbitsValueLabel.Location = new System.Drawing.Point(108, 610);
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
            this.portParityValueLabel.Location = new System.Drawing.Point(108, 630);
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
            this.monitoringStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.monitoringStatusLabel.Location = new System.Drawing.Point(684, 530);
            this.monitoringStatusLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.monitoringStatusLabel.Name = "monitoringStatusLabel";
            this.monitoringStatusLabel.Size = new System.Drawing.Size(135, 20);
            this.monitoringStatusLabel.TabIndex = 47;
            this.monitoringStatusLabel.Text = "Monitoring status";
            this.monitoringStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startStopMonitoringButton
            // 
            this.startStopMonitoringButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.startStopMonitoringButton.Location = new System.Drawing.Point(684, 573);
            this.startStopMonitoringButton.Name = "startStopMonitoringButton";
            this.table.SetRowSpan(this.startStopMonitoringButton, 2);
            this.startStopMonitoringButton.Size = new System.Drawing.Size(283, 29);
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
            this.monitoringStatusValueLabel.Location = new System.Drawing.Point(684, 550);
            this.monitoringStatusValueLabel.Name = "monitoringStatusValueLabel";
            this.monitoringStatusValueLabel.Size = new System.Drawing.Size(28, 20);
            this.monitoringStatusValueLabel.TabIndex = 49;
            this.monitoringStatusValueLabel.Text = "off";
            this.monitoringStatusValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sentTextClearOnPortInitCheckBox
            // 
            this.sentTextClearOnPortInitCheckBox.AutoSize = true;
            this.sentTextClearOnPortInitCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.sentTextClearOnPortInitCheckBox.Location = new System.Drawing.Point(108, 298);
            this.sentTextClearOnPortInitCheckBox.Name = "sentTextClearOnPortInitCheckBox";
            this.sentTextClearOnPortInitCheckBox.Size = new System.Drawing.Size(184, 24);
            this.sentTextClearOnPortInitCheckBox.TabIndex = 50;
            this.sentTextClearOnPortInitCheckBox.Text = "When port is initialized";
            this.sentTextClearOnPortInitCheckBox.UseVisualStyleBackColor = true;
            // 
            // sentTextClearOnMonitoringStartCheckBox
            // 
            this.sentTextClearOnMonitoringStartCheckBox.AutoSize = true;
            this.sentTextClearOnMonitoringStartCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.sentTextClearOnMonitoringStartCheckBox.Location = new System.Drawing.Point(108, 328);
            this.sentTextClearOnMonitoringStartCheckBox.Name = "sentTextClearOnMonitoringStartCheckBox";
            this.sentTextClearOnMonitoringStartCheckBox.Size = new System.Drawing.Size(186, 24);
            this.sentTextClearOnMonitoringStartCheckBox.TabIndex = 51;
            this.sentTextClearOnMonitoringStartCheckBox.Text = "When monitoring starts";
            this.sentTextClearOnMonitoringStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // receivedTextClearOnPortInitCheckBox
            // 
            this.receivedTextClearOnPortInitCheckBox.AutoSize = true;
            this.receivedTextClearOnPortInitCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.receivedTextClearOnPortInitCheckBox.Location = new System.Drawing.Point(396, 298);
            this.receivedTextClearOnPortInitCheckBox.Name = "receivedTextClearOnPortInitCheckBox";
            this.receivedTextClearOnPortInitCheckBox.Size = new System.Drawing.Size(184, 24);
            this.receivedTextClearOnPortInitCheckBox.TabIndex = 52;
            this.receivedTextClearOnPortInitCheckBox.Text = "When port is initialized";
            this.receivedTextClearOnPortInitCheckBox.UseVisualStyleBackColor = true;
            // 
            // receivedTextClearOnMonitoringStartCheckBox
            // 
            this.receivedTextClearOnMonitoringStartCheckBox.AutoSize = true;
            this.receivedTextClearOnMonitoringStartCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.receivedTextClearOnMonitoringStartCheckBox.Location = new System.Drawing.Point(396, 328);
            this.receivedTextClearOnMonitoringStartCheckBox.Name = "receivedTextClearOnMonitoringStartCheckBox";
            this.receivedTextClearOnMonitoringStartCheckBox.Size = new System.Drawing.Size(186, 24);
            this.receivedTextClearOnMonitoringStartCheckBox.TabIndex = 53;
            this.receivedTextClearOnMonitoringStartCheckBox.Text = "When monitoring starts";
            this.receivedTextClearOnMonitoringStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // sentTextAutoScrollCheckBox
            // 
            this.sentTextAutoScrollCheckBox.AutoSize = true;
            this.sentTextAutoScrollCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.sentTextAutoScrollCheckBox.Location = new System.Drawing.Point(108, 358);
            this.sentTextAutoScrollCheckBox.Name = "sentTextAutoScrollCheckBox";
            this.sentTextAutoScrollCheckBox.Size = new System.Drawing.Size(63, 24);
            this.sentTextAutoScrollCheckBox.TabIndex = 54;
            this.sentTextAutoScrollCheckBox.Text = "Auto";
            this.sentTextAutoScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // receivedTextAutoScrollCheckBox
            // 
            this.receivedTextAutoScrollCheckBox.AutoSize = true;
            this.receivedTextAutoScrollCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.receivedTextAutoScrollCheckBox.Location = new System.Drawing.Point(396, 358);
            this.receivedTextAutoScrollCheckBox.Name = "receivedTextAutoScrollCheckBox";
            this.receivedTextAutoScrollCheckBox.Size = new System.Drawing.Size(63, 24);
            this.receivedTextAutoScrollCheckBox.TabIndex = 55;
            this.receivedTextAutoScrollCheckBox.Text = "Auto";
            this.receivedTextAutoScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // sentTextScrollLabel
            // 
            this.sentTextScrollLabel.AutoSize = true;
            this.sentTextScrollLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sentTextScrollLabel.Location = new System.Drawing.Point(3, 355);
            this.sentTextScrollLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.sentTextScrollLabel.Name = "sentTextScrollLabel";
            this.sentTextScrollLabel.Size = new System.Drawing.Size(49, 30);
            this.sentTextScrollLabel.TabIndex = 58;
            this.sentTextScrollLabel.Text = "Scroll:";
            this.sentTextScrollLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sentTextClearLabel
            // 
            this.sentTextClearLabel.AutoSize = true;
            this.sentTextClearLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sentTextClearLabel.Location = new System.Drawing.Point(3, 260);
            this.sentTextClearLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.sentTextClearLabel.Name = "sentTextClearLabel";
            this.sentTextClearLabel.Size = new System.Drawing.Size(46, 35);
            this.sentTextClearLabel.TabIndex = 60;
            this.sentTextClearLabel.Text = "Clear:";
            this.sentTextClearLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bothLabel
            // 
            this.bothLabel.AutoSize = true;
            this.bothLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.bothLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bothLabel.Location = new System.Drawing.Point(684, 0);
            this.bothLabel.Name = "bothLabel";
            this.bothLabel.Size = new System.Drawing.Size(283, 20);
            this.bothLabel.TabIndex = 61;
            this.bothLabel.Text = "Both";
            // 
            // bothTextClearOnPortInitCheckBox
            // 
            this.bothTextClearOnPortInitCheckBox.AutoSize = true;
            this.bothTextClearOnPortInitCheckBox.Location = new System.Drawing.Point(684, 298);
            this.bothTextClearOnPortInitCheckBox.Name = "bothTextClearOnPortInitCheckBox";
            this.bothTextClearOnPortInitCheckBox.Size = new System.Drawing.Size(184, 24);
            this.bothTextClearOnPortInitCheckBox.TabIndex = 62;
            this.bothTextClearOnPortInitCheckBox.Text = "When port is initialized";
            this.bothTextClearOnPortInitCheckBox.UseVisualStyleBackColor = true;
            // 
            // bothTextClearOnMonitoringStartCheckBox
            // 
            this.bothTextClearOnMonitoringStartCheckBox.AutoSize = true;
            this.bothTextClearOnMonitoringStartCheckBox.Location = new System.Drawing.Point(684, 328);
            this.bothTextClearOnMonitoringStartCheckBox.Name = "bothTextClearOnMonitoringStartCheckBox";
            this.bothTextClearOnMonitoringStartCheckBox.Size = new System.Drawing.Size(186, 24);
            this.bothTextClearOnMonitoringStartCheckBox.TabIndex = 63;
            this.bothTextClearOnMonitoringStartCheckBox.Text = "When monitoring starts";
            this.bothTextClearOnMonitoringStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // bothTextAutoScrollCheckBox
            // 
            this.bothTextAutoScrollCheckBox.AutoSize = true;
            this.bothTextAutoScrollCheckBox.Location = new System.Drawing.Point(684, 358);
            this.bothTextAutoScrollCheckBox.Name = "bothTextAutoScrollCheckBox";
            this.bothTextAutoScrollCheckBox.Size = new System.Drawing.Size(63, 24);
            this.bothTextAutoScrollCheckBox.TabIndex = 64;
            this.bothTextAutoScrollCheckBox.Text = "Auto";
            this.bothTextAutoScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // SerialPortMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 753);
            this.HeaderText = "Serial port monitor";
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.MinimumSize = new System.Drawing.Size(500, 538);
            this.Name = "SerialPortMonitorForm";
            this.Text = "Serial port monitor";
            this.Load += new System.EventHandler(this.SerialPortMonitorForm_Load);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Button receiveDataButton;
        private System.Windows.Forms.Button sentTextClearButton;
        private System.Windows.Forms.Button receivedTextClearButton;
        private System.Windows.Forms.Label sentTextBoxEncodingLabel;
        private System.Windows.Forms.ComboBox sentTextBoxEncodingDropDown;
        private System.Windows.Forms.ComboBox receivedTextBoxEncodingDropDown;
        private System.Windows.Forms.ComboBox simulateReceivingEncodingDropDown;
        private System.Windows.Forms.TextBox simulateReceivingDataTextBox;
        private System.Windows.Forms.ComboBox bothTextBoxEncodingDropDown;
        private System.Windows.Forms.Button bothTextClearButton;
        private System.Windows.Forms.Label portSettingsLabel;
        private System.Windows.Forms.Label portNameLabel;
        private System.Windows.Forms.Label portBaudrateLabel;
        private System.Windows.Forms.Label portDatabitsLabel;
        private System.Windows.Forms.Label portNameValueLabel;
        private System.Windows.Forms.Label portBaudrateValueLabel;
        private System.Windows.Forms.Label portDatabitsValueLabel;
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
        private System.Windows.Forms.CheckBox sentTextClearOnPortInitCheckBox;
        private System.Windows.Forms.CheckBox sentTextClearOnMonitoringStartCheckBox;
        private System.Windows.Forms.CheckBox receivedTextClearOnPortInitCheckBox;
        private System.Windows.Forms.CheckBox receivedTextClearOnMonitoringStartCheckBox;
        private System.Windows.Forms.CheckBox sentTextAutoScrollCheckBox;
        private System.Windows.Forms.CheckBox receivedTextAutoScrollCheckBox;
        private System.Windows.Forms.Label sentTextScrollLabel;
        private System.Windows.Forms.Label sentTextClearLabel;
        private System.Windows.Forms.Label bothLabel;
        private System.Windows.Forms.CheckBox bothTextClearOnPortInitCheckBox;
        private System.Windows.Forms.CheckBox bothTextClearOnMonitoringStartCheckBox;
        private System.Windows.Forms.CheckBox bothTextAutoScrollCheckBox;
    }
}