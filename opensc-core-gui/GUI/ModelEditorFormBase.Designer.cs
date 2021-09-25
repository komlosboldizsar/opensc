namespace OpenSC.GUI
{
    partial class ModelEditorFormBase
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
            this.saveAndCloseButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.customElementsPanel = new System.Windows.Forms.Panel();
            this.identifiersGroupBox = new System.Windows.Forms.GroupBox();
            this.identifiersTable = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.mainContainer.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.identifiersGroupBox.SuspendLayout();
            this.identifiersTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainContainer.Controls.Add(this.customElementsPanel);
            this.mainContainer.Controls.Add(this.buttonsPanel);
            this.mainContainer.Size = new System.Drawing.Size(518, 337);
            // 
            // saveAndCloseButton
            // 
            this.saveAndCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAndCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveAndCloseButton.Location = new System.Drawing.Point(369, 22);
            this.saveAndCloseButton.Name = "saveAndCloseButton";
            this.saveAndCloseButton.Size = new System.Drawing.Size(137, 35);
            this.saveAndCloseButton.TabIndex = 3;
            this.saveAndCloseButton.Text = "Save and close";
            this.saveAndCloseButton.UseVisualStyleBackColor = true;
            this.saveAndCloseButton.Click += new System.EventHandler(this.saveAndCloseButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(191, 22);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(83, 35);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(280, 22);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(83, 35);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.deleteButton.Location = new System.Drawing.Point(12, 22);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(83, 35);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.deleteButton);
            this.buttonsPanel.Controls.Add(this.saveButton);
            this.buttonsPanel.Controls.Add(this.cancelButton);
            this.buttonsPanel.Controls.Add(this.saveAndCloseButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 268);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(518, 69);
            this.buttonsPanel.TabIndex = 8;
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customElementsPanel.Controls.Add(this.identifiersGroupBox);
            this.customElementsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customElementsPanel.Location = new System.Drawing.Point(0, 0);
            this.customElementsPanel.Name = "customElementsPanel";
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(518, 268);
            this.customElementsPanel.TabIndex = 9;
            // 
            // identifiersGroupBox
            // 
            this.identifiersGroupBox.AutoSize = true;
            this.identifiersGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.identifiersGroupBox.Controls.Add(this.identifiersTable);
            this.identifiersGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.identifiersGroupBox.Location = new System.Drawing.Point(10, 10);
            this.identifiersGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.identifiersGroupBox.Name = "identifiersGroupBox";
            this.identifiersGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.identifiersGroupBox.Size = new System.Drawing.Size(498, 83);
            this.identifiersGroupBox.TabIndex = 2;
            this.identifiersGroupBox.TabStop = false;
            this.identifiersGroupBox.Text = "Identifiers";
            // 
            // identifiersTable
            // 
            this.identifiersTable.AutoSize = true;
            this.identifiersTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.identifiersTable.ColumnCount = 2;
            this.identifiersTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.identifiersTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.identifiersTable.Controls.Add(this.idLabel, 0, 0);
            this.identifiersTable.Controls.Add(this.nameLabel, 0, 1);
            this.identifiersTable.Controls.Add(this.idNumericField, 1, 0);
            this.identifiersTable.Controls.Add(this.nameTextBox, 1, 1);
            this.identifiersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.identifiersTable.Location = new System.Drawing.Point(8, 19);
            this.identifiersTable.Name = "identifiersTable";
            this.identifiersTable.RowCount = 2;
            this.identifiersTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.identifiersTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.identifiersTable.Size = new System.Drawing.Size(482, 56);
            this.identifiersTable.TabIndex = 0;
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
            this.nameTextBox.Size = new System.Drawing.Size(413, 22);
            this.nameTextBox.TabIndex = 3;
            // 
            // ModelEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(518, 393);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "ModelEditorFormBase";
            this.Text = "";
            this.mainContainer.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.identifiersGroupBox.ResumeLayout(false);
            this.identifiersGroupBox.PerformLayout();
            this.identifiersTable.ResumeLayout(false);
            this.identifiersTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveAndCloseButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button deleteButton;
        protected System.Windows.Forms.Panel customElementsPanel;
        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.GroupBox identifiersGroupBox;
        private System.Windows.Forms.TableLayoutPanel identifiersTable;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        protected System.Windows.Forms.TextBox nameTextBox;
    }
}