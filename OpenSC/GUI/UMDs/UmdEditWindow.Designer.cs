namespace OpenSC.GUI.UMDs
{
    partial class UmdEditWindow
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
            this.operationGroupBox = new System.Windows.Forms.GroupBox();
            this.modeTable = new System.Windows.Forms.TableLayoutPanel();
            this.staticTextLabel = new System.Windows.Forms.Label();
            this.currentTextLabel = new System.Windows.Forms.Label();
            this.currentTextTextBox = new System.Windows.Forms.TextBox();
            this.staticTextTextBox = new System.Windows.Forms.TextBox();
            this.useStaticTextLabel = new System.Windows.Forms.Label();
            this.useStaticTextCheckBox = new System.Windows.Forms.CheckBox();
            this.saveAndCloseButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.mainContainer.SuspendLayout();
            this.basicDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.basicDataGroupBox.SuspendLayout();
            this.operationGroupBox.SuspendLayout();
            this.modeTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.saveButton);
            this.mainContainer.Controls.Add(this.cancelButton);
            this.mainContainer.Controls.Add(this.saveAndCloseButton);
            this.mainContainer.Controls.Add(this.operationGroupBox);
            this.mainContainer.Controls.Add(this.basicDataGroupBox);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(482, 337);
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
            this.basicDataTable.Size = new System.Drawing.Size(446, 56);
            this.basicDataTable.TabIndex = 0;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.idLabel.Location = new System.Drawing.Point(3, 0);
            this.idLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
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
            this.nameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 28);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(71, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 2;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameTextBox.Location = new System.Drawing.Point(71, 31);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(372, 22);
            this.nameTextBox.TabIndex = 3;
            // 
            // basicDataGroupBox
            // 
            this.basicDataGroupBox.AutoSize = true;
            this.basicDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basicDataGroupBox.Controls.Add(this.basicDataTable);
            this.basicDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.basicDataGroupBox.Location = new System.Drawing.Point(10, 10);
            this.basicDataGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.basicDataGroupBox.Name = "basicDataGroupBox";
            this.basicDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.basicDataGroupBox.Size = new System.Drawing.Size(462, 83);
            this.basicDataGroupBox.TabIndex = 1;
            this.basicDataGroupBox.TabStop = false;
            this.basicDataGroupBox.Text = "Basic data";
            // 
            // operationGroupBox
            // 
            this.operationGroupBox.AutoSize = true;
            this.operationGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operationGroupBox.Controls.Add(this.modeTable);
            this.operationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.operationGroupBox.Location = new System.Drawing.Point(10, 93);
            this.operationGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.operationGroupBox.Name = "operationGroupBox";
            this.operationGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.operationGroupBox.Size = new System.Drawing.Size(462, 106);
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
            this.modeTable.Size = new System.Drawing.Size(446, 79);
            this.modeTable.TabIndex = 0;
            // 
            // staticTextLabel
            // 
            this.staticTextLabel.AutoSize = true;
            this.staticTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.staticTextLabel.Location = new System.Drawing.Point(3, 28);
            this.staticTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
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
            this.currentTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.currentTextLabel.Name = "currentTextLabel";
            this.currentTextLabel.Size = new System.Drawing.Size(81, 28);
            this.currentTextLabel.TabIndex = 4;
            this.currentTextLabel.Text = "Current text";
            this.currentTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // currentTextTextBox
            // 
            this.currentTextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentTextTextBox.Location = new System.Drawing.Point(122, 3);
            this.currentTextTextBox.Name = "currentTextTextBox";
            this.currentTextTextBox.ReadOnly = true;
            this.currentTextTextBox.Size = new System.Drawing.Size(321, 22);
            this.currentTextTextBox.TabIndex = 5;
            // 
            // staticTextTextBox
            // 
            this.staticTextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.staticTextTextBox.Location = new System.Drawing.Point(122, 31);
            this.staticTextTextBox.Name = "staticTextTextBox";
            this.staticTextTextBox.Size = new System.Drawing.Size(321, 22);
            this.staticTextTextBox.TabIndex = 6;
            // 
            // useStaticTextLabel
            // 
            this.useStaticTextLabel.AutoSize = true;
            this.useStaticTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.useStaticTextLabel.Location = new System.Drawing.Point(3, 56);
            this.useStaticTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.useStaticTextLabel.Name = "useStaticTextLabel";
            this.useStaticTextLabel.Size = new System.Drawing.Size(96, 23);
            this.useStaticTextLabel.TabIndex = 7;
            this.useStaticTextLabel.Text = "Use static text";
            this.useStaticTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // useStaticTextCheckBox
            // 
            this.useStaticTextCheckBox.AutoSize = true;
            this.useStaticTextCheckBox.Location = new System.Drawing.Point(122, 59);
            this.useStaticTextCheckBox.Name = "useStaticTextCheckBox";
            this.useStaticTextCheckBox.Size = new System.Drawing.Size(18, 17);
            this.useStaticTextCheckBox.TabIndex = 8;
            this.useStaticTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveAndCloseButton
            // 
            this.saveAndCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAndCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveAndCloseButton.Location = new System.Drawing.Point(332, 289);
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
            this.cancelButton.Location = new System.Drawing.Point(154, 288);
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
            this.saveButton.Location = new System.Drawing.Point(243, 289);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(83, 35);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // UmdEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 393);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "UmdEditWindow";
            this.Text = "Edit UMD";
            this.Load += new System.EventHandler(this.UmdEditWindow_Load);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.basicDataTable.ResumeLayout(false);
            this.basicDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.basicDataGroupBox.ResumeLayout(false);
            this.basicDataGroupBox.PerformLayout();
            this.operationGroupBox.ResumeLayout(false);
            this.operationGroupBox.PerformLayout();
            this.modeTable.ResumeLayout(false);
            this.modeTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox basicDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel basicDataTable;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.GroupBox operationGroupBox;
        private System.Windows.Forms.TableLayoutPanel modeTable;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveAndCloseButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label staticTextLabel;
        private System.Windows.Forms.Label currentTextLabel;
        private System.Windows.Forms.TextBox currentTextTextBox;
        private System.Windows.Forms.TextBox staticTextTextBox;
        private System.Windows.Forms.Label useStaticTextLabel;
        private System.Windows.Forms.CheckBox useStaticTextCheckBox;
    }
}