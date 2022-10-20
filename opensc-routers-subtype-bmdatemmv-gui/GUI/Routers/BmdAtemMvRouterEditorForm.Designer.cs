namespace OpenSC.GUI.Routers
{
    partial class BmdAtemMvRouterEditorForm
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
            this.mixerLabel = new System.Windows.Forms.Label();
            this.mixerDropDown = new System.Windows.Forms.ComboBox();
            this.baseDataTabPage.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.connectionPanel.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.connectionTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.Controls.Add(this.connectionPanel);
            this.baseDataTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.baseDataTabPage.Size = new System.Drawing.Size(954, 549);
            // 
            // inputsButtonsPanel
            // 
            this.inputsButtonsPanel.Location = new System.Drawing.Point(3, 322);
            this.inputsButtonsPanel.Size = new System.Drawing.Size(1048, 55);
            // 
            // outputsButtonsPanel
            // 
            this.outputsButtonsPanel.Location = new System.Drawing.Point(3, 322);
            this.outputsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 19, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(982, 708);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.mainContainer.Size = new System.Drawing.Size(982, 794);
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
            this.connectionPanel.Size = new System.Drawing.Size(948, 85);
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
            this.connectionGroupBox.Size = new System.Drawing.Size(948, 76);
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
            this.connectionTable.Controls.Add(this.mixerLabel, 0, 0);
            this.connectionTable.Controls.Add(this.mixerDropDown, 1, 0);
            this.connectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTable.Location = new System.Drawing.Point(8, 30);
            this.connectionTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 1;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.connectionTable.Size = new System.Drawing.Size(932, 36);
            this.connectionTable.TabIndex = 0;
            // 
            // mixerLabel
            // 
            this.mixerLabel.AutoSize = true;
            this.mixerLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.mixerLabel.Location = new System.Drawing.Point(3, 0);
            this.mixerLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.mixerLabel.Name = "mixerLabel";
            this.mixerLabel.Size = new System.Drawing.Size(49, 36);
            this.mixerLabel.TabIndex = 0;
            this.mixerLabel.Text = "Mixer:";
            this.mixerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mixerDropDown
            // 
            this.mixerDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mixerDropDown.FormattingEnabled = true;
            this.mixerDropDown.Location = new System.Drawing.Point(70, 4);
            this.mixerDropDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mixerDropDown.Name = "mixerDropDown";
            this.mixerDropDown.Size = new System.Drawing.Size(342, 28);
            this.mixerDropDown.TabIndex = 4;
            // 
            // BmdAtemMvRouterEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 864);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(1000, 911);
            this.Name = "BmdAtemMvRouterEditorForm";
            this.baseDataTabPage.ResumeLayout(false);
            this.baseDataTabPage.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.connectionPanel.ResumeLayout(false);
            this.connectionPanel.PerformLayout();
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel connectionPanel;
        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label mixerLabel;
        private System.Windows.Forms.ComboBox mixerDropDown;
    }
}