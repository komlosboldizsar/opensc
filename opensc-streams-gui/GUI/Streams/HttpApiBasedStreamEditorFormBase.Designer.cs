namespace OpenSC.GUI.Streams
{
    partial class HttpApiBasedStreamEditorFormBase
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
            this.restApiGroupBox = new System.Windows.Forms.GroupBox();
            this.restApiTable = new System.Windows.Forms.TableLayoutPanel();
            this.updateIntervalLabel = new System.Windows.Forms.Label();
            this.updateIntervalNumericField = new System.Windows.Forms.NumericUpDown();
            this.periodicUpdateEnabledLabel = new System.Windows.Forms.Label();
            this.periodicUpdateEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.restApiGroupBox.SuspendLayout();
            this.restApiTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateIntervalNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.restApiGroupBox);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 19, 10, 0);
            this.customElementsPanel.Controls.SetChildIndex(this.restApiGroupBox, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            // 
            // restApiGroupBox
            // 
            this.restApiGroupBox.AutoSize = true;
            this.restApiGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.restApiGroupBox.Controls.Add(this.restApiTable);
            this.restApiGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.restApiGroupBox.Location = new System.Drawing.Point(10, 124);
            this.restApiGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.restApiGroupBox.Name = "restApiGroupBox";
            this.restApiGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.restApiGroupBox.Size = new System.Drawing.Size(462, 91);
            this.restApiGroupBox.TabIndex = 3;
            this.restApiGroupBox.TabStop = false;
            this.restApiGroupBox.Text = "REST API";
            // 
            // restApiTable
            // 
            this.restApiTable.AutoSize = true;
            this.restApiTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.restApiTable.ColumnCount = 2;
            this.restApiTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.restApiTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.restApiTable.Controls.Add(this.updateIntervalLabel, 0, 0);
            this.restApiTable.Controls.Add(this.updateIntervalNumericField, 1, 0);
            this.restApiTable.Controls.Add(this.periodicUpdateEnabledLabel, 0, 1);
            this.restApiTable.Controls.Add(this.periodicUpdateEnabledCheckBox, 1, 1);
            this.restApiTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restApiTable.Location = new System.Drawing.Point(8, 25);
            this.restApiTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.restApiTable.Name = "restApiTable";
            this.restApiTable.RowCount = 2;
            this.restApiTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.restApiTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.restApiTable.Size = new System.Drawing.Size(446, 56);
            this.restApiTable.TabIndex = 0;
            // 
            // updateIntervalLabel
            // 
            this.updateIntervalLabel.AutoSize = true;
            this.updateIntervalLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.updateIntervalLabel.Location = new System.Drawing.Point(3, 0);
            this.updateIntervalLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.updateIntervalLabel.Name = "updateIntervalLabel";
            this.updateIntervalLabel.Size = new System.Drawing.Size(178, 33);
            this.updateIntervalLabel.TabIndex = 0;
            this.updateIntervalLabel.Text = "Update interval (seconds)";
            this.updateIntervalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // updateIntervalNumericField
            // 
            this.updateIntervalNumericField.Dock = System.Windows.Forms.DockStyle.Left;
            this.updateIntervalNumericField.Location = new System.Drawing.Point(199, 3);
            this.updateIntervalNumericField.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.updateIntervalNumericField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updateIntervalNumericField.Name = "updateIntervalNumericField";
            this.updateIntervalNumericField.Size = new System.Drawing.Size(150, 27);
            this.updateIntervalNumericField.TabIndex = 1;
            this.updateIntervalNumericField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // periodicUpdateEnabledLabel
            // 
            this.periodicUpdateEnabledLabel.AutoSize = true;
            this.periodicUpdateEnabledLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.periodicUpdateEnabledLabel.Location = new System.Drawing.Point(3, 33);
            this.periodicUpdateEnabledLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.periodicUpdateEnabledLabel.Name = "periodicUpdateEnabledLabel";
            this.periodicUpdateEnabledLabel.Size = new System.Drawing.Size(171, 23);
            this.periodicUpdateEnabledLabel.TabIndex = 2;
            this.periodicUpdateEnabledLabel.Text = "Periodic update enabled";
            this.periodicUpdateEnabledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // periodicUpdateEnabledCheckBox
            // 
            this.periodicUpdateEnabledCheckBox.AutoSize = true;
            this.periodicUpdateEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.periodicUpdateEnabledCheckBox.Location = new System.Drawing.Point(199, 36);
            this.periodicUpdateEnabledCheckBox.Name = "periodicUpdateEnabledCheckBox";
            this.periodicUpdateEnabledCheckBox.Size = new System.Drawing.Size(18, 17);
            this.periodicUpdateEnabledCheckBox.TabIndex = 3;
            this.periodicUpdateEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // HttpApiBasedStreamEditorFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 562);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(500, 346);
            this.Name = "HttpApiBasedStreamEditorFormBase";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.restApiGroupBox.ResumeLayout(false);
            this.restApiGroupBox.PerformLayout();
            this.restApiTable.ResumeLayout(false);
            this.restApiTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateIntervalNumericField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox restApiGroupBox;
        private System.Windows.Forms.TableLayoutPanel restApiTable;
        private System.Windows.Forms.Label updateIntervalLabel;
        private System.Windows.Forms.NumericUpDown updateIntervalNumericField;
        private System.Windows.Forms.Label periodicUpdateEnabledLabel;
        private System.Windows.Forms.CheckBox periodicUpdateEnabledCheckBox;
    }
}