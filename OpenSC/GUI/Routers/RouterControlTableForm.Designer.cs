namespace OpenSC.GUI.Routers
{
    partial class RouterControlTableForm
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
            this.takeButton = new System.Windows.Forms.Button();
            this.topButtonPanel = new System.Windows.Forms.Panel();
            this.autotakeButton = new System.Windows.Forms.Button();
            this.crosspointsTable = new System.Windows.Forms.DataGridView();
            this.crosspointsTableContainer = new System.Windows.Forms.Panel();
            this.mainContainer.SuspendLayout();
            this.topButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crosspointsTable)).BeginInit();
            this.crosspointsTableContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.crosspointsTableContainer);
            this.mainContainer.Controls.Add(this.topButtonPanel);
            this.mainContainer.Size = new System.Drawing.Size(1182, 497);
            // 
            // takeButton
            // 
            this.takeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.takeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.takeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.takeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.takeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.takeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.takeButton.Location = new System.Drawing.Point(1044, 11);
            this.takeButton.Margin = new System.Windows.Forms.Padding(8);
            this.takeButton.Name = "takeButton";
            this.takeButton.Size = new System.Drawing.Size(121, 43);
            this.takeButton.TabIndex = 0;
            this.takeButton.Text = "TAKE";
            this.takeButton.UseVisualStyleBackColor = false;
            this.takeButton.Click += new System.EventHandler(this.takeButton_Click);
            // 
            // topButtonPanel
            // 
            this.topButtonPanel.Controls.Add(this.autotakeButton);
            this.topButtonPanel.Controls.Add(this.takeButton);
            this.topButtonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.topButtonPanel.MinimumSize = new System.Drawing.Size(0, 30);
            this.topButtonPanel.Name = "topButtonPanel";
            this.topButtonPanel.Size = new System.Drawing.Size(1182, 67);
            this.topButtonPanel.TabIndex = 0;
            // 
            // autotakeButton
            // 
            this.autotakeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.autotakeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.autotakeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.autotakeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autotakeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.autotakeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.autotakeButton.Location = new System.Drawing.Point(907, 11);
            this.autotakeButton.Margin = new System.Windows.Forms.Padding(8);
            this.autotakeButton.Name = "autotakeButton";
            this.autotakeButton.Size = new System.Drawing.Size(121, 43);
            this.autotakeButton.TabIndex = 1;
            this.autotakeButton.Text = "AUTOTAKE";
            this.autotakeButton.UseVisualStyleBackColor = false;
            // 
            // crosspointsTable
            // 
            this.crosspointsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.crosspointsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crosspointsTable.Location = new System.Drawing.Point(0, 0);
            this.crosspointsTable.Name = "crosspointsTable";
            this.crosspointsTable.RowHeadersWidth = 51;
            this.crosspointsTable.RowTemplate.Height = 24;
            this.crosspointsTable.Size = new System.Drawing.Size(1182, 430);
            this.crosspointsTable.TabIndex = 1;
            // 
            // crosspointsTableContainer
            // 
            this.crosspointsTableContainer.Controls.Add(this.crosspointsTable);
            this.crosspointsTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crosspointsTableContainer.Location = new System.Drawing.Point(0, 67);
            this.crosspointsTableContainer.Name = "crosspointsTableContainer";
            this.crosspointsTableContainer.Size = new System.Drawing.Size(1182, 430);
            this.crosspointsTableContainer.TabIndex = 2;
            // 
            // RouterControlTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 553);
            this.HeaderText = "Router crosspoints";
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "RouterControlTableForm";
            this.Text = "Router crosspoints";
            this.Load += new System.EventHandler(this.RouterControlForm_Load);
            this.mainContainer.ResumeLayout(false);
            this.topButtonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.crosspointsTable)).EndInit();
            this.crosspointsTableContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topButtonPanel;
        private System.Windows.Forms.Button takeButton;
        private System.Windows.Forms.DataGridView crosspointsTable;
        private System.Windows.Forms.Button autotakeButton;
        private System.Windows.Forms.Panel crosspointsTableContainer;
    }
}