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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.umdListTable = new System.Windows.Forms.DataGridView();
            this.umdIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.umdNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.umdStaticTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.umdUseStaticTextColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.umdCurrentTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.umdTally1Coulmn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.umdTally2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.umdEditButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.umdDeleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.mainContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1232, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1226, 79);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // umdListTable
            // 
            this.umdListTable.AllowUserToAddRows = false;
            this.umdListTable.AllowUserToDeleteRows = false;
            this.umdListTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.umdListTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.umdIdColumn,
            this.umdNameColumn,
            this.umdStaticTextColumn,
            this.umdUseStaticTextColumn,
            this.umdCurrentTextColumn,
            this.umdTally1Coulmn,
            this.umdTally2Column,
            this.umdEditButtonColumn,
            this.umdDeleteButtonColumn});
            this.umdListTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.umdListTable.Location = new System.Drawing.Point(0, 100);
            this.umdListTable.Name = "umdListTable";
            this.umdListTable.RowTemplate.Height = 24;
            this.umdListTable.Size = new System.Drawing.Size(1232, 397);
            this.umdListTable.TabIndex = 2;
            // 
            // umdIdColumn
            // 
            this.umdIdColumn.HeaderText = "ID";
            this.umdIdColumn.Name = "umdIdColumn";
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
            // 
            // umdStaticTextColumn
            // 
            this.umdStaticTextColumn.HeaderText = "Static text";
            this.umdStaticTextColumn.Name = "umdStaticTextColumn";
            this.umdStaticTextColumn.Width = 200;
            // 
            // umdUseStaticTextColumn
            // 
            this.umdUseStaticTextColumn.HeaderText = "Static";
            this.umdUseStaticTextColumn.Name = "umdUseStaticTextColumn";
            this.umdUseStaticTextColumn.Width = 50;
            // 
            // umdCurrentTextColumn
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.umdCurrentTextColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.umdCurrentTextColumn.DividerWidth = 3;
            this.umdCurrentTextColumn.HeaderText = "Current text";
            this.umdCurrentTextColumn.Name = "umdCurrentTextColumn";
            this.umdCurrentTextColumn.Width = 200;
            // 
            // umdTally1Coulmn
            // 
            this.umdTally1Coulmn.HeaderText = "T1";
            this.umdTally1Coulmn.Name = "umdTally1Coulmn";
            this.umdTally1Coulmn.Width = 30;
            // 
            // umdTally2Column
            // 
            this.umdTally2Column.DividerWidth = 3;
            this.umdTally2Column.HeaderText = "T2";
            this.umdTally2Column.Name = "umdTally2Column";
            this.umdTally2Column.Width = 33;
            // 
            // umdEditButtonColumn
            // 
            this.umdEditButtonColumn.HeaderText = "Edit";
            this.umdEditButtonColumn.Name = "umdEditButtonColumn";
            this.umdEditButtonColumn.Width = 70;
            // 
            // umdDeleteButtonColumn
            // 
            this.umdDeleteButtonColumn.DividerWidth = 3;
            this.umdDeleteButtonColumn.HeaderText = "Delete";
            this.umdDeleteButtonColumn.Name = "umdDeleteButtonColumn";
            this.umdDeleteButtonColumn.Width = 70;
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
            // UmdList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 553);
            this.Name = "UmdList";
            this.Text = "List of UMDs";
            this.mainContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn umdIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn umdNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn umdStaticTextColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn umdUseStaticTextColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn umdCurrentTextColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn umdTally1Coulmn;
        private System.Windows.Forms.DataGridViewTextBoxColumn umdTally2Column;
        private System.Windows.Forms.DataGridViewButtonColumn umdEditButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn umdDeleteButtonColumn;
    }
}