namespace OpenSC.GUI.Macros
{
    partial class MacroEditorForm
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
            this.baseDataPanel = new System.Windows.Forms.Panel();
            this.baseDataGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.baseDataTabPage = new System.Windows.Forms.TabPage();
            this.commandsTabPage = new System.Windows.Forms.TabPage();
            this.commandsEditorContainerPanel = new System.Windows.Forms.Panel();
            this.commandsButtonsPanel = new System.Windows.Forms.Panel();
            this.addCommandButton = new System.Windows.Forms.Button();
            this.triggersTabPage = new System.Windows.Forms.TabPage();
            this.triggersTableContainerPanel = new System.Windows.Forms.Panel();
            this.triggersTable = new System.Windows.Forms.DataGridView();
            this.triggersButtonsPanel = new System.Windows.Forms.Panel();
            this.addTriggerButton = new System.Windows.Forms.Button();
            this.commandsEditorTextBox = new System.Windows.Forms.RichTextBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.baseDataPanel.SuspendLayout();
            this.baseDataGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.tabControl.SuspendLayout();
            this.baseDataTabPage.SuspendLayout();
            this.commandsTabPage.SuspendLayout();
            this.commandsEditorContainerPanel.SuspendLayout();
            this.commandsButtonsPanel.SuspendLayout();
            this.triggersTabPage.SuspendLayout();
            this.triggersTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.triggersTable)).BeginInit();
            this.triggersButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.tabControl);
            this.customElementsPanel.Size = new System.Drawing.Size(982, 428);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(982, 497);
            // 
            // baseDataPanel
            // 
            this.baseDataPanel.AutoSize = true;
            this.baseDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataPanel.Controls.Add(this.baseDataGroupBox);
            this.baseDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseDataPanel.Location = new System.Drawing.Point(3, 3);
            this.baseDataPanel.Name = "baseDataPanel";
            this.baseDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.baseDataPanel.Size = new System.Drawing.Size(948, 94);
            this.baseDataPanel.TabIndex = 0;
            // 
            // baseDataGroupBox
            // 
            this.baseDataGroupBox.AutoSize = true;
            this.baseDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.baseDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.baseDataGroupBox.Name = "baseDataGroupBox";
            this.baseDataGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.baseDataGroupBox.Size = new System.Drawing.Size(948, 87);
            this.baseDataGroupBox.TabIndex = 0;
            this.baseDataGroupBox.TabStop = false;
            this.baseDataGroupBox.Text = "Base data";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.idLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.idNumericField, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(932, 56);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.idLabel.Location = new System.Drawing.Point(3, 0);
            this.idLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(25, 28);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "ID:";
            this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.nameLabel.Location = new System.Drawing.Point(3, 28);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(49, 28);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name:";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameTextBox.Location = new System.Drawing.Point(70, 31);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(859, 22);
            this.nameTextBox.TabIndex = 2;
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(70, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.baseDataTabPage);
            this.tabControl.Controls.Add(this.commandsTabPage);
            this.tabControl.Controls.Add(this.triggersTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(962, 418);
            this.tabControl.TabIndex = 1;
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.baseDataTabPage.Controls.Add(this.baseDataPanel);
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 25);
            this.baseDataTabPage.Name = "baseDataTabPage";
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.baseDataTabPage.Size = new System.Drawing.Size(954, 389);
            this.baseDataTabPage.TabIndex = 0;
            this.baseDataTabPage.Text = "Base data";
            // 
            // commandsTabPage
            // 
            this.commandsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.commandsTabPage.Controls.Add(this.commandsEditorContainerPanel);
            this.commandsTabPage.Controls.Add(this.commandsButtonsPanel);
            this.commandsTabPage.Location = new System.Drawing.Point(4, 25);
            this.commandsTabPage.Name = "commandsTabPage";
            this.commandsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.commandsTabPage.Size = new System.Drawing.Size(954, 389);
            this.commandsTabPage.TabIndex = 1;
            this.commandsTabPage.Text = "Commands";
            // 
            // commandsEditorContainerPanel
            // 
            this.commandsEditorContainerPanel.Controls.Add(this.commandsEditorTextBox);
            this.commandsEditorContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandsEditorContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.commandsEditorContainerPanel.Name = "commandsEditorContainerPanel";
            this.commandsEditorContainerPanel.Size = new System.Drawing.Size(948, 339);
            this.commandsEditorContainerPanel.TabIndex = 2;
            // 
            // commandsButtonsPanel
            // 
            this.commandsButtonsPanel.Controls.Add(this.addCommandButton);
            this.commandsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.commandsButtonsPanel.Location = new System.Drawing.Point(3, 342);
            this.commandsButtonsPanel.Name = "commandsButtonsPanel";
            this.commandsButtonsPanel.Size = new System.Drawing.Size(948, 44);
            this.commandsButtonsPanel.TabIndex = 1;
            // 
            // addCommandButton
            // 
            this.addCommandButton.Location = new System.Drawing.Point(6, 6);
            this.addCommandButton.Margin = new System.Windows.Forms.Padding(6);
            this.addCommandButton.Name = "addCommandButton";
            this.addCommandButton.Size = new System.Drawing.Size(126, 26);
            this.addCommandButton.TabIndex = 0;
            this.addCommandButton.Text = "Add command";
            this.addCommandButton.UseVisualStyleBackColor = true;
            this.addCommandButton.Click += new System.EventHandler(this.addCommandButton_Click);
            // 
            // triggersTabPage
            // 
            this.triggersTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.triggersTabPage.Controls.Add(this.triggersTableContainerPanel);
            this.triggersTabPage.Controls.Add(this.triggersButtonsPanel);
            this.triggersTabPage.Location = new System.Drawing.Point(4, 25);
            this.triggersTabPage.Name = "triggersTabPage";
            this.triggersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.triggersTabPage.Size = new System.Drawing.Size(954, 389);
            this.triggersTabPage.TabIndex = 2;
            this.triggersTabPage.Text = "Triggers";
            // 
            // triggersTableContainerPanel
            // 
            this.triggersTableContainerPanel.Controls.Add(this.triggersTable);
            this.triggersTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersTableContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.triggersTableContainerPanel.Name = "triggersTableContainerPanel";
            this.triggersTableContainerPanel.Size = new System.Drawing.Size(948, 339);
            this.triggersTableContainerPanel.TabIndex = 2;
            // 
            // triggersTable
            // 
            this.triggersTable.AllowUserToAddRows = false;
            this.triggersTable.AllowUserToDeleteRows = false;
            this.triggersTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.triggersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersTable.Location = new System.Drawing.Point(0, 0);
            this.triggersTable.Name = "triggersTable";
            this.triggersTable.ReadOnly = true;
            this.triggersTable.RowHeadersWidth = 51;
            this.triggersTable.RowTemplate.Height = 24;
            this.triggersTable.Size = new System.Drawing.Size(948, 339);
            this.triggersTable.TabIndex = 0;
            // 
            // triggersButtonsPanel
            // 
            this.triggersButtonsPanel.Controls.Add(this.addTriggerButton);
            this.triggersButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.triggersButtonsPanel.Location = new System.Drawing.Point(3, 342);
            this.triggersButtonsPanel.Name = "triggersButtonsPanel";
            this.triggersButtonsPanel.Size = new System.Drawing.Size(948, 44);
            this.triggersButtonsPanel.TabIndex = 1;
            // 
            // addTriggerButton
            // 
            this.addTriggerButton.Location = new System.Drawing.Point(6, 6);
            this.addTriggerButton.Margin = new System.Windows.Forms.Padding(6);
            this.addTriggerButton.Name = "addTriggerButton";
            this.addTriggerButton.Size = new System.Drawing.Size(126, 26);
            this.addTriggerButton.TabIndex = 0;
            this.addTriggerButton.Text = "Add trigger";
            this.addTriggerButton.UseVisualStyleBackColor = true;
            this.addTriggerButton.Click += new System.EventHandler(this.addTriggerButton_Click);
            // 
            // commandsEditorTextBox
            // 
            this.commandsEditorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandsEditorTextBox.Location = new System.Drawing.Point(0, 0);
            this.commandsEditorTextBox.Name = "commandsEditorTextBox";
            this.commandsEditorTextBox.Size = new System.Drawing.Size(948, 339);
            this.commandsEditorTextBox.TabIndex = 0;
            this.commandsEditorTextBox.Text = "";
            // 
            // MacroEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "Edit macro";
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MacroEditorForm";
            this.Text = "Edit macro";
            this.customElementsPanel.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.baseDataPanel.ResumeLayout(false);
            this.baseDataPanel.PerformLayout();
            this.baseDataGroupBox.ResumeLayout(false);
            this.baseDataGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.baseDataTabPage.ResumeLayout(false);
            this.baseDataTabPage.PerformLayout();
            this.commandsTabPage.ResumeLayout(false);
            this.commandsEditorContainerPanel.ResumeLayout(false);
            this.commandsButtonsPanel.ResumeLayout(false);
            this.triggersTabPage.ResumeLayout(false);
            this.triggersTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.triggersTable)).EndInit();
            this.triggersButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel baseDataPanel;
        private System.Windows.Forms.GroupBox baseDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage commandsTabPage;
        private System.Windows.Forms.Button addCommandButton;
        private System.Windows.Forms.TabPage triggersTabPage;
        private System.Windows.Forms.DataGridView triggersTable;
        private System.Windows.Forms.Button addTriggerButton;
        private System.Windows.Forms.Panel triggersTableContainerPanel;
        private System.Windows.Forms.Panel commandsEditorContainerPanel;
        protected System.Windows.Forms.TabPage baseDataTabPage;
        protected System.Windows.Forms.Panel commandsButtonsPanel;
        protected System.Windows.Forms.Panel triggersButtonsPanel;
        private System.Windows.Forms.RichTextBox commandsEditorTextBox;
    }
}