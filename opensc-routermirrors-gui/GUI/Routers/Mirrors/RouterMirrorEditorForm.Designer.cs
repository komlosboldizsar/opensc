namespace OpenSC.GUI.Routers.Mirrors
{
    partial class RouterMirrorEditorForm
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
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.baseDataTabPage = new System.Windows.Forms.TabPage();
            this.synchronizationPanel = new System.Windows.Forms.Panel();
            this.synchronizationGroupBox = new System.Windows.Forms.GroupBox();
            this.synchronizationTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.synchronizationBaseLabel = new System.Windows.Forms.Label();
            this.synchronizationModeDropDown = new System.Windows.Forms.ComboBox();
            this.routerSelectPanel = new System.Windows.Forms.Panel();
            this.routerSelectGroupBox = new System.Windows.Forms.GroupBox();
            this.routerSelectTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.routerBdropDown = new System.Windows.Forms.ComboBox();
            this.routerAlabel = new System.Windows.Forms.Label();
            this.routerBlabel = new System.Windows.Forms.Label();
            this.routerAdropDown = new System.Windows.Forms.ComboBox();
            this.inputsTabPage = new System.Windows.Forms.TabPage();
            this.inputsTableContainerPanel = new System.Windows.Forms.Panel();
            this.inputAssociationsTable = new System.Windows.Forms.DataGridView();
            this.inputsButtonsPanel = new System.Windows.Forms.Panel();
            this.set11InputAssociationsButton = new System.Windows.Forms.Button();
            this.clearInputAssociationsButton = new System.Windows.Forms.Button();
            this.outputsTabPage = new System.Windows.Forms.TabPage();
            this.outputsTableContainerPanel = new System.Windows.Forms.Panel();
            this.outputAssociationsTable = new System.Windows.Forms.DataGridView();
            this.outputsButtonsPanel = new System.Windows.Forms.Panel();
            this.set11OutputAssociationsButton = new System.Windows.Forms.Button();
            this.clearOutputAssociations = new System.Windows.Forms.Button();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.tabControl.SuspendLayout();
            this.baseDataTabPage.SuspendLayout();
            this.synchronizationPanel.SuspendLayout();
            this.synchronizationGroupBox.SuspendLayout();
            this.synchronizationTableLayout.SuspendLayout();
            this.routerSelectPanel.SuspendLayout();
            this.routerSelectGroupBox.SuspendLayout();
            this.routerSelectTableLayout.SuspendLayout();
            this.inputsTabPage.SuspendLayout();
            this.inputsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputAssociationsTable)).BeginInit();
            this.inputsButtonsPanel.SuspendLayout();
            this.outputsTabPage.SuspendLayout();
            this.outputsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputAssociationsTable)).BeginInit();
            this.outputsButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.tabControl);
            this.customElementsPanel.Size = new System.Drawing.Size(1082, 428);
            this.customElementsPanel.Controls.SetChildIndex(this.tabControl, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(1082, 497);
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
            this.nameTextBox.Size = new System.Drawing.Size(959, 22);
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
            this.tabControl.Controls.Add(this.inputsTabPage);
            this.tabControl.Controls.Add(this.outputsTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(10, 93);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1062, 335);
            this.tabControl.TabIndex = 1;
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.baseDataTabPage.Controls.Add(this.synchronizationPanel);
            this.baseDataTabPage.Controls.Add(this.routerSelectPanel);
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 25);
            this.baseDataTabPage.Name = "baseDataTabPage";
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.baseDataTabPage.Size = new System.Drawing.Size(1054, 306);
            this.baseDataTabPage.TabIndex = 0;
            this.baseDataTabPage.Text = "Base data";
            // 
            // synchronizationPanel
            // 
            this.synchronizationPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.synchronizationPanel.Controls.Add(this.synchronizationGroupBox);
            this.synchronizationPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.synchronizationPanel.Location = new System.Drawing.Point(3, 103);
            this.synchronizationPanel.Name = "synchronizationPanel";
            this.synchronizationPanel.Size = new System.Drawing.Size(1048, 100);
            this.synchronizationPanel.TabIndex = 3;
            // 
            // synchronizationGroupBox
            // 
            this.synchronizationGroupBox.AutoSize = true;
            this.synchronizationGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.synchronizationGroupBox.Controls.Add(this.synchronizationTableLayout);
            this.synchronizationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.synchronizationGroupBox.Location = new System.Drawing.Point(0, 0);
            this.synchronizationGroupBox.Name = "synchronizationGroupBox";
            this.synchronizationGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.synchronizationGroupBox.Size = new System.Drawing.Size(1048, 61);
            this.synchronizationGroupBox.TabIndex = 1;
            this.synchronizationGroupBox.TabStop = false;
            this.synchronizationGroupBox.Text = "Routers";
            // 
            // synchronizationTableLayout
            // 
            this.synchronizationTableLayout.AutoSize = true;
            this.synchronizationTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.synchronizationTableLayout.ColumnCount = 2;
            this.synchronizationTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.synchronizationTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.synchronizationTableLayout.Controls.Add(this.synchronizationBaseLabel, 0, 0);
            this.synchronizationTableLayout.Controls.Add(this.synchronizationModeDropDown, 1, 0);
            this.synchronizationTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.synchronizationTableLayout.Location = new System.Drawing.Point(8, 23);
            this.synchronizationTableLayout.Name = "synchronizationTableLayout";
            this.synchronizationTableLayout.RowCount = 1;
            this.synchronizationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.synchronizationTableLayout.Size = new System.Drawing.Size(1032, 30);
            this.synchronizationTableLayout.TabIndex = 0;
            // 
            // synchronizationBaseLabel
            // 
            this.synchronizationBaseLabel.AutoSize = true;
            this.synchronizationBaseLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.synchronizationBaseLabel.Location = new System.Drawing.Point(3, 0);
            this.synchronizationBaseLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.synchronizationBaseLabel.Name = "synchronizationBaseLabel";
            this.synchronizationBaseLabel.Size = new System.Drawing.Size(200, 30);
            this.synchronizationBaseLabel.TabIndex = 0;
            this.synchronizationBaseLabel.Text = "Synchronization on reconnect:";
            this.synchronizationBaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // synchronizationModeDropDown
            // 
            this.synchronizationModeDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.synchronizationModeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.synchronizationModeDropDown.FormattingEnabled = true;
            this.synchronizationModeDropDown.Location = new System.Drawing.Point(221, 3);
            this.synchronizationModeDropDown.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.synchronizationModeDropDown.Name = "synchronizationModeDropDown";
            this.synchronizationModeDropDown.Size = new System.Drawing.Size(208, 24);
            this.synchronizationModeDropDown.TabIndex = 3;
            // 
            // routerSelectPanel
            // 
            this.routerSelectPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerSelectPanel.Controls.Add(this.routerSelectGroupBox);
            this.routerSelectPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.routerSelectPanel.Location = new System.Drawing.Point(3, 3);
            this.routerSelectPanel.Name = "routerSelectPanel";
            this.routerSelectPanel.Size = new System.Drawing.Size(1048, 100);
            this.routerSelectPanel.TabIndex = 2;
            // 
            // routerSelectGroupBox
            // 
            this.routerSelectGroupBox.AutoSize = true;
            this.routerSelectGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerSelectGroupBox.Controls.Add(this.routerSelectTableLayout);
            this.routerSelectGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.routerSelectGroupBox.Location = new System.Drawing.Point(0, 0);
            this.routerSelectGroupBox.Name = "routerSelectGroupBox";
            this.routerSelectGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.routerSelectGroupBox.Size = new System.Drawing.Size(1048, 91);
            this.routerSelectGroupBox.TabIndex = 1;
            this.routerSelectGroupBox.TabStop = false;
            this.routerSelectGroupBox.Text = "Routers";
            // 
            // routerSelectTableLayout
            // 
            this.routerSelectTableLayout.AutoSize = true;
            this.routerSelectTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerSelectTableLayout.ColumnCount = 2;
            this.routerSelectTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.routerSelectTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.routerSelectTableLayout.Controls.Add(this.routerBdropDown, 1, 1);
            this.routerSelectTableLayout.Controls.Add(this.routerAlabel, 0, 0);
            this.routerSelectTableLayout.Controls.Add(this.routerBlabel, 0, 1);
            this.routerSelectTableLayout.Controls.Add(this.routerAdropDown, 1, 0);
            this.routerSelectTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routerSelectTableLayout.Location = new System.Drawing.Point(8, 23);
            this.routerSelectTableLayout.Name = "routerSelectTableLayout";
            this.routerSelectTableLayout.RowCount = 2;
            this.routerSelectTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerSelectTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerSelectTableLayout.Size = new System.Drawing.Size(1032, 60);
            this.routerSelectTableLayout.TabIndex = 0;
            // 
            // routerBdropDown
            // 
            this.routerBdropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerBdropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerBdropDown.FormattingEnabled = true;
            this.routerBdropDown.Location = new System.Drawing.Point(89, 33);
            this.routerBdropDown.Name = "routerBdropDown";
            this.routerBdropDown.Size = new System.Drawing.Size(203, 24);
            this.routerBdropDown.TabIndex = 4;
            // 
            // routerAlabel
            // 
            this.routerAlabel.AutoSize = true;
            this.routerAlabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerAlabel.Location = new System.Drawing.Point(3, 0);
            this.routerAlabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerAlabel.Name = "routerAlabel";
            this.routerAlabel.Size = new System.Drawing.Size(68, 30);
            this.routerAlabel.TabIndex = 0;
            this.routerAlabel.Text = "Router A:";
            this.routerAlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerBlabel
            // 
            this.routerBlabel.AutoSize = true;
            this.routerBlabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerBlabel.Location = new System.Drawing.Point(3, 30);
            this.routerBlabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerBlabel.Name = "routerBlabel";
            this.routerBlabel.Size = new System.Drawing.Size(68, 30);
            this.routerBlabel.TabIndex = 1;
            this.routerBlabel.Text = "Router B:";
            this.routerBlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerAdropDown
            // 
            this.routerAdropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerAdropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerAdropDown.FormattingEnabled = true;
            this.routerAdropDown.Location = new System.Drawing.Point(89, 3);
            this.routerAdropDown.Name = "routerAdropDown";
            this.routerAdropDown.Size = new System.Drawing.Size(203, 24);
            this.routerAdropDown.TabIndex = 3;
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
            this.inputsTableContainerPanel.Controls.Add(this.inputAssociationsTable);
            this.inputsTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsTableContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.inputsTableContainerPanel.Name = "inputsTableContainerPanel";
            this.inputsTableContainerPanel.Size = new System.Drawing.Size(484, 96);
            this.inputsTableContainerPanel.TabIndex = 2;
            // 
            // inputAssociationsTable
            // 
            this.inputAssociationsTable.AllowUserToAddRows = false;
            this.inputAssociationsTable.AllowUserToDeleteRows = false;
            this.inputAssociationsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputAssociationsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputAssociationsTable.Location = new System.Drawing.Point(0, 0);
            this.inputAssociationsTable.Name = "inputAssociationsTable";
            this.inputAssociationsTable.ReadOnly = true;
            this.inputAssociationsTable.RowHeadersWidth = 51;
            this.inputAssociationsTable.RowTemplate.Height = 24;
            this.inputAssociationsTable.Size = new System.Drawing.Size(484, 96);
            this.inputAssociationsTable.TabIndex = 0;
            // 
            // inputsButtonsPanel
            // 
            this.inputsButtonsPanel.Controls.Add(this.set11InputAssociationsButton);
            this.inputsButtonsPanel.Controls.Add(this.clearInputAssociationsButton);
            this.inputsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputsButtonsPanel.Location = new System.Drawing.Point(3, 99);
            this.inputsButtonsPanel.Name = "inputsButtonsPanel";
            this.inputsButtonsPanel.Size = new System.Drawing.Size(484, 44);
            this.inputsButtonsPanel.TabIndex = 1;
            // 
            // set11InputAssociationsButton
            // 
            this.set11InputAssociationsButton.Location = new System.Drawing.Point(197, 6);
            this.set11InputAssociationsButton.Margin = new System.Windows.Forms.Padding(6);
            this.set11InputAssociationsButton.Name = "set11InputAssociationsButton";
            this.set11InputAssociationsButton.Size = new System.Drawing.Size(179, 26);
            this.set11InputAssociationsButton.TabIndex = 1;
            this.set11InputAssociationsButton.Text = "Set 1:1";
            this.set11InputAssociationsButton.UseVisualStyleBackColor = true;
            this.set11InputAssociationsButton.Click += new System.EventHandler(this.set11InputAssociationsButton_Click);
            // 
            // clearInputAssociationsButton
            // 
            this.clearInputAssociationsButton.Location = new System.Drawing.Point(6, 6);
            this.clearInputAssociationsButton.Margin = new System.Windows.Forms.Padding(6);
            this.clearInputAssociationsButton.Name = "clearInputAssociationsButton";
            this.clearInputAssociationsButton.Size = new System.Drawing.Size(179, 26);
            this.clearInputAssociationsButton.TabIndex = 0;
            this.clearInputAssociationsButton.Text = "Clear associations";
            this.clearInputAssociationsButton.UseVisualStyleBackColor = true;
            this.clearInputAssociationsButton.Click += new System.EventHandler(this.clearInputAssociationsButton_Click);
            // 
            // outputsTabPage
            // 
            this.outputsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.outputsTabPage.Controls.Add(this.outputsTableContainerPanel);
            this.outputsTabPage.Controls.Add(this.outputsButtonsPanel);
            this.outputsTabPage.Location = new System.Drawing.Point(4, 25);
            this.outputsTabPage.Name = "outputsTabPage";
            this.outputsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outputsTabPage.Size = new System.Drawing.Size(490, 146);
            this.outputsTabPage.TabIndex = 2;
            this.outputsTabPage.Text = "Outputs";
            // 
            // outputsTableContainerPanel
            // 
            this.outputsTableContainerPanel.Controls.Add(this.outputAssociationsTable);
            this.outputsTableContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputsTableContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.outputsTableContainerPanel.Name = "outputsTableContainerPanel";
            this.outputsTableContainerPanel.Size = new System.Drawing.Size(484, 96);
            this.outputsTableContainerPanel.TabIndex = 2;
            // 
            // outputAssociationsTable
            // 
            this.outputAssociationsTable.AllowUserToAddRows = false;
            this.outputAssociationsTable.AllowUserToDeleteRows = false;
            this.outputAssociationsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputAssociationsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputAssociationsTable.Location = new System.Drawing.Point(0, 0);
            this.outputAssociationsTable.Name = "outputAssociationsTable";
            this.outputAssociationsTable.ReadOnly = true;
            this.outputAssociationsTable.RowHeadersWidth = 51;
            this.outputAssociationsTable.RowTemplate.Height = 24;
            this.outputAssociationsTable.Size = new System.Drawing.Size(484, 96);
            this.outputAssociationsTable.TabIndex = 0;
            // 
            // outputsButtonsPanel
            // 
            this.outputsButtonsPanel.Controls.Add(this.set11OutputAssociationsButton);
            this.outputsButtonsPanel.Controls.Add(this.clearOutputAssociations);
            this.outputsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.outputsButtonsPanel.Location = new System.Drawing.Point(3, 99);
            this.outputsButtonsPanel.Name = "outputsButtonsPanel";
            this.outputsButtonsPanel.Size = new System.Drawing.Size(484, 44);
            this.outputsButtonsPanel.TabIndex = 1;
            // 
            // set11OutputAssociationsButton
            // 
            this.set11OutputAssociationsButton.Location = new System.Drawing.Point(197, 6);
            this.set11OutputAssociationsButton.Margin = new System.Windows.Forms.Padding(6);
            this.set11OutputAssociationsButton.Name = "set11OutputAssociationsButton";
            this.set11OutputAssociationsButton.Size = new System.Drawing.Size(179, 26);
            this.set11OutputAssociationsButton.TabIndex = 2;
            this.set11OutputAssociationsButton.Text = "Set 1:1";
            this.set11OutputAssociationsButton.UseVisualStyleBackColor = true;
            this.set11OutputAssociationsButton.Click += new System.EventHandler(this.set11OutputAssociationsButton_Click);
            // 
            // clearOutputAssociations
            // 
            this.clearOutputAssociations.Location = new System.Drawing.Point(6, 6);
            this.clearOutputAssociations.Margin = new System.Windows.Forms.Padding(6);
            this.clearOutputAssociations.Name = "clearOutputAssociations";
            this.clearOutputAssociations.Size = new System.Drawing.Size(179, 26);
            this.clearOutputAssociations.TabIndex = 0;
            this.clearOutputAssociations.Text = "Clear associations";
            this.clearOutputAssociations.UseVisualStyleBackColor = true;
            this.clearOutputAssociations.Click += new System.EventHandler(this.clearOutputAssociations_Click);
            // 
            // RouterMirrorEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 553);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New router mirror";
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "RouterMirrorEditorForm";
            this.SubjectPlural = "router mirrors";
            this.SubjectSingular = "router mirror";
            this.Text = "New router mirror";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.baseDataTabPage.ResumeLayout(false);
            this.synchronizationPanel.ResumeLayout(false);
            this.synchronizationPanel.PerformLayout();
            this.synchronizationGroupBox.ResumeLayout(false);
            this.synchronizationGroupBox.PerformLayout();
            this.synchronizationTableLayout.ResumeLayout(false);
            this.synchronizationTableLayout.PerformLayout();
            this.routerSelectPanel.ResumeLayout(false);
            this.routerSelectPanel.PerformLayout();
            this.routerSelectGroupBox.ResumeLayout(false);
            this.routerSelectGroupBox.PerformLayout();
            this.routerSelectTableLayout.ResumeLayout(false);
            this.routerSelectTableLayout.PerformLayout();
            this.inputsTabPage.ResumeLayout(false);
            this.inputsTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputAssociationsTable)).EndInit();
            this.inputsButtonsPanel.ResumeLayout(false);
            this.outputsTabPage.ResumeLayout(false);
            this.outputsTableContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputAssociationsTable)).EndInit();
            this.outputsButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage inputsTabPage;
        private System.Windows.Forms.DataGridView inputAssociationsTable;
        private System.Windows.Forms.Button clearInputAssociationsButton;
        private System.Windows.Forms.TabPage outputsTabPage;
        private System.Windows.Forms.DataGridView outputAssociationsTable;
        private System.Windows.Forms.Button clearOutputAssociations;
        private System.Windows.Forms.Panel outputsTableContainerPanel;
        private System.Windows.Forms.Panel inputsTableContainerPanel;
        protected System.Windows.Forms.TabPage baseDataTabPage;
        protected System.Windows.Forms.Panel inputsButtonsPanel;
        protected System.Windows.Forms.Panel outputsButtonsPanel;
        private System.Windows.Forms.GroupBox routerSelectGroupBox;
        private System.Windows.Forms.TableLayoutPanel routerSelectTableLayout;
        private System.Windows.Forms.ComboBox routerBdropDown;
        private System.Windows.Forms.Label routerAlabel;
        private System.Windows.Forms.Label routerBlabel;
        private System.Windows.Forms.ComboBox routerAdropDown;
        private System.Windows.Forms.Panel routerSelectPanel;
        private System.Windows.Forms.Panel synchronizationPanel;
        private System.Windows.Forms.GroupBox synchronizationGroupBox;
        private System.Windows.Forms.TableLayoutPanel synchronizationTableLayout;
        private System.Windows.Forms.Label synchronizationBaseLabel;
        private System.Windows.Forms.ComboBox synchronizationModeDropDown;
        private System.Windows.Forms.Button set11InputAssociationsButton;
        private System.Windows.Forms.Button set11OutputAssociationsButton;
    }
}