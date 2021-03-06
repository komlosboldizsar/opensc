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
            this.basicDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.basicDataGroupBox = new System.Windows.Forms.GroupBox();
            this.categoryGroupBox = new System.Windows.Forms.GroupBox();
            this.categoryTable = new System.Windows.Forms.TableLayoutPanel();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.categoryDropDown = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.basicDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.basicDataGroupBox.SuspendLayout();
            this.categoryGroupBox.SuspendLayout();
            this.categoryTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.categoryGroupBox);
            this.customElementsPanel.Controls.Add(this.basicDataGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 289);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(509, 378);
            // 
            // basicDataTable
            // 
            this.basicDataTable.AutoSize = true;
            this.basicDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basicDataTable.ColumnCount = 2;
            this.basicDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.basicDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.basicDataTable.Controls.Add(this.idLabel, 0, 0);
            this.basicDataTable.Controls.Add(this.nameLabel, 0, 1);
            this.basicDataTable.Controls.Add(this.idNumericField, 1, 0);
            this.basicDataTable.Controls.Add(this.nameTextBox, 1, 1);
            this.basicDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basicDataTable.Location = new System.Drawing.Point(8, 19);
            this.basicDataTable.Name = "basicDataTable";
            this.basicDataTable.RowCount = 2;
            this.basicDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.basicDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.basicDataTable.Size = new System.Drawing.Size(473, 56);
            this.basicDataTable.TabIndex = 0;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.idLabel.Location = new System.Drawing.Point(3, 0);
            this.idLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(21, 28);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "ID";
            this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.nameLabel.Location = new System.Drawing.Point(3, 28);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 28);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(66, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 2;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameTextBox.Location = new System.Drawing.Point(66, 31);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(404, 22);
            this.nameTextBox.TabIndex = 3;
            // 
            // basicDataGroupBox
            // 
            this.basicDataGroupBox.AutoSize = true;
            this.basicDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basicDataGroupBox.Controls.Add(this.basicDataTable);
            this.basicDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.basicDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.basicDataGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.basicDataGroupBox.Name = "basicDataGroupBox";
            this.basicDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.basicDataGroupBox.Size = new System.Drawing.Size(489, 83);
            this.basicDataGroupBox.TabIndex = 1;
            this.basicDataGroupBox.TabStop = false;
            this.basicDataGroupBox.Text = "Base data";
            // 
            // categoryGroupBox
            // 
            this.categoryGroupBox.AutoSize = true;
            this.categoryGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.categoryGroupBox.Controls.Add(this.categoryTable);
            this.categoryGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoryGroupBox.Location = new System.Drawing.Point(0, 83);
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
            this.HeaderText = "Edit external signal";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "ExternalSignalEditorForm";
            this.Text = "Edit external signal";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.basicDataTable.ResumeLayout(false);
            this.basicDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.basicDataGroupBox.ResumeLayout(false);
            this.basicDataGroupBox.PerformLayout();
            this.categoryGroupBox.ResumeLayout(false);
            this.categoryGroupBox.PerformLayout();
            this.categoryTable.ResumeLayout(false);
            this.categoryTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox basicDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel basicDataTable;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.GroupBox categoryGroupBox;
        private System.Windows.Forms.TableLayoutPanel categoryTable;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox categoryDropDown;
    }
}