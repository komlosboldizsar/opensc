namespace OpenSC.GUI.Settings
{
    partial class OpenFileSettingEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.copyToProgramFolderCheckbox = new System.Windows.Forms.CheckBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.customElementsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.copyToProgramFolderCheckbox);
            this.customElementsPanel.Controls.Add(this.browseButton);
            this.customElementsPanel.Controls.Add(this.filePathTextBox);
            this.customElementsPanel.MinimumSize = new System.Drawing.Size(250, 12);
            this.customElementsPanel.Size = new System.Drawing.Size(494, 69);
            this.customElementsPanel.Controls.SetChildIndex(this.filePathTextBox, 0);
            this.customElementsPanel.Controls.SetChildIndex(this.browseButton, 0);
            this.customElementsPanel.Controls.SetChildIndex(this.copyToProgramFolderCheckbox, 0);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 27);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(427, 27);
            this.textBox2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Location = new System.Drawing.Point(427, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 40);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.AllowDrop = true;
            this.filePathTextBox.Location = new System.Drawing.Point(8, 8);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(252, 27);
            this.filePathTextBox.TabIndex = 6;
            this.filePathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.filePathTextBox_DragDrop);
            this.filePathTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.filePathTextBox_DragOver);
            // 
            // browseButton
            // 
            this.browseButton.Image = global::OpenSC.GUI.GeneralIcons._16_folder;
            this.browseButton.Location = new System.Drawing.Point(266, 7);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(34, 29);
            this.browseButton.TabIndex = 7;
            this.browseButton.Text = "button3";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // copyToProgramFolderCheckbox
            // 
            this.copyToProgramFolderCheckbox.AutoSize = true;
            this.copyToProgramFolderCheckbox.Location = new System.Drawing.Point(17, 40);
            this.copyToProgramFolderCheckbox.Name = "copyToProgramFolderCheckbox";
            this.copyToProgramFolderCheckbox.Size = new System.Drawing.Size(212, 24);
            this.copyToProgramFolderCheckbox.TabIndex = 8;
            this.copyToProgramFolderCheckbox.Text = "Copy to applications folder";
            this.copyToProgramFolderCheckbox.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // OpenFileSettingEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(200, 78);
            this.Name = "OpenFileSettingEditor";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.Size = new System.Drawing.Size(500, 138);
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.CheckBox copyToProgramFolderCheckbox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
