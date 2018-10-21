namespace OpenSC.GUI.UMDs
{
    partial class UmdPortEditorFormBase
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
            this.baseDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.baseDataGroupBox = new System.Windows.Forms.GroupBox();
            this.baseDataPanel = new System.Windows.Forms.Panel();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.baseDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.baseDataGroupBox.SuspendLayout();
            this.baseDataPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.baseDataPanel);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 289);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(509, 378);
            // 
            // baseDataTable
            // 
            this.baseDataTable.AutoSize = true;
            this.baseDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataTable.ColumnCount = 2;
            this.baseDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.baseDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.baseDataTable.Controls.Add(this.idLabel, 0, 0);
            this.baseDataTable.Controls.Add(this.nameLabel, 0, 1);
            this.baseDataTable.Controls.Add(this.idNumericField, 1, 0);
            this.baseDataTable.Controls.Add(this.nameTextBox, 1, 1);
            this.baseDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseDataTable.Location = new System.Drawing.Point(8, 23);
            this.baseDataTable.Name = "baseDataTable";
            this.baseDataTable.RowCount = 2;
            this.baseDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.baseDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.baseDataTable.Size = new System.Drawing.Size(473, 56);
            this.baseDataTable.TabIndex = 0;
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
            // baseDataGroupBox
            // 
            this.baseDataGroupBox.AutoSize = true;
            this.baseDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.baseDataGroupBox.Controls.Add(this.baseDataTable);
            this.baseDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.baseDataGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.baseDataGroupBox.Name = "baseDataGroupBox";
            this.baseDataGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.baseDataGroupBox.Size = new System.Drawing.Size(489, 87);
            this.baseDataGroupBox.TabIndex = 1;
            this.baseDataGroupBox.TabStop = false;
            this.baseDataGroupBox.Text = "Base data";
            // 
            // baseDataPanel
            // 
            this.baseDataPanel.Controls.Add(this.baseDataGroupBox);
            this.baseDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseDataPanel.Location = new System.Drawing.Point(0, 0);
            this.baseDataPanel.Name = "baseDataPanel";
            this.baseDataPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.baseDataPanel.Size = new System.Drawing.Size(489, 100);
            this.baseDataPanel.TabIndex = 2;
            // 
            // UmdPortEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 434);
            this.DeleteButtonVisible = true;
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "UmdPortEditorFormBase";
            this.Text = "Edit UMD port";
            this.customElementsPanel.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.baseDataTable.ResumeLayout(false);
            this.baseDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.baseDataGroupBox.ResumeLayout(false);
            this.baseDataGroupBox.PerformLayout();
            this.baseDataPanel.ResumeLayout(false);
            this.baseDataPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox baseDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel baseDataTable;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Panel baseDataPanel;
    }
}