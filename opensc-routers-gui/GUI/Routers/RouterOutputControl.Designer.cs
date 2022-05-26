namespace OpenSC.GUI.Routers
{
    partial class RouterOutputControl
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
            this.components = new System.ComponentModel.Container();
            this.protectButton = new System.Windows.Forms.Button();
            this.lockButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
            this.button.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.button.Text = "ButtonText\r\ntwo lines";
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // protectButton
            // 
            this.protectButton.BackColor = System.Drawing.Color.White;
            this.protectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.protectButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.protectButton.Location = new System.Drawing.Point(5, 6);
            this.protectButton.Name = "protectButton";
            this.protectButton.Size = new System.Drawing.Size(27, 27);
            this.protectButton.TabIndex = 2;
            this.protectButton.Text = "P";
            this.protectButton.UseVisualStyleBackColor = false;
            // 
            // lockButton
            // 
            this.lockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lockButton.BackColor = System.Drawing.Color.White;
            this.lockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lockButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lockButton.Location = new System.Drawing.Point(99, 6);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(27, 27);
            this.lockButton.TabIndex = 3;
            this.lockButton.Text = "L";
            this.lockButton.UseVisualStyleBackColor = false;
            // 
            // RouterOutputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.protectButton);
            this.Controls.Add(this.lockButton);
            this.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.Name = "RouterOutputControl";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.Size = new System.Drawing.Size(131, 147);
            this.Load += new System.EventHandler(this.RouterOutputControl_Load);
            this.Controls.SetChildIndex(this.button, 0);
            this.Controls.SetChildIndex(this.label, 0);
            this.Controls.SetChildIndex(this.lockButton, 0);
            this.Controls.SetChildIndex(this.protectButton, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button protectButton;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
