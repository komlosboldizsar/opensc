﻿namespace OpenSC.GUI.Signals
{
    partial class ExternalSignalCategoryEditorForm
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
            this.appearanceGroupBox = new System.Windows.Forms.GroupBox();
            this.appearanceTable = new System.Windows.Forms.TableLayoutPanel();
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorContainerPanel = new System.Windows.Forms.Panel();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.setColorButton = new System.Windows.Forms.Button();
            this.chooseColorDialog = new System.Windows.Forms.ColorDialog();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.appearanceGroupBox.SuspendLayout();
            this.appearanceTable.SuspendLayout();
            this.colorContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.appearanceGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 289);
            this.customElementsPanel.Controls.SetChildIndex(this.appearanceGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(509, 378);
            // 
            // appearanceGroupBox
            // 
            this.appearanceGroupBox.AutoSize = true;
            this.appearanceGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.appearanceGroupBox.Controls.Add(this.appearanceTable);
            this.appearanceGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.appearanceGroupBox.Location = new System.Drawing.Point(0, 83);
            this.appearanceGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.appearanceGroupBox.Name = "appearanceGroupBox";
            this.appearanceGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.appearanceGroupBox.Size = new System.Drawing.Size(489, 67);
            this.appearanceGroupBox.TabIndex = 2;
            this.appearanceGroupBox.TabStop = false;
            this.appearanceGroupBox.Text = "Base data";
            // 
            // appearanceTable
            // 
            this.appearanceTable.AutoSize = true;
            this.appearanceTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.appearanceTable.ColumnCount = 2;
            this.appearanceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.appearanceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.appearanceTable.Controls.Add(this.colorLabel, 0, 0);
            this.appearanceTable.Controls.Add(this.colorContainerPanel, 1, 0);
            this.appearanceTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appearanceTable.Location = new System.Drawing.Point(8, 19);
            this.appearanceTable.Name = "appearanceTable";
            this.appearanceTable.RowCount = 1;
            this.appearanceTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.appearanceTable.Size = new System.Drawing.Size(473, 40);
            this.appearanceTable.TabIndex = 0;
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.colorLabel.Location = new System.Drawing.Point(3, 0);
            this.colorLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(41, 40);
            this.colorLabel.TabIndex = 0;
            this.colorLabel.Text = "Color";
            this.colorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colorContainerPanel
            // 
            this.colorContainerPanel.AutoSize = true;
            this.colorContainerPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.colorContainerPanel.Controls.Add(this.colorPanel);
            this.colorContainerPanel.Controls.Add(this.setColorButton);
            this.colorContainerPanel.Location = new System.Drawing.Point(62, 3);
            this.colorContainerPanel.Name = "colorContainerPanel";
            this.colorContainerPanel.Size = new System.Drawing.Size(145, 34);
            this.colorContainerPanel.TabIndex = 1;
            // 
            // colorPanel
            // 
            this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPanel.Location = new System.Drawing.Point(4, 4);
            this.colorPanel.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(50, 26);
            this.colorPanel.TabIndex = 3;
            // 
            // setColorButton
            // 
            this.setColorButton.Location = new System.Drawing.Point(67, 3);
            this.setColorButton.Name = "setColorButton";
            this.setColorButton.Size = new System.Drawing.Size(75, 28);
            this.setColorButton.TabIndex = 4;
            this.setColorButton.Text = "set";
            this.setColorButton.UseVisualStyleBackColor = true;
            this.setColorButton.Click += new System.EventHandler(this.setColorButton_Click);
            // 
            // ExternalSignalCategoryEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 434);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "New external signal category";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "ExternalSignalCategoryEditorForm";
            this.SubjectPlural = "external signal categories";
            this.SubjectSingular = "external signal category";
            this.Text = "New external signal category";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.appearanceGroupBox.ResumeLayout(false);
            this.appearanceGroupBox.PerformLayout();
            this.appearanceTable.ResumeLayout(false);
            this.appearanceTable.PerformLayout();
            this.colorContainerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox appearanceGroupBox;
        private System.Windows.Forms.TableLayoutPanel appearanceTable;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Button setColorButton;
        private System.Windows.Forms.ColorDialog chooseColorDialog;
        private System.Windows.Forms.Panel colorContainerPanel;
    }
}