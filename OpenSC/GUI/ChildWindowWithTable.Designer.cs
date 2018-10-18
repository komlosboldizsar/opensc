using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
using OpenSC.Model.UMDs;

namespace OpenSC.GUI
{
    partial class ChildWindowWithTable
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
            this._table = new System.Windows.Forms.DataGridView();
            this.bottomPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._table)).BeginInit();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this._table);
            // 
            // _table
            // 
            this._table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._table.Dock = System.Windows.Forms.DockStyle.Fill;
            this._table.Location = new System.Drawing.Point(0, 0);
            this._table.Name = "_table";
            this._table.RowTemplate.Height = 24;
            this._table.Size = new System.Drawing.Size(800, 374);
            this._table.TabIndex = 0;
            // 
            // ChildWindowWithTableBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ChildWindowWithTableBase";
            this.Text = "ChildWindowWithTitleAndTable";
            this.bottomPanel.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._table)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _table;

    }
}