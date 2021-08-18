namespace OpenSC.GUI.Routers.CrosspointStores
{
    partial class CrosspointStoreEditorForm
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
            this.basicDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.idNumericField = new System.Windows.Forms.NumericUpDown();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.basicDataGroupBox = new System.Windows.Forms.GroupBox();
            this.routerInputGroupBox = new System.Windows.Forms.GroupBox();
            this.routerInputTable = new System.Windows.Forms.TableLayoutPanel();
            this.routerInputRouterLabel = new System.Windows.Forms.Label();
            this.routerInputRouterDropDown = new System.Windows.Forms.ComboBox();
            this.routerInputInputDropDown = new System.Windows.Forms.ComboBox();
            this.routerInputInputLabel = new System.Windows.Forms.Label();
            this.routerOutputGroupBox = new System.Windows.Forms.GroupBox();
            this.routerOutputTable = new System.Windows.Forms.TableLayoutPanel();
            this.routerOutputOutputLabel = new System.Windows.Forms.Label();
            this.routerOutputOutputDropDown = new System.Windows.Forms.ComboBox();
            this.routerOutputRouterLabel = new System.Windows.Forms.Label();
            this.routerOutputRouterDropDown = new System.Windows.Forms.ComboBox();
            this.modeLabel = new System.Windows.Forms.GroupBox();
            this.modeTable = new System.Windows.Forms.TableLayoutPanel();
            this.autotakeAfterOutputSetLabel = new System.Windows.Forms.Label();
            this.autotakeAfterOutputSetCheckbox = new System.Windows.Forms.CheckBox();
            this.clearInputAfterTakeLabel = new System.Windows.Forms.Label();
            this.clearInputAfterTakeCheckbox = new System.Windows.Forms.CheckBox();
            this.clearOutputAfterTakeCheckbox = new System.Windows.Forms.CheckBox();
            this.clearOutputAfterTakeLabel = new System.Windows.Forms.Label();
            this.customElementsPanel.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.basicDataTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).BeginInit();
            this.basicDataGroupBox.SuspendLayout();
            this.routerInputGroupBox.SuspendLayout();
            this.routerInputTable.SuspendLayout();
            this.routerOutputGroupBox.SuspendLayout();
            this.routerOutputTable.SuspendLayout();
            this.modeLabel.SuspendLayout();
            this.modeTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // customElementsPanel
            // 
            this.customElementsPanel.Controls.Add(this.modeLabel);
            this.customElementsPanel.Controls.Add(this.routerOutputGroupBox);
            this.customElementsPanel.Controls.Add(this.routerInputGroupBox);
            this.customElementsPanel.Controls.Add(this.basicDataGroupBox);
            this.customElementsPanel.Location = new System.Drawing.Point(10, 10);
            this.customElementsPanel.Padding = new System.Windows.Forms.Padding(0);
            this.customElementsPanel.Size = new System.Drawing.Size(489, 406);
            // 
            // mainContainer
            // 
            this.mainContainer.Padding = new System.Windows.Forms.Padding(10);
            this.mainContainer.Size = new System.Drawing.Size(509, 495);
            // 
            // basicDataTable
            // 
            this.basicDataTable.AutoSize = true;
            this.basicDataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basicDataTable.ColumnCount = 2;
            this.basicDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.basicDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.basicDataTable.Controls.Add(this.idLabel, 0, 0);
            this.basicDataTable.Controls.Add(this.nameLabel, 0, 1);
            this.basicDataTable.Controls.Add(this.idNumericField, 1, 0);
            this.basicDataTable.Controls.Add(this.nameTextBox, 1, 1);
            this.basicDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basicDataTable.Location = new System.Drawing.Point(8, 19);
            this.basicDataTable.Name = "basicDataTable";
            this.basicDataTable.RowCount = 2;
            this.basicDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.basicDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.basicDataTable.Size = new System.Drawing.Size(473, 56);
            this.basicDataTable.TabIndex = 0;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.idLabel.Location = new System.Drawing.Point(3, 0);
            this.idLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(21, 28);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "ID";
            this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.nameLabel.Location = new System.Drawing.Point(3, 28);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 28);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // idNumericField
            // 
            this.idNumericField.Location = new System.Drawing.Point(66, 3);
            this.idNumericField.Name = "idNumericField";
            this.idNumericField.Size = new System.Drawing.Size(120, 22);
            this.idNumericField.TabIndex = 2;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.nameTextBox.Location = new System.Drawing.Point(66, 31);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(404, 22);
            this.nameTextBox.TabIndex = 3;
            // 
            // basicDataGroupBox
            // 
            this.basicDataGroupBox.AutoSize = true;
            this.basicDataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basicDataGroupBox.Controls.Add(this.basicDataTable);
            this.basicDataGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.basicDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.basicDataGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.basicDataGroupBox.Name = "basicDataGroupBox";
            this.basicDataGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.basicDataGroupBox.Size = new System.Drawing.Size(489, 83);
            this.basicDataGroupBox.TabIndex = 1;
            this.basicDataGroupBox.TabStop = false;
            this.basicDataGroupBox.Text = "Base data";
            // 
            // routerInputGroupBox
            // 
            this.routerInputGroupBox.AutoSize = true;
            this.routerInputGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerInputGroupBox.Controls.Add(this.routerInputTable);
            this.routerInputGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.routerInputGroupBox.Location = new System.Drawing.Point(0, 83);
            this.routerInputGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.routerInputGroupBox.Name = "routerInputGroupBox";
            this.routerInputGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.routerInputGroupBox.Size = new System.Drawing.Size(489, 87);
            this.routerInputGroupBox.TabIndex = 2;
            this.routerInputGroupBox.TabStop = false;
            this.routerInputGroupBox.Text = "Current input";
            // 
            // routerInputTable
            // 
            this.routerInputTable.AutoSize = true;
            this.routerInputTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerInputTable.ColumnCount = 2;
            this.routerInputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.routerInputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.routerInputTable.Controls.Add(this.routerInputInputLabel, 0, 1);
            this.routerInputTable.Controls.Add(this.routerInputInputDropDown, 1, 1);
            this.routerInputTable.Controls.Add(this.routerInputRouterLabel, 0, 0);
            this.routerInputTable.Controls.Add(this.routerInputRouterDropDown, 1, 0);
            this.routerInputTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routerInputTable.Location = new System.Drawing.Point(8, 19);
            this.routerInputTable.Name = "routerInputTable";
            this.routerInputTable.RowCount = 2;
            this.routerInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerInputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerInputTable.Size = new System.Drawing.Size(473, 60);
            this.routerInputTable.TabIndex = 0;
            // 
            // routerInputRouterLabel
            // 
            this.routerInputRouterLabel.AutoSize = true;
            this.routerInputRouterLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerInputRouterLabel.Location = new System.Drawing.Point(3, 0);
            this.routerInputRouterLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerInputRouterLabel.Name = "routerInputRouterLabel";
            this.routerInputRouterLabel.Size = new System.Drawing.Size(51, 30);
            this.routerInputRouterLabel.TabIndex = 0;
            this.routerInputRouterLabel.Text = "Router";
            this.routerInputRouterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerInputRouterDropDown
            // 
            this.routerInputRouterDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerInputRouterDropDown.FormattingEnabled = true;
            this.routerInputRouterDropDown.Location = new System.Drawing.Point(72, 3);
            this.routerInputRouterDropDown.Name = "routerInputRouterDropDown";
            this.routerInputRouterDropDown.Size = new System.Drawing.Size(339, 24);
            this.routerInputRouterDropDown.TabIndex = 3;
            // 
            // routerInputInputDropDown
            // 
            this.routerInputInputDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerInputInputDropDown.FormattingEnabled = true;
            this.routerInputInputDropDown.Location = new System.Drawing.Point(72, 33);
            this.routerInputInputDropDown.Name = "routerInputInputDropDown";
            this.routerInputInputDropDown.Size = new System.Drawing.Size(339, 24);
            this.routerInputInputDropDown.TabIndex = 4;
            // 
            // routerInputInputLabel
            // 
            this.routerInputInputLabel.AutoSize = true;
            this.routerInputInputLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerInputInputLabel.Location = new System.Drawing.Point(3, 30);
            this.routerInputInputLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerInputInputLabel.Name = "routerInputInputLabel";
            this.routerInputInputLabel.Size = new System.Drawing.Size(39, 30);
            this.routerInputInputLabel.TabIndex = 5;
            this.routerInputInputLabel.Text = "Input";
            this.routerInputInputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerOutputGroupBox
            // 
            this.routerOutputGroupBox.AutoSize = true;
            this.routerOutputGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerOutputGroupBox.Controls.Add(this.routerOutputTable);
            this.routerOutputGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.routerOutputGroupBox.Location = new System.Drawing.Point(0, 170);
            this.routerOutputGroupBox.Margin = new System.Windows.Forms.Padding(10);
            this.routerOutputGroupBox.Name = "routerOutputGroupBox";
            this.routerOutputGroupBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.routerOutputGroupBox.Size = new System.Drawing.Size(489, 87);
            this.routerOutputGroupBox.TabIndex = 3;
            this.routerOutputGroupBox.TabStop = false;
            this.routerOutputGroupBox.Text = "Current output";
            // 
            // routerOutputTable
            // 
            this.routerOutputTable.AutoSize = true;
            this.routerOutputTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.routerOutputTable.ColumnCount = 2;
            this.routerOutputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.routerOutputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.routerOutputTable.Controls.Add(this.routerOutputOutputLabel, 0, 1);
            this.routerOutputTable.Controls.Add(this.routerOutputOutputDropDown, 1, 1);
            this.routerOutputTable.Controls.Add(this.routerOutputRouterLabel, 0, 0);
            this.routerOutputTable.Controls.Add(this.routerOutputRouterDropDown, 1, 0);
            this.routerOutputTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routerOutputTable.Location = new System.Drawing.Point(8, 19);
            this.routerOutputTable.Name = "routerOutputTable";
            this.routerOutputTable.RowCount = 2;
            this.routerOutputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerOutputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.routerOutputTable.Size = new System.Drawing.Size(473, 60);
            this.routerOutputTable.TabIndex = 0;
            // 
            // routerOutputOutputLabel
            // 
            this.routerOutputOutputLabel.AutoSize = true;
            this.routerOutputOutputLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerOutputOutputLabel.Location = new System.Drawing.Point(3, 30);
            this.routerOutputOutputLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerOutputOutputLabel.Name = "routerOutputOutputLabel";
            this.routerOutputOutputLabel.Size = new System.Drawing.Size(51, 30);
            this.routerOutputOutputLabel.TabIndex = 5;
            this.routerOutputOutputLabel.Text = "Output";
            this.routerOutputOutputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerOutputOutputDropDown
            // 
            this.routerOutputOutputDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerOutputOutputDropDown.FormattingEnabled = true;
            this.routerOutputOutputDropDown.Location = new System.Drawing.Point(72, 33);
            this.routerOutputOutputDropDown.Name = "routerOutputOutputDropDown";
            this.routerOutputOutputDropDown.Size = new System.Drawing.Size(339, 24);
            this.routerOutputOutputDropDown.TabIndex = 4;
            // 
            // routerOutputRouterLabel
            // 
            this.routerOutputRouterLabel.AutoSize = true;
            this.routerOutputRouterLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.routerOutputRouterLabel.Location = new System.Drawing.Point(3, 0);
            this.routerOutputRouterLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.routerOutputRouterLabel.Name = "routerOutputRouterLabel";
            this.routerOutputRouterLabel.Size = new System.Drawing.Size(51, 30);
            this.routerOutputRouterLabel.TabIndex = 0;
            this.routerOutputRouterLabel.Text = "Router";
            this.routerOutputRouterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerOutputRouterDropDown
            // 
            this.routerOutputRouterDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.routerOutputRouterDropDown.FormattingEnabled = true;
            this.routerOutputRouterDropDown.Location = new System.Drawing.Point(72, 3);
            this.routerOutputRouterDropDown.Name = "routerOutputRouterDropDown";
            this.routerOutputRouterDropDown.Size = new System.Drawing.Size(339, 24);
            this.routerOutputRouterDropDown.TabIndex = 3;
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modeLabel.Controls.Add(this.modeTable);
            this.modeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.modeLabel.Location = new System.Drawing.Point(0, 257);
            this.modeLabel.Margin = new System.Windows.Forms.Padding(10);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.modeLabel.Size = new System.Drawing.Size(489, 96);
            this.modeLabel.TabIndex = 4;
            this.modeLabel.TabStop = false;
            this.modeLabel.Text = "Mode";
            // 
            // modeTable
            // 
            this.modeTable.AutoSize = true;
            this.modeTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modeTable.ColumnCount = 2;
            this.modeTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.modeTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.modeTable.Controls.Add(this.clearOutputAfterTakeLabel, 0, 2);
            this.modeTable.Controls.Add(this.clearInputAfterTakeCheckbox, 1, 1);
            this.modeTable.Controls.Add(this.clearInputAfterTakeLabel, 0, 1);
            this.modeTable.Controls.Add(this.autotakeAfterOutputSetLabel, 0, 0);
            this.modeTable.Controls.Add(this.autotakeAfterOutputSetCheckbox, 1, 0);
            this.modeTable.Controls.Add(this.clearOutputAfterTakeCheckbox, 1, 2);
            this.modeTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeTable.Location = new System.Drawing.Point(8, 19);
            this.modeTable.Name = "modeTable";
            this.modeTable.RowCount = 3;
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.modeTable.Size = new System.Drawing.Size(473, 69);
            this.modeTable.TabIndex = 0;
            // 
            // autotakeAfterOutputSetLabel
            // 
            this.autotakeAfterOutputSetLabel.AutoSize = true;
            this.autotakeAfterOutputSetLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.autotakeAfterOutputSetLabel.Location = new System.Drawing.Point(3, 0);
            this.autotakeAfterOutputSetLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.autotakeAfterOutputSetLabel.Name = "autotakeAfterOutputSetLabel";
            this.autotakeAfterOutputSetLabel.Size = new System.Drawing.Size(164, 23);
            this.autotakeAfterOutputSetLabel.TabIndex = 0;
            this.autotakeAfterOutputSetLabel.Text = "Autotake after output set";
            this.autotakeAfterOutputSetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autotakeAfterOutputSetCheckbox
            // 
            this.autotakeAfterOutputSetCheckbox.AutoSize = true;
            this.autotakeAfterOutputSetCheckbox.Location = new System.Drawing.Point(185, 3);
            this.autotakeAfterOutputSetCheckbox.Name = "autotakeAfterOutputSetCheckbox";
            this.autotakeAfterOutputSetCheckbox.Size = new System.Drawing.Size(18, 17);
            this.autotakeAfterOutputSetCheckbox.TabIndex = 1;
            this.autotakeAfterOutputSetCheckbox.UseVisualStyleBackColor = true;
            // 
            // clearInputAfterTakeLabel
            // 
            this.clearInputAfterTakeLabel.AutoSize = true;
            this.clearInputAfterTakeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.clearInputAfterTakeLabel.Location = new System.Drawing.Point(3, 23);
            this.clearInputAfterTakeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.clearInputAfterTakeLabel.Name = "clearInputAfterTakeLabel";
            this.clearInputAfterTakeLabel.Size = new System.Drawing.Size(140, 23);
            this.clearInputAfterTakeLabel.TabIndex = 2;
            this.clearInputAfterTakeLabel.Text = "Clear input after take";
            this.clearInputAfterTakeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clearInputAfterTakeCheckbox
            // 
            this.clearInputAfterTakeCheckbox.AutoSize = true;
            this.clearInputAfterTakeCheckbox.Location = new System.Drawing.Point(185, 26);
            this.clearInputAfterTakeCheckbox.Name = "clearInputAfterTakeCheckbox";
            this.clearInputAfterTakeCheckbox.Size = new System.Drawing.Size(18, 17);
            this.clearInputAfterTakeCheckbox.TabIndex = 3;
            this.clearInputAfterTakeCheckbox.UseVisualStyleBackColor = true;
            // 
            // clearOutputAfterTakeCheckbox
            // 
            this.clearOutputAfterTakeCheckbox.AutoSize = true;
            this.clearOutputAfterTakeCheckbox.Location = new System.Drawing.Point(185, 49);
            this.clearOutputAfterTakeCheckbox.Name = "clearOutputAfterTakeCheckbox";
            this.clearOutputAfterTakeCheckbox.Size = new System.Drawing.Size(18, 17);
            this.clearOutputAfterTakeCheckbox.TabIndex = 5;
            this.clearOutputAfterTakeCheckbox.UseVisualStyleBackColor = true;
            // 
            // clearOutputAfterTakeLabel
            // 
            this.clearOutputAfterTakeLabel.AutoSize = true;
            this.clearOutputAfterTakeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.clearOutputAfterTakeLabel.Location = new System.Drawing.Point(3, 46);
            this.clearOutputAfterTakeLabel.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.clearOutputAfterTakeLabel.Name = "clearOutputAfterTakeLabel";
            this.clearOutputAfterTakeLabel.Size = new System.Drawing.Size(149, 23);
            this.clearOutputAfterTakeLabel.TabIndex = 6;
            this.clearOutputAfterTakeLabel.Text = "Clear output after take";
            this.clearOutputAfterTakeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CrosspointStoreEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 551);
            this.DeleteButtonVisible = true;
            this.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.HeaderText = "Edit crosspoint store";
            this.MinimumSize = new System.Drawing.Size(500, 440);
            this.Name = "CrosspointStoreEditorForm";
            this.Text = "Edit crosspoint store";
            this.customElementsPanel.ResumeLayout(false);
            this.customElementsPanel.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.basicDataTable.ResumeLayout(false);
            this.basicDataTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idNumericField)).EndInit();
            this.basicDataGroupBox.ResumeLayout(false);
            this.basicDataGroupBox.PerformLayout();
            this.routerInputGroupBox.ResumeLayout(false);
            this.routerInputGroupBox.PerformLayout();
            this.routerInputTable.ResumeLayout(false);
            this.routerInputTable.PerformLayout();
            this.routerOutputGroupBox.ResumeLayout(false);
            this.routerOutputGroupBox.PerformLayout();
            this.routerOutputTable.ResumeLayout(false);
            this.routerOutputTable.PerformLayout();
            this.modeLabel.ResumeLayout(false);
            this.modeLabel.PerformLayout();
            this.modeTable.ResumeLayout(false);
            this.modeTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox basicDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel basicDataTable;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.NumericUpDown idNumericField;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.GroupBox routerInputGroupBox;
        private System.Windows.Forms.TableLayoutPanel routerInputTable;
        private System.Windows.Forms.Label routerInputRouterLabel;
        private System.Windows.Forms.ComboBox routerInputRouterDropDown;
        private System.Windows.Forms.Label routerInputInputLabel;
        private System.Windows.Forms.ComboBox routerInputInputDropDown;
        private System.Windows.Forms.GroupBox modeLabel;
        private System.Windows.Forms.TableLayoutPanel modeTable;
        private System.Windows.Forms.Label autotakeAfterOutputSetLabel;
        private System.Windows.Forms.CheckBox autotakeAfterOutputSetCheckbox;
        private System.Windows.Forms.GroupBox routerOutputGroupBox;
        private System.Windows.Forms.TableLayoutPanel routerOutputTable;
        private System.Windows.Forms.Label routerOutputOutputLabel;
        private System.Windows.Forms.ComboBox routerOutputOutputDropDown;
        private System.Windows.Forms.Label routerOutputRouterLabel;
        private System.Windows.Forms.ComboBox routerOutputRouterDropDown;
        private System.Windows.Forms.CheckBox clearOutputAfterTakeCheckbox;
        private System.Windows.Forms.CheckBox clearInputAfterTakeCheckbox;
        private System.Windows.Forms.Label clearInputAfterTakeLabel;
        private System.Windows.Forms.Label clearOutputAfterTakeLabel;
    }
}