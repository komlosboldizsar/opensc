namespace OpenSC.GUI.Macros
{
    partial class MacroPanelElementButton
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
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.BackColor = System.Drawing.Color.White;
            this.button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button.Location = new System.Drawing.Point(0, 0);
            this.button.Margin = new System.Windows.Forms.Padding(0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(134, 66);
            this.button.TabIndex = 0;
            this.button.Text = "ButtonText";
            this.button.UseVisualStyleBackColor = false;
            this.button.Click += new System.EventHandler(this.button_Click);
            this.button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_MouseMove);
            this.button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // MacroPanelElementButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.button);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MacroPanelElementButton";
            this.Size = new System.Drawing.Size(134, 66);
            this.Load += new System.EventHandler(this.MacroPanelElementButton_Load);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Button button;
    }
}
