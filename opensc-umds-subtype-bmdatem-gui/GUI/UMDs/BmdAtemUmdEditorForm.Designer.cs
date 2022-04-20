namespace OpenSC.GUI.UMDs
{
    partial class BmdAtemUmdEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BmdAtemUmdEditorForm));
            this.mixerInputGroupBox = new System.Windows.Forms.GroupBox();
            this.mixerInputTable = new System.Windows.Forms.TableLayoutPanel();
            this.mixerLabel = new System.Windows.Forms.Label();
            this.inputLabel = new System.Windows.Forms.Label();
            this.mixerDropDown = new System.Windows.Forms.ComboBox();
            this.inputDropDown = new System.Windows.Forms.ComboBox();
            this.textsInfoLabel2 = new OpenSC.GUI.GeneralComponents.GrowLabel();
            this.textsInfoLabel1 = new OpenSC.GUI.GeneralComponents.GrowLabel();
            this.mainTabControl.SuspendLayout();
            this.textsTabPage.SuspendLayout();
            this.fullStaticTextTabPage.SuspendLayout();
            this.connectionTabPage.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.mixerInputGroupBox.SuspendLayout();
            this.mixerInputTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mainTabControl.Size = new System.Drawing.Size(780, 293);
            // 
            // textsTabPage
            // 
            this.textsTabPage.Controls.Add(this.textsInfoLabel1);
            this.textsTabPage.Size = new System.Drawing.Size(772, 260);
            this.textsTabPage.Controls.SetChildIndex(this.textsInfoLabel1, 0);
            // 
            // talliesTabPage
            // 
            this.talliesTabPage.Size = new System.Drawing.Size(772, 260);
            // 
            // fullStaticTextTabPage
            // 
            this.fullStaticTextTabPage.Controls.Add(this.textsInfoLabel2);
            this.fullStaticTextTabPage.Size = new System.Drawing.Size(772, 260);
            this.fullStaticTextTabPage.Controls.SetChildIndex(this.textsInfoLabel2, 0);
            // 
            // connectionTabPage
            // 
            this.connectionTabPage.Controls.Add(this.mixerInputGroupBox);
            this.connectionTabPage.Size = new System.Drawing.Size(772, 260);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(780, 529);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(800, 639);
            // 
            // mixerInputGroupBox
            // 
            this.mixerInputGroupBox.AutoSize = true;
            this.mixerInputGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mixerInputGroupBox.Controls.Add(this.mixerInputTable);
            this.mixerInputGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.mixerInputGroupBox.Location = new System.Drawing.Point(3, 3);
            this.mixerInputGroupBox.Name = "mixerInputGroupBox";
            this.mixerInputGroupBox.Size = new System.Drawing.Size(766, 94);
            this.mixerInputGroupBox.TabIndex = 0;
            this.mixerInputGroupBox.TabStop = false;
            this.mixerInputGroupBox.Text = "Mixer and input";
            // 
            // mixerInputTable
            // 
            this.mixerInputTable.AutoSize = true;
            this.mixerInputTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mixerInputTable.ColumnCount = 2;
            this.mixerInputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mixerInputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mixerInputTable.Controls.Add(this.mixerLabel, 0, 0);
            this.mixerInputTable.Controls.Add(this.inputLabel, 0, 1);
            this.mixerInputTable.Controls.Add(this.mixerDropDown, 1, 0);
            this.mixerInputTable.Controls.Add(this.inputDropDown, 1, 1);
            this.mixerInputTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mixerInputTable.Location = new System.Drawing.Point(3, 23);
            this.mixerInputTable.Name = "mixerInputTable";
            this.mixerInputTable.RowCount = 2;
            this.mixerInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mixerInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mixerInputTable.Size = new System.Drawing.Size(760, 68);
            this.mixerInputTable.TabIndex = 0;
            // 
            // mixerLabel
            // 
            this.mixerLabel.AutoSize = true;
            this.mixerLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.mixerLabel.Location = new System.Drawing.Point(3, 0);
            this.mixerLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.mixerLabel.Name = "mixerLabel";
            this.mixerLabel.Size = new System.Drawing.Size(46, 34);
            this.mixerLabel.TabIndex = 0;
            this.mixerLabel.Text = "Mixer";
            this.mixerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.inputLabel.Location = new System.Drawing.Point(3, 34);
            this.inputLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(43, 34);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "Input";
            this.inputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mixerDropDown
            // 
            this.mixerDropDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mixerDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mixerDropDown.FormattingEnabled = true;
            this.mixerDropDown.Location = new System.Drawing.Point(67, 3);
            this.mixerDropDown.Name = "mixerDropDown";
            this.mixerDropDown.Size = new System.Drawing.Size(690, 28);
            this.mixerDropDown.TabIndex = 2;
            // 
            // inputDropDown
            // 
            this.inputDropDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputDropDown.FormattingEnabled = true;
            this.inputDropDown.Location = new System.Drawing.Point(67, 37);
            this.inputDropDown.Name = "inputDropDown";
            this.inputDropDown.Size = new System.Drawing.Size(690, 28);
            this.inputDropDown.TabIndex = 3;
            // 
            // textsInfoLabel2
            // 
            this.textsInfoLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.textsInfoLabel2.Location = new System.Drawing.Point(3, 133);
            this.textsInfoLabel2.Name = "textsInfoLabel2";
            this.textsInfoLabel2.Size = new System.Drawing.Size(766, 120);
            this.textsInfoLabel2.TabIndex = 3;
            this.textsInfoLabel2.Text = resources.GetString("textsInfoLabel2.Text");
            // 
            // textsInfoLabel1
            // 
            this.textsInfoLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textsInfoLabel1.Location = new System.Drawing.Point(3, 165);
            this.textsInfoLabel1.Name = "textsInfoLabel1";
            this.textsInfoLabel1.Size = new System.Drawing.Size(766, 40);
            this.textsInfoLabel1.TabIndex = 5;
            this.textsInfoLabel1.Text = "As short text only 4 characters are displayable, as long text only 16. The texts " +
    "sources can have any length, they will be automatically trimmed when sending to " +
    "hardware.";
            // 
            // BmdAtemUmdEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 709);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(500, 661);
            this.Name = "BmdAtemUmdEditorForm";
            this.mainTabControl.ResumeLayout(false);
            this.textsTabPage.ResumeLayout(false);
            this.textsTabPage.PerformLayout();
            this.fullStaticTextTabPage.ResumeLayout(false);
            this.fullStaticTextTabPage.PerformLayout();
            this.connectionTabPage.ResumeLayout(false);
            this.connectionTabPage.PerformLayout();
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.mixerInputGroupBox.ResumeLayout(false);
            this.mixerInputGroupBox.PerformLayout();
            this.mixerInputTable.ResumeLayout(false);
            this.mixerInputTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox mixerInputGroupBox;
        private System.Windows.Forms.TableLayoutPanel mixerInputTable;
        private System.Windows.Forms.Label mixerLabel;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.ComboBox mixerDropDown;
        private System.Windows.Forms.ComboBox inputDropDown;
        private GeneralComponents.GrowLabel textsInfoLabel1;
        private GeneralComponents.GrowLabel textsInfoLabel2;
    }
}