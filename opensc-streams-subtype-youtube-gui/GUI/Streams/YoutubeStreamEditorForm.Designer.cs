namespace OpenSC.GUI.Streams
{
    partial class YoutubeStreamEditorForm
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
            this.youtubeStreamDataPanel = new System.Windows.Forms.Panel();
            this.youtubeStreamDataGroupBox = new System.Windows.Forms.GroupBox();
            this.youtubeStreamDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.videoIdLabel = new System.Windows.Forms.Label();
            this.videoIdTextBox = new System.Windows.Forms.TextBox();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.youtubeStreamDataPanel.SuspendLayout();
            this.youtubeStreamDataGroupBox.SuspendLayout();
            this.youtubeStreamDataTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.youtubeStreamDataPanel);
            this.customElementsPanel.Size = new System.Drawing.Size(482, 196);
            this.customElementsPanel.Controls.SetChildIndex(this.youtubeStreamDataPanel, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(482, 265);
            // 
            // youtubeStreamDataPanel
            // 
            this.youtubeStreamDataPanel.AutoSize = true;
            this.youtubeStreamDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.youtubeStreamDataPanel.Controls.Add(this.youtubeStreamDataGroupBox);
            this.youtubeStreamDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.youtubeStreamDataPanel.Location = new System.Drawing.Point(0, 94);
            this.youtubeStreamDataPanel.Name = "youtubeStreamDataPanel";
            this.youtubeStreamDataPanel.Size = new System.Drawing.Size(482, 59);
            this.youtubeStreamDataPanel.TabIndex = 1;
            // 
            // youtubeStreamDataGroupBox
            // 
            this.youtubeStreamDataGroupBox.AutoSize = true;
            this.youtubeStreamDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.youtubeStreamDataGroupBox.Controls.Add(this.youtubeStreamDataTable);
            this.youtubeStreamDataGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.youtubeStreamDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.youtubeStreamDataGroupBox.Name = "youtubeStreamDataGroupBox";
            this.youtubeStreamDataGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.youtubeStreamDataGroupBox.Size = new System.Drawing.Size(482, 59);
            this.youtubeStreamDataGroupBox.TabIndex = 0;
            this.youtubeStreamDataGroupBox.TabStop = false;
            this.youtubeStreamDataGroupBox.Text = "YouTube";
            // 
            // youtubeStreamDataTable
            // 
            this.youtubeStreamDataTable.AutoSize = true;
            this.youtubeStreamDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.youtubeStreamDataTable.ColumnCount = 2;
            this.youtubeStreamDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.youtubeStreamDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.youtubeStreamDataTable.Controls.Add(this.videoIdLabel, 0, 0);
            this.youtubeStreamDataTable.Controls.Add(this.videoIdTextBox, 1, 0);
            this.youtubeStreamDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.youtubeStreamDataTable.Location = new System.Drawing.Point(8, 23);
            this.youtubeStreamDataTable.Name = "youtubeStreamDataTable";
            this.youtubeStreamDataTable.RowCount = 1;
            this.youtubeStreamDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.youtubeStreamDataTable.Size = new System.Drawing.Size(466, 28);
            this.youtubeStreamDataTable.TabIndex = 0;
            // 
            // videoIdLabel
            // 
            this.videoIdLabel.AutoSize = true;
            this.videoIdLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.videoIdLabel.Location = new System.Drawing.Point(3, 0);
            this.videoIdLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.videoIdLabel.Name = "videoIdLabel";
            this.videoIdLabel.Size = new System.Drawing.Size(65, 28);
            this.videoIdLabel.TabIndex = 0;
            this.videoIdLabel.Text = "Video ID:";
            this.videoIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // videoIdTextBox
            // 
            this.videoIdTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.videoIdTextBox.Location = new System.Drawing.Point(86, 3);
            this.videoIdTextBox.Name = "videoIdTextBox";
            this.videoIdTextBox.Size = new System.Drawing.Size(377, 22);
            this.videoIdTextBox.TabIndex = 1;
            // 
            // YoutubeStreamEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 321);
            this.DeleteButtonVisible = true;
            this.Name = "YoutubeStreamEditorForm";
            this.Text = "YoutubeStreamEditorForm";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.youtubeStreamDataPanel.ResumeLayout(false);
            this.youtubeStreamDataPanel.PerformLayout();
            this.youtubeStreamDataGroupBox.ResumeLayout(false);
            this.youtubeStreamDataGroupBox.PerformLayout();
            this.youtubeStreamDataTable.ResumeLayout(false);
            this.youtubeStreamDataTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel youtubeStreamDataPanel;
        private System.Windows.Forms.GroupBox youtubeStreamDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel youtubeStreamDataTable;
        private System.Windows.Forms.Label videoIdLabel;
        private System.Windows.Forms.TextBox videoIdTextBox;
    }
}