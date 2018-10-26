namespace OpenSC.GUI.Variables
{
    partial class DynamicTextEditorForm
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
            this.labelLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.labelTextBox = new System.Windows.Forms.TextBox();
            this.basicDataGroupBox = new System.Windows.Forms.GroupBox();
            this.contentGroupBox = new System.Windows.Forms.GroupBox();
            this.contentTable = new System.Windows.Forms.TableLayoutPanel();
            this.currentTextLabel = new System.Windows.Forms.Label();
            this.formulaLabel = new System.Windows.Forms.Label();
            this.formulaTextBox = new System.Windows.Forms.TextBox();
            this.currentTextTextBox = new System.Windows.Forms.TextBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.basicDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.basicDataGroupBox.SuspendLayout();
            this.contentGroupBox.SuspendLayout();
            this.contentTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.contentGroupBox);
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
            this.basicDataTable.Controls.Add(this.labelLabel, 0, 1);
            this.basicDataTable.Controls.Add(this.idNumericField, 1, 0);
            this.basicDataTable.Controls.Add(this.labelTextBox, 1, 1);
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
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelLabel.Location = new System.Drawing.Point(3, 28);
            this.labelLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(43, 28);
            this.labelLabel.TabIndex = 1;
            this.labelLabel.Text = "Label";
            this.labelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(64, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 2;
            // 
            // labelTextBox
            // 
            this.labelTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTextBox.Location = new System.Drawing.Point(64, 31);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(406, 22);
            this.labelTextBox.TabIndex = 3;
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
            // contentGroupBox
            // 
            this.contentGroupBox.AutoSize = true;
            this.contentGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contentGroupBox.Controls.Add(this.contentTable);
            this.contentGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.contentGroupBox.Location = new System.Drawing.Point(0, 83);
            this.contentGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.contentGroupBox.Name = "contentGroupBox";
            this.contentGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.contentGroupBox.Size = new System.Drawing.Size(489, 83);
            this.contentGroupBox.TabIndex = 2;
            this.contentGroupBox.TabStop = false;
            this.contentGroupBox.Text = "Content";
            // 
            // contentTable
            // 
            this.contentTable.AutoSize = true;
            this.contentTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contentTable.ColumnCount = 2;
            this.contentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentTable.Controls.Add(this.currentTextLabel, 0, 1);
            this.contentTable.Controls.Add(this.formulaLabel, 0, 0);
            this.contentTable.Controls.Add(this.formulaTextBox, 1, 0);
            this.contentTable.Controls.Add(this.currentTextTextBox, 1, 1);
            this.contentTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentTable.Location = new System.Drawing.Point(8, 19);
            this.contentTable.Name = "contentTable";
            this.contentTable.RowCount = 2;
            this.contentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contentTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.contentTable.Size = new System.Drawing.Size(473, 56);
            this.contentTable.TabIndex = 0;
            // 
            // currentTextLabel
            // 
            this.currentTextLabel.AutoSize = true;
            this.currentTextLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.currentTextLabel.Location = new System.Drawing.Point(3, 28);
            this.currentTextLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.currentTextLabel.Name = "currentTextLabel";
            this.currentTextLabel.Size = new System.Drawing.Size(81, 28);
            this.currentTextLabel.TabIndex = 1;
            this.currentTextLabel.Text = "Current text";
            this.currentTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // formulaLabel
            // 
            this.formulaLabel.AutoSize = true;
            this.formulaLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.formulaLabel.Location = new System.Drawing.Point(3, 0);
            this.formulaLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.formulaLabel.Name = "formulaLabel";
            this.formulaLabel.Size = new System.Drawing.Size(59, 28);
            this.formulaLabel.TabIndex = 4;
            this.formulaLabel.Text = "Formula";
            this.formulaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // formulaTextBox
            // 
            this.formulaTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formulaTextBox.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.formulaTextBox.Location = new System.Drawing.Point(102, 3);
            this.formulaTextBox.Name = "formulaTextBox";
            this.formulaTextBox.Size = new System.Drawing.Size(368, 22);
            this.formulaTextBox.TabIndex = 5;
            // 
            // currentTextTextBox
            // 
            this.currentTextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentTextTextBox.Location = new System.Drawing.Point(102, 31);
            this.currentTextTextBox.Name = "currentTextTextBox";
            this.currentTextTextBox.ReadOnly = true;
            this.currentTextTextBox.Size = new System.Drawing.Size(368, 22);
            this.currentTextTextBox.TabIndex = 6;
            // 
            // DynamicTextEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 434);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "DynamicTextEditorForm";
            this.Text = "Edit dynamic text";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.basicDataTable.ResumeLayout(false);
            this.basicDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.basicDataGroupBox.ResumeLayout(false);
            this.basicDataGroupBox.PerformLayout();
            this.contentGroupBox.ResumeLayout(false);
            this.contentGroupBox.PerformLayout();
            this.contentTable.ResumeLayout(false);
            this.contentTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox basicDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel basicDataTable;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.GroupBox contentGroupBox;
        private System.Windows.Forms.TableLayoutPanel contentTable;
        private System.Windows.Forms.Label currentTextLabel;
        private System.Windows.Forms.Label formulaLabel;
        private System.Windows.Forms.TextBox formulaTextBox;
        private System.Windows.Forms.TextBox currentTextTextBox;
    }
}