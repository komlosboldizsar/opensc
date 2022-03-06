namespace OpenSC.GUI.Labelsets
{
    partial class AllLabelsetEditorForm
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
            this.labelsGroupBox = new System.Windows.Forms.GroupBox();
            this.labelsTableContainerPanel = new System.Windows.Forms.Panel();
            this.labelsTable = new System.Windows.Forms.DataGridView();
            this.mainContainer.SuspendLayout();
            this.labelsGroupBox.SuspendLayout();
            this.labelsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.labelsGroupBox);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(982, 621);
            // 
            // labelsGroupBox
            // 
            this.labelsGroupBox.AutoSize = true;
            this.labelsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelsGroupBox.Controls.Add(this.labelsTableContainerPanel);
            this.labelsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelsGroupBox.Location = new System.Drawing.Point(10, 10);
            this.labelsGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelsGroupBox.Name = "labelsGroupBox";
            this.labelsGroupBox.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.labelsGroupBox.Size = new System.Drawing.Size(962, 601);
            this.labelsGroupBox.TabIndex = 0;
            this.labelsGroupBox.TabStop = false;
            this.labelsGroupBox.Text = "Labels";
            // 
            // labelsTableContainerPanel
            // 
            this.labelsTableContainerPanel.Controls.Add(this.labelsTable);
            this.labelsTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelsTableContainerPanel.Location = new System.Drawing.Point(8, 30);
            this.labelsTableContainerPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelsTableContainerPanel.Name = "labelsTableContainerPanel";
            this.labelsTableContainerPanel.Size = new System.Drawing.Size(946, 561);
            this.labelsTableContainerPanel.TabIndex = 1;
            // 
            // labelsTable
            // 
            this.labelsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.labelsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelsTable.Location = new System.Drawing.Point(0, 0);
            this.labelsTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelsTable.Name = "labelsTable";
            this.labelsTable.RowHeadersWidth = 51;
            this.labelsTable.RowTemplate.Height = 24;
            this.labelsTable.Size = new System.Drawing.Size(946, 561);
            this.labelsTable.TabIndex = 0;
            // 
            // AllLabelsetEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 691);
            this.HeaderText = "Edit all labels";
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.MinimumSize = new System.Drawing.Size(1000, 738);
            this.Name = "AllLabelsetEditorForm";
            this.Text = "Edit all labels";
            this.Load += new System.EventHandler(this.AllLabelsetEditorForm_Load);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.labelsGroupBox.ResumeLayout(false);
            this.labelsTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.labelsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox labelsGroupBox;
        private System.Windows.Forms.DataGridView labelsTable;
        private System.Windows.Forms.Panel labelsTableContainerPanel;
    }
}