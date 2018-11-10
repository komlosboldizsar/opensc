namespace OpenSC.GUI.Signals
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
            this.basicDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.basicDataGroupBox = new System.Windows.Forms.GroupBox();
            this.appearanceGroupBox = new System.Windows.Forms.GroupBox();
            this.appearanceTable = new System.Windows.Forms.TableLayoutPanel();
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorContainerPanel = new System.Windows.Forms.Panel();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.setColorButton = new System.Windows.Forms.Button();
            this.chooseColorDialog = new System.Windows.Forms.ColorDialog();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.basicDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.basicDataGroupBox.SuspendLayout();
            this.appearanceGroupBox.SuspendLayout();
            this.appearanceTable.SuspendLayout();
            this.colorContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.appearanceGroupBox);
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
            // SignalCategoryEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 434);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "Edit signal category";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "SignalCategoryEditorForm";
            this.Text = "Edit signal category";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.basicDataTable.ResumeLayout(false);
            this.basicDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.basicDataGroupBox.ResumeLayout(false);
            this.basicDataGroupBox.PerformLayout();
            this.appearanceGroupBox.ResumeLayout(false);
            this.appearanceGroupBox.PerformLayout();
            this.appearanceTable.ResumeLayout(false);
            this.appearanceTable.PerformLayout();
            this.colorContainerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox basicDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel basicDataTable;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.GroupBox appearanceGroupBox;
        private System.Windows.Forms.TableLayoutPanel appearanceTable;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Button setColorButton;
        private System.Windows.Forms.ColorDialog chooseColorDialog;
        private System.Windows.Forms.Panel colorContainerPanel;
    }
}