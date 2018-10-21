namespace OpenSC.GUI.UMDs
{
    partial class SerialUmdPortEditorForm
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
            this.serialPortDataPanel = new System.Windows.Forms.Panel();
            this.serialPortDataGroupBox = new System.Windows.Forms.GroupBox();
            this.serialPortDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.portNameDropDown = new System.Windows.Forms.ComboBox();
            this.portNameLabel = new System.Windows.Forms.Label();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.serialPortDataPanel.SuspendLayout();
            this.serialPortDataGroupBox.SuspendLayout();
            this.serialPortDataTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.serialPortDataPanel);
            this.customElementsPanel.Size = new System.Drawing.Size(496, 305);
            this.customElementsPanel.Controls.SetChildIndex(this.serialPortDataPanel, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(516, 394);
            // 
            // serialPortDataPanel
            // 
            this.serialPortDataPanel.AutoSize = true;
            this.serialPortDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortDataPanel.Controls.Add(this.serialPortDataGroupBox);
            this.serialPortDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.serialPortDataPanel.Location = new System.Drawing.Point(0, 100);
            this.serialPortDataPanel.Name = "serialPortDataPanel";
            this.serialPortDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.serialPortDataPanel.Size = new System.Drawing.Size(496, 58);
            this.serialPortDataPanel.TabIndex = 2;
            // 
            // serialPortDataGroupBox
            // 
            this.serialPortDataGroupBox.AutoSize = true;
            this.serialPortDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortDataGroupBox.Controls.Add(this.serialPortDataTable);
            this.serialPortDataGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serialPortDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.serialPortDataGroupBox.Name = "serialPortDataGroupBox";
            this.serialPortDataGroupBox.Size = new System.Drawing.Size(496, 51);
            this.serialPortDataGroupBox.TabIndex = 0;
            this.serialPortDataGroupBox.TabStop = false;
            this.serialPortDataGroupBox.Text = "Serial port";
            // 
            // serialPortDataTable
            // 
            this.serialPortDataTable.AutoSize = true;
            this.serialPortDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortDataTable.ColumnCount = 2;
            this.serialPortDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.serialPortDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.serialPortDataTable.Controls.Add(this.portNameDropDown, 1, 0);
            this.serialPortDataTable.Controls.Add(this.portNameLabel, 0, 0);
            this.serialPortDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serialPortDataTable.Location = new System.Drawing.Point(3, 18);
            this.serialPortDataTable.Name = "serialPortDataTable";
            this.serialPortDataTable.RowCount = 1;
            this.serialPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.serialPortDataTable.Size = new System.Drawing.Size(490, 30);
            this.serialPortDataTable.TabIndex = 0;
            // 
            // portNameDropDown
            // 
            this.portNameDropDown.FormattingEnabled = true;
            this.portNameDropDown.Location = new System.Drawing.Point(70, 3);
            this.portNameDropDown.Name = "portNameDropDown";
            this.portNameDropDown.Size = new System.Drawing.Size(179, 24);
            this.portNameDropDown.TabIndex = 1;
            // 
            // portNameLabel
            // 
            this.portNameLabel.AutoSize = true;
            this.portNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portNameLabel.Location = new System.Drawing.Point(3, 0);
            this.portNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portNameLabel.Name = "portNameLabel";
            this.portNameLabel.Size = new System.Drawing.Size(49, 30);
            this.portNameLabel.TabIndex = 0;
            this.portNameLabel.Text = "Name:";
            this.portNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SerialUmdPortEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 450);
            this.DeleteButtonVisible = true;
            this.Name = "SerialUmdPortEditorForm";
            this.Text = "SerialUmdPortEditorForm";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.serialPortDataPanel.ResumeLayout(false);
            this.serialPortDataPanel.PerformLayout();
            this.serialPortDataGroupBox.ResumeLayout(false);
            this.serialPortDataGroupBox.PerformLayout();
            this.serialPortDataTable.ResumeLayout(false);
            this.serialPortDataTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel serialPortDataPanel;
        private System.Windows.Forms.GroupBox serialPortDataGroupBox;
        private System.Windows.Forms.ComboBox portNameDropDown;
        private System.Windows.Forms.TableLayoutPanel serialPortDataTable;
        private System.Windows.Forms.Label portNameLabel;
    }
}