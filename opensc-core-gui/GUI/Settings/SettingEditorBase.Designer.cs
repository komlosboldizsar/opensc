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
            this.resetToDefaultButton = new System.Windows.Forms.Button();
            this.resetToCurrentButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.mainContainer = new System.Windows.Forms.Panel();
            this.settingDescriptionContainer = new System.Windows.Forms.Panel();
            this.settingDescriptionLabel = new System.Windows.Forms.Label();
            this.settingTitleContainer = new System.Windows.Forms.Panel();
            this.settingTitleLabel = new System.Windows.Forms.Label();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.settingDescriptionContainer.SuspendLayout();
            this.settingTitleContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.AutoSize = true;
            this.customElementsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customElementsPanel.Controls.Add(this.resetToDefaultButton);
            this.customElementsPanel.Controls.Add(this.resetToCurrentButton);
            this.customElementsPanel.Controls.Add(this.saveButton);
            this.customElementsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.customElementsPanel.Location = new System.Drawing.Point(2, 59);
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.customElementsPanel.MinimumSize = new System.Drawing.Size(40, 12);
            this.customElementsPanel.Name = "customElementsPanel";
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.customElementsPanel.Size = new System.Drawing.Size(494, 42);
            this.customElementsPanel.TabIndex = 1;
            // 
            // resetToDefaultButton
            // 
            this.resetToDefaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetToDefaultButton.BackgroundImage = global::OpenSC.GUI.GeneralIcons._16_x;
            this.resetToDefaultButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetToDefaultButton.Location = new System.Drawing.Point(371, 6);
            this.resetToDefaultButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resetToDefaultButton.Name = "resetToDefaultButton";
            this.resetToDefaultButton.Size = new System.Drawing.Size(34, 30);
            this.resetToDefaultButton.TabIndex = 5;
            this.resetToDefaultButton.UseVisualStyleBackColor = true;
            this.resetToDefaultButton.Click += new System.EventHandler(this.resetToDefaultButton_Click);
            // 
            // resetToCurrentButton
            // 
            this.resetToCurrentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetToCurrentButton.BackgroundImage = global::OpenSC.GUI.GeneralIcons._16_reset;
            this.resetToCurrentButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.resetToCurrentButton.Location = new System.Drawing.Point(411, 6);
            this.resetToCurrentButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resetToCurrentButton.Name = "resetToCurrentButton";
            this.resetToCurrentButton.Size = new System.Drawing.Size(34, 30);
            this.resetToCurrentButton.TabIndex = 4;
            this.resetToCurrentButton.UseVisualStyleBackColor = true;
            this.resetToCurrentButton.Click += new System.EventHandler(this.resetToCurrentButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.BackgroundImage = global::OpenSC.GUI.GeneralIcons._16_tick;
            this.saveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.saveButton.Location = new System.Drawing.Point(451, 6);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(34, 30);
            this.saveButton.TabIndex = 3;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
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
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainContainer.MinimumSize = new System.Drawing.Size(500, 2);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Padding = new System.Windows.Forms.Padding(2);
            this.mainContainer.Size = new System.Drawing.Size(500, 105);
            this.mainContainer.TabIndex = 0;
            // 
            // settingDescriptionContainer
            // 
            this.settingDescriptionContainer.AutoSize = true;
            this.settingDescriptionContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.settingDescriptionContainer.Controls.Add(this.settingDescriptionLabel);
            this.settingDescriptionContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingDescriptionContainer.Location = new System.Drawing.Point(2, 32);
            this.settingDescriptionContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.settingDescriptionContainer.Name = "settingDescriptionContainer";
            this.settingDescriptionContainer.Padding = new System.Windows.Forms.Padding(3, 1, 3, 6);
            this.settingDescriptionContainer.Size = new System.Drawing.Size(494, 27);
            this.settingDescriptionContainer.TabIndex = 6;
            // 
            // settingDescriptionLabel
            // 
            this.settingDescriptionLabel.AutoSize = true;
            this.settingDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingDescriptionLabel.Location = new System.Drawing.Point(3, 1);
            this.settingDescriptionLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.settingDescriptionLabel.Name = "settingDescriptionLabel";
            this.settingDescriptionLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.settingDescriptionLabel.Size = new System.Drawing.Size(153, 20);
            this.settingDescriptionLabel.TabIndex = 3;
            this.settingDescriptionLabel.Text = "Description of setting";
            // 
            // settingTitleContainer
            // 
            this.settingTitleContainer.AutoSize = true;
            this.settingTitleContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.settingTitleContainer.Controls.Add(this.settingTitleLabel);
            this.settingTitleContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingTitleContainer.Location = new System.Drawing.Point(2, 2);
            this.settingTitleContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.settingTitleContainer.Name = "settingTitleContainer";
            this.settingTitleContainer.Padding = new System.Windows.Forms.Padding(3, 4, 3, 8);
            this.settingTitleContainer.Size = new System.Drawing.Size(494, 30);
            this.settingTitleContainer.TabIndex = 5;
            // 
            // settingTitleLabel
            // 
            this.settingTitleLabel.AutoSize = true;
            this.settingTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.settingTitleLabel.Location = new System.Drawing.Point(3, 4);
            this.settingTitleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.settingTitleLabel.Name = "settingTitleLabel";
            this.settingTitleLabel.Size = new System.Drawing.Size(115, 18);
            this.settingTitleLabel.TabIndex = 4;
            this.settingTitleLabel.Text = "Title of setting";
            // 
            // SettingEditorBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.mainContainer);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(200, 62);
            this.Name = "SettingEditorBase";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.Size = new System.Drawing.Size(500, 110);
            this.Load += new System.EventHandler(this.SettingEditorBase_Load);
            this.customElementsPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Button resetToCurrentButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button resetToDefaultButton;
    }
}
