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
            this.demoEditor4 = new OpenSC.GUI.Settings.SettingEditorBase();
            this.demoEditor3 = new OpenSC.GUI.Settings.SettingEditorBase();
            this.demoEditor2 = new OpenSC.GUI.Settings.SettingEditorBase();
            this.demoEditor1 = new OpenSC.GUI.Settings.SettingEditorBase();
            this.mainContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.demoTabPage.SuspendLayout();
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
            this.demoTabPage.AutoScroll = true;
            this.demoTabPage.Controls.Add(this.demoEditor4);
            this.demoTabPage.Controls.Add(this.demoEditor3);
            this.demoTabPage.Controls.Add(this.demoEditor2);
            this.demoTabPage.Controls.Add(this.demoEditor1);
            this.demoTabPage.Location = new System.Drawing.Point(4, 25);
            this.demoTabPage.Name = "demoTabPage";
            this.demoTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.demoTabPage.Size = new System.Drawing.Size(792, 365);
            this.demoTabPage.TabIndex = 0;
            this.demoTabPage.Text = "Demo page";
            this.demoTabPage.UseVisualStyleBackColor = true;
            // 
            // demoEditor4
            // 
            this.demoEditor4.AutoSize = true;
            this.demoEditor4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.demoEditor4.Dock = System.Windows.Forms.DockStyle.Top;
            this.demoEditor4.Location = new System.Drawing.Point(3, 291);
            this.demoEditor4.MinimumSize = new System.Drawing.Size(10, 10);
            this.demoEditor4.Name = "demoEditor4";
            this.demoEditor4.Size = new System.Drawing.Size(765, 96);
            this.demoEditor4.TabIndex = 3;
            // 
            // demoEditor3
            // 
            this.demoEditor3.AutoSize = true;
            this.demoEditor3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.demoEditor3.Dock = System.Windows.Forms.DockStyle.Top;
            this.demoEditor3.Location = new System.Drawing.Point(3, 195);
            this.demoEditor3.MinimumSize = new System.Drawing.Size(10, 10);
            this.demoEditor3.Name = "demoEditor3";
            this.demoEditor3.Size = new System.Drawing.Size(765, 96);
            this.demoEditor3.TabIndex = 2;
            // 
            // demoEditor2
            // 
            this.demoEditor2.AutoSize = true;
            this.demoEditor2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.demoEditor2.Dock = System.Windows.Forms.DockStyle.Top;
            this.demoEditor2.Location = new System.Drawing.Point(3, 99);
            this.demoEditor2.MinimumSize = new System.Drawing.Size(10, 10);
            this.demoEditor2.Name = "demoEditor2";
            this.demoEditor2.Size = new System.Drawing.Size(765, 96);
            this.demoEditor2.TabIndex = 1;
            // 
            // demoEditor1
            // 
            this.demoEditor1.AutoSize = true;
            this.demoEditor1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.demoEditor1.Dock = System.Windows.Forms.DockStyle.Top;
            this.demoEditor1.Location = new System.Drawing.Point(3, 3);
            this.demoEditor1.MinimumSize = new System.Drawing.Size(10, 10);
            this.demoEditor1.Name = "demoEditor1";
            this.demoEditor1.Size = new System.Drawing.Size(765, 96);
            this.demoEditor1.TabIndex = 0;
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
            this.demoTabPage.ResumeLayout(false);
            this.demoTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage demoTabPage;
        private SettingEditorBase demoEditor4;
        private SettingEditorBase demoEditor3;
        private SettingEditorBase demoEditor2;
        private SettingEditorBase demoEditor1;
    }
}