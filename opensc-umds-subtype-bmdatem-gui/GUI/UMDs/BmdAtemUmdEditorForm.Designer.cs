namespace OpenSC.GUI.UMDs
{
    partial class BmdAtemUmdEditorForm
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
            this.inputDataGroupBox = new System.Windows.Forms.GroupBox();
            this.inputDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.mixerLabel = new System.Windows.Forms.Label();
            this.inputLabel = new System.Windows.Forms.Label();
            this.nameTypeLabel = new System.Windows.Forms.Label();
            this.mixerDropDown = new System.Windows.Forms.ComboBox();
            this.inputDropDown = new System.Windows.Forms.ComboBox();
            this.nameTypeRadioButtonTable = new System.Windows.Forms.TableLayoutPanel();
            this.nameTypeShortRadioButton = new System.Windows.Forms.RadioButton();
            this.nameTypeLongRadioButton = new System.Windows.Forms.RadioButton();
            this.mainTabControl.SuspendLayout();
            this.baseDataTabPage.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.inputDataGroupBox.SuspendLayout();
            this.inputDataTable.SuspendLayout();
            this.nameTypeRadioButtonTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Location = new System.Drawing.Point(0, 111);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mainTabControl.Size = new System.Drawing.Size(780, 412);
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.Controls.Add(this.inputDataGroupBox);
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 29);
            this.baseDataTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.baseDataTabPage.Size = new System.Drawing.Size(772, 379);
            this.baseDataTabPage.Controls.SetChildIndex(this.inputDataGroupBox, 0);
            // 
            // dynamicDataTabPage
            // 
            this.dynamicDataTabPage.Location = new System.Drawing.Point(4, 29);
            this.dynamicDataTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dynamicDataTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dynamicDataTabPage.Size = new System.Drawing.Size(481, 286);
            // 
            // talliesTabPage
            // 
            this.talliesTabPage.Location = new System.Drawing.Point(4, 29);
            this.talliesTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.talliesTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.talliesTabPage.Size = new System.Drawing.Size(481, 286);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Location = new System.Drawing.Point(10, 15);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.customElementsPanel.Size = new System.Drawing.Size(780, 523);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.mainContainer.Size = new System.Drawing.Size(800, 639);
            // 
            // inputDataGroupBox
            // 
            this.inputDataGroupBox.AutoSize = true;
            this.inputDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inputDataGroupBox.Controls.Add(this.inputDataTable);
            this.inputDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputDataGroupBox.Location = new System.Drawing.Point(3, 134);
            this.inputDataGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.inputDataGroupBox.Name = "inputDataGroupBox";
            this.inputDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.inputDataGroupBox.Size = new System.Drawing.Size(766, 169);
            this.inputDataGroupBox.TabIndex = 3;
            this.inputDataGroupBox.TabStop = false;
            this.inputDataGroupBox.Text = "Input";
            // 
            // inputDataTable
            // 
            this.inputDataTable.AutoSize = true;
            this.inputDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inputDataTable.ColumnCount = 2;
            this.inputDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.inputDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.inputDataTable.Controls.Add(this.mixerLabel, 0, 0);
            this.inputDataTable.Controls.Add(this.inputLabel, 0, 1);
            this.inputDataTable.Controls.Add(this.nameTypeLabel, 0, 2);
            this.inputDataTable.Controls.Add(this.mixerDropDown, 1, 0);
            this.inputDataTable.Controls.Add(this.inputDropDown, 1, 1);
            this.inputDataTable.Controls.Add(this.nameTypeRadioButtonTable, 1, 2);
            this.inputDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputDataTable.Location = new System.Drawing.Point(8, 25);
            this.inputDataTable.Name = "inputDataTable";
            this.inputDataTable.RowCount = 3;
            this.inputDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inputDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inputDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inputDataTable.Size = new System.Drawing.Size(750, 134);
            this.inputDataTable.TabIndex = 0;
            // 
            // mixerLabel
            // 
            this.mixerLabel.AutoSize = true;
            this.mixerLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.mixerLabel.Location = new System.Drawing.Point(3, 0);
            this.mixerLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.mixerLabel.Name = "mixerLabel";
            this.mixerLabel.Size = new System.Drawing.Size(46, 34);
            this.mixerLabel.TabIndex = 0;
            this.mixerLabel.Text = "Mixer";
            this.mixerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.inputLabel.Location = new System.Drawing.Point(3, 34);
            this.inputLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(43, 34);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "Input";
            this.inputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameTypeLabel
            // 
            this.nameTypeLabel.AutoSize = true;
            this.nameTypeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.nameTypeLabel.Location = new System.Drawing.Point(3, 68);
            this.nameTypeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.nameTypeLabel.Name = "nameTypeLabel";
            this.nameTypeLabel.Size = new System.Drawing.Size(82, 66);
            this.nameTypeLabel.TabIndex = 2;
            this.nameTypeLabel.Text = "Name type";
            this.nameTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mixerDropDown
            // 
            this.mixerDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.mixerDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mixerDropDown.FormattingEnabled = true;
            this.mixerDropDown.Location = new System.Drawing.Point(103, 3);
            this.mixerDropDown.Name = "mixerDropDown";
            this.mixerDropDown.Size = new System.Drawing.Size(377, 28);
            this.mixerDropDown.TabIndex = 3;
            // 
            // inputDropDown
            // 
            this.inputDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.inputDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputDropDown.FormattingEnabled = true;
            this.inputDropDown.Location = new System.Drawing.Point(103, 37);
            this.inputDropDown.Name = "inputDropDown";
            this.inputDropDown.Size = new System.Drawing.Size(377, 28);
            this.inputDropDown.TabIndex = 4;
            // 
            // nameTypeRadioButtonTable
            // 
            this.nameTypeRadioButtonTable.AutoSize = true;
            this.nameTypeRadioButtonTable.ColumnCount = 1;
            this.nameTypeRadioButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nameTypeRadioButtonTable.Controls.Add(this.nameTypeShortRadioButton, 0, 0);
            this.nameTypeRadioButtonTable.Controls.Add(this.nameTypeLongRadioButton, 0, 1);
            this.nameTypeRadioButtonTable.Location = new System.Drawing.Point(103, 71);
            this.nameTypeRadioButtonTable.Name = "nameTypeRadioButtonTable";
            this.nameTypeRadioButtonTable.RowCount = 2;
            this.nameTypeRadioButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nameTypeRadioButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nameTypeRadioButtonTable.Size = new System.Drawing.Size(71, 60);
            this.nameTypeRadioButtonTable.TabIndex = 5;
            // 
            // nameTypeShortRadioButton
            // 
            this.nameTypeShortRadioButton.AutoSize = true;
            this.nameTypeShortRadioButton.Location = new System.Drawing.Point(3, 3);
            this.nameTypeShortRadioButton.Name = "nameTypeShortRadioButton";
            this.nameTypeShortRadioButton.Size = new System.Drawing.Size(65, 24);
            this.nameTypeShortRadioButton.TabIndex = 0;
            this.nameTypeShortRadioButton.TabStop = true;
            this.nameTypeShortRadioButton.Text = "Short";
            this.nameTypeShortRadioButton.UseVisualStyleBackColor = true;
            // 
            // nameTypeLongRadioButton
            // 
            this.nameTypeLongRadioButton.AutoSize = true;
            this.nameTypeLongRadioButton.Location = new System.Drawing.Point(3, 33);
            this.nameTypeLongRadioButton.Name = "nameTypeLongRadioButton";
            this.nameTypeLongRadioButton.Size = new System.Drawing.Size(63, 24);
            this.nameTypeLongRadioButton.TabIndex = 1;
            this.nameTypeLongRadioButton.TabStop = true;
            this.nameTypeLongRadioButton.Text = "Long";
            this.nameTypeLongRadioButton.UseVisualStyleBackColor = true;
            // 
            // BmdAtemUmdEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 709);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(500, 661);
            this.Name = "BmdAtemUmdEditorForm";
            this.mainTabControl.ResumeLayout(false);
            this.baseDataTabPage.ResumeLayout(false);
            this.baseDataTabPage.PerformLayout();
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.inputDataGroupBox.ResumeLayout(false);
            this.inputDataGroupBox.PerformLayout();
            this.inputDataTable.ResumeLayout(false);
            this.inputDataTable.PerformLayout();
            this.nameTypeRadioButtonTable.ResumeLayout(false);
            this.nameTypeRadioButtonTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox inputDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel inputDataTable;
        private System.Windows.Forms.Label mixerLabel;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Label nameTypeLabel;
        private System.Windows.Forms.ComboBox mixerDropDown;
        private System.Windows.Forms.ComboBox inputDropDown;
        private System.Windows.Forms.TableLayoutPanel nameTypeRadioButtonTable;
        private System.Windows.Forms.RadioButton nameTypeShortRadioButton;
        private System.Windows.Forms.RadioButton nameTypeLongRadioButton;
    }
}