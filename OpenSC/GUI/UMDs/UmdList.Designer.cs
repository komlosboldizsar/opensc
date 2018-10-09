namespace OpenSC.GUI.UMDs
{
    partial class UmdList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.umdListTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.umdIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.umdNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.umdEditButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.umdDeleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.umdListTable)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.umdListTable);
            this.mainContainer.Controls.Add(this.groupBox1);
            this.mainContainer.Size = new System.Drawing.Size(1232, 497);
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1232, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // umdListTable
            // 
            this.umdListTable.AllowUserToAddRows = false;
            this.umdListTable.AllowUserToDeleteRows = false;
            this.umdListTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.umdListTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.umdIdColumn,
            this.umdNameColumn,
            this.umdEditButtonColumn,
            this.umdDeleteButtonColumn});
            this.umdListTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.umdListTable.Location = new System.Drawing.Point(0, 100);
            this.umdListTable.Name = "umdListTable";
            this.umdListTable.ReadOnly = true;
            this.umdListTable.RowTemplate.Height = 24;
            this.umdListTable.Size = new System.Drawing.Size(1232, 397);
            this.umdListTable.TabIndex = 2;
            this.umdListTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.umdListTable_CellContentClick);
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "Edit";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Width = 70;
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.DividerWidth = 3;
            this.dataGridViewButtonColumn2.HeaderText = "Delete";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.ReadOnly = true;
            this.dataGridViewButtonColumn2.Width = 70;
            // 
            // dataGridViewButtonColumn3
            // 
            this.dataGridViewButtonColumn3.HeaderText = "";
            this.dataGridViewButtonColumn3.Name = "dataGridViewButtonColumn3";
            this.dataGridViewButtonColumn3.ReadOnly = true;
            this.dataGridViewButtonColumn3.Width = 40;
            // 
            // dataGridViewButtonColumn4
            // 
            this.dataGridViewButtonColumn4.HeaderText = "";
            this.dataGridViewButtonColumn4.Name = "dataGridViewButtonColumn4";
            this.dataGridViewButtonColumn4.ReadOnly = true;
            this.dataGridViewButtonColumn4.Width = 40;
            // 
            // dataGridViewButtonColumn5
            // 
            this.dataGridViewButtonColumn5.DividerWidth = 3;
            this.dataGridViewButtonColumn5.HeaderText = "";
            this.dataGridViewButtonColumn5.Name = "dataGridViewButtonColumn5";
            this.dataGridViewButtonColumn5.ReadOnly = true;
            this.dataGridViewButtonColumn5.Width = 40;
            // 
            // dataGridViewButtonColumn6
            // 
            this.dataGridViewButtonColumn6.HeaderText = "Open";
            this.dataGridViewButtonColumn6.Name = "dataGridViewButtonColumn6";
            this.dataGridViewButtonColumn6.ReadOnly = true;
            // 
            // umdIdColumn
            // 
            this.umdIdColumn.HeaderText = "ID";
            this.umdIdColumn.Name = "umdIdColumn";
            this.umdIdColumn.ReadOnly = true;
            this.umdIdColumn.Width = 50;
            // 
            // umdNameColumn
            // 
            this.umdNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.umdNameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.umdNameColumn.DividerWidth = 3;
            this.umdNameColumn.HeaderText = "Name";
            this.umdNameColumn.Name = "umdNameColumn";
            this.umdNameColumn.ReadOnly = true;
            // 
            // umdEditButtonColumn
            // 
            this.umdEditButtonColumn.HeaderText = "Edit";
            this.umdEditButtonColumn.Name = "umdEditButtonColumn";
            this.umdEditButtonColumn.ReadOnly = true;
            this.umdEditButtonColumn.Width = 70;
            // 
            // umdDeleteButtonColumn
            // 
            this.umdDeleteButtonColumn.DividerWidth = 3;
            this.umdDeleteButtonColumn.HeaderText = "Delete";
            this.umdDeleteButtonColumn.Name = "umdDeleteButtonColumn";
            this.umdDeleteButtonColumn.ReadOnly = true;
            this.umdDeleteButtonColumn.Width = 70;
            // 
            // UmdList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 553);
            this.Name = "UmdList";
            this.Text = "List of UMDs";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UmdList_FormClosed);
            this.Load += new System.EventHandler(this.UmdList_Load);
            this.mainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.umdListTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView umdListTable;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn5;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn umdIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn umdNameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn umdEditButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn umdDeleteButtonColumn;
    }
}