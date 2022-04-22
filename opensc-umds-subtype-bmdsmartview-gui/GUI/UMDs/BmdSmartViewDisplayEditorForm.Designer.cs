namespace OpenSC.GUI.UMDs
{
    partial class BmdSmartViewDisplayEditorForm
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
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.connectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.unitLabel = new System.Windows.Forms.Label();
            this.positionLabel = new System.Windows.Forms.Label();
            this.unitDropDown = new System.Windows.Forms.ComboBox();
            this.positionRadioButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.positionSingleRadioButton = new System.Windows.Forms.RadioButton();
            this.positionDualLeftRadioButton = new System.Windows.Forms.RadioButton();
            this.positionDualRightRadioButton = new System.Windows.Forms.RadioButton();
            this.talliesPriorityGroupBox = new System.Windows.Forms.GroupBox();
            this.talliesPriorityTable = new System.Windows.Forms.TableLayoutPanel();
            this.tallyPriorityNameExampleLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.talliesTabPage.SuspendLayout();
            this.connectionTabPage.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.connectionTable.SuspendLayout();
            this.positionRadioButtonsPanel.SuspendLayout();
            this.talliesPriorityGroupBox.SuspendLayout();
            this.talliesPriorityTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Size = new System.Drawing.Size(780, 198);
            // 
            // textsTabPage
            // 
            this.textsTabPage.Size = new System.Drawing.Size(687, 254);
            // 
            // talliesTabPage
            // 
            this.talliesTabPage.Controls.Add(this.talliesPriorityGroupBox);
            this.talliesTabPage.Size = new System.Drawing.Size(772, 165);
            this.talliesTabPage.Controls.SetChildIndex(this.talliesPriorityGroupBox, 0);
            // 
            // fullStaticTextTabPage
            // 
            this.fullStaticTextTabPage.Size = new System.Drawing.Size(687, 254);
            // 
            // connectionTabPage
            // 
            this.connectionTabPage.Controls.Add(this.connectionGroupBox);
            this.connectionTabPage.Size = new System.Drawing.Size(772, 165);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(780, 434);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(800, 544);
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.AutoSize = true;
            this.connectionGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionGroupBox.Controls.Add(this.connectionTable);
            this.connectionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionGroupBox.Location = new System.Drawing.Point(3, 3);
            this.connectionGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.connectionGroupBox.Size = new System.Drawing.Size(766, 105);
            this.connectionGroupBox.TabIndex = 1;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Display data";
            // 
            // connectionTable
            // 
            this.connectionTable.AutoSize = true;
            this.connectionTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionTable.ColumnCount = 2;
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.connectionTable.Controls.Add(this.unitLabel, 0, 0);
            this.connectionTable.Controls.Add(this.positionLabel, 0, 1);
            this.connectionTable.Controls.Add(this.unitDropDown, 1, 0);
            this.connectionTable.Controls.Add(this.positionRadioButtonsPanel, 1, 1);
            this.connectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTable.Location = new System.Drawing.Point(8, 25);
            this.connectionTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 2;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(750, 70);
            this.connectionTable.TabIndex = 0;
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.unitLabel.Location = new System.Drawing.Point(3, 0);
            this.unitLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(36, 34);
            this.unitLabel.TabIndex = 0;
            this.unitLabel.Text = "Unit";
            this.unitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.positionLabel.Location = new System.Drawing.Point(3, 34);
            this.positionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(61, 36);
            this.positionLabel.TabIndex = 1;
            this.positionLabel.Text = "Position";
            this.positionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // unitDropDown
            // 
            this.unitDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.unitDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitDropDown.FormattingEnabled = true;
            this.unitDropDown.Location = new System.Drawing.Point(82, 3);
            this.unitDropDown.Name = "unitDropDown";
            this.unitDropDown.Size = new System.Drawing.Size(305, 28);
            this.unitDropDown.TabIndex = 3;
            // 
            // positionRadioButtonsPanel
            // 
            this.positionRadioButtonsPanel.AutoSize = true;
            this.positionRadioButtonsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.positionRadioButtonsPanel.Controls.Add(this.positionSingleRadioButton);
            this.positionRadioButtonsPanel.Controls.Add(this.positionDualLeftRadioButton);
            this.positionRadioButtonsPanel.Controls.Add(this.positionDualRightRadioButton);
            this.positionRadioButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionRadioButtonsPanel.Location = new System.Drawing.Point(82, 37);
            this.positionRadioButtonsPanel.Name = "positionRadioButtonsPanel";
            this.positionRadioButtonsPanel.Size = new System.Drawing.Size(665, 30);
            this.positionRadioButtonsPanel.TabIndex = 4;
            // 
            // positionSingleRadioButton
            // 
            this.positionSingleRadioButton.AutoSize = true;
            this.positionSingleRadioButton.Location = new System.Drawing.Point(3, 3);
            this.positionSingleRadioButton.Name = "positionSingleRadioButton";
            this.positionSingleRadioButton.Size = new System.Drawing.Size(71, 24);
            this.positionSingleRadioButton.TabIndex = 0;
            this.positionSingleRadioButton.TabStop = true;
            this.positionSingleRadioButton.Text = "Single";
            this.positionSingleRadioButton.UseVisualStyleBackColor = true;
            // 
            // positionDualLeftRadioButton
            // 
            this.positionDualLeftRadioButton.AutoSize = true;
            this.positionDualLeftRadioButton.Location = new System.Drawing.Point(80, 3);
            this.positionDualLeftRadioButton.Name = "positionDualLeftRadioButton";
            this.positionDualLeftRadioButton.Size = new System.Drawing.Size(90, 24);
            this.positionDualLeftRadioButton.TabIndex = 1;
            this.positionDualLeftRadioButton.TabStop = true;
            this.positionDualLeftRadioButton.Text = "Dual, left";
            this.positionDualLeftRadioButton.UseVisualStyleBackColor = true;
            // 
            // positionDualRightRadioButton
            // 
            this.positionDualRightRadioButton.AutoSize = true;
            this.positionDualRightRadioButton.Location = new System.Drawing.Point(176, 3);
            this.positionDualRightRadioButton.Name = "positionDualRightRadioButton";
            this.positionDualRightRadioButton.Size = new System.Drawing.Size(99, 24);
            this.positionDualRightRadioButton.TabIndex = 2;
            this.positionDualRightRadioButton.TabStop = true;
            this.positionDualRightRadioButton.Text = "Dual, right";
            this.positionDualRightRadioButton.UseVisualStyleBackColor = true;
            // 
            // talliesPriorityGroupBox
            // 
            this.talliesPriorityGroupBox.AutoSize = true;
            this.talliesPriorityGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.talliesPriorityGroupBox.Controls.Add(this.talliesPriorityTable);
            this.talliesPriorityGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.talliesPriorityGroupBox.Location = new System.Drawing.Point(3, 75);
            this.talliesPriorityGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.talliesPriorityGroupBox.Name = "talliesPriorityGroupBox";
            this.talliesPriorityGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.talliesPriorityGroupBox.Size = new System.Drawing.Size(766, 70);
            this.talliesPriorityGroupBox.TabIndex = 4;
            this.talliesPriorityGroupBox.TabStop = false;
            this.talliesPriorityGroupBox.Text = "Priorities";
            // 
            // talliesPriorityTable
            // 
            this.talliesPriorityTable.AutoSize = true;
            this.talliesPriorityTable.ColumnCount = 3;
            this.talliesPriorityTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.talliesPriorityTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.talliesPriorityTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.talliesPriorityTable.Controls.Add(this.tallyPriorityNameExampleLabel, 0, 0);
            this.talliesPriorityTable.Controls.Add(this.button1, 1, 0);
            this.talliesPriorityTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talliesPriorityTable.Location = new System.Drawing.Point(8, 25);
            this.talliesPriorityTable.Name = "talliesPriorityTable";
            this.talliesPriorityTable.RowCount = 1;
            this.talliesPriorityTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.talliesPriorityTable.Size = new System.Drawing.Size(750, 35);
            this.talliesPriorityTable.TabIndex = 0;
            // 
            // tallyPriorityNameExampleLabel
            // 
            this.tallyPriorityNameExampleLabel.AutoSize = true;
            this.tallyPriorityNameExampleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tallyPriorityNameExampleLabel.Location = new System.Drawing.Point(3, 0);
            this.tallyPriorityNameExampleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.tallyPriorityNameExampleLabel.Name = "tallyPriorityNameExampleLabel";
            this.tallyPriorityNameExampleLabel.Size = new System.Drawing.Size(59, 35);
            this.tallyPriorityNameExampleLabel.TabIndex = 0;
            this.tallyPriorityNameExampleLabel.Text = "Tally #1";
            this.tallyPriorityNameExampleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // BmdSmartViewDisplayEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 614);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(500, 661);
            this.Name = "BmdSmartViewDisplayEditorForm";
            this.mainTabControl.ResumeLayout(false);
            this.talliesTabPage.ResumeLayout(false);
            this.talliesTabPage.PerformLayout();
            this.connectionTabPage.ResumeLayout(false);
            this.connectionTabPage.PerformLayout();
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            this.positionRadioButtonsPanel.ResumeLayout(false);
            this.positionRadioButtonsPanel.PerformLayout();
            this.talliesPriorityGroupBox.ResumeLayout(false);
            this.talliesPriorityGroupBox.PerformLayout();
            this.talliesPriorityTable.ResumeLayout(false);
            this.talliesPriorityTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label unitLabel;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.ComboBox unitDropDown;
        private System.Windows.Forms.FlowLayoutPanel positionRadioButtonsPanel;
        private System.Windows.Forms.RadioButton positionSingleRadioButton;
        private System.Windows.Forms.RadioButton positionDualLeftRadioButton;
        private System.Windows.Forms.RadioButton positionDualRightRadioButton;
        private System.Windows.Forms.GroupBox talliesPriorityGroupBox;
        private System.Windows.Forms.TableLayoutPanel talliesPriorityTable;
        private System.Windows.Forms.Label tallyPriorityNameExampleLabel;
        private System.Windows.Forms.Button button1;
    }
}