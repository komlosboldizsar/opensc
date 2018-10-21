namespace OpenSC.GUI.VTRs
{
    partial class VtrList
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
            this.components = new System.ComponentModel.Container();
            this.topPanelInner = new System.Windows.Forms.Panel();
            this.addableVtrTypesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVtrButton = new OpenSC.GUI.GeneralComponents.SplitButton();
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
            this.topPanelInner.Controls.Add(this.addVtrButton);
            this.topPanelInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelInner.Location = new System.Drawing.Point(0, 0);
            this.topPanelInner.Name = "topPanelInner";
            this.topPanelInner.Padding = new System.Windows.Forms.Padding(10);
            this.topPanelInner.Size = new System.Drawing.Size(800, 54);
            this.topPanelInner.TabIndex = 0;
            // 
            // addableVtrTypesMenu
            // 
            this.addableVtrTypesMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.addableVtrTypesMenu.Name = "addableVtrTypesMenu";
            this.addableVtrTypesMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // addVtrButton
            // 
            this.addVtrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addVtrButton.Location = new System.Drawing.Point(650, 13);
            this.addVtrButton.Menu = this.addableVtrTypesMenu;
            this.addVtrButton.Name = "addVtrButton";
            this.addVtrButton.Size = new System.Drawing.Size(137, 28);
            this.addVtrButton.TabIndex = 0;
            this.addVtrButton.Text = "Add VTR";
            this.addVtrButton.UseVisualStyleBackColor = true;
            // 
            // VtrList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "VtrList";
            this.Text = "List of VTRs";
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.topPanelInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanelInner;
        private GeneralComponents.SplitButton addVtrButton;
        private System.Windows.Forms.ContextMenuStrip addableVtrTypesMenu;
    }
}