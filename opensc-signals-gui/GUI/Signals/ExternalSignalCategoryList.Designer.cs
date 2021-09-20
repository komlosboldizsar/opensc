namespace OpenSC.GUI.Signals
{
    partial class ExternalSignalCategoryList
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
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(736, 54);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Size = new System.Drawing.Size(736, 307);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(736, 361);
            // 
            // ExternalSignalCategoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 417);
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "List of signal categories";
            this.Name = "ExternalSignalCategoryList";
            this.Text = "List of signal categories";
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}