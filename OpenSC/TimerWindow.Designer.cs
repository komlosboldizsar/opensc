namespace OpenSC.GUI.Timers
{
    partial class TimerWindow
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
            this.timeLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.buttonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.resetButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.timerDisplayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorRedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorGreenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorYellowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorOrangeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorBlueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPurpleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorBlackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllSegmentsContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blinkWhenExpiredContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonsContextItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeLabelContainer = new System.Windows.Forms.Panel();
            this.timeLabelBackground = new System.Windows.Forms.Label();
            this.modeImageContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.modeImageBackwards = new System.Windows.Forms.PictureBox();
            this.modeImageForwards = new System.Windows.Forms.PictureBox();
            this.modeImageClock = new System.Windows.Forms.PictureBox();
            this.stateImageContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.runningStateImage = new System.Windows.Forms.PictureBox();
            this.buttonsPanel.SuspendLayout();
            this.timerDisplayContextMenu.SuspendLayout();
            this.timeLabelContainer.SuspendLayout();
            this.modeImageContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modeImageBackwards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modeImageForwards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modeImageClock)).BeginInit();
            this.stateImageContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runningStateImage)).BeginInit();
            this.SuspendLayout();
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabel.Font = new System.Drawing.Font("DSEG7 Classic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Red;
            this.timeLabel.Location = new System.Drawing.Point(0, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.timeLabel.Size = new System.Drawing.Size(522, 100);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "12:00:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.titleLabel.Size = new System.Drawing.Size(522, 37);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Title of this counter";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.ColumnCount = 3;
            this.buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.buttonsPanel.Controls.Add(this.resetButton, 2, 0);
            this.buttonsPanel.Controls.Add(this.startButton, 0, 0);
            this.buttonsPanel.Controls.Add(this.stopButton, 1, 0);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 141);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.RowCount = 1;
            this.buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonsPanel.Size = new System.Drawing.Size(522, 32);
            this.buttonsPanel.TabIndex = 2;
            // 
            // resetButton
            // 
            this.resetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetButton.Location = new System.Drawing.Point(351, 3);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(168, 26);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "RESET";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // startButton
            // 
            this.startButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startButton.Location = new System.Drawing.Point(3, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(168, 26);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopButton.Location = new System.Drawing.Point(177, 3);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(168, 26);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // timerDisplayContextMenu
            // 
            this.timerDisplayContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.timerDisplayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setColorMenuItem,
            this.showAllSegmentsContextItem,
            this.blinkWhenExpiredContextItem,
            this.buttonsContextItem,
            this.contextMenuSeparator1,
            this.settingsToolStripMenuItem});
            this.timerDisplayContextMenu.Name = "timerDisplayContextMenu";
            this.timerDisplayContextMenu.Size = new System.Drawing.Size(211, 168);
            // 
            // setColorMenuItem
            // 
            this.setColorMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorRedMenuItem,
            this.colorGreenMenuItem,
            this.colorYellowMenuItem,
            this.colorOrangeMenuItem,
            this.colorBlueMenuItem,
            this.colorPurpleMenuItem,
            this.colorBlackMenuItem});
            this.setColorMenuItem.Name = "setColorMenuItem";
            this.setColorMenuItem.Size = new System.Drawing.Size(210, 26);
            this.setColorMenuItem.Text = "Color";
            // 
            // colorRedMenuItem
            // 
            this.colorRedMenuItem.Image = global::OpenSC.Properties.Resources.solid_red;
            this.colorRedMenuItem.Name = "colorRedMenuItem";
            this.colorRedMenuItem.Size = new System.Drawing.Size(133, 26);
            this.colorRedMenuItem.Tag = "#FF0000";
            this.colorRedMenuItem.Text = "Red";
            // 
            // colorGreenMenuItem
            // 
            this.colorGreenMenuItem.Image = global::OpenSC.Properties.Resources.solid_green;
            this.colorGreenMenuItem.Name = "colorGreenMenuItem";
            this.colorGreenMenuItem.Size = new System.Drawing.Size(133, 26);
            this.colorGreenMenuItem.Tag = "#008800";
            this.colorGreenMenuItem.Text = "Green";
            // 
            // colorYellowMenuItem
            // 
            this.colorYellowMenuItem.Image = global::OpenSC.Properties.Resources.solid_yellow;
            this.colorYellowMenuItem.Name = "colorYellowMenuItem";
            this.colorYellowMenuItem.Size = new System.Drawing.Size(133, 26);
            this.colorYellowMenuItem.Tag = "#EEEE00";
            this.colorYellowMenuItem.Text = "Yellow";
            // 
            // colorOrangeMenuItem
            // 
            this.colorOrangeMenuItem.Image = global::OpenSC.Properties.Resources.solid_orange;
            this.colorOrangeMenuItem.Name = "colorOrangeMenuItem";
            this.colorOrangeMenuItem.Size = new System.Drawing.Size(133, 26);
            this.colorOrangeMenuItem.Tag = "#FF9900";
            this.colorOrangeMenuItem.Text = "Orange";
            // 
            // colorBlueMenuItem
            // 
            this.colorBlueMenuItem.Image = global::OpenSC.Properties.Resources.solid_blue;
            this.colorBlueMenuItem.Name = "colorBlueMenuItem";
            this.colorBlueMenuItem.Size = new System.Drawing.Size(133, 26);
            this.colorBlueMenuItem.Tag = "#0000FF";
            this.colorBlueMenuItem.Text = "Blue";
            // 
            // colorPurpleMenuItem
            // 
            this.colorPurpleMenuItem.Image = global::OpenSC.Properties.Resources.solid_purple;
            this.colorPurpleMenuItem.Name = "colorPurpleMenuItem";
            this.colorPurpleMenuItem.Size = new System.Drawing.Size(133, 26);
            this.colorPurpleMenuItem.Tag = "#AA00FF";
            this.colorPurpleMenuItem.Text = "Purple";
            // 
            // colorBlackMenuItem
            // 
            this.colorBlackMenuItem.Image = global::OpenSC.Properties.Resources.solid_black;
            this.colorBlackMenuItem.Name = "colorBlackMenuItem";
            this.colorBlackMenuItem.Size = new System.Drawing.Size(133, 26);
            this.colorBlackMenuItem.Tag = "#000000";
            this.colorBlackMenuItem.Text = "Black";
            // 
            // showAllSegmentsContextItem
            // 
            this.showAllSegmentsContextItem.Name = "showAllSegmentsContextItem";
            this.showAllSegmentsContextItem.Size = new System.Drawing.Size(210, 26);
            this.showAllSegmentsContextItem.Text = "Show all segments";
            this.showAllSegmentsContextItem.Click += new System.EventHandler(this.showAllSegmentsContextItem_Click);
            // 
            // blinkWhenExpiredContextItem
            // 
            this.blinkWhenExpiredContextItem.Checked = true;
            this.blinkWhenExpiredContextItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.blinkWhenExpiredContextItem.Name = "blinkWhenExpiredContextItem";
            this.blinkWhenExpiredContextItem.Size = new System.Drawing.Size(210, 26);
            this.blinkWhenExpiredContextItem.Text = "Blink when expired";
            this.blinkWhenExpiredContextItem.Click += new System.EventHandler(this.blinkWhenExpiredContextItem_Click);
            // 
            // buttonsContextItem
            // 
            this.buttonsContextItem.Checked = true;
            this.buttonsContextItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonsContextItem.Name = "buttonsContextItem";
            this.buttonsContextItem.Size = new System.Drawing.Size(210, 26);
            this.buttonsContextItem.Text = "Buttons";
            this.buttonsContextItem.Click += new System.EventHandler(this.buttonsToolStripMenuItem_Click);
            // 
            // contextMenuSeparator1
            // 
            this.contextMenuSeparator1.Name = "contextMenuSeparator1";
            this.contextMenuSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // timeLabelContainer
            // 
            this.timeLabelContainer.Controls.Add(this.timeLabel);
            this.timeLabelContainer.Controls.Add(this.timeLabelBackground);
            this.timeLabelContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.timeLabelContainer.Location = new System.Drawing.Point(0, 37);
            this.timeLabelContainer.Name = "timeLabelContainer";
            this.timeLabelContainer.Size = new System.Drawing.Size(522, 100);
            this.timeLabelContainer.TabIndex = 3;
            // 
            // timeLabelBackground
            // 
            this.timeLabelBackground.BackColor = System.Drawing.SystemColors.Control;
            this.timeLabelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabelBackground.Font = new System.Drawing.Font("DSEG7 Classic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabelBackground.ForeColor = System.Drawing.Color.Gainsboro;
            this.timeLabelBackground.Location = new System.Drawing.Point(0, 0);
            this.timeLabelBackground.Name = "timeLabelBackground";
            this.timeLabelBackground.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.timeLabelBackground.Size = new System.Drawing.Size(522, 100);
            this.timeLabelBackground.TabIndex = 1;
            this.timeLabelBackground.Text = "88:88:88";
            this.timeLabelBackground.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // modeImageContainer
            // 
            this.modeImageContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modeImageContainer.Controls.Add(this.modeImageBackwards);
            this.modeImageContainer.Controls.Add(this.modeImageForwards);
            this.modeImageContainer.Controls.Add(this.modeImageClock);
            this.modeImageContainer.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.modeImageContainer.Location = new System.Drawing.Point(415, 0);
            this.modeImageContainer.Name = "modeImageContainer";
            this.modeImageContainer.Padding = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.modeImageContainer.Size = new System.Drawing.Size(107, 37);
            this.modeImageContainer.TabIndex = 5;
            // 
            // modeImageBackwards
            // 
            this.modeImageBackwards.Image = global::OpenSC.Properties.Resources.timer_backward_inactive;
            this.modeImageBackwards.Location = new System.Drawing.Point(77, 6);
            this.modeImageBackwards.Name = "modeImageBackwards";
            this.modeImageBackwards.Size = new System.Drawing.Size(24, 24);
            this.modeImageBackwards.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modeImageBackwards.TabIndex = 4;
            this.modeImageBackwards.TabStop = false;
            // 
            // modeImageForwards
            // 
            this.modeImageForwards.Image = global::OpenSC.Properties.Resources.timer_forward_inactive;
            this.modeImageForwards.Location = new System.Drawing.Point(47, 6);
            this.modeImageForwards.Name = "modeImageForwards";
            this.modeImageForwards.Size = new System.Drawing.Size(24, 24);
            this.modeImageForwards.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modeImageForwards.TabIndex = 5;
            this.modeImageForwards.TabStop = false;
            // 
            // modeImageClock
            // 
            this.modeImageClock.Image = global::OpenSC.Properties.Resources.timer_clock_inactive;
            this.modeImageClock.Location = new System.Drawing.Point(17, 6);
            this.modeImageClock.Name = "modeImageClock";
            this.modeImageClock.Size = new System.Drawing.Size(24, 24);
            this.modeImageClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modeImageClock.TabIndex = 6;
            this.modeImageClock.TabStop = false;
            // 
            // stateImageContainer
            // 
            this.stateImageContainer.Controls.Add(this.runningStateImage);
            this.stateImageContainer.Location = new System.Drawing.Point(0, 0);
            this.stateImageContainer.Name = "stateImageContainer";
            this.stateImageContainer.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.stateImageContainer.Size = new System.Drawing.Size(107, 37);
            this.stateImageContainer.TabIndex = 6;
            // 
            // runningStateImage
            // 
            this.runningStateImage.Image = global::OpenSC.Properties.Resources.timer_stopped;
            this.runningStateImage.Location = new System.Drawing.Point(6, 6);
            this.runningStateImage.Name = "runningStateImage";
            this.runningStateImage.Size = new System.Drawing.Size(24, 24);
            this.runningStateImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.runningStateImage.TabIndex = 6;
            this.runningStateImage.TabStop = false;
            // 
            // TimerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(522, 173);
            this.ContextMenuStrip = this.timerDisplayContextMenu;
            this.Controls.Add(this.stateImageContainer);
            this.Controls.Add(this.modeImageContainer);
            this.Controls.Add(this.timeLabelContainer);
            this.Controls.Add(this.buttonsPanel);
            this.Controls.Add(this.titleLabel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(540, 220);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(540, 220);
            this.Name = "TimerWindow";
            this.Text = "TimerWindow";
            this.Load += new System.EventHandler(this.TimerWindow_Load);
            this.buttonsPanel.ResumeLayout(false);
            this.timerDisplayContextMenu.ResumeLayout(false);
            this.timeLabelContainer.ResumeLayout(false);
            this.modeImageContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.modeImageBackwards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modeImageForwards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modeImageClock)).EndInit();
            this.stateImageContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.runningStateImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TableLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.ContextMenuStrip timerDisplayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem setColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorRedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorGreenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorYellowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorOrangeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorBlueMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorPurpleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorBlackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonsContextItem;
        private System.Windows.Forms.ToolStripSeparator contextMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Panel timeLabelContainer;
        private System.Windows.Forms.Label timeLabelBackground;
        private System.Windows.Forms.ToolStripMenuItem showAllSegmentsContextItem;
        private System.Windows.Forms.PictureBox modeImageBackwards;
        private System.Windows.Forms.FlowLayoutPanel modeImageContainer;
        private System.Windows.Forms.PictureBox modeImageForwards;
        private System.Windows.Forms.PictureBox modeImageClock;
        private System.Windows.Forms.ToolStripMenuItem blinkWhenExpiredContextItem;
        private System.Windows.Forms.FlowLayoutPanel stateImageContainer;
        private System.Windows.Forms.PictureBox runningStateImage;
    }
}