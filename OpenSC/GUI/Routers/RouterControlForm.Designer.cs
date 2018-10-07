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
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.inputsPanel = new System.Windows.Forms.Panel();
            this.inputsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.outputsPanel = new System.Windows.Forms.Panel();
            this.outputsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.mainContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.inputsPanel.SuspendLayout();
            this.outputsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.button1);
            this.mainContainer.Controls.Add(this.tableLayoutPanel1);
            this.mainContainer.Controls.Add(this.label1);
            this.mainContainer.Size = new System.Drawing.Size(1082, 497);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Router: MTX-1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.inputsPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.outputsPanel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 55);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1082, 442);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // inputsPanel
            // 
            this.inputsPanel.BackColor = System.Drawing.Color.PowderBlue;
            this.inputsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputsPanel.Controls.Add(this.inputsContainer);
            this.inputsPanel.Controls.Add(this.label2);
            this.inputsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.inputsPanel.Location = new System.Drawing.Point(10, 10);
            this.inputsPanel.Margin = new System.Windows.Forms.Padding(10, 10, 5, 10);
            this.inputsPanel.Name = "inputsPanel";
            this.inputsPanel.Size = new System.Drawing.Size(526, 422);
            this.inputsPanel.TabIndex = 0;
            // 
            // inputsContainer
            // 
            this.inputsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsContainer.Location = new System.Drawing.Point(0, 33);
            this.inputsContainer.Name = "inputsContainer";
            this.inputsContainer.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.inputsContainer.Size = new System.Drawing.Size(524, 387);
            this.inputsContainer.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(524, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "INPUTS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outputsPanel
            // 
            this.outputsPanel.BackColor = System.Drawing.Color.Pink;
            this.outputsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputsPanel.Controls.Add(this.outputsContainer);
            this.outputsPanel.Controls.Add(this.label3);
            this.outputsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputsPanel.Location = new System.Drawing.Point(546, 10);
            this.outputsPanel.Margin = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.outputsPanel.Name = "outputsPanel";
            this.outputsPanel.Size = new System.Drawing.Size(526, 422);
            this.outputsPanel.TabIndex = 1;
            // 
            // outputsContainer
            // 
            this.outputsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputsContainer.Location = new System.Drawing.Point(0, 33);
            this.outputsContainer.Name = "outputsContainer";
            this.outputsContainer.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.outputsContainer.Size = new System.Drawing.Size(524, 387);
            this.outputsContainer.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(524, 33);
            this.label3.TabIndex = 1;
            this.label3.Text = "OUTPUTS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.DarkGreen;
            this.button1.Location = new System.Drawing.Point(971, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 42);
            this.button1.TabIndex = 2;
            this.button1.Text = "TAKE";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // RouterControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 553);
            this.Name = "RouterControlForm";
            this.Text = "RouterControlForm";
            this.Load += new System.EventHandler(this.RouterControlForm_Load);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.inputsPanel.ResumeLayout(false);
            this.outputsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel inputsPanel;
        private System.Windows.Forms.Panel outputsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel inputsContainer;
        private System.Windows.Forms.FlowLayoutPanel outputsContainer;
    }
}