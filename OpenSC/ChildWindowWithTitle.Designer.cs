namespace OpenSC.GUI
{
    partial class ChildWindowWithTitle
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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.mainContainer = new System.Windows.Forms.Panel();
            this.headerPanelBorder = new System.Windows.Forms.Panel();
            this.headerText = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.headerPanel.Controls.Add(this.headerText);
            this.headerPanel.Controls.Add(this.headerPanelBorder);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(739, 56);
            this.headerPanel.TabIndex = 0;
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 56);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(739, 362);
            this.mainContainer.TabIndex = 1;
            // 
            // headerPanelBorder
            // 
            this.headerPanelBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.headerPanelBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.headerPanelBorder.Location = new System.Drawing.Point(0, 53);
            this.headerPanelBorder.Name = "headerPanelBorder";
            this.headerPanelBorder.Size = new System.Drawing.Size(739, 3);
            this.headerPanelBorder.TabIndex = 0;
            // 
            // headerText
            // 
            this.headerText.AutoSize = true;
            this.headerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.headerText.Location = new System.Drawing.Point(3, 9);
            this.headerText.Name = "headerText";
            this.headerText.Size = new System.Drawing.Size(269, 36);
            this.headerText.TabIndex = 1;
            this.headerText.Text = "Sample header text";
            // 
            // ChildWindowBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 418);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.headerPanel);
            this.Name = "ChildWindowBase";
            this.Text = "ChildWindowBase";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerText;
        private System.Windows.Forms.Panel headerPanelBorder;
        protected System.Windows.Forms.Panel mainContainer;
    }
}