namespace OpenSC.GUI.UMDs
{
    partial class Tsl31UmdEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tsl31UmdEditorForm));
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.connectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.portLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.addressNumericInput = new System.Windows.Forms.NumericUpDown();
            this.portDropDown = new System.Windows.Forms.ComboBox();
            this.textLengthInfoLabel1 = new OpenSC.GUI.GeneralComponents.GrowLabel();
            this.textLengthInfoLabel2 = new OpenSC.GUI.GeneralComponents.GrowLabel();
            this.tallyInfoLabel = new OpenSC.GUI.GeneralComponents.GrowLabel();
            this.tallyOverrideGroupBox = new System.Windows.Forms.GroupBox();
            this.tallyOverrideTable = new System.Windows.Forms.TableLayoutPanel();
            this.tally12overrideLabel = new System.Windows.Forms.Label();
            this.tallyOverrideInfoLabel = new OpenSC.GUI.GeneralComponents.GrowLabel();
            this.tally34overrideLabel = new System.Windows.Forms.Label();
            this.tally12overrideLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.tally12paralellRadioButton = new System.Windows.Forms.RadioButton();
            this.tally1overrides2RadioButton = new System.Windows.Forms.RadioButton();
            this.tally2overrides1RadioButton = new System.Windows.Forms.RadioButton();
            this.tally34overrideLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.tally34paralellRadioButton = new System.Windows.Forms.RadioButton();
            this.tally3overrides4RadioButton = new System.Windows.Forms.RadioButton();
            this.tally4overrides3RadioButton = new System.Windows.Forms.RadioButton();
            this.mainTabControl.SuspendLayout();
            this.textsTabPage.SuspendLayout();
            this.talliesTabPage.SuspendLayout();
            this.fullStaticTextTabPage.SuspendLayout();
            this.connectionTabPage.SuspendLayout();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.connectionGroupBox.SuspendLayout();
            this.connectionTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addressNumericInput)).BeginInit();
            this.tallyOverrideGroupBox.SuspendLayout();
            this.tallyOverrideTable.SuspendLayout();
            this.tally12overrideLayout.SuspendLayout();
            this.tally34overrideLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Size = new System.Drawing.Size(780, 275);
            // 
            // textsTabPage
            // 
            this.textsTabPage.Controls.Add(this.textLengthInfoLabel1);
            this.textsTabPage.Size = new System.Drawing.Size(687, 254);
            this.textsTabPage.Controls.SetChildIndex(this.textLengthInfoLabel1, 0);
            // 
            // talliesTabPage
            // 
            this.talliesTabPage.Controls.Add(this.tallyOverrideGroupBox);
            this.talliesTabPage.Controls.Add(this.tallyInfoLabel);
            this.talliesTabPage.Size = new System.Drawing.Size(772, 242);
            this.talliesTabPage.Controls.SetChildIndex(this.tallyInfoLabel, 0);
            this.talliesTabPage.Controls.SetChildIndex(this.tallyOverrideGroupBox, 0);
            // 
            // fullStaticTextTabPage
            // 
            this.fullStaticTextTabPage.Controls.Add(this.textLengthInfoLabel2);
            this.fullStaticTextTabPage.Size = new System.Drawing.Size(687, 254);
            this.fullStaticTextTabPage.Controls.SetChildIndex(this.textLengthInfoLabel2, 0);
            // 
            // connectionTabPage
            // 
            this.connectionTabPage.Controls.Add(this.connectionGroupBox);
            this.connectionTabPage.Size = new System.Drawing.Size(772, 242);
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Size = new System.Drawing.Size(780, 511);
            // 
            // mainContainer
            // 
            this.mainContainer.Size = new System.Drawing.Size(800, 621);
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
            this.connectionGroupBox.Size = new System.Drawing.Size(766, 102);
            this.connectionGroupBox.TabIndex = 2;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Connection";
            // 
            // connectionTable
            // 
            this.connectionTable.AutoSize = true;
            this.connectionTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionTable.ColumnCount = 2;
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.connectionTable.Controls.Add(this.portLabel, 0, 0);
            this.connectionTable.Controls.Add(this.addressLabel, 0, 1);
            this.connectionTable.Controls.Add(this.addressNumericInput, 1, 1);
            this.connectionTable.Controls.Add(this.portDropDown, 1, 0);
            this.connectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTable.Location = new System.Drawing.Point(8, 25);
            this.connectionTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 2;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(750, 67);
            this.connectionTable.TabIndex = 0;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.portLabel.Location = new System.Drawing.Point(3, 0);
            this.portLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(35, 34);
            this.portLabel.TabIndex = 0;
            this.portLabel.Text = "Port";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.addressLabel.Location = new System.Drawing.Point(3, 34);
            this.addressLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(62, 33);
            this.addressLabel.TabIndex = 1;
            this.addressLabel.Text = "Address";
            this.addressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addressNumericInput
            // 
            this.addressNumericInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.addressNumericInput.Location = new System.Drawing.Point(83, 37);
            this.addressNumericInput.Maximum = new decimal(new int[] {
            126,
            0,
            0,
            0});
            this.addressNumericInput.Name = "addressNumericInput";
            this.addressNumericInput.Size = new System.Drawing.Size(150, 27);
            this.addressNumericInput.TabIndex = 2;
            // 
            // portDropDown
            // 
            this.portDropDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.portDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portDropDown.FormattingEnabled = true;
            this.portDropDown.Location = new System.Drawing.Point(83, 3);
            this.portDropDown.Name = "portDropDown";
            this.portDropDown.Size = new System.Drawing.Size(305, 28);
            this.portDropDown.TabIndex = 3;
            // 
            // textLengthInfoLabel1
            // 
            this.textLengthInfoLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textLengthInfoLabel1.Location = new System.Drawing.Point(3, 165);
            this.textLengthInfoLabel1.Name = "textLengthInfoLabel1";
            this.textLengthInfoLabel1.Size = new System.Drawing.Size(681, 80);
            this.textLengthInfoLabel1.TabIndex = 5;
            this.textLengthInfoLabel1.Text = resources.GetString("textLengthInfoLabel1.Text");
            this.textLengthInfoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textLengthInfoLabel2
            // 
            this.textLengthInfoLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.textLengthInfoLabel2.Location = new System.Drawing.Point(3, 133);
            this.textLengthInfoLabel2.Name = "textLengthInfoLabel2";
            this.textLengthInfoLabel2.Size = new System.Drawing.Size(681, 80);
            this.textLengthInfoLabel2.TabIndex = 3;
            this.textLengthInfoLabel2.Text = resources.GetString("textLengthInfoLabel2.Text");
            // 
            // tallyInfoLabel
            // 
            this.tallyInfoLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tallyInfoLabel.Location = new System.Drawing.Point(3, 75);
            this.tallyInfoLabel.Name = "tallyInfoLabel";
            this.tallyInfoLabel.Size = new System.Drawing.Size(745, 40);
            this.tallyInfoLabel.TabIndex = 4;
            this.tallyInfoLabel.Text = "Only the first two tallies are used for single displays.\r\nTallies 3&&4 are shown " +
    "by the second (right) unit if the hardware is a dual display.";
            // 
            // tallyOverrideGroupBox
            // 
            this.tallyOverrideGroupBox.AutoSize = true;
            this.tallyOverrideGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tallyOverrideGroupBox.Controls.Add(this.tallyOverrideTable);
            this.tallyOverrideGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.tallyOverrideGroupBox.Location = new System.Drawing.Point(3, 115);
            this.tallyOverrideGroupBox.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.tallyOverrideGroupBox.Name = "tallyOverrideGroupBox";
            this.tallyOverrideGroupBox.Padding = new System.Windows.Forms.Padding(8, 5, 8, 10);
            this.tallyOverrideGroupBox.Size = new System.Drawing.Size(745, 187);
            this.tallyOverrideGroupBox.TabIndex = 5;
            this.tallyOverrideGroupBox.TabStop = false;
            this.tallyOverrideGroupBox.Text = "Override";
            // 
            // tallyOverrideTable
            // 
            this.tallyOverrideTable.AutoSize = true;
            this.tallyOverrideTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tallyOverrideTable.ColumnCount = 2;
            this.tallyOverrideTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tallyOverrideTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tallyOverrideTable.Controls.Add(this.tally12overrideLabel, 0, 0);
            this.tallyOverrideTable.Controls.Add(this.tallyOverrideInfoLabel, 0, 2);
            this.tallyOverrideTable.Controls.Add(this.tally34overrideLabel, 0, 1);
            this.tallyOverrideTable.Controls.Add(this.tally12overrideLayout, 1, 0);
            this.tallyOverrideTable.Controls.Add(this.tally34overrideLayout, 1, 1);
            this.tallyOverrideTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tallyOverrideTable.Location = new System.Drawing.Point(8, 25);
            this.tallyOverrideTable.Name = "tallyOverrideTable";
            this.tallyOverrideTable.RowCount = 3;
            this.tallyOverrideTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tallyOverrideTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tallyOverrideTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tallyOverrideTable.Size = new System.Drawing.Size(729, 152);
            this.tallyOverrideTable.TabIndex = 0;
            // 
            // tally12overrideLabel
            // 
            this.tally12overrideLabel.AutoSize = true;
            this.tally12overrideLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tally12overrideLabel.Location = new System.Drawing.Point(3, 0);
            this.tally12overrideLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.tally12overrideLabel.Name = "tally12overrideLabel";
            this.tally12overrideLabel.Size = new System.Drawing.Size(31, 36);
            this.tally12overrideLabel.TabIndex = 3;
            this.tally12overrideLabel.Text = "1-2";
            this.tally12overrideLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tallyOverrideInfoLabel
            // 
            this.tallyOverrideInfoLabel.AutoSize = true;
            this.tallyOverrideTable.SetColumnSpan(this.tallyOverrideInfoLabel, 2);
            this.tallyOverrideInfoLabel.Location = new System.Drawing.Point(3, 72);
            this.tallyOverrideInfoLabel.Name = "tallyOverrideInfoLabel";
            this.tallyOverrideInfoLabel.Size = new System.Drawing.Size(431, 20);
            this.tallyOverrideInfoLabel.TabIndex = 2;
            this.tallyOverrideInfoLabel.Text = "Tally X overrides Y: when both are on, only X is sent to hardware.";
            // 
            // tally34overrideLabel
            // 
            this.tally34overrideLabel.AutoSize = true;
            this.tally34overrideLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.tally34overrideLabel.Location = new System.Drawing.Point(3, 36);
            this.tally34overrideLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.tally34overrideLabel.Name = "tally34overrideLabel";
            this.tally34overrideLabel.Size = new System.Drawing.Size(31, 36);
            this.tally34overrideLabel.TabIndex = 4;
            this.tally34overrideLabel.Text = "3-4";
            this.tally34overrideLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tally12overrideLayout
            // 
            this.tally12overrideLayout.AutoSize = true;
            this.tally12overrideLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tally12overrideLayout.Controls.Add(this.tally12paralellRadioButton);
            this.tally12overrideLayout.Controls.Add(this.tally1overrides2RadioButton);
            this.tally12overrideLayout.Controls.Add(this.tally2overrides1RadioButton);
            this.tally12overrideLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tally12overrideLayout.Location = new System.Drawing.Point(52, 3);
            this.tally12overrideLayout.Name = "tally12overrideLayout";
            this.tally12overrideLayout.Size = new System.Drawing.Size(674, 30);
            this.tally12overrideLayout.TabIndex = 5;
            // 
            // tally12paralellRadioButton
            // 
            this.tally12paralellRadioButton.AutoSize = true;
            this.tally12paralellRadioButton.Location = new System.Drawing.Point(3, 3);
            this.tally12paralellRadioButton.Name = "tally12paralellRadioButton";
            this.tally12paralellRadioButton.Size = new System.Drawing.Size(168, 24);
            this.tally12paralellRadioButton.TabIndex = 0;
            this.tally12paralellRadioButton.TabStop = true;
            this.tally12paralellRadioButton.Text = "Paralell/independent";
            this.tally12paralellRadioButton.UseVisualStyleBackColor = true;
            // 
            // tally1overrides2RadioButton
            // 
            this.tally1overrides2RadioButton.AutoSize = true;
            this.tally1overrides2RadioButton.Location = new System.Drawing.Point(177, 3);
            this.tally1overrides2RadioButton.Name = "tally1overrides2RadioButton";
            this.tally1overrides2RadioButton.Size = new System.Drawing.Size(115, 24);
            this.tally1overrides2RadioButton.TabIndex = 1;
            this.tally1overrides2RadioButton.TabStop = true;
            this.tally1overrides2RadioButton.Text = "1 overrides 2";
            this.tally1overrides2RadioButton.UseVisualStyleBackColor = true;
            // 
            // tally2overrides1RadioButton
            // 
            this.tally2overrides1RadioButton.AutoSize = true;
            this.tally2overrides1RadioButton.Location = new System.Drawing.Point(298, 3);
            this.tally2overrides1RadioButton.Name = "tally2overrides1RadioButton";
            this.tally2overrides1RadioButton.Size = new System.Drawing.Size(115, 24);
            this.tally2overrides1RadioButton.TabIndex = 2;
            this.tally2overrides1RadioButton.TabStop = true;
            this.tally2overrides1RadioButton.Text = "2 overrides 1";
            this.tally2overrides1RadioButton.UseVisualStyleBackColor = true;
            // 
            // tally34overrideLayout
            // 
            this.tally34overrideLayout.AutoSize = true;
            this.tally34overrideLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tally34overrideLayout.Controls.Add(this.tally34paralellRadioButton);
            this.tally34overrideLayout.Controls.Add(this.tally3overrides4RadioButton);
            this.tally34overrideLayout.Controls.Add(this.tally4overrides3RadioButton);
            this.tally34overrideLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tally34overrideLayout.Location = new System.Drawing.Point(52, 39);
            this.tally34overrideLayout.Name = "tally34overrideLayout";
            this.tally34overrideLayout.Size = new System.Drawing.Size(674, 30);
            this.tally34overrideLayout.TabIndex = 6;
            // 
            // tally34paralellRadioButton
            // 
            this.tally34paralellRadioButton.AutoSize = true;
            this.tally34paralellRadioButton.Location = new System.Drawing.Point(3, 3);
            this.tally34paralellRadioButton.Name = "tally34paralellRadioButton";
            this.tally34paralellRadioButton.Size = new System.Drawing.Size(168, 24);
            this.tally34paralellRadioButton.TabIndex = 0;
            this.tally34paralellRadioButton.TabStop = true;
            this.tally34paralellRadioButton.Text = "Paralell/independent";
            this.tally34paralellRadioButton.UseVisualStyleBackColor = true;
            // 
            // tally3overrides4RadioButton
            // 
            this.tally3overrides4RadioButton.AutoSize = true;
            this.tally3overrides4RadioButton.Location = new System.Drawing.Point(177, 3);
            this.tally3overrides4RadioButton.Name = "tally3overrides4RadioButton";
            this.tally3overrides4RadioButton.Size = new System.Drawing.Size(115, 24);
            this.tally3overrides4RadioButton.TabIndex = 1;
            this.tally3overrides4RadioButton.TabStop = true;
            this.tally3overrides4RadioButton.Text = "3 overrides 4";
            this.tally3overrides4RadioButton.UseVisualStyleBackColor = true;
            // 
            // tally4overrides3RadioButton
            // 
            this.tally4overrides3RadioButton.AutoSize = true;
            this.tally4overrides3RadioButton.Location = new System.Drawing.Point(298, 3);
            this.tally4overrides3RadioButton.Name = "tally4overrides3RadioButton";
            this.tally4overrides3RadioButton.Size = new System.Drawing.Size(115, 24);
            this.tally4overrides3RadioButton.TabIndex = 2;
            this.tally4overrides3RadioButton.TabStop = true;
            this.tally4overrides3RadioButton.Text = "4 overrides 3";
            this.tally4overrides3RadioButton.UseVisualStyleBackColor = true;
            // 
            // Tsl31UmdEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 691);
            this.DeleteButtonVisible = true;
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.MinimumSize = new System.Drawing.Size(500, 661);
            this.Name = "Tsl31UmdEditorForm";
            this.mainTabControl.ResumeLayout(false);
            this.textsTabPage.ResumeLayout(false);
            this.textsTabPage.PerformLayout();
            this.talliesTabPage.ResumeLayout(false);
            this.talliesTabPage.PerformLayout();
            this.fullStaticTextTabPage.ResumeLayout(false);
            this.fullStaticTextTabPage.PerformLayout();
            this.connectionTabPage.ResumeLayout(false);
            this.connectionTabPage.PerformLayout();
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addressNumericInput)).EndInit();
            this.tallyOverrideGroupBox.ResumeLayout(false);
            this.tallyOverrideGroupBox.PerformLayout();
            this.tallyOverrideTable.ResumeLayout(false);
            this.tallyOverrideTable.PerformLayout();
            this.tally12overrideLayout.ResumeLayout(false);
            this.tally12overrideLayout.PerformLayout();
            this.tally34overrideLayout.ResumeLayout(false);
            this.tally34overrideLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.NumericUpDown addressNumericInput;
        private System.Windows.Forms.ComboBox portDropDown;
        private OpenSC.GUI.GeneralComponents.GrowLabel textLengthInfoLabel1;
        private OpenSC.GUI.GeneralComponents.GrowLabel textLengthInfoLabel2;
        private GeneralComponents.GrowLabel tallyInfoLabel;
        private System.Windows.Forms.GroupBox tallyOverrideGroupBox;
        private System.Windows.Forms.TableLayoutPanel tallyOverrideTable;
        private GeneralComponents.GrowLabel tallyOverrideInfoLabel;
        private System.Windows.Forms.Label tally12overrideLabel;
        private System.Windows.Forms.Label tally34overrideLabel;
        private System.Windows.Forms.FlowLayoutPanel tally12overrideLayout;
        private System.Windows.Forms.RadioButton tally12paralellRadioButton;
        private System.Windows.Forms.RadioButton tally1overrides2RadioButton;
        private System.Windows.Forms.RadioButton tally2overrides1RadioButton;
        private System.Windows.Forms.FlowLayoutPanel tally34overrideLayout;
        private System.Windows.Forms.RadioButton tally34paralellRadioButton;
        private System.Windows.Forms.RadioButton tally3overrides4RadioButton;
        private System.Windows.Forms.RadioButton tally4overrides3RadioButton;
    }
}