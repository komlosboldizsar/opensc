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
            // baseDataTabPage
            // 
            this.baseDataTabPage.Size = new System.Drawing.Size(1054, 389);
            // 
            // outputsButtonsPanel
            // 
            this.outputsButtonsPanel.Location = new System.Drawing.Point(3, 342);
            this.outputsButtonsPanel.Size = new System.Drawing.Size(1048, 44);
            // 
            // VirtualRouterEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 553);
            this.DeleteButtonVisible = true;
            this.Name = "VirtualRouterEditorForm";
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}