namespace OpenSC.GUI.UMDs
{
    partial class McCurdyUmd1EditorForm
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
            this.portLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.addressNumericInput = new System.Windows.Forms.NumericUpDown();
            this.portDropDown = new System.Windows.Forms.ComboBox();
            this.textsLayoutGroupBox = new System.Windows.Forms.GroupBox();
            this.textColumnWidthLabelsTable = new System.Windows.Forms.TableLayoutPanel();
            this.textColumnWidthLabelExample = new OpenSC.GUI.GeneralComponents.GoodOneLineLabel();
            this.textColumnWidthNumericFieldsTable = new System.Windows.Forms.TableLayoutPanel();
            this.textColumnWidthNumericFieldExample = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.mainTabControl.SuspendLayout();
            this.textsTabPage.SuspendLayout();
            this.connectionTabPage.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.connectionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addressNumericInput)).BeginInit();
            this.textsLayoutGroupBox.SuspendLayout();
            this.textColumnWidthLabelsTable.SuspendLayout();
            this.textColumnWidthNumericFieldsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textColumnWidthNumericFieldExample)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Size = new System.Drawing.Size(775, 402);
            // 
            // textsTabPage
            // 
            this.textsTabPage.Controls.Add(this.textsLayoutGroupBox);
            this.textsTabPage.Size = new System.Drawing.Size(767, 369);
            this.textsTabPage.Controls.SetChildIndex(this.textsLayoutGroupBox, 0);
            // 
            // fullStaticTextTabPage
            // 
            this.fullStaticTextTabPage.Size = new System.Drawing.Size(687, 254);
            // 
            // connectionTabPage
            // 
            this.connectionTabPage.Controls.Add(this.connectionGroupBox);
            this.connectionTabPage.Size = new System.Drawing.Size(767, 369);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(775, 638);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(795, 748);
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
            this.connectionGroupBox.Size = new System.Drawing.Size(761, 102);
            this.connectionGroupBox.TabIndex = 3;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Connection";
            // 
            // connectionTable
            // 
            this.connectionTable.AutoSize = true;
            this.connectionTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionTable.ColumnCount = 2;
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.connectionTable.Controls.Add(this.portLabel, 0, 0);
            this.connectionTable.Controls.Add(this.addressLabel, 0, 1);
            this.connectionTable.Controls.Add(this.addressNumericInput, 1, 1);
            this.connectionTable.Controls.Add(this.portDropDown, 1, 0);
            this.connectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTable.Location = new System.Drawing.Point(8, 25);
            this.connectionTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 2;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(745, 67);
            this.connectionTable.TabIndex = 0;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portLabel.Location = new System.Drawing.Point(3, 0);
            this.portLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(35, 34);
            this.portLabel.TabIndex = 0;
            this.portLabel.Text = "Port";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.addressLabel.Location = new System.Drawing.Point(3, 34);
            this.addressLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(62, 33);
            this.addressLabel.TabIndex = 1;
            this.addressLabel.Text = "Address";
            this.addressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addressNumericInput
            // 
            this.addressNumericInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.addressNumericInput.Location = new System.Drawing.Point(83, 37);
            this.addressNumericInput.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.addressNumericInput.Name = "addressNumericInput";
            this.addressNumericInput.Size = new System.Drawing.Size(150, 27);
            this.addressNumericInput.TabIndex = 2;
            // 
            // portDropDown
            // 
            this.portDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.portDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portDropDown.FormattingEnabled = true;
            this.portDropDown.Location = new System.Drawing.Point(83, 3);
            this.portDropDown.Name = "portDropDown";
            this.portDropDown.Size = new System.Drawing.Size(305, 28);
            this.portDropDown.TabIndex = 3;
            // 
            // textsLayoutGroupBox
            // 
            this.textsLayoutGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textsLayoutGroupBox.Controls.Add(this.textColumnWidthLabelsTable);
            this.textsLayoutGroupBox.Controls.Add(this.textColumnWidthNumericFieldsTable);
            this.textsLayoutGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.textsLayoutGroupBox.Location = new System.Drawing.Point(3, 165);
            this.textsLayoutGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.textsLayoutGroupBox.Name = "textsLayoutGroupBox";
            this.textsLayoutGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.textsLayoutGroupBox.Size = new System.Drawing.Size(761, 135);
            this.textsLayoutGroupBox.TabIndex = 5;
            this.textsLayoutGroupBox.TabStop = false;
            this.textsLayoutGroupBox.Text = "Layout";
            // 
            // textColumnWidthLabelsTable
            // 
            this.textColumnWidthLabelsTable.ColumnCount = 1;
            this.textColumnWidthLabelsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.textColumnWidthLabelsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.textColumnWidthLabelsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.textColumnWidthLabelsTable.Controls.Add(this.textColumnWidthLabelExample, 0, 0);
            this.textColumnWidthLabelsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textColumnWidthLabelsTable.Location = new System.Drawing.Point(8, 58);
            this.textColumnWidthLabelsTable.Name = "textColumnWidthLabelsTable";
            this.textColumnWidthLabelsTable.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.textColumnWidthLabelsTable.RowCount = 1;
            this.textColumnWidthLabelsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.textColumnWidthLabelsTable.Size = new System.Drawing.Size(745, 67);
            this.textColumnWidthLabelsTable.TabIndex = 1;
            // 
            // textColumnWidthLabelExample
            // 
            this.textColumnWidthLabelExample.BackColor = System.Drawing.Color.DarkBlue;
            this.textColumnWidthLabelExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textColumnWidthLabelExample.ForeColor = System.Drawing.Color.White;
            this.textColumnWidthLabelExample.Location = new System.Drawing.Point(2, 5);
            this.textColumnWidthLabelExample.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.textColumnWidthLabelExample.Name = "textColumnWidthLabelExample";
            this.textColumnWidthLabelExample.Padding = new System.Windows.Forms.Padding(5);
            this.textColumnWidthLabelExample.Size = new System.Drawing.Size(741, 62);
            this.textColumnWidthLabelExample.TabIndex = 0;
            this.textColumnWidthLabelExample.Text = "Text #N";
            this.textColumnWidthLabelExample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textColumnWidthNumericFieldsTable
            // 
            this.textColumnWidthNumericFieldsTable.AutoSize = true;
            this.textColumnWidthNumericFieldsTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textColumnWidthNumericFieldsTable.ColumnCount = 3;
            this.textColumnWidthNumericFieldsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.textColumnWidthNumericFieldsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.textColumnWidthNumericFieldsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.textColumnWidthNumericFieldsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.textColumnWidthNumericFieldsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.textColumnWidthNumericFieldsTable.Controls.Add(this.textColumnWidthNumericFieldExample, 1, 0);
            this.textColumnWidthNumericFieldsTable.Controls.Add(this.label4, 0, 0);
            this.textColumnWidthNumericFieldsTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.textColumnWidthNumericFieldsTable.Location = new System.Drawing.Point(8, 25);
            this.textColumnWidthNumericFieldsTable.Name = "textColumnWidthNumericFieldsTable";
            this.textColumnWidthNumericFieldsTable.RowCount = 1;
            this.textColumnWidthNumericFieldsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.textColumnWidthNumericFieldsTable.Size = new System.Drawing.Size(745, 33);
            this.textColumnWidthNumericFieldsTable.TabIndex = 0;
            // 
            // textColumnWidthNumericFieldExample
            // 
            this.textColumnWidthNumericFieldExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textColumnWidthNumericFieldExample.Location = new System.Drawing.Point(300, 3);
            this.textColumnWidthNumericFieldExample.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.textColumnWidthNumericFieldExample.Name = "textColumnWidthNumericFieldExample";
            this.textColumnWidthNumericFieldExample.Size = new System.Drawing.Size(144, 27);
            this.textColumnWidthNumericFieldExample.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 33);
            this.label4.TabIndex = 3;
            this.label4.Text = "Column widths:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // McCurdyUmd1EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 818);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(500, 661);
            this.Name = "McCurdyUmd1EditorForm";
            this.mainTabControl.ResumeLayout(false);
            this.textsTabPage.ResumeLayout(false);
            this.textsTabPage.PerformLayout();
            this.connectionTabPage.ResumeLayout(false);
            this.connectionTabPage.PerformLayout();
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addressNumericInput)).EndInit();
            this.textsLayoutGroupBox.ResumeLayout(false);
            this.textsLayoutGroupBox.PerformLayout();
            this.textColumnWidthLabelsTable.ResumeLayout(false);
            this.textColumnWidthNumericFieldsTable.ResumeLayout(false);
            this.textColumnWidthNumericFieldsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textColumnWidthNumericFieldExample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.NumericUpDown addressNumericInput;
        private System.Windows.Forms.ComboBox portDropDown;
        private System.Windows.Forms.GroupBox textsLayoutGroupBox;
        private System.Windows.Forms.TableLayoutPanel textColumnWidthLabelsTable;
        private System.Windows.Forms.TableLayoutPanel textColumnWidthNumericFieldsTable;
        private System.Windows.Forms.NumericUpDown textColumnWidthNumericFieldExample;
        private System.Windows.Forms.Label label4;
        private GeneralComponents.GoodOneLineLabel textColumnWidthLabelExample;
    }
}