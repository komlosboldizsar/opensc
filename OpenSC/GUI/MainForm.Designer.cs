using OpenSC.GUI.GeneralComponents.Menus;

namespace OpenSC.GUI
{
    partial class MainForm
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
            this.menuStrip = new OpenSC.GUI.GeneralComponents.Menus.CustomMenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tileWindowsHorizontallyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileWindowsVerticallyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeWindowsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripEmptySpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripClock = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.clockUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.AssociatedMenuItem = null;
            this.menuStrip.DynamicChildrenInsertPosition = 0;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.windowsMenu,
            this.settingsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1482, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(44, 24);
            this.fileMenu.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileWindowsHorizontallyMenuItem,
            this.tileWindowsVerticallyMenuItem,
            this.cascadeWindowsMenuItem,
            this.toolStripSeparator1});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(82, 24);
            this.windowsMenu.Text = "Windows";
            // 
            // tileWindowsHorizontallyMenuItem
            // 
            this.tileWindowsHorizontallyMenuItem.Image = global::OpenSC.Properties.Resources._32_tile_horizontally;
            this.tileWindowsHorizontallyMenuItem.Name = "tileWindowsHorizontallyMenuItem";
            this.tileWindowsHorizontallyMenuItem.Size = new System.Drawing.Size(252, 26);
            this.tileWindowsHorizontallyMenuItem.Text = "Tile windows horizontally";
            this.tileWindowsHorizontallyMenuItem.Click += new System.EventHandler(this.arrangeWindowsMenuItemClickHandler);
            // 
            // tileWindowsVerticallyMenuItem
            // 
            this.tileWindowsVerticallyMenuItem.Image = global::OpenSC.Properties.Resources._32_tile_vertically;
            this.tileWindowsVerticallyMenuItem.Name = "tileWindowsVerticallyMenuItem";
            this.tileWindowsVerticallyMenuItem.Size = new System.Drawing.Size(252, 26);
            this.tileWindowsVerticallyMenuItem.Text = "Tile windows vertically";
            this.tileWindowsVerticallyMenuItem.Click += new System.EventHandler(this.arrangeWindowsMenuItemClickHandler);
            // 
            // cascadeWindowsMenuItem
            // 
            this.cascadeWindowsMenuItem.Name = "cascadeWindowsMenuItem";
            this.cascadeWindowsMenuItem.Size = new System.Drawing.Size(252, 26);
            this.cascadeWindowsMenuItem.Text = "Cascade windows";
            this.cascadeWindowsMenuItem.Click += new System.EventHandler(this.arrangeWindowsMenuItemClickHandler);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // globalSettingsToolStripMenuItem
            // 
            this.globalSettingsToolStripMenuItem.Name = "globalSettingsToolStripMenuItem";
            this.globalSettingsToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.globalSettingsToolStripMenuItem.Text = "Global settings";
            this.globalSettingsToolStripMenuItem.Click += new System.EventHandler(this.globalSettingsToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripEmptySpace,
            this.statusStripClock,
            this.statusStripVersion,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 728);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1482, 25);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusStripEmptySpace
            // 
            this.statusStripEmptySpace.Name = "statusStripEmptySpace";
            this.statusStripEmptySpace.Size = new System.Drawing.Size(1207, 20);
            this.statusStripEmptySpace.Spring = true;
            // 
            // statusStripClock
            // 
            this.statusStripClock.Name = "statusStripClock";
            this.statusStripClock.Size = new System.Drawing.Size(140, 20);
            this.statusStripClock.Text = "1970.01.01. 12:00:00";
            // 
            // statusStripVersion
            // 
            this.statusStripVersion.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.statusStripVersion.Name = "statusStripVersion";
            this.statusStripVersion.Size = new System.Drawing.Size(85, 20);
            this.statusStripVersion.Text = "OpenSC 1.0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(25, 20);
            this.toolStripStatusLabel1.Text = "    ";
            // 
            // clockUpdateTimer
            // 
            this.clockUpdateTimer.Enabled = true;
            this.clockUpdateTimer.Interval = 500;
            this.clockUpdateTimer.Tick += new System.EventHandler(this.clockUpdateTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 753);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "OpenSC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusStripEmptySpace;
        private System.Windows.Forms.ToolStripStatusLabel statusStripClock;
        private System.Windows.Forms.Timer clockUpdateTimer;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusStripVersion;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem tileWindowsHorizontallyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileWindowsVerticallyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeWindowsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalSettingsToolStripMenuItem;
    }
}

