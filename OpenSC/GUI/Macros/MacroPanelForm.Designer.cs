namespace OpenSC.GUI.Macros
{
    partial class MacroPanelForm
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
            this.elementsPanel = new System.Windows.Forms.Panel();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.elementsPanel);
            this.mainContainer.Size = new System.Drawing.Size(800, 394);
            // 
            // elementsPanel
            // 
            this.elementsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementsPanel.Location = new System.Drawing.Point(0, 0);
            this.elementsPanel.Name = "elementsPanel";
            this.elementsPanel.Size = new System.Drawing.Size(800, 394);
            this.elementsPanel.TabIndex = 0;
            // 
            // MacroPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.HeaderColor = System.Drawing.Color.Gold;
            this.HeaderText = "??";
            this.Name = "MacroPanelForm";
            this.Text = "[Macro panel] ??";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MacroPanelForm_FormClosing);
            this.Load += new System.EventHandler(this.MacroPanelForm_Load);
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel elementsPanel;
    }
}