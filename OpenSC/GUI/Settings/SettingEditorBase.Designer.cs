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
            this.mainContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.customElementsPanel = new System.Windows.Forms.Panel();
            this.settingTitleLabel = new GrowLabel();
            this.settingDescriptionLabel = new GrowLabel();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.AutoSize = true;
            this.mainContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainContainer.BackColor = System.Drawing.SystemColors.Control;
            this.mainContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainContainer.Controls.Add(this.settingTitleLabel);
            this.mainContainer.Controls.Add(this.settingDescriptionLabel);
            this.mainContainer.Controls.Add(this.customElementsPanel);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.MinimumSize = new System.Drawing.Size(40, 40);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.mainContainer.Size = new System.Drawing.Size(150, 98);
            this.mainContainer.TabIndex = 0;
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.AutoSize = true;
            this.customElementsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customElementsPanel.Location = new System.Drawing.Point(3, 53);
            this.customElementsPanel.MinimumSize = new System.Drawing.Size(40, 40);
            this.customElementsPanel.Name = "customElementsPanel";
            this.customElementsPanel.Size = new System.Drawing.Size(142, 40);
            this.customElementsPanel.TabIndex = 1;
            // 
            // settingTitleLabel
            // 
            this.settingTitleLabel.AutoSize = true;
            this.settingTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.settingTitleLabel.Location = new System.Drawing.Point(3, 6);
            this.settingTitleLabel.Name = "settingTitleLabel";
            this.settingTitleLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.settingTitleLabel.Size = new System.Drawing.Size(115, 18);
            this.settingTitleLabel.TabIndex = 4;
            this.settingTitleLabel.Text = "Title of setting";
            // 
            // settingDescriptionLabel
            // 
            this.settingDescriptionLabel.AutoSize = true;
            this.settingDescriptionLabel.Location = new System.Drawing.Point(3, 30);
            this.settingDescriptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.settingDescriptionLabel.Name = "settingDescriptionLabel";
            this.settingDescriptionLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.settingDescriptionLabel.Size = new System.Drawing.Size(142, 17);
            this.settingDescriptionLabel.TabIndex = 3;
            this.settingDescriptionLabel.Text = "Description of setting";
            // 
            // SettingEditorBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.mainContainer);
            this.MinimumSize = new System.Drawing.Size(10, 10);
            this.Name = "SettingEditorBase";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.Size = new System.Drawing.Size(150, 102);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel mainContainer;
        protected System.Windows.Forms.Panel customElementsPanel;
        private GrowLabel settingTitleLabel;
        private GrowLabel settingDescriptionLabel;
    }
}
