namespace OpenSC.GUI.Mixers
{
    partial class MixerEditorFormBase
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
            this.baseDataTabPage = new System.Windows.Forms.TabPage();
            this.inputsTabPage = new System.Windows.Forms.TabPage();
            this.inputsTableContainerPanel = new System.Windows.Forms.Panel();
            this.inputsTable = new System.Windows.Forms.DataGridView();
            this.inputsButtonsPanel = new System.Windows.Forms.Panel();
            this.addInputButton = new System.Windows.Forms.Button();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.inputsTabPage.SuspendLayout();
            this.inputsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputsTable)).BeginInit();
            this.inputsButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.tabControl);
            this.customElementsPanel.Size = new System.Drawing.Size(832, 345);
            this.customElementsPanel.Controls.SetChildIndex(this.tabControl, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(832, 414);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.baseDataTabPage);
            this.tabControl.Controls.Add(this.inputsTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(10, 93);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(812, 252);
            this.tabControl.TabIndex = 1;
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 25);
            this.baseDataTabPage.Name = "baseDataTabPage";
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.baseDataTabPage.Size = new System.Drawing.Size(804, 223);
            this.baseDataTabPage.TabIndex = 0;
            this.baseDataTabPage.Text = "Base data";
            // 
            // inputsTabPage
            // 
            this.inputsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.inputsTabPage.Controls.Add(this.inputsTableContainerPanel);
            this.inputsTabPage.Controls.Add(this.inputsButtonsPanel);
            this.inputsTabPage.Location = new System.Drawing.Point(4, 25);
            this.inputsTabPage.Name = "inputsTabPage";
            this.inputsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputsTabPage.Size = new System.Drawing.Size(490, 146);
            this.inputsTabPage.TabIndex = 1;
            this.inputsTabPage.Text = "Inputs";
            // 
            // inputsTableContainerPanel
            // 
            this.inputsTableContainerPanel.Controls.Add(this.inputsTable);
            this.inputsTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsTableContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.inputsTableContainerPanel.Name = "inputsTableContainerPanel";
            this.inputsTableContainerPanel.Size = new System.Drawing.Size(484, 96);
            this.inputsTableContainerPanel.TabIndex = 2;
            // 
            // inputsTable
            // 
            this.inputsTable.AllowUserToAddRows = false;
            this.inputsTable.AllowUserToDeleteRows = false;
            this.inputsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsTable.Location = new System.Drawing.Point(0, 0);
            this.inputsTable.Name = "inputsTable";
            this.inputsTable.ReadOnly = true;
            this.inputsTable.RowHeadersWidth = 51;
            this.inputsTable.RowTemplate.Height = 24;
            this.inputsTable.Size = new System.Drawing.Size(484, 96);
            this.inputsTable.TabIndex = 0;
            // 
            // inputsButtonsPanel
            // 
            this.inputsButtonsPanel.Controls.Add(this.addInputButton);
            this.inputsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputsButtonsPanel.Location = new System.Drawing.Point(3, 99);
            this.inputsButtonsPanel.Name = "inputsButtonsPanel";
            this.inputsButtonsPanel.Size = new System.Drawing.Size(484, 44);
            this.inputsButtonsPanel.TabIndex = 1;
            // 
            // addInputButton
            // 
            this.addInputButton.Location = new System.Drawing.Point(6, 6);
            this.addInputButton.Margin = new System.Windows.Forms.Padding(6);
            this.addInputButton.Name = "addInputButton";
            this.addInputButton.Size = new System.Drawing.Size(126, 26);
            this.addInputButton.TabIndex = 0;
            this.addInputButton.Text = "Add input";
            this.addInputButton.UseVisualStyleBackColor = true;
            this.addInputButton.Click += new System.EventHandler(this.addInputButton_Click);
            // 
            // MixerEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 470);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New mixer";
            this.MinimumSize = new System.Drawing.Size(850, 500);
            this.Name = "MixerEditorFormBase";
            this.SubjectPlural = "mixers";
            this.SubjectSingular = "mixer";
            this.Text = "New mixer";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.inputsTabPage.ResumeLayout(false);
            this.inputsTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputsTable)).EndInit();
            this.inputsButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage inputsTabPage;
        private System.Windows.Forms.DataGridView inputsTable;
        private System.Windows.Forms.Button addInputButton;
        private System.Windows.Forms.Panel inputsTableContainerPanel;
        protected System.Windows.Forms.TabPage baseDataTabPage;
        protected System.Windows.Forms.Panel inputsButtonsPanel;
    }
}