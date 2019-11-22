namespace OpenSC.GUI.Macros
{
    partial class MacroList
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
            this.topPanelInner = new System.Windows.Forms.Panel();
            this.addMacroButton = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.topPanelInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.topPanelInner);
            this.topPanel.Size = new System.Drawing.Size(800, 54);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 54);
            this.bottomPanel.Size = new System.Drawing.Size(800, 340);
            // 
            // topPanelInner
            // 
            this.topPanelInner.AutoSize = true;
            this.topPanelInner.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.topPanelInner.Controls.Add(this.addMacroButton);
            this.topPanelInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelInner.Location = new System.Drawing.Point(0, 0);
            this.topPanelInner.Name = "topPanelInner";
            this.topPanelInner.Padding = new System.Windows.Forms.Padding(10);
            this.topPanelInner.Size = new System.Drawing.Size(800, 54);
            this.topPanelInner.TabIndex = 0;
            // 
            // addMacroButton
            // 
            this.addMacroButton.Location = new System.Drawing.Point(650, 13);
            this.addMacroButton.Name = "addMacroButton";
            this.addMacroButton.Size = new System.Drawing.Size(137, 28);
            this.addMacroButton.TabIndex = 1;
            this.addMacroButton.Text = "Add macro";
            this.addMacroButton.UseVisualStyleBackColor = true;
            this.addMacroButton.Click += new System.EventHandler(this.addDynamicTextButton_Click);
            // 
            // MacroList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "List of macros";
            this.Name = "MacroList";
            this.Text = "List of macros";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.topPanelInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanelInner;
        private System.Windows.Forms.Button addMacroButton;
    }
}