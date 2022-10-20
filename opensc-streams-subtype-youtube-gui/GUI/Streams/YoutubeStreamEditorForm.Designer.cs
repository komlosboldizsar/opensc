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
            this.videoIdTextBox = new OpenSC.GUI.Streams.StreamIdPasteTextBox();
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
            this.customElementsPanel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(10, 24, 10, 0);
            this.customElementsPanel.Size = new System.Drawing.Size(482, 329);
            this.customElementsPanel.Controls.SetChildIndex(this.youtubeStreamDataPanel, 0);
            // 
            // mainContainer
            // 
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.mainContainer.Size = new System.Drawing.Size(482, 415);
            // 
            // youtubeStreamDataPanel
            // 
            this.youtubeStreamDataPanel.AutoSize = true;
            this.youtubeStreamDataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.youtubeStreamDataPanel.Controls.Add(this.youtubeStreamDataGroupBox);
            this.youtubeStreamDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.youtubeStreamDataPanel.Location = new System.Drawing.Point(10, 220);
            this.youtubeStreamDataPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.youtubeStreamDataPanel.Name = "youtubeStreamDataPanel";
            this.youtubeStreamDataPanel.Size = new System.Drawing.Size(462, 75);
            this.youtubeStreamDataPanel.TabIndex = 1;
            // 
            // youtubeStreamDataGroupBox
            // 
            this.youtubeStreamDataGroupBox.AutoSize = true;
            this.youtubeStreamDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.youtubeStreamDataGroupBox.Controls.Add(this.youtubeStreamDataTable);
            this.youtubeStreamDataGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.youtubeStreamDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.youtubeStreamDataGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.youtubeStreamDataGroupBox.Name = "youtubeStreamDataGroupBox";
            this.youtubeStreamDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.youtubeStreamDataGroupBox.Size = new System.Drawing.Size(462, 75);
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
            this.youtubeStreamDataTable.Location = new System.Drawing.Point(8, 30);
            this.youtubeStreamDataTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.youtubeStreamDataTable.Name = "youtubeStreamDataTable";
            this.youtubeStreamDataTable.RowCount = 1;
            this.youtubeStreamDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.youtubeStreamDataTable.Size = new System.Drawing.Size(446, 35);
            this.youtubeStreamDataTable.TabIndex = 0;
            // 
            // videoIdLabel
            // 
            this.videoIdLabel.AutoSize = true;
            this.videoIdLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.videoIdLabel.Location = new System.Drawing.Point(3, 0);
            this.videoIdLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.videoIdLabel.Name = "videoIdLabel";
            this.videoIdLabel.Size = new System.Drawing.Size(70, 35);
            this.videoIdLabel.TabIndex = 0;
            this.videoIdLabel.Text = "Video ID:";
            this.videoIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // videoIdTextBox
            // 
            this.videoIdTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.videoIdTextBox.Location = new System.Drawing.Point(91, 4);
            this.videoIdTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.videoIdTextBox.Name = "videoIdTextBox";
            this.videoIdTextBox.Size = new System.Drawing.Size(352, 27);
            this.videoIdTextBox.TabIndex = 1;
            // 
            // YoutubeStreamEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 485);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 12, 3, 12);
            this.MinimumSize = new System.Drawing.Size(500, 421);
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
        private StreamIdPasteTextBox videoIdTextBox;
    }
}