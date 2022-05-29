namespace OpenSC.GUI.Routers
{
    partial class RouterEditorFormBase
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
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.baseDataTabPage = new System.Windows.Forms.TabPage();
            this.inputsTabPage = new System.Windows.Forms.TabPage();
            this.inputsTableContainerPanel = new System.Windows.Forms.Panel();
            this.inputsTable = new System.Windows.Forms.DataGridView();
            this.inputsButtonsPanel = new System.Windows.Forms.Panel();
            this.inputNamesButton = new OpenSC.GUI.GeneralComponents.SplitButton();
            this.inputNamesButtonContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importInputNamesFromRemoteToLocalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportInputNamesToRemoteFromLocalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputNamesButtonContextMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.followRemoteInputNameChangesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.followLocalInputNameChangesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addInputButton = new System.Windows.Forms.Button();
            this.outputsTabPage = new System.Windows.Forms.TabPage();
            this.outputsTableContainerPanel = new System.Windows.Forms.Panel();
            this.outputsTable = new System.Windows.Forms.DataGridView();
            this.outputsButtonsPanel = new System.Windows.Forms.Panel();
            this.splitButton1 = new OpenSC.GUI.GeneralComponents.SplitButton();
            this.addOutputButton = new System.Windows.Forms.Button();
            this.outputNamesButtonContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importOutputNamesFromRemoteToLocalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportOutputNamesToRemoteFromLocalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputNamesButtonContextMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.followRemoteOutputNameChangesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.followLocalOutputNameChangesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.inputsTabPage.SuspendLayout();
            this.inputsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputsTable)).BeginInit();
            this.inputsButtonsPanel.SuspendLayout();
            this.inputNamesButtonContextMenu.SuspendLayout();
            this.outputsTabPage.SuspendLayout();
            this.outputsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputsTable)).BeginInit();
            this.outputsButtonsPanel.SuspendLayout();
            this.outputNamesButtonContextMenu.SuspendLayout();
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
            this.tabControl.Controls.Add(this.baseDataTabPage);
            this.tabControl.Controls.Add(this.inputsTabPage);
            this.tabControl.Controls.Add(this.outputsTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(10, 120);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1062, 415);
            this.tabControl.TabIndex = 1;
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 29);
            this.baseDataTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.baseDataTabPage.Name = "baseDataTabPage";
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.baseDataTabPage.Size = new System.Drawing.Size(1054, 382);
            this.baseDataTabPage.TabIndex = 0;
            this.baseDataTabPage.Text = "Base data";
            // 
            // inputsTabPage
            // 
            this.inputsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.inputsTabPage.Controls.Add(this.inputsTableContainerPanel);
            this.inputsTabPage.Controls.Add(this.inputsButtonsPanel);
            this.inputsTabPage.Location = new System.Drawing.Point(4, 29);
            this.inputsTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputsTabPage.Name = "inputsTabPage";
            this.inputsTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputsTabPage.Size = new System.Drawing.Size(490, 182);
            this.inputsTabPage.TabIndex = 1;
            this.inputsTabPage.Text = "Inputs";
            // 
            // inputsTableContainerPanel
            // 
            this.inputsTableContainerPanel.Controls.Add(this.inputsTable);
            this.inputsTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsTableContainerPanel.Location = new System.Drawing.Point(3, 4);
            this.inputsTableContainerPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputsTableContainerPanel.Name = "inputsTableContainerPanel";
            this.inputsTableContainerPanel.Size = new System.Drawing.Size(484, 119);
            this.inputsTableContainerPanel.TabIndex = 2;
            // 
            // inputsTable
            // 
            this.inputsTable.AllowUserToAddRows = false;
            this.inputsTable.AllowUserToDeleteRows = false;
            this.inputsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsTable.Location = new System.Drawing.Point(0, 0);
            this.inputsTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputsTable.Name = "inputsTable";
            this.inputsTable.ReadOnly = true;
            this.inputsTable.RowHeadersWidth = 51;
            this.inputsTable.RowTemplate.Height = 24;
            this.inputsTable.Size = new System.Drawing.Size(484, 119);
            this.inputsTable.TabIndex = 0;
            // 
            // inputsButtonsPanel
            // 
            this.inputsButtonsPanel.Controls.Add(this.inputNamesButton);
            this.inputsButtonsPanel.Controls.Add(this.addInputButton);
            this.inputsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputsButtonsPanel.Location = new System.Drawing.Point(3, 123);
            this.inputsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputsButtonsPanel.Name = "inputsButtonsPanel";
            this.inputsButtonsPanel.Size = new System.Drawing.Size(484, 55);
            this.inputsButtonsPanel.TabIndex = 1;
            // 
            // inputNamesButton
            // 
            this.inputNamesButton.Location = new System.Drawing.Point(141, 8);
            this.inputNamesButton.Menu = this.inputNamesButtonContextMenu;
            this.inputNamesButton.Name = "inputNamesButton";
            this.inputNamesButton.Padding = new System.Windows.Forms.Padding(0);
            this.inputNamesButton.Size = new System.Drawing.Size(120, 32);
            this.inputNamesButton.SplitWidth = 25;
            this.inputNamesButton.TabIndex = 1;
            this.inputNamesButton.Text = "Names";
            this.inputNamesButton.UseVisualStyleBackColor = true;
            // 
            // inputNamesButtonContextMenu
            // 
            this.inputNamesButtonContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.inputNamesButtonContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importInputNamesFromRemoteToLocalMenuItem,
            this.exportInputNamesToRemoteFromLocalMenuItem,
            this.inputNamesButtonContextMenuSeparator1,
            this.followRemoteInputNameChangesMenuItem,
            this.followLocalInputNameChangesMenuItem});
            this.inputNamesButtonContextMenu.Name = "inputNamesButtonContextMenu";
            this.inputNamesButtonContextMenu.Size = new System.Drawing.Size(416, 106);
            // 
            // importInputNamesFromRemoteToLocalMenuItem
            // 
            this.importInputNamesFromRemoteToLocalMenuItem.Name = "importInputNamesFromRemoteToLocalMenuItem";
            this.importInputNamesFromRemoteToLocalMenuItem.Size = new System.Drawing.Size(415, 24);
            this.importInputNamesFromRemoteToLocalMenuItem.Text = "Import remote to local";
            this.importInputNamesFromRemoteToLocalMenuItem.Click += new System.EventHandler(this.importInputNamesFromRemoteToLocalMenuItem_Click);
            // 
            // exportInputNamesToRemoteFromLocalMenuItem
            // 
            this.exportInputNamesToRemoteFromLocalMenuItem.Name = "exportInputNamesToRemoteFromLocalMenuItem";
            this.exportInputNamesToRemoteFromLocalMenuItem.Size = new System.Drawing.Size(415, 24);
            this.exportInputNamesToRemoteFromLocalMenuItem.Text = "Export local to remote";
            this.exportInputNamesToRemoteFromLocalMenuItem.Click += new System.EventHandler(this.exportInputNamesToRemoteFromLocalMenuItem_Click);
            // 
            // inputNamesButtonContextMenuSeparator1
            // 
            this.inputNamesButtonContextMenuSeparator1.Name = "inputNamesButtonContextMenuSeparator1";
            this.inputNamesButtonContextMenuSeparator1.Size = new System.Drawing.Size(412, 6);
            // 
            // followRemoteInputNameChangesMenuItem
            // 
            this.followRemoteInputNameChangesMenuItem.CheckOnClick = true;
            this.followRemoteInputNameChangesMenuItem.Name = "followRemoteInputNameChangesMenuItem";
            this.followRemoteInputNameChangesMenuItem.Size = new System.Drawing.Size(415, 24);
            this.followRemoteInputNameChangesMenuItem.Text = "Follow remote changes (import on remote update)";
            // 
            // followLocalInputNameChangesMenuItem
            // 
            this.followLocalInputNameChangesMenuItem.CheckOnClick = true;
            this.followLocalInputNameChangesMenuItem.Name = "followLocalInputNameChangesMenuItem";
            this.followLocalInputNameChangesMenuItem.Size = new System.Drawing.Size(415, 24);
            this.followLocalInputNameChangesMenuItem.Text = "Follow local changes (export on local update)";
            // 
            // addInputButton
            // 
            this.addInputButton.Location = new System.Drawing.Point(6, 8);
            this.addInputButton.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.addInputButton.Name = "addInputButton";
            this.addInputButton.Size = new System.Drawing.Size(126, 32);
            this.addInputButton.TabIndex = 0;
            this.addInputButton.Text = "Add input";
            this.addInputButton.UseVisualStyleBackColor = true;
            this.addInputButton.Click += new System.EventHandler(this.addInputButton_Click);
            // 
            // outputsTabPage
            // 
            this.outputsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.outputsTabPage.Controls.Add(this.outputsTableContainerPanel);
            this.outputsTabPage.Controls.Add(this.outputsButtonsPanel);
            this.outputsTabPage.Location = new System.Drawing.Point(4, 29);
            this.outputsTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.outputsTabPage.Name = "outputsTabPage";
            this.outputsTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.outputsTabPage.Size = new System.Drawing.Size(1054, 382);
            this.outputsTabPage.TabIndex = 2;
            this.outputsTabPage.Text = "Outputs";
            // 
            // outputsTableContainerPanel
            // 
            this.outputsTableContainerPanel.Controls.Add(this.outputsTable);
            this.outputsTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputsTableContainerPanel.Location = new System.Drawing.Point(3, 4);
            this.outputsTableContainerPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.outputsTableContainerPanel.Name = "outputsTableContainerPanel";
            this.outputsTableContainerPanel.Size = new System.Drawing.Size(1048, 319);
            this.outputsTableContainerPanel.TabIndex = 2;
            // 
            // outputsTable
            // 
            this.outputsTable.AllowUserToAddRows = false;
            this.outputsTable.AllowUserToDeleteRows = false;
            this.outputsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputsTable.Location = new System.Drawing.Point(0, 0);
            this.outputsTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.outputsTable.Name = "outputsTable";
            this.outputsTable.ReadOnly = true;
            this.outputsTable.RowHeadersWidth = 51;
            this.outputsTable.RowTemplate.Height = 24;
            this.outputsTable.Size = new System.Drawing.Size(1048, 319);
            this.outputsTable.TabIndex = 0;
            // 
            // outputsButtonsPanel
            // 
            this.outputsButtonsPanel.Controls.Add(this.splitButton1);
            this.outputsButtonsPanel.Controls.Add(this.addOutputButton);
            this.outputsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.outputsButtonsPanel.Location = new System.Drawing.Point(3, 323);
            this.outputsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.outputsButtonsPanel.Name = "outputsButtonsPanel";
            this.outputsButtonsPanel.Size = new System.Drawing.Size(1048, 55);
            this.outputsButtonsPanel.TabIndex = 1;
            // 
            // splitButton1
            // 
            this.splitButton1.Location = new System.Drawing.Point(142, 8);
            this.splitButton1.Menu = this.outputNamesButtonContextMenu;
            this.splitButton1.Name = "splitButton1";
            this.splitButton1.Size = new System.Drawing.Size(120, 32);
            this.splitButton1.TabIndex = 1;
            this.splitButton1.Text = "Names";
            this.splitButton1.UseVisualStyleBackColor = true;
            // 
            // addOutputButton
            // 
            this.addOutputButton.Location = new System.Drawing.Point(6, 8);
            this.addOutputButton.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.addOutputButton.Name = "addOutputButton";
            this.addOutputButton.Size = new System.Drawing.Size(126, 32);
            this.addOutputButton.TabIndex = 0;
            this.addOutputButton.Text = "Add output";
            this.addOutputButton.UseVisualStyleBackColor = true;
            this.addOutputButton.Click += new System.EventHandler(this.addOutputButton_Click);
            // 
            // outputNamesButtonContextMenu
            // 
            this.outputNamesButtonContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.outputNamesButtonContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importOutputNamesFromRemoteToLocalMenuItem,
            this.exportOutputNamesToRemoteFromLocalMenuItem,
            this.outputNamesButtonContextMenuSeparator1,
            this.followRemoteOutputNameChangesMenuItem,
            this.followLocalOutputNameChangesMenuItem});
            this.outputNamesButtonContextMenu.Name = "inputNamesButtonContextMenu";
            this.outputNamesButtonContextMenu.Size = new System.Drawing.Size(416, 106);
            // 
            // importOutputNamesFromRemoteToLocalMenuItem
            // 
            this.importOutputNamesFromRemoteToLocalMenuItem.Name = "importOutputNamesFromRemoteToLocalMenuItem";
            this.importOutputNamesFromRemoteToLocalMenuItem.Size = new System.Drawing.Size(415, 24);
            this.importOutputNamesFromRemoteToLocalMenuItem.Text = "Import remote to local";
            this.importOutputNamesFromRemoteToLocalMenuItem.Click += new System.EventHandler(this.importOutputNamesFromRemoteToLocalMenuItem_Click);
            // 
            // exportOutputNamesToRemoteFromLocalMenuItem
            // 
            this.exportOutputNamesToRemoteFromLocalMenuItem.Name = "exportOutputNamesToRemoteFromLocalMenuItem";
            this.exportOutputNamesToRemoteFromLocalMenuItem.Size = new System.Drawing.Size(415, 24);
            this.exportOutputNamesToRemoteFromLocalMenuItem.Text = "Export local to remote";
            this.exportOutputNamesToRemoteFromLocalMenuItem.Click += new System.EventHandler(this.exportOutputNamesToRemoteFromLocalMenuItem_Click);
            // 
            // outputNamesButtonContextMenuSeparator1
            // 
            this.outputNamesButtonContextMenuSeparator1.Name = "outputNamesButtonContextMenuSeparator1";
            this.outputNamesButtonContextMenuSeparator1.Size = new System.Drawing.Size(412, 6);
            // 
            // followRemoteOutputNameChangesMenuItem
            // 
            this.followRemoteOutputNameChangesMenuItem.CheckOnClick = true;
            this.followRemoteOutputNameChangesMenuItem.Name = "followRemoteOutputNameChangesMenuItem";
            this.followRemoteOutputNameChangesMenuItem.Size = new System.Drawing.Size(415, 24);
            this.followRemoteOutputNameChangesMenuItem.Text = "Follow remote changes (import on remote update)";
            // 
            // followLocalOutputNameChangesMenuItem
            // 
            this.followLocalOutputNameChangesMenuItem.CheckOnClick = true;
            this.followLocalOutputNameChangesMenuItem.Name = "followLocalOutputNameChangesMenuItem";
            this.followLocalOutputNameChangesMenuItem.Size = new System.Drawing.Size(415, 24);
            this.followLocalOutputNameChangesMenuItem.Text = "Follow local changes (export on local update)";
            // 
            // RouterEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 691);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New router";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(1000, 738);
            this.Name = "RouterEditorFormBase";
            this.SubjectPlural = "routers";
            this.SubjectSingular = "router";
            this.Text = "New router";
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.inputsTabPage.ResumeLayout(false);
            this.inputsTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputsTable)).EndInit();
            this.inputsButtonsPanel.ResumeLayout(false);
            this.inputNamesButtonContextMenu.ResumeLayout(false);
            this.outputsTabPage.ResumeLayout(false);
            this.outputsTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputsTable)).EndInit();
            this.outputsButtonsPanel.ResumeLayout(false);
            this.outputNamesButtonContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage inputsTabPage;
        private System.Windows.Forms.DataGridView inputsTable;
        private System.Windows.Forms.Button addInputButton;
        private System.Windows.Forms.TabPage outputsTabPage;
        private System.Windows.Forms.DataGridView outputsTable;
        private System.Windows.Forms.Button addOutputButton;
        private System.Windows.Forms.Panel outputsTableContainerPanel;
        private System.Windows.Forms.Panel inputsTableContainerPanel;
        protected System.Windows.Forms.TabPage baseDataTabPage;
        protected System.Windows.Forms.Panel inputsButtonsPanel;
        protected System.Windows.Forms.Panel outputsButtonsPanel;
        private GeneralComponents.SplitButton inputNamesButton;
        private System.Windows.Forms.ContextMenuStrip inputNamesButtonContextMenu;
        private System.Windows.Forms.ToolStripMenuItem importInputNamesFromRemoteToLocalMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportInputNamesToRemoteFromLocalMenuItem;
        private System.Windows.Forms.ToolStripSeparator inputNamesButtonContextMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem followLocalInputNameChangesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem followRemoteInputNameChangesMenuItem;
        private GeneralComponents.SplitButton splitButton1;
        private System.Windows.Forms.ContextMenuStrip outputNamesButtonContextMenu;
        private System.Windows.Forms.ToolStripMenuItem importOutputNamesFromRemoteToLocalMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportOutputNamesToRemoteFromLocalMenuItem;
        private System.Windows.Forms.ToolStripSeparator outputNamesButtonContextMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem followRemoteOutputNameChangesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem followLocalOutputNameChangesMenuItem;
    }
}