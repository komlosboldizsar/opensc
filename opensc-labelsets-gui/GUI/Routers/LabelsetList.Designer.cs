namespace OpenSC.GUI.Routers
{
    partial class LabelsetList
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
            this.addLabelsetButton = new System.Windows.Forms.Button();
            this.addableRouterTypesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.topPanelInner.Controls.Add(this.addLabelsetButton);
            this.topPanelInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelInner.Location = new System.Drawing.Point(0, 0);
            this.topPanelInner.Name = "topPanelInner";
            this.topPanelInner.Padding = new System.Windows.Forms.Padding(10);
            this.topPanelInner.Size = new System.Drawing.Size(800, 54);
            this.topPanelInner.TabIndex = 0;
            // 
            // addLabelsetButton
            // 
            this.addLabelsetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addLabelsetButton.Location = new System.Drawing.Point(650, 13);
            this.addLabelsetButton.Name = "addLabelsetButton";
            this.addLabelsetButton.Size = new System.Drawing.Size(137, 28);
            this.addLabelsetButton.TabIndex = 0;
            this.addLabelsetButton.Text = "Add labelset";
            this.addLabelsetButton.Click += new System.EventHandler(addLabelsetMenuItemClick);
            // 
            // addableRouterTypesMenu
            // 
            this.addableRouterTypesMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.addableRouterTypesMenu.Name = "addableVtrTypesMenu";
            this.addableRouterTypesMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // LabelsetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.HeaderText = "List of labelsets";
            this.Name = "LabelsetList";
            this.Text = "List of labelsets";
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
        private System.Windows.Forms.Button addLabelsetButton;
        private System.Windows.Forms.ContextMenuStrip addableRouterTypesMenu;
    }
}