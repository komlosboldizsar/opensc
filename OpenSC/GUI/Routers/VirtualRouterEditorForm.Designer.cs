﻿namespace OpenSC.GUI.Routers
{
    partial class VirtualRouterEditorForm
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
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(505, 325);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(505, 394);
            // 
            // VirtualRouterEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 450);
            this.DeleteButtonVisible = true;
            this.Name = "VirtualRouterEditorForm";
            this.Text = "VirtualRouterEditorForm";
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}