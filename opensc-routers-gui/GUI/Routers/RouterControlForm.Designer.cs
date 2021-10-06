namespace OpenSC.GUI.Routers
{
    partial class RouterControlForm
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
            this.topButtonPanel = new System.Windows.Forms.Panel();
            this.takeButton = new System.Windows.Forms.Button();
            this.splitterTable = new System.Windows.Forms.TableLayoutPanel();
            this.inputsContainerPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.routerInputControl1 = new OpenSC.GUI.Routers.RouterInputControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitLinePanel = new System.Windows.Forms.Panel();
            this.outputsContainerPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.routerOutputControl1 = new OpenSC.GUI.Routers.RouterOutputControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.mainContainer.SuspendLayout();
            this.topButtonPanel.SuspendLayout();
            this.splitterTable.SuspendLayout();
            this.inputsContainerPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.outputsContainerPanel.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.splitterTable);
            this.mainContainer.Controls.Add(this.topButtonPanel);
            this.mainContainer.Size = new System.Drawing.Size(1182, 497);
            // 
            // topButtonPanel
            // 
            this.topButtonPanel.Controls.Add(this.takeButton);
            this.topButtonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.topButtonPanel.MinimumSize = new System.Drawing.Size(0, 30);
            this.topButtonPanel.Name = "topButtonPanel";
            this.topButtonPanel.Size = new System.Drawing.Size(1182, 67);
            this.topButtonPanel.TabIndex = 0;
            // 
            // takeButton
            // 
            this.takeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.takeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.takeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.takeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.takeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.takeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.takeButton.Location = new System.Drawing.Point(1074, 11);
            this.takeButton.Margin = new System.Windows.Forms.Padding(8);
            this.takeButton.Name = "takeButton";
            this.takeButton.Size = new System.Drawing.Size(91, 43);
            this.takeButton.TabIndex = 0;
            this.takeButton.Text = "TAKE";
            this.takeButton.UseVisualStyleBackColor = false;
            this.takeButton.Click += new System.EventHandler(this.takeButton_Click);
            // 
            // splitterTable
            // 
            this.splitterTable.ColumnCount = 3;
            this.splitterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.splitterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.splitterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.splitterTable.Controls.Add(this.inputsContainerPanel, 0, 0);
            this.splitterTable.Controls.Add(this.splitLinePanel, 1, 0);
            this.splitterTable.Controls.Add(this.outputsContainerPanel, 2, 0);
            this.splitterTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterTable.Location = new System.Drawing.Point(0, 67);
            this.splitterTable.Name = "splitterTable";
            this.splitterTable.RowCount = 1;
            this.splitterTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.splitterTable.Size = new System.Drawing.Size(1182, 430);
            this.splitterTable.TabIndex = 1;
            // 
            // inputsContainerPanel
            // 
            this.inputsContainerPanel.AutoScroll = true;
            this.inputsContainerPanel.Controls.Add(this.flowLayoutPanel1);
            this.inputsContainerPanel.Controls.Add(this.panel1);
            this.inputsContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsContainerPanel.Location = new System.Drawing.Point(3, 3);
            this.inputsContainerPanel.Name = "inputsContainerPanel";
            this.inputsContainerPanel.Size = new System.Drawing.Size(582, 424);
            this.inputsContainerPanel.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.routerInputControl1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 46);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(582, 378);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // routerInputControl1
            // 
            this.routerInputControl1.AutoSize = true;
            this.routerInputControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerInputControl1.Location = new System.Drawing.Point(10, 10);
            this.routerInputControl1.Margin = new System.Windows.Forms.Padding(5);
            this.routerInputControl1.Name = "routerInputControl1";
            this.routerInputControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.routerInputControl1.Selected = false;
            this.routerInputControl1.Size = new System.Drawing.Size(113, 91);
            this.routerInputControl1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(582, 46);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(572, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "INPUTS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitLinePanel
            // 
            this.splitLinePanel.BackColor = System.Drawing.Color.Black;
            this.splitLinePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLinePanel.Location = new System.Drawing.Point(591, 3);
            this.splitLinePanel.Name = "splitLinePanel";
            this.splitLinePanel.Size = new System.Drawing.Size(1, 424);
            this.splitLinePanel.TabIndex = 2;
            // 
            // outputsContainerPanel
            // 
            this.outputsContainerPanel.AutoScroll = true;
            this.outputsContainerPanel.Controls.Add(this.flowLayoutPanel2);
            this.outputsContainerPanel.Controls.Add(this.panel2);
            this.outputsContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputsContainerPanel.Location = new System.Drawing.Point(596, 3);
            this.outputsContainerPanel.Name = "outputsContainerPanel";
            this.outputsContainerPanel.Size = new System.Drawing.Size(583, 424);
            this.outputsContainerPanel.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Controls.Add(this.routerOutputControl1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 46);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(583, 378);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // routerOutputControl1
            // 
            this.routerOutputControl1.AutoSize = true;
            this.routerOutputControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerOutputControl1.Location = new System.Drawing.Point(10, 10);
            this.routerOutputControl1.Margin = new System.Windows.Forms.Padding(5);
            this.routerOutputControl1.Name = "routerOutputControl1";
            this.routerOutputControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.routerOutputControl1.Selected = false;
            this.routerOutputControl1.Size = new System.Drawing.Size(113, 91);
            this.routerOutputControl1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(583, 46);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Olive;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(573, 36);
            this.label2.TabIndex = 0;
            this.label2.Text = "OUTPUTS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RouterControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 553);
            this.HeaderText = "Router crosspoints";
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "RouterControlForm";
            this.Text = "Router crosspoints";
            this.Load += new System.EventHandler(this.RouterControlForm_Load);
            this.mainContainer.ResumeLayout(false);
            this.topButtonPanel.ResumeLayout(false);
            this.splitterTable.ResumeLayout(false);
            this.inputsContainerPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.outputsContainerPanel.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel splitterTable;
        private System.Windows.Forms.Panel inputsContainerPanel;
        private System.Windows.Forms.Panel outputsContainerPanel;
        private System.Windows.Forms.Panel topButtonPanel;
        private System.Windows.Forms.Button takeButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel splitLinePanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private RouterInputControl routerInputControl1;
        private RouterOutputControl routerOutputControl1;
    }
}