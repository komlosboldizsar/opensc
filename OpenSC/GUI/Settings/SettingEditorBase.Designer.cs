namespace OpenSC.GUI.Settings
{
    partial class SettingEditorBase
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.customElementsPanel = new System.Windows.Forms.Panel();
            this.mainContainer = new System.Windows.Forms.Panel();
            this.settingDescriptionLabel = new System.Windows.Forms.Label();
            this.settingTitleLabel = new System.Windows.Forms.Label();
            this.settingDescriptionContainer = new System.Windows.Forms.Panel();
            this.settingTitleContainer = new System.Windows.Forms.Panel();
            this.mainContainer.SuspendLayout();
            this.settingDescriptionContainer.SuspendLayout();
            this.settingTitleContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.AutoSize = true;
            this.customElementsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customElementsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.customElementsPanel.Location = new System.Drawing.Point(2, 52);
            this.customElementsPanel.MinimumSize = new System.Drawing.Size(40, 10);
            this.customElementsPanel.Name = "customElementsPanel";
            this.customElementsPanel.Size = new System.Drawing.Size(494, 10);
            this.customElementsPanel.TabIndex = 1;
            // 
            // mainContainer
            // 
            this.mainContainer.AutoSize = true;
            this.mainContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainContainer.Controls.Add(this.customElementsPanel);
            this.mainContainer.Controls.Add(this.settingDescriptionContainer);
            this.mainContainer.Controls.Add(this.settingTitleContainer);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.MinimumSize = new System.Drawing.Size(500, 2);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Padding = new System.Windows.Forms.Padding(2);
            this.mainContainer.Size = new System.Drawing.Size(500, 66);
            this.mainContainer.TabIndex = 0;
            // 
            // settingDescriptionLabel
            // 
            this.settingDescriptionLabel.AutoSize = true;
            this.settingDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingDescriptionLabel.Location = new System.Drawing.Point(3, 1);
            this.settingDescriptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.settingDescriptionLabel.Name = "settingDescriptionLabel";
            this.settingDescriptionLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.settingDescriptionLabel.Size = new System.Drawing.Size(142, 17);
            this.settingDescriptionLabel.TabIndex = 3;
            this.settingDescriptionLabel.Text = "Description of setting";
            // 
            // settingTitleLabel
            // 
            this.settingTitleLabel.AutoSize = true;
            this.settingTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.settingTitleLabel.Location = new System.Drawing.Point(3, 3);
            this.settingTitleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.settingTitleLabel.Name = "settingTitleLabel";
            this.settingTitleLabel.Size = new System.Drawing.Size(115, 18);
            this.settingTitleLabel.TabIndex = 4;
            this.settingTitleLabel.Text = "Title of setting";
            // 
            // settingDescriptionContainer
            // 
            this.settingDescriptionContainer.AutoSize = true;
            this.settingDescriptionContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.settingDescriptionContainer.Controls.Add(this.settingDescriptionLabel);
            this.settingDescriptionContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingDescriptionContainer.Location = new System.Drawing.Point(2, 29);
            this.settingDescriptionContainer.Name = "settingDescriptionContainer";
            this.settingDescriptionContainer.Padding = new System.Windows.Forms.Padding(3, 1, 3, 5);
            this.settingDescriptionContainer.Size = new System.Drawing.Size(494, 23);
            this.settingDescriptionContainer.TabIndex = 6;
            // 
            // settingTitleContainer
            // 
            this.settingTitleContainer.AutoSize = true;
            this.settingTitleContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.settingTitleContainer.Controls.Add(this.settingTitleLabel);
            this.settingTitleContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingTitleContainer.Location = new System.Drawing.Point(2, 2);
            this.settingTitleContainer.Name = "settingTitleContainer";
            this.settingTitleContainer.Padding = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.settingTitleContainer.Size = new System.Drawing.Size(494, 27);
            this.settingTitleContainer.TabIndex = 5;
            // 
            // SettingEditorBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.mainContainer);
            this.MinimumSize = new System.Drawing.Size(200, 50);
            this.Name = "SettingEditorBase";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.Size = new System.Drawing.Size(500, 70);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.settingDescriptionContainer.ResumeLayout(false);
            this.settingDescriptionContainer.PerformLayout();
            this.settingTitleContainer.ResumeLayout(false);
            this.settingTitleContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label settingTitleLabel;
        private System.Windows.Forms.Label settingDescriptionLabel;
        protected System.Windows.Forms.Panel customElementsPanel;
        private System.Windows.Forms.Panel mainContainer;
        private System.Windows.Forms.Panel settingDescriptionContainer;
        private System.Windows.Forms.Panel settingTitleContainer;
    }
}
