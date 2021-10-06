namespace OpenSC.GUI.Routers
{
    partial class RouterInputOutputControlBase
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
            this.button = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.BackColor = System.Drawing.Color.White;
            this.button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button.Location = new System.Drawing.Point(3, 3);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(107, 53);
            this.button.TabIndex = 0;
            this.button.Text = "ButtonText";
            this.button.UseVisualStyleBackColor = false;
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(3, 59);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(107, 38);
            this.label.TabIndex = 1;
            this.label.Text = "LabelText1\r\nLabelText2";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RouterInputOutputControlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label);
            this.Controls.Add(this.button);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "RouterInputOutputControlBase";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.Size = new System.Drawing.Size(113, 102);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label label;
        protected System.Windows.Forms.Button button;
    }
}
