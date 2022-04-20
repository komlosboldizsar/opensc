namespace OpenSC.GUI.Mixers
{
    partial class BmdMixerEditorForm
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
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 29);
            this.baseDataTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.baseDataTabPage.Size = new System.Drawing.Size(804, 389);
            // 
            // inputsButtonsPanel
            // 
            this.inputsButtonsPanel.Location = new System.Drawing.Point(3, 329);
            this.inputsButtonsPanel.Size = new System.Drawing.Size(798, 55);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 19, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(832, 552);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.mainContainer.Size = new System.Drawing.Size(832, 638);
            // 
            // connectionPanel
            // 
            this.connectionPanel.AutoSize = true;
            this.connectionPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionPanel.Controls.Add(this.connectionGroupBox);
            this.connectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionPanel.Location = new System.Drawing.Point(3, 5);
            this.connectionPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectionPanel.Name = "connectionPanel";
            this.connectionPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.connectionPanel.Size = new System.Drawing.Size(798, 149);
            this.connectionPanel.TabIndex = 2;
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.AutoSize = true;
            this.connectionGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionGroupBox.Controls.Add(this.connectionTable);
            this.connectionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionGroupBox.Location = new System.Drawing.Point(0, 0);
            this.connectionGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.connectionGroupBox.Size = new System.Drawing.Size(798, 140);
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
            this.connectionTable.Location = new System.Drawing.Point(8, 30);
            this.connectionTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 3;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(782, 100);
            this.connectionTable.TabIndex = 0;
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ipAddressLabel.Location = new System.Drawing.Point(3, 0);
            this.ipAddressLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(79, 35);
            this.ipAddressLabel.TabIndex = 0;
            this.ipAddressLabel.Text = "IP address:";
            this.ipAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ipAddressTextbox
            // 
            this.ipAddressTextbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ipAddressTextbox.Location = new System.Drawing.Point(134, 4);
            this.ipAddressTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ipAddressTextbox.Name = "ipAddressTextbox";
            this.ipAddressTextbox.Size = new System.Drawing.Size(645, 27);
            this.ipAddressTextbox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.disconnectButton);
            this.panel1.Controls.Add(this.connectButton);
            this.panel1.Location = new System.Drawing.Point(131, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 40);
            this.panel1.TabIndex = 2;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(106, 4);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(97, 32);
            this.disconnectButton.TabIndex = 1;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(3, 4);
            this.connectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(97, 32);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // autoReconnectCheckbox
            // 
            this.autoReconnectCheckbox.AutoSize = true;
            this.autoReconnectCheckbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.autoReconnectCheckbox.Location = new System.Drawing.Point(134, 79);
            this.autoReconnectCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.autoReconnectCheckbox.Name = "autoReconnectCheckbox";
            this.autoReconnectCheckbox.Size = new System.Drawing.Size(18, 17);
            this.autoReconnectCheckbox.TabIndex = 3;
            this.autoReconnectCheckbox.UseVisualStyleBackColor = true;
            // 
            // autoReconnectLabel
            // 
            this.autoReconnectLabel.AutoSize = true;
            this.autoReconnectLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.autoReconnectLabel.Location = new System.Drawing.Point(3, 75);
            this.autoReconnectLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.autoReconnectLabel.Name = "autoReconnectLabel";
            this.autoReconnectLabel.Size = new System.Drawing.Size(113, 25);
            this.autoReconnectLabel.TabIndex = 4;
            this.autoReconnectLabel.Text = "Auto reconnect:";
            this.autoReconnectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BmdMixerEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 708);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(850, 755);
            this.Name = "BmdMixerEditorForm";
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