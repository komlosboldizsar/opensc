namespace OpenSC.GUI.Routers
{
    partial class LabelsetEditorForm
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
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.labelsGroupBox.SuspendLayout();
            this.labelsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.labelsGroupBox);
            this.customElementsPanel.Size = new System.Drawing.Size(982, 428);
            this.customElementsPanel.Controls.SetChildIndex(this.labelsGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(982, 497);
            // 
            // labelsGroupBox
            // 
            this.labelsGroupBox.AutoSize = true;
            this.labelsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelsGroupBox.Controls.Add(this.labelsTableContainerPanel);
            this.labelsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelsGroupBox.Location = new System.Drawing.Point(10, 93);
            this.labelsGroupBox.Name = "labelsGroupBox";
            this.labelsGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.labelsGroupBox.Size = new System.Drawing.Size(962, 335);
            this.labelsGroupBox.TabIndex = 0;
            this.labelsGroupBox.TabStop = false;
            this.labelsGroupBox.Text = "Labels";
            // 
            // labelsTableContainerPanel
            // 
            this.labelsTableContainerPanel.Controls.Add(this.labelsTable);
            this.labelsTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelsTableContainerPanel.Location = new System.Drawing.Point(8, 23);
            this.labelsTableContainerPanel.Name = "labelsTableContainerPanel";
            this.labelsTableContainerPanel.Size = new System.Drawing.Size(946, 304);
            this.labelsTableContainerPanel.TabIndex = 1;
            // 
            // labelsTable
            // 
            this.labelsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.labelsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelsTable.Location = new System.Drawing.Point(0, 0);
            this.labelsTable.Name = "labelsTable";
            this.labelsTable.RowHeadersWidth = 51;
            this.labelsTable.RowTemplate.Height = 24;
            this.labelsTable.Size = new System.Drawing.Size(946, 304);
            this.labelsTable.TabIndex = 0;
            // 
            // LabelsetEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New labelset";
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "LabelsetEditorForm";
            this.SubjectPlural = "labelsets";
            this.SubjectSingular = "labelset";
            this.Text = "New labelset";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
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