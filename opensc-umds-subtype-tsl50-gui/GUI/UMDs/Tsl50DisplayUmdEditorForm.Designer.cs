namespace OpenSC.GUI.UMDs
{
    partial class Tsl50DisplayUmdEditorForm
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
            this.displayIdentifierGroupBox = new System.Windows.Forms.GroupBox();
            this.displayIdentifierTable = new System.Windows.Forms.TableLayoutPanel();
            this.screenLabel = new System.Windows.Forms.Label();
            this.screenDropDown = new System.Windows.Forms.ComboBox();
            this.indexLabel = new System.Windows.Forms.Label();
            this.indexNumericInput = new System.Windows.Forms.NumericUpDown();
            this.mainTabControl.SuspendLayout();
            this.baseDataTabPage.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.displayIdentifierGroupBox.SuspendLayout();
            this.displayIdentifierTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indexNumericInput)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Location = new System.Drawing.Point(0, 111);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mainTabControl.Size = new System.Drawing.Size(780, 317);
            // 
            // baseDataTabPage
            // 
            this.baseDataTabPage.Controls.Add(this.displayIdentifierGroupBox);
            this.baseDataTabPage.Location = new System.Drawing.Point(4, 29);
            this.baseDataTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.baseDataTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.baseDataTabPage.Size = new System.Drawing.Size(772, 284);
            this.baseDataTabPage.Controls.SetChildIndex(this.displayIdentifierGroupBox, 0);
            // 
            // dynamicDataTabPage
            // 
            this.dynamicDataTabPage.Location = new System.Drawing.Point(4, 29);
            this.dynamicDataTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dynamicDataTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dynamicDataTabPage.Size = new System.Drawing.Size(772, 284);
            // 
            // talliesTabPage
            // 
            this.talliesTabPage.Location = new System.Drawing.Point(4, 29);
            this.talliesTabPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.talliesTabPage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.talliesTabPage.Size = new System.Drawing.Size(772, 284);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Location = new System.Drawing.Point(10, 15);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.customElementsPanel.Size = new System.Drawing.Size(780, 428);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.mainContainer.Size = new System.Drawing.Size(800, 544);
            // 
            // displayIdentifierGroupBox
            // 
            this.displayIdentifierGroupBox.AutoSize = true;
            this.displayIdentifierGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.displayIdentifierGroupBox.Controls.Add(this.displayIdentifierTable);
            this.displayIdentifierGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.displayIdentifierGroupBox.Location = new System.Drawing.Point(3, 134);
            this.displayIdentifierGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.displayIdentifierGroupBox.Name = "displayIdentifierGroupBox";
            this.displayIdentifierGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.displayIdentifierGroupBox.Size = new System.Drawing.Size(766, 102);
            this.displayIdentifierGroupBox.TabIndex = 3;
            this.displayIdentifierGroupBox.TabStop = false;
            this.displayIdentifierGroupBox.Text = "Display identifier";
            // 
            // displayIdentifierTable
            // 
            this.displayIdentifierTable.AutoSize = true;
            this.displayIdentifierTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.displayIdentifierTable.ColumnCount = 2;
            this.displayIdentifierTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.displayIdentifierTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.displayIdentifierTable.Controls.Add(this.screenLabel, 0, 0);
            this.displayIdentifierTable.Controls.Add(this.screenDropDown, 1, 0);
            this.displayIdentifierTable.Controls.Add(this.indexLabel, 0, 1);
            this.displayIdentifierTable.Controls.Add(this.indexNumericInput, 1, 1);
            this.displayIdentifierTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayIdentifierTable.Location = new System.Drawing.Point(8, 25);
            this.displayIdentifierTable.Name = "displayIdentifierTable";
            this.displayIdentifierTable.RowCount = 2;
            this.displayIdentifierTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.displayIdentifierTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.displayIdentifierTable.Size = new System.Drawing.Size(750, 67);
            this.displayIdentifierTable.TabIndex = 0;
            // 
            // screenLabel
            // 
            this.screenLabel.AutoSize = true;
            this.screenLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.screenLabel.Location = new System.Drawing.Point(3, 0);
            this.screenLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.screenLabel.Name = "screenLabel";
            this.screenLabel.Size = new System.Drawing.Size(53, 34);
            this.screenLabel.TabIndex = 0;
            this.screenLabel.Text = "Screen";
            this.screenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // screenDropDown
            // 
            this.screenDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.screenDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.screenDropDown.FormattingEnabled = true;
            this.screenDropDown.Location = new System.Drawing.Point(74, 3);
            this.screenDropDown.Name = "screenDropDown";
            this.screenDropDown.Size = new System.Drawing.Size(322, 28);
            this.screenDropDown.TabIndex = 1;
            // 
            // indexLabel
            // 
            this.indexLabel.AutoSize = true;
            this.indexLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.indexLabel.Location = new System.Drawing.Point(3, 34);
            this.indexLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.indexLabel.Name = "indexLabel";
            this.indexLabel.Size = new System.Drawing.Size(45, 33);
            this.indexLabel.TabIndex = 2;
            this.indexLabel.Text = "Index";
            this.indexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // indexNumericInput
            // 
            this.indexNumericInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.indexNumericInput.Location = new System.Drawing.Point(74, 37);
            this.indexNumericInput.Name = "indexNumericInput";
            this.indexNumericInput.Size = new System.Drawing.Size(151, 27);
            this.indexNumericInput.TabIndex = 3;
            // 
            // Tsl50DisplayUmdEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 614);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(500, 661);
            this.Name = "Tsl50DisplayUmdEditorForm";
            this.mainTabControl.ResumeLayout(false);
            this.baseDataTabPage.ResumeLayout(false);
            this.baseDataTabPage.PerformLayout();
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.displayIdentifierGroupBox.ResumeLayout(false);
            this.displayIdentifierGroupBox.PerformLayout();
            this.displayIdentifierTable.ResumeLayout(false);
            this.displayIdentifierTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indexNumericInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox displayIdentifierGroupBox;
        private System.Windows.Forms.TableLayoutPanel displayIdentifierTable;
        private System.Windows.Forms.Label screenLabel;
        private System.Windows.Forms.ComboBox screenDropDown;
        private System.Windows.Forms.Label indexLabel;
        private System.Windows.Forms.NumericUpDown indexNumericInput;
    }
}