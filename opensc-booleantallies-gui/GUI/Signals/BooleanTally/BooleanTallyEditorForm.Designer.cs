namespace OpenSC.GUI.Signals.BooleanTallies
{
    partial class BooleanTallyEditorForm
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
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.fromSourceGroupBox = new System.Windows.Forms.GroupBox();
            this.fromSourceTable = new System.Windows.Forms.TableLayoutPanel();
            this.fromBooleanLabel = new System.Windows.Forms.Label();
            this.fromBooleanDropDown = new System.Windows.Forms.ComboBox();
            this.toSourceGroupBox = new System.Windows.Forms.GroupBox();
            this.toSourceTable = new System.Windows.Forms.TableLayoutPanel();
            this.toColorDropDown = new System.Windows.Forms.ComboBox();
            this.toColorLabel = new System.Windows.Forms.Label();
            this.toSourceLabel = new System.Windows.Forms.Label();
            this.toSignalDropDown = new System.Windows.Forms.ComboBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.fromSourceGroupBox.SuspendLayout();
            this.fromSourceTable.SuspendLayout();
            this.toSourceGroupBox.SuspendLayout();
            this.toSourceTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.toSourceGroupBox);
            this.customElementsPanel.Controls.Add(this.fromSourceGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 289);
            this.customElementsPanel.Controls.SetChildIndex(this.fromSourceGroupBox, 0);
            this.customElementsPanel.Controls.SetChildIndex(this.toSourceGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(509, 378);
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
            // fromSourceGroupBox
            // 
            this.fromSourceGroupBox.AutoSize = true;
            this.fromSourceGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fromSourceGroupBox.Controls.Add(this.fromSourceTable);
            this.fromSourceGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.fromSourceGroupBox.Location = new System.Drawing.Point(0, 83);
            this.fromSourceGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.fromSourceGroupBox.Name = "fromSourceGroupBox";
            this.fromSourceGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.fromSourceGroupBox.Size = new System.Drawing.Size(489, 57);
            this.fromSourceGroupBox.TabIndex = 2;
            this.fromSourceGroupBox.TabStop = false;
            this.fromSourceGroupBox.Text = "From";
            // 
            // fromSourceTable
            // 
            this.fromSourceTable.AutoSize = true;
            this.fromSourceTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fromSourceTable.ColumnCount = 2;
            this.fromSourceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.fromSourceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.fromSourceTable.Controls.Add(this.fromBooleanLabel, 0, 0);
            this.fromSourceTable.Controls.Add(this.fromBooleanDropDown, 1, 0);
            this.fromSourceTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromSourceTable.Location = new System.Drawing.Point(8, 19);
            this.fromSourceTable.Name = "fromSourceTable";
            this.fromSourceTable.RowCount = 1;
            this.fromSourceTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.fromSourceTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.fromSourceTable.Size = new System.Drawing.Size(473, 30);
            this.fromSourceTable.TabIndex = 0;
            // 
            // fromBooleanLabel
            // 
            this.fromBooleanLabel.AutoSize = true;
            this.fromBooleanLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.fromBooleanLabel.Location = new System.Drawing.Point(3, 0);
            this.fromBooleanLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.fromBooleanLabel.Name = "fromBooleanLabel";
            this.fromBooleanLabel.Size = new System.Drawing.Size(60, 30);
            this.fromBooleanLabel.TabIndex = 0;
            this.fromBooleanLabel.Text = "Boolean";
            this.fromBooleanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fromBooleanDropDown
            // 
            this.fromBooleanDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fromBooleanDropDown.FormattingEnabled = true;
            this.fromBooleanDropDown.Location = new System.Drawing.Point(81, 3);
            this.fromBooleanDropDown.Name = "fromBooleanDropDown";
            this.fromBooleanDropDown.Size = new System.Drawing.Size(339, 24);
            this.fromBooleanDropDown.TabIndex = 3;
            // 
            // toSourceGroupBox
            // 
            this.toSourceGroupBox.AutoSize = true;
            this.toSourceGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.toSourceGroupBox.Controls.Add(this.toSourceTable);
            this.toSourceGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.toSourceGroupBox.Location = new System.Drawing.Point(0, 140);
            this.toSourceGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.toSourceGroupBox.Name = "toSourceGroupBox";
            this.toSourceGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.toSourceGroupBox.Size = new System.Drawing.Size(489, 87);
            this.toSourceGroupBox.TabIndex = 3;
            this.toSourceGroupBox.TabStop = false;
            this.toSourceGroupBox.Text = "From";
            // 
            // toSourceTable
            // 
            this.toSourceTable.AutoSize = true;
            this.toSourceTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.toSourceTable.ColumnCount = 2;
            this.toSourceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.toSourceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.toSourceTable.Controls.Add(this.toColorDropDown, 1, 1);
            this.toSourceTable.Controls.Add(this.toColorLabel, 0, 1);
            this.toSourceTable.Controls.Add(this.toSourceLabel, 0, 0);
            this.toSourceTable.Controls.Add(this.toSignalDropDown, 1, 0);
            this.toSourceTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toSourceTable.Location = new System.Drawing.Point(8, 19);
            this.toSourceTable.Name = "toSourceTable";
            this.toSourceTable.RowCount = 2;
            this.toSourceTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.toSourceTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.toSourceTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.toSourceTable.Size = new System.Drawing.Size(473, 60);
            this.toSourceTable.TabIndex = 0;
            // 
            // toColorDropDown
            // 
            this.toColorDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toColorDropDown.FormattingEnabled = true;
            this.toColorDropDown.Location = new System.Drawing.Point(68, 33);
            this.toColorDropDown.Name = "toColorDropDown";
            this.toColorDropDown.Size = new System.Drawing.Size(139, 24);
            this.toColorDropDown.TabIndex = 5;
            // 
            // toColorLabel
            // 
            this.toColorLabel.AutoSize = true;
            this.toColorLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.toColorLabel.Location = new System.Drawing.Point(3, 30);
            this.toColorLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.toColorLabel.Name = "toColorLabel";
            this.toColorLabel.Size = new System.Drawing.Size(41, 30);
            this.toColorLabel.TabIndex = 4;
            this.toColorLabel.Text = "Color";
            this.toColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toSourceLabel
            // 
            this.toSourceLabel.AutoSize = true;
            this.toSourceLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.toSourceLabel.Location = new System.Drawing.Point(3, 0);
            this.toSourceLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.toSourceLabel.Name = "toSourceLabel";
            this.toSourceLabel.Size = new System.Drawing.Size(47, 30);
            this.toSourceLabel.TabIndex = 0;
            this.toSourceLabel.Text = "Signal";
            this.toSourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toSignalDropDown
            // 
            this.toSignalDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toSignalDropDown.FormattingEnabled = true;
            this.toSignalDropDown.Location = new System.Drawing.Point(68, 3);
            this.toSignalDropDown.Name = "toSignalDropDown";
            this.toSignalDropDown.Size = new System.Drawing.Size(352, 24);
            this.toSignalDropDown.TabIndex = 3;
            // 
            // BooleanTallyEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 434);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "New boolean tally";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "BooleanTallyEditorForm";
            this.SubjectPlural = "boolean tallies";
            this.SubjectSingular = "boolean tally";
            this.Text = "New boolean tally";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.fromSourceGroupBox.ResumeLayout(false);
            this.fromSourceGroupBox.PerformLayout();
            this.fromSourceTable.ResumeLayout(false);
            this.fromSourceTable.PerformLayout();
            this.toSourceGroupBox.ResumeLayout(false);
            this.toSourceGroupBox.PerformLayout();
            this.toSourceTable.ResumeLayout(false);
            this.toSourceTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox fromSourceGroupBox;
        private System.Windows.Forms.TableLayoutPanel fromSourceTable;
        private System.Windows.Forms.Label fromBooleanLabel;
        private System.Windows.Forms.ComboBox fromBooleanDropDown;
        private System.Windows.Forms.GroupBox toSourceGroupBox;
        private System.Windows.Forms.TableLayoutPanel toSourceTable;
        private System.Windows.Forms.ComboBox toColorDropDown;
        private System.Windows.Forms.Label toColorLabel;
        private System.Windows.Forms.Label toSourceLabel;
        private System.Windows.Forms.ComboBox toSignalDropDown;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TextBox nameTextBox;
    }
}