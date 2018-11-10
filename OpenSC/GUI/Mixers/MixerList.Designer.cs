namespace OpenSC.GUI.Mixers
{
    partial class MixerList
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
            this.addMixerButton = new OpenSC.GUI.GeneralComponents.SplitButton();
            this.addableMixerTypesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.topPanelInner.Controls.Add(this.addMixerButton);
            this.topPanelInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelInner.Location = new System.Drawing.Point(0, 0);
            this.topPanelInner.Name = "topPanelInner";
            this.topPanelInner.Padding = new System.Windows.Forms.Padding(10);
            this.topPanelInner.Size = new System.Drawing.Size(800, 54);
            this.topPanelInner.TabIndex = 0;
            // 
            // addMixerButton
            // 
            this.addMixerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addMixerButton.Location = new System.Drawing.Point(650, 13);
            this.addMixerButton.Menu = this.addableMixerTypesMenu;
            this.addMixerButton.Name = "addMixerButton";
            this.addMixerButton.Size = new System.Drawing.Size(137, 28);
            this.addMixerButton.TabIndex = 0;
            this.addMixerButton.Text = "Add mixer";
            this.addMixerButton.UseVisualStyleBackColor = true;
            // 
            // addableMixerTypesMenu
            // 
            this.addableMixerTypesMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.addableMixerTypesMenu.Name = "addableVtrTypesMenu";
            this.addableMixerTypesMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // MixerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.HeaderText = "List of mixers";
            this.Name = "MixerList";
            this.Text = "List of mixers";
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
        private GeneralComponents.SplitButton addMixerButton;
        private System.Windows.Forms.ContextMenuStrip addableMixerTypesMenu;
    }
}