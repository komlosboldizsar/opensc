namespace OpenSC.GUI.UMDs
{
    partial class UmdEditorFormBase
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
            this.operationGroupBox = new System.Windows.Forms.GroupBox();
            this.modeTable = new System.Windows.Forms.TableLayoutPanel();
            this.staticTextLabel = new System.Windows.Forms.Label();
            this.currentTextLabel = new System.Windows.Forms.Label();
            this.currentTextTextBox = new System.Windows.Forms.TextBox();
            this.staticTextTextBox = new System.Windows.Forms.TextBox();
            this.useStaticTextLabel = new System.Windows.Forms.Label();
            this.useStaticTextCheckBox = new System.Windows.Forms.CheckBox();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.baseDataTabPage = new System.Windows.Forms.TabPage();
            this.dynamicTextTabPage = new System.Windows.Forms.TabPage();
            this.talliesTabPage = new System.Windows.Forms.TabPage();
            this.talliesGroupBox = new System.Windows.Forms.GroupBox();
            this.talliesTable = new System.Windows.Forms.TableLayoutPanel();
            this.tallyExampleSourceLabel = new System.Windows.Forms.Label();
            this.tallySourceExampleComboBox = new System.Windows.Forms.ComboBox();
            this.dynamicDataGroupBox = new System.Windows.Forms.GroupBox();
            this.dynamicSourcesTable = new System.Windows.Forms.TableLayoutPanel();
            this.dynamicSourceLabel1 = new System.Windows.Forms.Label();
            this.dynamicSourceDropDown1 = new System.Windows.Forms.ComboBox();
            this.dynamicSourceAlignmentDropDown1 = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.operationGroupBox.SuspendLayout();
            this.modeTable.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.baseDataTabPage.SuspendLayout();
            this.dynamicTextTabPage.SuspendLayout();
            this.talliesTabPage.SuspendLayout();
            this.talliesGroupBox.SuspendLayout();
            this.talliesTable.SuspendLayout();
            this.dynamicDataGroupBox.SuspendLayout();
            this.dynamicSourcesTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.mainTabControl);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 12);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 362);
            this.customElementsPanel.Controls.SetChildIndex(this.mainTabControl, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.mainContainer.Size = new System.Drawing.Size(509, 472);
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.AutoSize = true;
            this.operationGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operationGroupBox.Controls.Add(this.modeTable);
            this.operationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.operationGroupBox.Location = new System.Drawing.Point(3, 4);
            this.operationGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.operationGroupBox.Name = "operationGroupBox";
            this.operationGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.operationGroupBox.Size = new System.Drawing.Size(475, 125);
            this.operationGroupBox.TabIndex = 2;
            this.operationGroupBox.TabStop = false;
            this.operationGroupBox.Text = "Content";
            // 
            // modeTable
            // 
            this.modeTable.AutoSize = true;
            this.modeTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modeTable.ColumnCount = 2;
            this.modeTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.modeTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modeTable.Controls.Add(this.staticTextLabel, 0, 1);
            this.modeTable.Controls.Add(this.currentTextLabel, 0, 0);
            this.modeTable.Controls.Add(this.currentTextTextBox, 1, 0);
            this.modeTable.Controls.Add(this.staticTextTextBox, 1, 1);
            this.modeTable.Controls.Add(this.useStaticTextLabel, 0, 2);
            this.modeTable.Controls.Add(this.useStaticTextCheckBox, 1, 2);
            this.modeTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeTable.Location = new System.Drawing.Point(8, 25);
            this.modeTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modeTable.Name = "modeTable";
            this.modeTable.RowCount = 3;
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.modeTable.Size = new System.Drawing.Size(459, 90);
            this.modeTable.TabIndex = 0;
            // 
            // staticTextLabel
            // 
            this.staticTextLabel.AutoSize = true;
            this.staticTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.staticTextLabel.Location = new System.Drawing.Point(3, 30);
            this.staticTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.staticTextLabel.Name = "staticTextLabel";
            this.staticTextLabel.Size = new System.Drawing.Size(75, 35);
            this.staticTextLabel.TabIndex = 1;
            this.staticTextLabel.Text = "Static text";
            this.staticTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // currentTextLabel
            // 
            this.currentTextLabel.AutoSize = true;
            this.currentTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.currentTextLabel.Location = new System.Drawing.Point(3, 0);
            this.currentTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.currentTextLabel.Name = "currentTextLabel";
            this.currentTextLabel.Size = new System.Drawing.Size(86, 30);
            this.currentTextLabel.TabIndex = 4;
            this.currentTextLabel.Text = "Current text";
            this.currentTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // currentTextTextBox
            // 
            this.currentTextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentTextTextBox.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.currentTextTextBox.Location = new System.Drawing.Point(122, 4);
            this.currentTextTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.currentTextTextBox.Name = "currentTextTextBox";
            this.currentTextTextBox.ReadOnly = true;
            this.currentTextTextBox.Size = new System.Drawing.Size(334, 22);
            this.currentTextTextBox.TabIndex = 5;
            // 
            // staticTextTextBox
            // 
            this.staticTextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.staticTextTextBox.Location = new System.Drawing.Point(122, 34);
            this.staticTextTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.staticTextTextBox.Name = "staticTextTextBox";
            this.staticTextTextBox.Size = new System.Drawing.Size(334, 27);
            this.staticTextTextBox.TabIndex = 6;
            // 
            // useStaticTextLabel
            // 
            this.useStaticTextLabel.AutoSize = true;
            this.useStaticTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.useStaticTextLabel.Location = new System.Drawing.Point(3, 65);
            this.useStaticTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.useStaticTextLabel.Name = "useStaticTextLabel";
            this.useStaticTextLabel.Size = new System.Drawing.Size(101, 25);
            this.useStaticTextLabel.TabIndex = 7;
            this.useStaticTextLabel.Text = "Use static text";
            this.useStaticTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // useStaticTextCheckBox
            // 
            this.useStaticTextCheckBox.AutoSize = true;
            this.useStaticTextCheckBox.Location = new System.Drawing.Point(122, 69);
            this.useStaticTextCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.useStaticTextCheckBox.Name = "useStaticTextCheckBox";
            this.useStaticTextCheckBox.Size = new System.Drawing.Size(18, 17);
            this.useStaticTextCheckBox.TabIndex = 8;
            this.useStaticTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.baseDataTabPage);
            this.mainTabControl.Controls.Add(this.dynamicTextTabPage);
            this.mainTabControl.Controls.Add(this.talliesTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 107);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(489, 255);
            this.mainTabControl.TabIndex = 3;
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.baseDataTabPage.Controls.Add(this.operationGroupBox);
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 29);
            this.baseDataTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.baseDataTabPage.Name = "baseDataTabPage";
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.baseDataTabPage.Size = new System.Drawing.Size(481, 222);
            this.baseDataTabPage.TabIndex = 0;
            this.baseDataTabPage.Text = "Base data";
            // 
            // dynamicTextTabPage
            // 
            this.dynamicTextTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.dynamicTextTabPage.Controls.Add(this.dynamicDataGroupBox);
            this.dynamicTextTabPage.Location = new System.Drawing.Point(4, 29);
            this.dynamicTextTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dynamicTextTabPage.Name = "dynamicTextTabPage";
            this.dynamicTextTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dynamicTextTabPage.Size = new System.Drawing.Size(481, 222);
            this.dynamicTextTabPage.TabIndex = 1;
            this.dynamicTextTabPage.Text = "Dynamic text";
            // 
            // talliesTabPage
            // 
            this.talliesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.talliesTabPage.Controls.Add(this.talliesGroupBox);
            this.talliesTabPage.Location = new System.Drawing.Point(4, 29);
            this.talliesTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesTabPage.Name = "talliesTabPage";
            this.talliesTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesTabPage.Size = new System.Drawing.Size(481, 222);
            this.talliesTabPage.TabIndex = 2;
            this.talliesTabPage.Text = "Tallies";
            // 
            // talliesGroupBox
            // 
            this.talliesGroupBox.AutoSize = true;
            this.talliesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesGroupBox.Controls.Add(this.talliesTable);
            this.talliesGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.talliesGroupBox.Location = new System.Drawing.Point(3, 4);
            this.talliesGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.talliesGroupBox.Name = "talliesGroupBox";
            this.talliesGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.talliesGroupBox.Size = new System.Drawing.Size(475, 71);
            this.talliesGroupBox.TabIndex = 2;
            this.talliesGroupBox.TabStop = false;
            this.talliesGroupBox.Text = "Tally sources";
            // 
            // talliesTable
            // 
            this.talliesTable.AutoSize = true;
            this.talliesTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesTable.ColumnCount = 2;
            this.talliesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.talliesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.talliesTable.Controls.Add(this.tallyExampleSourceLabel, 0, 0);
            this.talliesTable.Controls.Add(this.tallySourceExampleComboBox, 1, 0);
            this.talliesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talliesTable.Location = new System.Drawing.Point(8, 25);
            this.talliesTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.talliesTable.Name = "talliesTable";
            this.talliesTable.RowCount = 1;
            this.talliesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.talliesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.talliesTable.Size = new System.Drawing.Size(459, 36);
            this.talliesTable.TabIndex = 0;
            // 
            // tallyExampleSourceLabel
            // 
            this.tallyExampleSourceLabel.AutoSize = true;
            this.tallyExampleSourceLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tallyExampleSourceLabel.Location = new System.Drawing.Point(3, 0);
            this.tallyExampleSourceLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.tallyExampleSourceLabel.Name = "tallyExampleSourceLabel";
            this.tallyExampleSourceLabel.Size = new System.Drawing.Size(106, 36);
            this.tallyExampleSourceLabel.TabIndex = 0;
            this.tallyExampleSourceLabel.Text = "Tally #1 source";
            this.tallyExampleSourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tallySourceExampleComboBox
            // 
            this.tallySourceExampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tallySourceExampleComboBox.FormattingEnabled = true;
            this.tallySourceExampleComboBox.Location = new System.Drawing.Point(127, 4);
            this.tallySourceExampleComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tallySourceExampleComboBox.Name = "tallySourceExampleComboBox";
            this.tallySourceExampleComboBox.Size = new System.Drawing.Size(279, 28);
            this.tallySourceExampleComboBox.TabIndex = 4;
            // 
            // dynamicDataGroupBox
            // 
            this.dynamicDataGroupBox.AutoSize = true;
            this.dynamicDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dynamicDataGroupBox.Controls.Add(this.dynamicSourcesTable);
            this.dynamicDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.dynamicDataGroupBox.Location = new System.Drawing.Point(3, 4);
            this.dynamicDataGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.dynamicDataGroupBox.Name = "dynamicDataGroupBox";
            this.dynamicDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 10, 4, 10);
            this.dynamicDataGroupBox.Size = new System.Drawing.Size(475, 74);
            this.dynamicDataGroupBox.TabIndex = 0;
            this.dynamicDataGroupBox.TabStop = false;
            this.dynamicDataGroupBox.Text = "Text sources";
            // 
            // dynamicSourcesTable
            // 
            this.dynamicSourcesTable.AutoSize = true;
            this.dynamicSourcesTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dynamicSourcesTable.ColumnCount = 3;
            this.dynamicSourcesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dynamicSourcesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dynamicSourcesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dynamicSourcesTable.Controls.Add(this.dynamicSourceLabel1, 0, 0);
            this.dynamicSourcesTable.Controls.Add(this.dynamicSourceDropDown1, 1, 0);
            this.dynamicSourcesTable.Controls.Add(this.dynamicSourceAlignmentDropDown1, 2, 0);
            this.dynamicSourcesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dynamicSourcesTable.Location = new System.Drawing.Point(8, 30);
            this.dynamicSourcesTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dynamicSourcesTable.Name = "dynamicSourcesTable";
            this.dynamicSourcesTable.RowCount = 1;
            this.dynamicSourcesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dynamicSourcesTable.Size = new System.Drawing.Size(463, 34);
            this.dynamicSourcesTable.TabIndex = 0;
            // 
            // dynamicSourceLabel1
            // 
            this.dynamicSourceLabel1.AutoSize = true;
            this.dynamicSourceLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dynamicSourceLabel1.Location = new System.Drawing.Point(3, 0);
            this.dynamicSourceLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.dynamicSourceLabel1.Name = "dynamicSourceLabel1";
            this.dynamicSourceLabel1.Size = new System.Drawing.Size(39, 34);
            this.dynamicSourceLabel1.TabIndex = 0;
            this.dynamicSourceLabel1.Text = "Text:";
            this.dynamicSourceLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dynamicSourceDropDown1
            // 
            this.dynamicSourceDropDown1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dynamicSourceDropDown1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dynamicSourceDropDown1.FormattingEnabled = true;
            this.dynamicSourceDropDown1.Location = new System.Drawing.Point(60, 3);
            this.dynamicSourceDropDown1.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.dynamicSourceDropDown1.Name = "dynamicSourceDropDown1";
            this.dynamicSourceDropDown1.Size = new System.Drawing.Size(151, 28);
            this.dynamicSourceDropDown1.TabIndex = 1;
            // 
            // dynamicSourceAlignmentDropDown1
            // 
            this.dynamicSourceAlignmentDropDown1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dynamicSourceAlignmentDropDown1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dynamicSourceAlignmentDropDown1.FormattingEnabled = true;
            this.dynamicSourceAlignmentDropDown1.Location = new System.Drawing.Point(229, 3);
            this.dynamicSourceAlignmentDropDown1.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.dynamicSourceAlignmentDropDown1.Name = "dynamicSourceAlignmentDropDown1";
            this.dynamicSourceAlignmentDropDown1.Size = new System.Drawing.Size(151, 28);
            this.dynamicSourceAlignmentDropDown1.TabIndex = 2;
            // 
            // UmdEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 542);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New UMD";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(500, 538);
            this.Name = "UmdEditorFormBase";
            this.SubjectPlural = "UMDs";
            this.SubjectSingular = "UMD";
            this.Text = "New UMD";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.operationGroupBox.ResumeLayout(false);
            this.operationGroupBox.PerformLayout();
            this.modeTable.ResumeLayout(false);
            this.modeTable.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.baseDataTabPage.ResumeLayout(false);
            this.baseDataTabPage.PerformLayout();
            this.dynamicTextTabPage.ResumeLayout(false);
            this.dynamicTextTabPage.PerformLayout();
            this.talliesTabPage.ResumeLayout(false);
            this.talliesTabPage.PerformLayout();
            this.talliesGroupBox.ResumeLayout(false);
            this.talliesGroupBox.PerformLayout();
            this.talliesTable.ResumeLayout(false);
            this.talliesTable.PerformLayout();
            this.dynamicDataGroupBox.ResumeLayout(false);
            this.dynamicDataGroupBox.PerformLayout();
            this.dynamicSourcesTable.ResumeLayout(false);
            this.dynamicSourcesTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox operationGroupBox;
        private System.Windows.Forms.TableLayoutPanel modeTable;
        private System.Windows.Forms.Label staticTextLabel;
        private System.Windows.Forms.Label currentTextLabel;
        private System.Windows.Forms.TextBox currentTextTextBox;
        private System.Windows.Forms.TextBox staticTextTextBox;
        private System.Windows.Forms.Label useStaticTextLabel;
        private System.Windows.Forms.CheckBox useStaticTextCheckBox;
        protected System.Windows.Forms.TabControl mainTabControl;
        protected System.Windows.Forms.TabPage baseDataTabPage;
        protected System.Windows.Forms.TabPage dynamicTextTabPage;
        protected System.Windows.Forms.TabPage talliesTabPage;
        private System.Windows.Forms.GroupBox talliesGroupBox;
        private System.Windows.Forms.TableLayoutPanel talliesTable;
        private System.Windows.Forms.Label tallyExampleSourceLabel;
        private System.Windows.Forms.ComboBox tallySourceExampleComboBox;
        private System.Windows.Forms.GroupBox dynamicDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel dynamicSourcesTable;
        private System.Windows.Forms.Label dynamicSourceLabel1;
        private System.Windows.Forms.ComboBox dynamicSourceDropDown1;
        private System.Windows.Forms.ComboBox dynamicSourceAlignmentDropDown1;
    }
}