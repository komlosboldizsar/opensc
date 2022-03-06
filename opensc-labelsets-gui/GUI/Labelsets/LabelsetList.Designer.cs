namespace OpenSC.GUI.Labelsets
{
    partial class LabelsetList
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
            this.components = new System.ComponentModel.Container();
            this.addableRouterTypesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editAllButton = new System.Windows.Forms.Button();
            this.topPanelInner.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanelInner
            // 
            this.topPanelInner.Controls.Add(this.editAllButton);
            this.topPanelInner.Controls.SetChildIndex(this.editAllButton, 1);
            // 
            // addableRouterTypesMenu
            // 
            this.addableRouterTypesMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.addableRouterTypesMenu.Name = "addableVtrTypesMenu";
            this.addableRouterTypesMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // editAllButton
            // 
            this.editAllButton.Location = new System.Drawing.Point(13, 16);
            this.editAllButton.Name = "editAllButton";
            this.editAllButton.Size = new System.Drawing.Size(130, 38);
            this.editAllButton.TabIndex = 1;
            this.editAllButton.Text = "Edit all";
            this.editAllButton.UseVisualStyleBackColor = true;
            this.editAllButton.Click += new System.EventHandler(this.editAllButton_Click);
            // 
            // LabelsetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 562);
            this.HeaderText = "List of labelsets";
            this.Margin = new System.Windows.Forms.Padding(3, 15, 3, 15);
            this.Name = "LabelsetList";
            this.Text = "List of labelsets";
            this.Controls.SetChildIndex(this.mainContainer, 0);
            this.topPanelInner.ResumeLayout(false);
            this.topPanelInner.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip addableRouterTypesMenu;
        private System.Windows.Forms.Button editAllButton;
    }
}