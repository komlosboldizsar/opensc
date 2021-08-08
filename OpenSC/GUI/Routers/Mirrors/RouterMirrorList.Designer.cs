﻿namespace OpenSC.GUI.Routers.Mirrors
{
    partial class RouterMirrorList
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
            this.addRouterMirrorButton = new System.Windows.Forms.Button();
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
            this.topPanelInner.Controls.Add(this.addRouterMirrorButton);
            this.topPanelInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelInner.Location = new System.Drawing.Point(0, 0);
            this.topPanelInner.Name = "topPanelInner";
            this.topPanelInner.Padding = new System.Windows.Forms.Padding(10);
            this.topPanelInner.Size = new System.Drawing.Size(800, 54);
            this.topPanelInner.TabIndex = 0;
            // 
            // addRouterMirrorButton
            // 
            this.addRouterMirrorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addRouterMirrorButton.Location = new System.Drawing.Point(638, 13);
            this.addRouterMirrorButton.Name = "addRouterMirrorButton";
            this.addRouterMirrorButton.Size = new System.Drawing.Size(150, 28);
            this.addRouterMirrorButton.TabIndex = 1;
            this.addRouterMirrorButton.Text = "Add router mirror";
            this.addRouterMirrorButton.UseVisualStyleBackColor = true;
            this.addRouterMirrorButton.Click += new System.EventHandler(this.addRouterMirrorButton_Click);
            // 
            // RouterMirrorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.HeaderText = "List of router mirrors";
            this.Name = "RouterMirrorList";
            this.Text = "List of router mirrors";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.topPanelInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanelInner;
        private System.Windows.Forms.Button addRouterMirrorButton;
    }
}