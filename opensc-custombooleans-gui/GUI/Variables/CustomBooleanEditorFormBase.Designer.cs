namespace OpenSC.GUI.Variables
{
    partial class CustomBooleanEditorFormBase
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
            this.booleanDataGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.identifierLabel = new System.Windows.Forms.Label();
            this.colorLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.identifierTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.colorButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.booleanDataGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.booleanDataGroupBox);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 15, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(1082, 535);
            this.customElementsPanel.Controls.SetChildIndex(this.booleanDataGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.mainContainer.Size = new System.Drawing.Size(1082, 621);
            // 
            // booleanDataGroupBox
            // 
            this.booleanDataGroupBox.AutoSize = true;
            this.booleanDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.booleanDataGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.booleanDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.booleanDataGroupBox.Location = new System.Drawing.Point(10, 122);
            this.booleanDataGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.booleanDataGroupBox.Name = "booleanDataGroupBox";
            this.booleanDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.booleanDataGroupBox.Size = new System.Drawing.Size(1062, 136);
            this.booleanDataGroupBox.TabIndex = 3;
            this.booleanDataGroupBox.TabStop = false;
            this.booleanDataGroupBox.Text = "Boolean data";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.identifierLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.colorLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.descriptionLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.identifierTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.descriptionTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.colorButton, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1046, 101);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // identifierLabel
            // 
            this.identifierLabel.AutoSize = true;
            this.identifierLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.identifierLabel.Location = new System.Drawing.Point(3, 0);
            this.identifierLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.identifierLabel.Name = "identifierLabel";
            this.identifierLabel.Size = new System.Drawing.Size(69, 33);
            this.identifierLabel.TabIndex = 0;
            this.identifierLabel.Text = "Identifier";
            this.identifierLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.colorLabel.Location = new System.Drawing.Point(3, 66);
            this.colorLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(45, 35);
            this.colorLabel.TabIndex = 1;
            this.colorLabel.Text = "Color";
            this.colorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.descriptionLabel.Location = new System.Drawing.Point(3, 33);
            this.descriptionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(85, 33);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Description";
            this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // identifierTextBox
            // 
            this.identifierTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.identifierTextBox.Location = new System.Drawing.Point(106, 3);
            this.identifierTextBox.Name = "identifierTextBox";
            this.identifierTextBox.Size = new System.Drawing.Size(937, 27);
            this.identifierTextBox.TabIndex = 3;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Location = new System.Drawing.Point(106, 36);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(937, 27);
            this.descriptionTextBox.TabIndex = 4;
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(106, 69);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(94, 29);
            this.colorButton.TabIndex = 5;
            this.colorButton.Text = "set";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // CustomBooleanEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 691);
            this.DeleteButtonVisible = true;
            this.HeaderText = "New custom boolean";
            this.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.MinimumSize = new System.Drawing.Size(1000, 738);
            this.Name = "CustomBooleanEditorFormBase";
            this.SubjectPlural = "custom booleans";
            this.SubjectSingular = "custom boolean";
            this.Text = "New custom boolean";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.booleanDataGroupBox.ResumeLayout(false);
            this.booleanDataGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox booleanDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label identifierLabel;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox identifierTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}