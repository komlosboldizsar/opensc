namespace OpenSC.GUI.Timers
{
    partial class TimerList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timerListTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.timerIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerTitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerModeImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.timerModeLabelColimn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerCurrentValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerStartValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerEditButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.timerDeleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.startButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.stopButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.resetButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.openTimerWindowButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timerListTable)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.timerListTable);
            this.mainContainer.Controls.Add(this.groupBox1);
            this.mainContainer.Size = new System.Drawing.Size(1125, 444);
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1125, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // timerListTable
            // 
            this.timerListTable.AllowUserToAddRows = false;
            this.timerListTable.AllowUserToDeleteRows = false;
            this.timerListTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timerListTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timerIdColumn,
            this.timerTitleColumn,
            this.timerModeImageColumn,
            this.timerModeLabelColimn,
            this.timerCurrentValueColumn,
            this.timerStartValueColumn,
            this.timerEditButtonColumn,
            this.timerDeleteButtonColumn,
            this.startButtonColumn,
            this.stopButtonColumn,
            this.resetButtonColumn,
            this.openTimerWindowButtonColumn});
            this.timerListTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timerListTable.Location = new System.Drawing.Point(0, 100);
            this.timerListTable.Name = "timerListTable";
            this.timerListTable.ReadOnly = true;
            this.timerListTable.RowTemplate.Height = 24;
            this.timerListTable.Size = new System.Drawing.Size(1125, 344);
            this.timerListTable.TabIndex = 2;
            this.timerListTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.timerListTable_CellContentClick);
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
            // timerIdColumn
            // 
            this.timerIdColumn.HeaderText = "ID";
            this.timerIdColumn.Name = "timerIdColumn";
            this.timerIdColumn.ReadOnly = true;
            this.timerIdColumn.Width = 30;
            // 
            // timerTitleColumn
            // 
            this.timerTitleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.timerTitleColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.timerTitleColumn.DividerWidth = 3;
            this.timerTitleColumn.HeaderText = "Title";
            this.timerTitleColumn.Name = "timerTitleColumn";
            this.timerTitleColumn.ReadOnly = true;
            // 
            // timerModeImageColumn
            // 
            this.timerModeImageColumn.HeaderText = "";
            this.timerModeImageColumn.Name = "timerModeImageColumn";
            this.timerModeImageColumn.ReadOnly = true;
            this.timerModeImageColumn.Width = 30;
            // 
            // timerModeLabelColimn
            // 
            this.timerModeLabelColimn.HeaderText = "Mode";
            this.timerModeLabelColimn.Name = "timerModeLabelColimn";
            this.timerModeLabelColimn.ReadOnly = true;
            // 
            // timerCurrentValueColumn
            // 
            this.timerCurrentValueColumn.HeaderText = "Current value";
            this.timerCurrentValueColumn.Name = "timerCurrentValueColumn";
            this.timerCurrentValueColumn.ReadOnly = true;
            this.timerCurrentValueColumn.Width = 120;
            // 
            // timerStartValueColumn
            // 
            this.timerStartValueColumn.DividerWidth = 3;
            this.timerStartValueColumn.HeaderText = "Start value";
            this.timerStartValueColumn.Name = "timerStartValueColumn";
            this.timerStartValueColumn.ReadOnly = true;
            this.timerStartValueColumn.Width = 120;
            // 
            // timerEditButtonColumn
            // 
            this.timerEditButtonColumn.HeaderText = "Edit";
            this.timerEditButtonColumn.Name = "timerEditButtonColumn";
            this.timerEditButtonColumn.ReadOnly = true;
            this.timerEditButtonColumn.Width = 70;
            // 
            // timerDeleteButtonColumn
            // 
            this.timerDeleteButtonColumn.DividerWidth = 3;
            this.timerDeleteButtonColumn.HeaderText = "Delete";
            this.timerDeleteButtonColumn.Name = "timerDeleteButtonColumn";
            this.timerDeleteButtonColumn.ReadOnly = true;
            this.timerDeleteButtonColumn.Width = 70;
            // 
            // startButtonColumn
            // 
            this.startButtonColumn.HeaderText = "";
            this.startButtonColumn.Name = "startButtonColumn";
            this.startButtonColumn.ReadOnly = true;
            this.startButtonColumn.Width = 40;
            // 
            // stopButtonColumn
            // 
            this.stopButtonColumn.HeaderText = "";
            this.stopButtonColumn.Name = "stopButtonColumn";
            this.stopButtonColumn.ReadOnly = true;
            this.stopButtonColumn.Width = 40;
            // 
            // resetButtonColumn
            // 
            this.resetButtonColumn.DividerWidth = 3;
            this.resetButtonColumn.HeaderText = "";
            this.resetButtonColumn.Name = "resetButtonColumn";
            this.resetButtonColumn.ReadOnly = true;
            this.resetButtonColumn.Width = 40;
            // 
            // openTimerWindowButtonColumn
            // 
            this.openTimerWindowButtonColumn.HeaderText = "Open";
            this.openTimerWindowButtonColumn.Name = "openTimerWindowButtonColumn";
            this.openTimerWindowButtonColumn.ReadOnly = true;
            this.openTimerWindowButtonColumn.Width = 80;
            // 
            // TimerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 500);
            this.Name = "TimerList";
            this.Text = "List of timers";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TimerList_FormClosed);
            this.Load += new System.EventHandler(this.TimerList_Load);
            this.mainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timerListTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView timerListTable;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn5;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn timerIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timerTitleColumn;
        private System.Windows.Forms.DataGridViewImageColumn timerModeImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timerModeLabelColimn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timerCurrentValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timerStartValueColumn;
        private System.Windows.Forms.DataGridViewButtonColumn timerEditButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn timerDeleteButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn startButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn stopButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn resetButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn openTimerWindowButtonColumn;
    }
}