namespace OpenSC.GUI.UMDs
{
    partial class Tsl31UmdEditorForm
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
            this.mainTabControl.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Size = new System.Drawing.Size(780, 305);
            // 
            // tabPage1
            // 
            this.baseDataTabPage.Size = new System.Drawing.Size(772, 276);
            // 
            // dynamicDataTabPage
            // 
            this.dynamicDataTabPage.Size = new System.Drawing.Size(481, 260);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(780, 305);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(800, 394);
            // 
            // Tsl31UmdEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DeleteButtonVisible = true;
            this.Name = "Tsl31UmdEditorForm";
            this.mainTabControl.ResumeLayout(false);
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}