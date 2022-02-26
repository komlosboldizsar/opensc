namespace OpenSC.GUI.Routers.Salvos
{
    partial class SalvoEditorForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.crosspointsTabPage = new System.Windows.Forms.TabPage();
            this.crosspointsTableContainerPanel = new System.Windows.Forms.Panel();
            this.crosspointsTable = new System.Windows.Forms.DataGridView();
            this.inputsButtonsPanel = new System.Windows.Forms.Panel();
            this.addInputButton = new System.Windows.Forms.Button();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.crosspointsTabPage.SuspendLayout();
            this.crosspointsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crosspointsTable)).BeginInit();
            this.inputsButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.tabControl);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 15, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(1082, 535);
            this.customElementsPanel.Controls.SetChildIndex(this.tabControl, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Size = new System.Drawing.Size(1082, 621);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.crosspointsTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(10, 120);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1062, 415);
            this.tabControl.TabIndex = 1;
            // 
            // crosspointsTabPage
            // 
            this.crosspointsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.crosspointsTabPage.Controls.Add(this.crosspointsTableContainerPanel);
            this.crosspointsTabPage.Controls.Add(this.inputsButtonsPanel);
            this.crosspointsTabPage.Location = new System.Drawing.Point(4, 29);
            this.crosspointsTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.crosspointsTabPage.Name = "crosspointsTabPage";
            this.crosspointsTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.crosspointsTabPage.Size = new System.Drawing.Size(1054, 382);
            this.crosspointsTabPage.TabIndex = 1;
            this.crosspointsTabPage.Text = "Crosspoints";
            // 
            // crosspointsTableContainerPanel
            // 
            this.crosspointsTableContainerPanel.Controls.Add(this.crosspointsTable);
            this.crosspointsTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crosspointsTableContainerPanel.Location = new System.Drawing.Point(3, 4);
            this.crosspointsTableContainerPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.crosspointsTableContainerPanel.Name = "crosspointsTableContainerPanel";
            this.crosspointsTableContainerPanel.Size = new System.Drawing.Size(1048, 319);
            this.crosspointsTableContainerPanel.TabIndex = 2;
            // 
            // crosspointsTable
            // 
            this.crosspointsTable.AllowUserToAddRows = false;
            this.crosspointsTable.AllowUserToDeleteRows = false;
            this.crosspointsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.crosspointsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crosspointsTable.Location = new System.Drawing.Point(0, 0);
            this.crosspointsTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.crosspointsTable.Name = "crosspointsTable";
            this.crosspointsTable.ReadOnly = true;
            this.crosspointsTable.RowHeadersWidth = 51;
            this.crosspointsTable.RowTemplate.Height = 24;
            this.crosspointsTable.Size = new System.Drawing.Size(1048, 319);
            this.crosspointsTable.TabIndex = 0;
            // 
            // inputsButtonsPanel
            // 
            this.inputsButtonsPanel.Controls.Add(this.addInputButton);
            this.inputsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputsButtonsPanel.Location = new System.Drawing.Point(3, 323);
            this.inputsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputsButtonsPanel.Name = "inputsButtonsPanel";
            this.inputsButtonsPanel.Size = new System.Drawing.Size(1048, 55);
            this.inputsButtonsPanel.TabIndex = 1;
            // 
            // addInputButton
            // 
            this.addInputButton.Location = new System.Drawing.Point(6, 8);
            this.addInputButton.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.addInputButton.Name = "addInputButton";
            this.addInputButton.Size = new System.Drawing.Size(126, 32);
            this.addInputButton.TabIndex = 0;
            this.addInputButton.Text = "Add crosspoint";
            this.addInputButton.UseVisualStyleBackColor = true;
            this.addInputButton.Click += new System.EventHandler(this.addCrosspointButton_Click);
            // 
            // SalvoEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 691);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New salvo";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(1000, 738);
            this.Name = "SalvoEditorForm";
            this.SubjectPlural = "salvos";
            this.SubjectSingular = "salvo";
            this.Text = "New salvo";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.crosspointsTabPage.ResumeLayout(false);
            this.crosspointsTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crosspointsTable)).EndInit();
            this.inputsButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage crosspointsTabPage;
        private System.Windows.Forms.DataGridView crosspointsTable;
        private System.Windows.Forms.Button addInputButton;
        private System.Windows.Forms.Panel crosspointsTableContainerPanel;
        protected System.Windows.Forms.Panel inputsButtonsPanel;
    }
}