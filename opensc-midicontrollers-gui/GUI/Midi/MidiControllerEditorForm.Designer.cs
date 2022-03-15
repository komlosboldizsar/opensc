namespace OpenSC.GUI.MidiControllers
{
    partial class MidiControllerEditorForm
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
            this.deviceIdNumericField = new System.Windows.Forms.NumericUpDown();
            this.deviceIdLabel = new System.Windows.Forms.Label();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.comPortDataPanel.SuspendLayout();
            this.comPortDataGroupBox.SuspendLayout();
            this.comPortDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceIdNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.comPortDataPanel);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 12);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(500, 429);
            this.customElementsPanel.Controls.SetChildIndex(this.comPortDataPanel, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.mainContainer.Size = new System.Drawing.Size(520, 539);
            // 
            // comPortDataPanel
            // 
            this.comPortDataPanel.AutoSize = true;
            this.comPortDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comPortDataPanel.Controls.Add(this.comPortDataGroupBox);
            this.comPortDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.comPortDataPanel.Location = new System.Drawing.Point(0, 107);
            this.comPortDataPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comPortDataPanel.Name = "comPortDataPanel";
            this.comPortDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.comPortDataPanel.Size = new System.Drawing.Size(500, 84);
            this.comPortDataPanel.TabIndex = 3;
            // 
            // comPortDataGroupBox
            // 
            this.comPortDataGroupBox.AutoSize = true;
            this.comPortDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comPortDataGroupBox.Controls.Add(this.comPortDataTable);
            this.comPortDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.comPortDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.comPortDataGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.comPortDataGroupBox.Name = "comPortDataGroupBox";
            this.comPortDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.comPortDataGroupBox.Size = new System.Drawing.Size(500, 75);
            this.comPortDataGroupBox.TabIndex = 1;
            this.comPortDataGroupBox.TabStop = false;
            this.comPortDataGroupBox.Text = "Hardware properties";
            // 
            // comPortDataTable
            // 
            this.comPortDataTable.AutoSize = true;
            this.comPortDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comPortDataTable.ColumnCount = 2;
            this.comPortDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.comPortDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.comPortDataTable.Controls.Add(this.deviceIdNumericField, 1, 0);
            this.comPortDataTable.Controls.Add(this.deviceIdLabel, 0, 0);
            this.comPortDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comPortDataTable.Location = new System.Drawing.Point(8, 30);
            this.comPortDataTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comPortDataTable.Name = "comPortDataTable";
            this.comPortDataTable.RowCount = 1;
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.comPortDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.comPortDataTable.Size = new System.Drawing.Size(484, 35);
            this.comPortDataTable.TabIndex = 0;
            // 
            // deviceIdNumericField
            // 
            this.deviceIdNumericField.Location = new System.Drawing.Point(94, 4);
            this.deviceIdNumericField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deviceIdNumericField.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.deviceIdNumericField.Name = "deviceIdNumericField";
            this.deviceIdNumericField.Size = new System.Drawing.Size(120, 27);
            this.deviceIdNumericField.TabIndex = 5;
            // 
            // deviceIdLabel
            // 
            this.deviceIdLabel.AutoSize = true;
            this.deviceIdLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.deviceIdLabel.Location = new System.Drawing.Point(3, 0);
            this.deviceIdLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.deviceIdLabel.Name = "deviceIdLabel";
            this.deviceIdLabel.Size = new System.Drawing.Size(73, 35);
            this.deviceIdLabel.TabIndex = 1;
            this.deviceIdLabel.Text = "Device ID";
            this.deviceIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MidiControllerEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 609);
            this.DeleteButtonVisible = true;
            this.HeaderText = "Edit MIDI controller";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(500, 538);
            this.Name = "MidiControllerEditorForm";
            this.Text = "Edit MIDI controller";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.comPortDataPanel.ResumeLayout(false);
            this.comPortDataPanel.PerformLayout();
            this.comPortDataGroupBox.ResumeLayout(false);
            this.comPortDataGroupBox.PerformLayout();
            this.comPortDataTable.ResumeLayout(false);
            this.comPortDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceIdNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel comPortDataPanel;
        private System.Windows.Forms.GroupBox comPortDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel comPortDataTable;
        private System.Windows.Forms.Label deviceIdLabel;
        private System.Windows.Forms.NumericUpDown deviceIdNumericField;
    }
}