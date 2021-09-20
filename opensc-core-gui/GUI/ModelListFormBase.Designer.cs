namespace OpenSC.GUI
{
    partial class ModelListFormBase
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
            this.addItemButton = new OpenSC.GUI.GeneralComponents.SplitButton();
            this.addableItemTypesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.topPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.topPanelInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.topPanelInner);
            this.topPanel.Size = new System.Drawing.Size(800, 59);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 59);
            this.bottomPanel.Size = new System.Drawing.Size(800, 335);
            // 
            // topPanelInner
            // 
            this.topPanelInner.AutoSize = true;
            this.topPanelInner.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.topPanelInner.Controls.Add(this.addItemButton);
            this.topPanelInner.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanelInner.Location = new System.Drawing.Point(0, 0);
            this.topPanelInner.Name = "topPanelInner";
            this.topPanelInner.Padding = new System.Windows.Forms.Padding(10);
            this.topPanelInner.Size = new System.Drawing.Size(800, 59);
            this.topPanelInner.TabIndex = 0;
            // 
            // addItemButton
            // 
            this.addItemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addItemButton.AutoSize = true;
            this.addItemButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addItemButton.Location = new System.Drawing.Point(660, 13);
            this.addItemButton.Menu = this.addableItemTypesMenu;
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Padding = new System.Windows.Forms.Padding(3);
            this.addItemButton.Size = new System.Drawing.Size(127, 33);
            this.addItemButton.TabIndex = 0;
            this.addItemButton.Text = "Add new subject";
            this.addItemButton.UseVisualStyleBackColor = true;
            // 
            // addableItemTypesMenu
            // 
            this.addableItemTypesMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.addableItemTypesMenu.Name = "addableVtrTypesMenu";
            this.addableItemTypesMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // ModelListFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ModelListFormBase";
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.topPanelInner.ResumeLayout(false);
            this.topPanelInner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanelInner;
        private GeneralComponents.SplitButton addItemButton;
        private System.Windows.Forms.ContextMenuStrip addableItemTypesMenu;
    }
}