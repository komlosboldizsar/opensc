namespace OpenSC.GUI.Settings
{
    partial class SettingsWindow
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.demoTabPage = new System.Windows.Forms.TabPage();
            this.mainContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.tabControl);
            this.mainContainer.Size = new System.Drawing.Size(800, 394);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.demoTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 394);
            this.tabControl.TabIndex = 0;
            // 
            // demoTabPage
            // 
            this.demoTabPage.Location = new System.Drawing.Point(4, 25);
            this.demoTabPage.Name = "demoTabPage";
            this.demoTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.demoTabPage.Size = new System.Drawing.Size(792, 365);
            this.demoTabPage.TabIndex = 0;
            this.demoTabPage.Text = "Demo page";
            this.demoTabPage.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.HeaderText = "Global settings";
            this.Name = "SettingsWindow";
            this.Text = "Global setting";
            this.mainContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage demoTabPage;
    }
}