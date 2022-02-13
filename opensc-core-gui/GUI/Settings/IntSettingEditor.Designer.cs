namespace OpenSC.GUI.Settings
{
    partial class IntSettingEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.valueNumericField = new System.Windows.Forms.NumericUpDown();
            this.minMaxHintLabel = new System.Windows.Forms.Label();
            this.customElementsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valueNumericField)).BeginInit();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.minMaxHintLabel);
            this.customElementsPanel.Controls.Add(this.valueNumericField);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.customElementsPanel.MinimumSize = new System.Drawing.Size(250, 12);
            this.customElementsPanel.Controls.SetChildIndex(this.valueNumericField, 0);
            this.customElementsPanel.Controls.SetChildIndex(this.minMaxHintLabel, 0);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 27);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(427, 27);
            this.textBox2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Location = new System.Drawing.Point(427, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 40);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // valueNumericField
            // 
            this.valueNumericField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueNumericField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.valueNumericField.Location = new System.Drawing.Point(8, 10);
            this.valueNumericField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.valueNumericField.Name = "valueNumericField";
            this.valueNumericField.Size = new System.Drawing.Size(240, 24);
            this.valueNumericField.TabIndex = 0;
            // 
            // minMaxHintLabel
            // 
            this.minMaxHintLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minMaxHintLabel.AutoSize = true;
            this.minMaxHintLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.minMaxHintLabel.Location = new System.Drawing.Point(254, 11);
            this.minMaxHintLabel.Name = "minMaxHintLabel";
            this.minMaxHintLabel.Size = new System.Drawing.Size(107, 20);
            this.minMaxHintLabel.TabIndex = 3;
            this.minMaxHintLabel.Text = "(min: ?, max: ?)";
            // 
            // IntSettingEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(200, 78);
            this.Name = "IntSettingEditor";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.Size = new System.Drawing.Size(500, 111);
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valueNumericField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown valueNumericField;
        private System.Windows.Forms.Label minMaxHintLabel;
    }
}
