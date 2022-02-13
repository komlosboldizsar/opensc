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
            this.talliesTabPage = new System.Windows.Forms.TabPage();
            this.talliesPanel = new System.Windows.Forms.Panel();
            this.talliesGroupBox = new System.Windows.Forms.GroupBox();
            this.talliesTable = new System.Windows.Forms.TableLayoutPanel();
            this.userMixersRedTallyLabel = new System.Windows.Forms.Label();
            this.userMixersGreenTallyLabel = new System.Windows.Forms.Label();
            this.userMixersRedTallyCheckbox = new System.Windows.Forms.CheckBox();
            this.userMixersGreenTallyCheckbox = new System.Windows.Forms.CheckBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.inputsTabPage.SuspendLayout();
            this.inputsTableContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputsTable)).BeginInit();
            this.inputsButtonsPanel.SuspendLayout();
            this.talliesTabPage.SuspendLayout();
            this.talliesPanel.SuspendLayout();
            this.talliesGroupBox.SuspendLayout();
            this.talliesTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.tabControl);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 15, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(832, 432);
            this.customElementsPanel.Controls.SetChildIndex(this.tabControl, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Size = new System.Drawing.Size(832, 518);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.baseDataTabPage);
            this.tabControl.Controls.Add(this.inputsTabPage);
            this.tabControl.Controls.Add(this.talliesTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(10, 120);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(812, 312);
            this.tabControl.TabIndex = 1;
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 29);
            this.baseDataTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.baseDataTabPage.Name = "baseDataTabPage";
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.baseDataTabPage.Size = new System.Drawing.Size(804, 279);
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
            this.inputsButtonsPanel.Controls.Add(this.addInputButton);
            this.inputsButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputsButtonsPanel.Location = new System.Drawing.Point(3, 123);
            this.inputsButtonsPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputsButtonsPanel.Name = "inputsButtonsPanel";
            this.inputsButtonsPanel.Size = new System.Drawing.Size(484, 55);
            this.inputsButtonsPanel.TabIndex = 1;
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
            // talliesTabPage
            // 
            this.talliesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.talliesTabPage.Controls.Add(this.talliesPanel);
            this.talliesTabPage.Location = new System.Drawing.Point(4, 29);
            this.talliesTabPage.Name = "talliesTabPage";
            this.talliesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.talliesTabPage.Size = new System.Drawing.Size(804, 279);
            this.talliesTabPage.TabIndex = 2;
            this.talliesTabPage.Text = "Tallies";
            // 
            // talliesPanel
            // 
            this.talliesPanel.AutoSize = true;
            this.talliesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesPanel.Controls.Add(this.talliesGroupBox);
            this.talliesPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.talliesPanel.Location = new System.Drawing.Point(3, 3);
            this.talliesPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesPanel.Name = "talliesPanel";
            this.talliesPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.talliesPanel.Size = new System.Drawing.Size(798, 95);
            this.talliesPanel.TabIndex = 0;
            // 
            // talliesGroupBox
            // 
            this.talliesGroupBox.AutoSize = true;
            this.talliesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesGroupBox.Controls.Add(this.talliesTable);
            this.talliesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talliesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.talliesGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesGroupBox.Name = "talliesGroupBox";
            this.talliesGroupBox.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.talliesGroupBox.Size = new System.Drawing.Size(798, 86);
            this.talliesGroupBox.TabIndex = 0;
            this.talliesGroupBox.TabStop = false;
            this.talliesGroupBox.Text = "Use tallies of mixer";
            // 
            // talliesTable
            // 
            this.talliesTable.AutoSize = true;
            this.talliesTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesTable.ColumnCount = 2;
            this.talliesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.talliesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.talliesTable.Controls.Add(this.userMixersRedTallyLabel, 0, 0);
            this.talliesTable.Controls.Add(this.userMixersGreenTallyLabel, 0, 1);
            this.talliesTable.Controls.Add(this.userMixersRedTallyCheckbox, 1, 0);
            this.talliesTable.Controls.Add(this.userMixersGreenTallyCheckbox, 1, 1);
            this.talliesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talliesTable.Location = new System.Drawing.Point(8, 30);
            this.talliesTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesTable.Name = "talliesTable";
            this.talliesTable.RowCount = 2;
            this.talliesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.talliesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.talliesTable.Size = new System.Drawing.Size(782, 46);
            this.talliesTable.TabIndex = 0;
            // 
            // userMixersRedTallyLabel
            // 
            this.userMixersRedTallyLabel.AutoSize = true;
            this.userMixersRedTallyLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.userMixersRedTallyLabel.Location = new System.Drawing.Point(3, 0);
            this.userMixersRedTallyLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.userMixersRedTallyLabel.Name = "userMixersRedTallyLabel";
            this.userMixersRedTallyLabel.Size = new System.Drawing.Size(38, 23);
            this.userMixersRedTallyLabel.TabIndex = 0;
            this.userMixersRedTallyLabel.Text = "Red:";
            this.userMixersRedTallyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // userMixersGreenTallyLabel
            // 
            this.userMixersGreenTallyLabel.AutoSize = true;
            this.userMixersGreenTallyLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.userMixersGreenTallyLabel.Location = new System.Drawing.Point(3, 23);
            this.userMixersGreenTallyLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.userMixersGreenTallyLabel.Name = "userMixersGreenTallyLabel";
            this.userMixersGreenTallyLabel.Size = new System.Drawing.Size(51, 23);
            this.userMixersGreenTallyLabel.TabIndex = 1;
            this.userMixersGreenTallyLabel.Text = "Green:";
            this.userMixersGreenTallyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // userMixersRedTallyCheckbox
            // 
            this.userMixersRedTallyCheckbox.AutoSize = true;
            this.userMixersRedTallyCheckbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.userMixersRedTallyCheckbox.Location = new System.Drawing.Point(72, 3);
            this.userMixersRedTallyCheckbox.Name = "userMixersRedTallyCheckbox";
            this.userMixersRedTallyCheckbox.Size = new System.Drawing.Size(18, 17);
            this.userMixersRedTallyCheckbox.TabIndex = 2;
            this.userMixersRedTallyCheckbox.UseVisualStyleBackColor = true;
            // 
            // userMixersGreenTallyCheckbox
            // 
            this.userMixersGreenTallyCheckbox.AutoSize = true;
            this.userMixersGreenTallyCheckbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.userMixersGreenTallyCheckbox.Location = new System.Drawing.Point(72, 26);
            this.userMixersGreenTallyCheckbox.Name = "userMixersGreenTallyCheckbox";
            this.userMixersGreenTallyCheckbox.Size = new System.Drawing.Size(18, 17);
            this.userMixersGreenTallyCheckbox.TabIndex = 3;
            this.userMixersGreenTallyCheckbox.UseVisualStyleBackColor = true;
            // 
            // MixerEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 588);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New mixer";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(850, 613);
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
            this.talliesTabPage.ResumeLayout(false);
            this.talliesTabPage.PerformLayout();
            this.talliesPanel.ResumeLayout(false);
            this.talliesPanel.PerformLayout();
            this.talliesGroupBox.ResumeLayout(false);
            this.talliesGroupBox.PerformLayout();
            this.talliesTable.ResumeLayout(false);
            this.talliesTable.PerformLayout();
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
        private System.Windows.Forms.TabPage talliesTabPage;
        private System.Windows.Forms.Panel talliesPanel;
        private System.Windows.Forms.GroupBox talliesGroupBox;
        private System.Windows.Forms.TableLayoutPanel talliesTable;
        private System.Windows.Forms.Label userMixersRedTallyLabel;
        private System.Windows.Forms.Label userMixersGreenTallyLabel;
        private System.Windows.Forms.CheckBox userMixersRedTallyCheckbox;
        private System.Windows.Forms.CheckBox userMixersGreenTallyCheckbox;
    }
}