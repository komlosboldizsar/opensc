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
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.connectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.screenLabel = new System.Windows.Forms.Label();
            this.indexLabel = new System.Windows.Forms.Label();
            this.indexNumericInput = new System.Windows.Forms.NumericUpDown();
            this.screenDropDown = new System.Windows.Forms.ComboBox();
            this.mainTabControl.SuspendLayout();
            this.connectionTabPage.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.connectionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indexNumericInput)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Size = new System.Drawing.Size(780, 198);
            // 
            // connectionTabPage
            // 
            this.connectionTabPage.Controls.Add(this.connectionGroupBox);
            this.connectionTabPage.Size = new System.Drawing.Size(772, 165);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(780, 434);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(800, 544);
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.AutoSize = true;
            this.connectionGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionGroupBox.Controls.Add(this.connectionTable);
            this.connectionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionGroupBox.Location = new System.Drawing.Point(3, 3);
            this.connectionGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.connectionGroupBox.Size = new System.Drawing.Size(766, 102);
            this.connectionGroupBox.TabIndex = 1;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Display data";
            // 
            // connectionTable
            // 
            this.connectionTable.AutoSize = true;
            this.connectionTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionTable.ColumnCount = 2;
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.connectionTable.Controls.Add(this.screenLabel, 0, 0);
            this.connectionTable.Controls.Add(this.indexLabel, 0, 1);
            this.connectionTable.Controls.Add(this.indexNumericInput, 1, 1);
            this.connectionTable.Controls.Add(this.screenDropDown, 1, 0);
            this.connectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTable.Location = new System.Drawing.Point(8, 25);
            this.connectionTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 2;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(750, 67);
            this.connectionTable.TabIndex = 0;
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
            // indexLabel
            // 
            this.indexLabel.AutoSize = true;
            this.indexLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.indexLabel.Location = new System.Drawing.Point(3, 34);
            this.indexLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.indexLabel.Name = "indexLabel";
            this.indexLabel.Size = new System.Drawing.Size(45, 33);
            this.indexLabel.TabIndex = 1;
            this.indexLabel.Text = "Index";
            this.indexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // indexNumericInput
            // 
            this.indexNumericInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.indexNumericInput.Location = new System.Drawing.Point(74, 37);
            this.indexNumericInput.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.indexNumericInput.Name = "indexNumericInput";
            this.indexNumericInput.Size = new System.Drawing.Size(150, 27);
            this.indexNumericInput.TabIndex = 2;
            // 
            // screenDropDown
            // 
            this.screenDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.screenDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.screenDropDown.FormattingEnabled = true;
            this.screenDropDown.Location = new System.Drawing.Point(74, 3);
            this.screenDropDown.Name = "screenDropDown";
            this.screenDropDown.Size = new System.Drawing.Size(305, 28);
            this.screenDropDown.TabIndex = 3;
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
            this.connectionTabPage.ResumeLayout(false);
            this.connectionTabPage.PerformLayout();
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indexNumericInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label screenLabel;
        private System.Windows.Forms.Label indexLabel;
        private System.Windows.Forms.NumericUpDown indexNumericInput;
        private System.Windows.Forms.ComboBox screenDropDown;
    }
}