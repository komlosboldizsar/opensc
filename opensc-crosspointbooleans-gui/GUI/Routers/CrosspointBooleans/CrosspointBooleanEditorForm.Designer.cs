﻿namespace OpenSC.GUI.Routers.CrosspointBooleans
{
    partial class CrosspointBooleanEditorForm
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
            this.routerInputGroupBox = new System.Windows.Forms.GroupBox();
            this.routerInputTable = new System.Windows.Forms.TableLayoutPanel();
            this.routerInputLabel = new System.Windows.Forms.Label();
            this.routerInputDropDown = new System.Windows.Forms.ComboBox();
            this.routerLabel = new System.Windows.Forms.Label();
            this.routerDropDown = new System.Windows.Forms.ComboBox();
            this.routerOutputLabel = new System.Windows.Forms.Label();
            this.routerOutputDropDown = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.routerInputGroupBox.SuspendLayout();
            this.routerInputTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.routerInputGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 319);
            this.customElementsPanel.Controls.SetChildIndex(this.routerInputGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(509, 408);
            // 
            // routerInputGroupBox
            // 
            this.routerInputGroupBox.AutoSize = true;
            this.routerInputGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerInputGroupBox.Controls.Add(this.routerInputTable);
            this.routerInputGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.routerInputGroupBox.Location = new System.Drawing.Point(0, 27);
            this.routerInputGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.routerInputGroupBox.Name = "routerInputGroupBox";
            this.routerInputGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.routerInputGroupBox.Size = new System.Drawing.Size(489, 117);
            this.routerInputGroupBox.TabIndex = 2;
            this.routerInputGroupBox.TabStop = false;
            this.routerInputGroupBox.Text = "Crosspoint";
            // 
            // routerInputTable
            // 
            this.routerInputTable.AutoSize = true;
            this.routerInputTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerInputTable.ColumnCount = 2;
            this.routerInputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.routerInputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.routerInputTable.Controls.Add(this.routerInputLabel, 0, 1);
            this.routerInputTable.Controls.Add(this.routerInputDropDown, 1, 1);
            this.routerInputTable.Controls.Add(this.routerLabel, 0, 0);
            this.routerInputTable.Controls.Add(this.routerDropDown, 1, 0);
            this.routerInputTable.Controls.Add(this.routerOutputLabel, 0, 2);
            this.routerInputTable.Controls.Add(this.routerOutputDropDown, 1, 2);
            this.routerInputTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routerInputTable.Location = new System.Drawing.Point(8, 19);
            this.routerInputTable.Name = "routerInputTable";
            this.routerInputTable.RowCount = 3;
            this.routerInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerInputTable.Size = new System.Drawing.Size(473, 90);
            this.routerInputTable.TabIndex = 0;
            // 
            // routerInputLabel
            // 
            this.routerInputLabel.AutoSize = true;
            this.routerInputLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerInputLabel.Location = new System.Drawing.Point(3, 30);
            this.routerInputLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerInputLabel.Name = "routerInputLabel";
            this.routerInputLabel.Size = new System.Drawing.Size(39, 30);
            this.routerInputLabel.TabIndex = 5;
            this.routerInputLabel.Text = "Input";
            this.routerInputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerInputDropDown
            // 
            this.routerInputDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerInputDropDown.FormattingEnabled = true;
            this.routerInputDropDown.Location = new System.Drawing.Point(72, 33);
            this.routerInputDropDown.Name = "routerInputDropDown";
            this.routerInputDropDown.Size = new System.Drawing.Size(339, 24);
            this.routerInputDropDown.TabIndex = 4;
            // 
            // routerLabel
            // 
            this.routerLabel.AutoSize = true;
            this.routerLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerLabel.Location = new System.Drawing.Point(3, 0);
            this.routerLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerLabel.Name = "routerLabel";
            this.routerLabel.Size = new System.Drawing.Size(51, 30);
            this.routerLabel.TabIndex = 0;
            this.routerLabel.Text = "Router";
            this.routerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerDropDown
            // 
            this.routerDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerDropDown.FormattingEnabled = true;
            this.routerDropDown.Location = new System.Drawing.Point(72, 3);
            this.routerDropDown.Name = "routerDropDown";
            this.routerDropDown.Size = new System.Drawing.Size(339, 24);
            this.routerDropDown.TabIndex = 3;
            // 
            // routerOutputLabel
            // 
            this.routerOutputLabel.AutoSize = true;
            this.routerOutputLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerOutputLabel.Location = new System.Drawing.Point(3, 60);
            this.routerOutputLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerOutputLabel.Name = "routerOutputLabel";
            this.routerOutputLabel.Size = new System.Drawing.Size(51, 30);
            this.routerOutputLabel.TabIndex = 5;
            this.routerOutputLabel.Text = "Output";
            this.routerOutputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerOutputDropDown
            // 
            this.routerOutputDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerOutputDropDown.FormattingEnabled = true;
            this.routerOutputDropDown.Location = new System.Drawing.Point(72, 63);
            this.routerOutputDropDown.Name = "routerOutputDropDown";
            this.routerOutputDropDown.Size = new System.Drawing.Size(339, 24);
            this.routerOutputDropDown.TabIndex = 4;
            // 
            // CrosspointBooleanEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 464);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "New crosspoint boolean";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "CrosspointBooleanEditorForm";
            this.SubjectPlural = "crosspoint booleans";
            this.SubjectSingular = "crosspoint boolean";
            this.Text = "New crosspoint boolean";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.routerInputGroupBox.ResumeLayout(false);
            this.routerInputGroupBox.PerformLayout();
            this.routerInputTable.ResumeLayout(false);
            this.routerInputTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox routerInputGroupBox;
        private System.Windows.Forms.TableLayoutPanel routerInputTable;
        private System.Windows.Forms.Label routerLabel;
        private System.Windows.Forms.ComboBox routerDropDown;
        private System.Windows.Forms.Label routerInputLabel;
        private System.Windows.Forms.ComboBox routerInputDropDown;
        private System.Windows.Forms.Label routerOutputLabel;
        private System.Windows.Forms.ComboBox routerOutputDropDown;

    }
}