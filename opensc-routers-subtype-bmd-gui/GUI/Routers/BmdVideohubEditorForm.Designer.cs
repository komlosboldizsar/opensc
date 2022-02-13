namespace OpenSC.GUI.Routers
{
    partial class BmdVideohubEditorForm
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
            this.connectionPanel = new System.Windows.Forms.Panel();
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.connectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.ipAddressTextbox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.autoReconnectCheckbox = new System.Windows.Forms.CheckBox();
            this.autoReconnectLabel = new System.Windows.Forms.Label();
            this.baseDataTabPage.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.connectionPanel.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.connectionTable.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.Controls.Add(this.connectionPanel);
            this.baseDataTabPage.Size = new System.Drawing.Size(954, 389);
            this.baseDataTabPage.Controls.SetChildIndex(this.connectionPanel, 0);
            // 
            // outputsButtonsPanel
            // 
            this.outputsButtonsPanel.Location = new System.Drawing.Point(3, 342);
            this.outputsButtonsPanel.Size = new System.Drawing.Size(1048, 44);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(982, 428);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(982, 497);
            // 
            // connectionPanel
            // 
            this.connectionPanel.AutoSize = true;
            this.connectionPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionPanel.Controls.Add(this.connectionGroupBox);
            this.connectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionPanel.Location = new System.Drawing.Point(3, 97);
            this.connectionPanel.Name = "connectionPanel";
            this.connectionPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.connectionPanel.Size = new System.Drawing.Size(948, 121);
            this.connectionPanel.TabIndex = 2;
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.AutoSize = true;
            this.connectionGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionGroupBox.Controls.Add(this.connectionTable);
            this.connectionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionGroupBox.Location = new System.Drawing.Point(0, 0);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.connectionGroupBox.Size = new System.Drawing.Size(948, 114);
            this.connectionGroupBox.TabIndex = 0;
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
            this.connectionTable.Controls.Add(this.ipAddressLabel, 0, 0);
            this.connectionTable.Controls.Add(this.ipAddressTextbox, 1, 0);
            this.connectionTable.Controls.Add(this.panel1, 1, 1);
            this.connectionTable.Controls.Add(this.autoReconnectCheckbox, 1, 2);
            this.connectionTable.Controls.Add(this.autoReconnectLabel, 0, 2);
            this.connectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTable.Location = new System.Drawing.Point(8, 23);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 3;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(932, 83);
            this.connectionTable.TabIndex = 0;
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ipAddressLabel.Location = new System.Drawing.Point(3, 0);
            this.ipAddressLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(79, 28);
            this.ipAddressLabel.TabIndex = 0;
            this.ipAddressLabel.Text = "IP address:";
            this.ipAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ipAddressTextbox
            // 
            this.ipAddressTextbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ipAddressTextbox.Location = new System.Drawing.Point(129, 3);
            this.ipAddressTextbox.Name = "ipAddressTextbox";
            this.ipAddressTextbox.Size = new System.Drawing.Size(800, 22);
            this.ipAddressTextbox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.disconnectButton);
            this.panel1.Controls.Add(this.connectButton);
            this.panel1.Location = new System.Drawing.Point(126, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 32);
            this.panel1.TabIndex = 2;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(106, 3);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(97, 26);
            this.disconnectButton.TabIndex = 1;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(3, 3);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(97, 26);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // autoReconnectCheckbox
            // 
            this.autoReconnectCheckbox.AutoSize = true;
            this.autoReconnectCheckbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.autoReconnectCheckbox.Location = new System.Drawing.Point(129, 63);
            this.autoReconnectCheckbox.Name = "autoReconnectCheckbox";
            this.autoReconnectCheckbox.Size = new System.Drawing.Size(18, 17);
            this.autoReconnectCheckbox.TabIndex = 3;
            this.autoReconnectCheckbox.UseVisualStyleBackColor = true;
            // 
            // autoReconnectLabel
            // 
            this.autoReconnectLabel.AutoSize = true;
            this.autoReconnectLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.autoReconnectLabel.Location = new System.Drawing.Point(3, 60);
            this.autoReconnectLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.autoReconnectLabel.Name = "autoReconnectLabel";
            this.autoReconnectLabel.Size = new System.Drawing.Size(108, 23);
            this.autoReconnectLabel.TabIndex = 4;
            this.autoReconnectLabel.Text = "Auto reconnect:";
            this.autoReconnectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BmdVideohubEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.DeleteButtonVisible = true;
            this.Name = "BmdVideohubEditorForm";
            this.baseDataTabPage.ResumeLayout(false);
            this.baseDataTabPage.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.connectionPanel.ResumeLayout(false);
            this.connectionPanel.PerformLayout();
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel connectionPanel;
        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.TextBox ipAddressTextbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.CheckBox autoReconnectCheckbox;
        private System.Windows.Forms.Label autoReconnectLabel;
    }
}