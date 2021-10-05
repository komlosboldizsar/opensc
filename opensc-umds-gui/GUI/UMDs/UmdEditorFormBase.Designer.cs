﻿namespace OpenSC.GUI.UMDs
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
            this.dynamicDataTabPage = new System.Windows.Forms.TabPage();
            this.talliesTabPage = new System.Windows.Forms.TabPage();
            this.talliesGroupBox = new System.Windows.Forms.GroupBox();
            this.talliesTable = new System.Windows.Forms.TableLayoutPanel();
            this.tallyExampleSourceLabel = new System.Windows.Forms.Label();
            this.tallySourceExampleComboBox = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.operationGroupBox.SuspendLayout();
            this.modeTable.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.baseDataTabPage.SuspendLayout();
            this.talliesTabPage.SuspendLayout();
            this.talliesGroupBox.SuspendLayout();
            this.talliesTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.mainTabControl);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 289);
            this.customElementsPanel.Controls.SetChildIndex(this.mainTabControl, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(509, 378);
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.AutoSize = true;
            this.operationGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operationGroupBox.Controls.Add(this.modeTable);
            this.operationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.operationGroupBox.Location = new System.Drawing.Point(3, 3);
            this.operationGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.operationGroupBox.Name = "operationGroupBox";
            this.operationGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.operationGroupBox.Size = new System.Drawing.Size(475, 106);
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
            this.modeTable.Location = new System.Drawing.Point(8, 19);
            this.modeTable.Name = "modeTable";
            this.modeTable.RowCount = 3;
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.modeTable.Size = new System.Drawing.Size(459, 79);
            this.modeTable.TabIndex = 0;
            // 
            // staticTextLabel
            // 
            this.staticTextLabel.AutoSize = true;
            this.staticTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.staticTextLabel.Location = new System.Drawing.Point(3, 28);
            this.staticTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.staticTextLabel.Name = "staticTextLabel";
            this.staticTextLabel.Size = new System.Drawing.Size(69, 28);
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
            this.currentTextLabel.Size = new System.Drawing.Size(81, 28);
            this.currentTextLabel.TabIndex = 4;
            this.currentTextLabel.Text = "Current text";
            this.currentTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // currentTextTextBox
            // 
            this.currentTextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentTextTextBox.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.currentTextTextBox.Location = new System.Drawing.Point(117, 3);
            this.currentTextTextBox.Name = "currentTextTextBox";
            this.currentTextTextBox.ReadOnly = true;
            this.currentTextTextBox.Size = new System.Drawing.Size(339, 22);
            this.currentTextTextBox.TabIndex = 5;
            // 
            // staticTextTextBox
            // 
            this.staticTextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.staticTextTextBox.Location = new System.Drawing.Point(117, 31);
            this.staticTextTextBox.Name = "staticTextTextBox";
            this.staticTextTextBox.Size = new System.Drawing.Size(339, 22);
            this.staticTextTextBox.TabIndex = 6;
            // 
            // useStaticTextLabel
            // 
            this.useStaticTextLabel.AutoSize = true;
            this.useStaticTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.useStaticTextLabel.Location = new System.Drawing.Point(3, 56);
            this.useStaticTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.useStaticTextLabel.Name = "useStaticTextLabel";
            this.useStaticTextLabel.Size = new System.Drawing.Size(96, 23);
            this.useStaticTextLabel.TabIndex = 7;
            this.useStaticTextLabel.Text = "Use static text";
            this.useStaticTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // useStaticTextCheckBox
            // 
            this.useStaticTextCheckBox.AutoSize = true;
            this.useStaticTextCheckBox.Location = new System.Drawing.Point(117, 59);
            this.useStaticTextCheckBox.Name = "useStaticTextCheckBox";
            this.useStaticTextCheckBox.Size = new System.Drawing.Size(18, 17);
            this.useStaticTextCheckBox.TabIndex = 8;
            this.useStaticTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.baseDataTabPage);
            this.mainTabControl.Controls.Add(this.dynamicDataTabPage);
            this.mainTabControl.Controls.Add(this.talliesTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 27);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(489, 262);
            this.mainTabControl.TabIndex = 3;
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.baseDataTabPage.Controls.Add(this.operationGroupBox);
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 25);
            this.baseDataTabPage.Name = "baseDataTabPage";
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.baseDataTabPage.Size = new System.Drawing.Size(481, 233);
            this.baseDataTabPage.TabIndex = 0;
            this.baseDataTabPage.Text = "Base data";
            // 
            // dynamicDataTabPage
            // 
            this.dynamicDataTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.dynamicDataTabPage.Location = new System.Drawing.Point(4, 25);
            this.dynamicDataTabPage.Name = "dynamicDataTabPage";
            this.dynamicDataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dynamicDataTabPage.Size = new System.Drawing.Size(490, 219);
            this.dynamicDataTabPage.TabIndex = 1;
            this.dynamicDataTabPage.Text = "Dynamic text";
            // 
            // talliesTabPage
            // 
            this.talliesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.talliesTabPage.Controls.Add(this.talliesGroupBox);
            this.talliesTabPage.Location = new System.Drawing.Point(4, 25);
            this.talliesTabPage.Name = "talliesTabPage";
            this.talliesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.talliesTabPage.Size = new System.Drawing.Size(490, 219);
            this.talliesTabPage.TabIndex = 2;
            this.talliesTabPage.Text = "Tallies";
            // 
            // talliesGroupBox
            // 
            this.talliesGroupBox.AutoSize = true;
            this.talliesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesGroupBox.Controls.Add(this.talliesTable);
            this.talliesGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.talliesGroupBox.Location = new System.Drawing.Point(3, 3);
            this.talliesGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.talliesGroupBox.Name = "talliesGroupBox";
            this.talliesGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.talliesGroupBox.Size = new System.Drawing.Size(484, 57);
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
            this.talliesTable.Location = new System.Drawing.Point(8, 19);
            this.talliesTable.Name = "talliesTable";
            this.talliesTable.RowCount = 1;
            this.talliesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.talliesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.talliesTable.Size = new System.Drawing.Size(468, 30);
            this.talliesTable.TabIndex = 0;
            // 
            // tallyExampleSourceLabel
            // 
            this.tallyExampleSourceLabel.AutoSize = true;
            this.tallyExampleSourceLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tallyExampleSourceLabel.Location = new System.Drawing.Point(3, 0);
            this.tallyExampleSourceLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.tallyExampleSourceLabel.Name = "tallyExampleSourceLabel";
            this.tallyExampleSourceLabel.Size = new System.Drawing.Size(105, 30);
            this.tallyExampleSourceLabel.TabIndex = 0;
            this.tallyExampleSourceLabel.Text = "Tally #1 source";
            this.tallyExampleSourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tallySourceExampleComboBox
            // 
            this.tallySourceExampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tallySourceExampleComboBox.FormattingEnabled = true;
            this.tallySourceExampleComboBox.Location = new System.Drawing.Point(126, 3);
            this.tallySourceExampleComboBox.Name = "tallySourceExampleComboBox";
            this.tallySourceExampleComboBox.Size = new System.Drawing.Size(279, 24);
            this.tallySourceExampleComboBox.TabIndex = 4;
            // 
            // UmdEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 434);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New UMD";
            this.MinimumSize = new System.Drawing.Size(500, 440);
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
            this.talliesTabPage.ResumeLayout(false);
            this.talliesTabPage.PerformLayout();
            this.talliesGroupBox.ResumeLayout(false);
            this.talliesGroupBox.PerformLayout();
            this.talliesTable.ResumeLayout(false);
            this.talliesTable.PerformLayout();
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
        protected System.Windows.Forms.TabPage dynamicDataTabPage;
        protected System.Windows.Forms.TabPage talliesTabPage;
        private System.Windows.Forms.GroupBox talliesGroupBox;
        private System.Windows.Forms.TableLayoutPanel talliesTable;
        private System.Windows.Forms.Label tallyExampleSourceLabel;
        private System.Windows.Forms.ComboBox tallySourceExampleComboBox;
    }
}