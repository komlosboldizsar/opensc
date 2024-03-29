﻿namespace OpenSC.GUI.Signals
{
    partial class ExternalSignalEditorForm
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
            this.categoryGroupBox = new System.Windows.Forms.GroupBox();
            this.categoryTable = new System.Windows.Forms.TableLayoutPanel();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.categoryDropDown = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.categoryGroupBox.SuspendLayout();
            this.categoryTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.categoryGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 289);
            this.customElementsPanel.Controls.SetChildIndex(this.categoryGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(509, 378);
            // 
            // categoryGroupBox
            // 
            this.categoryGroupBox.AutoSize = true;
            this.categoryGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.categoryGroupBox.Controls.Add(this.categoryTable);
            this.categoryGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoryGroupBox.Location = new System.Drawing.Point(0, 27);
            this.categoryGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.categoryGroupBox.Name = "categoryGroupBox";
            this.categoryGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.categoryGroupBox.Size = new System.Drawing.Size(489, 57);
            this.categoryGroupBox.TabIndex = 2;
            this.categoryGroupBox.TabStop = false;
            this.categoryGroupBox.Text = "Base data";
            // 
            // categoryTable
            // 
            this.categoryTable.AutoSize = true;
            this.categoryTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.categoryTable.ColumnCount = 2;
            this.categoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.categoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.categoryTable.Controls.Add(this.categoryLabel, 0, 0);
            this.categoryTable.Controls.Add(this.categoryDropDown, 1, 0);
            this.categoryTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryTable.Location = new System.Drawing.Point(8, 19);
            this.categoryTable.Name = "categoryTable";
            this.categoryTable.RowCount = 2;
            this.categoryTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.categoryTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.categoryTable.Size = new System.Drawing.Size(473, 30);
            this.categoryTable.TabIndex = 0;
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.categoryLabel.Location = new System.Drawing.Point(3, 0);
            this.categoryLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(65, 30);
            this.categoryLabel.TabIndex = 0;
            this.categoryLabel.Text = "Category";
            this.categoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // categoryDropDown
            // 
            this.categoryDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.Location = new System.Drawing.Point(86, 3);
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.Size = new System.Drawing.Size(228, 24);
            this.categoryDropDown.TabIndex = 3;
            // 
            // ExternalSignalEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 434);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "New external signals";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "ExternalSignalEditorForm";
            this.SubjectPlural = "external signal";
            this.SubjectSingular = "external signals";
            this.Text = "New external signals";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.categoryGroupBox.ResumeLayout(false);
            this.categoryGroupBox.PerformLayout();
            this.categoryTable.ResumeLayout(false);
            this.categoryTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox categoryGroupBox;
        private System.Windows.Forms.TableLayoutPanel categoryTable;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox categoryDropDown;
    }
}