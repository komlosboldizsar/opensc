using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.Helpers;
using OpenSC.Model;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class McCurdyUmd1EditorForm : UmdEditorFormBase, IModelEditorForm<Umd>
    {

        public virtual IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Umd);
        public virtual IModelEditorForm<Umd> GetInstanceT(Umd modelInstance) => new McCurdyUmd1EditorForm(modelInstance);

        public McCurdyUmd1EditorForm() : base() => InitializeComponent();

        public McCurdyUmd1EditorForm(Umd umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is McCurdyUMD1))
                throw new ArgumentException($"Type of UMD should be {nameof(McCurdyUMD1)}.", nameof(umd));
            initTextsTabLayoutGroupBox();
            initDropDowns();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Umd, McCurdyUMD1>(this, UmdDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
            portDropDown.SelectByValue(mcCurdyUmd1.Port);
            addressNumericInput.Value = mcCurdyUmd1.Address;
            updatingWidthNumericFieldsFromCode = true;
            for (int i = 0; i < mcCurdyUmd1.TextInfo.Length; i++)
            {
                textColumnWidthControls[i].AssignInfo(mcCurdyUmd1.TextInfo[i]);
                textColumnWidthControls[i].Load(mcCurdyUmd1.Texts[i]);
            }
            updatingWidthNumericFieldsFromCode = false;
            recalculateColumnWidthsByUserInput();
        }

        protected override void writeFields()
        {
            base.writeFields();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
            mcCurdyUmd1.Port = portDropDown.SelectedValue as SerialPort;
            mcCurdyUmd1.Address = (int)addressNumericInput.Value;
            for (int i = 0; i < mcCurdyUmd1.TextInfo.Length; i++)
                textColumnWidthControls[i].Write();
        }

        protected override void validateFields()
        {
            base.validateFields();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
            mcCurdyUmd1.ValidateAddress((int)addressNumericInput.Value);
        }

        private void initDropDowns()
        {
            // Ports
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, port => port.Name, true, "(not connected)");
            portDropDown.ReceiveSystemObjectDrop().FilterByType<SerialPort>();
        }

        protected class TextColumnWidthControls
        {

            private UmdEditorFormBase parentForm;
            public readonly NumericUpDown WidthNumericField;
            public readonly Label WidthLabel;

            public TextColumnWidthControls(UmdEditorFormBase parentForm, NumericUpDown widthNumericField, Label widthLabel)
            {
                this.parentForm = parentForm;
                WidthNumericField = widthNumericField;
                WidthLabel = widthLabel;
                WidthNumericField.Tag = this;
                WidthLabel.Tag = this;
            }

            public UmdTextInfo TextInfo { get; private set; }
            public UmdText Text { get; private set; }

            public void AssignInfo(UmdTextInfo textInfo)
            {
                TextInfo = textInfo;
                WidthLabel.Text = TextInfo.Name;
            }

            public void Load(UmdText text)
            {
                Text = text;
                SetUsed(text.Used);
                WidthNumericField.Value = ((McCurdyUmd1Text)Text).ColumnWidth;
            }

            public void Write()
            {
                if (Text == null)
                    return;
                ((McCurdyUmd1Text)Text).ColumnWidth = (int)WidthNumericField.Value;
            }

            public void SetUsed(bool textUsed)
            {
                if (textUsed)
                {
                    WidthLabel.BackColor = TEXT_WIDTH_LABEL_ACTIVE_BG;
                    WidthLabel.ForeColor = TEXT_WIDTH_LABEL_ACTIVE_FG;
                    WidthNumericField.Enabled = true;
                }
                else
                {
                    WidthLabel.BackColor = TEXT_WIDTH_LABEL_INACTIVE_BG;
                    WidthLabel.ForeColor = TEXT_WIDTH_LABEL_INACTIVE_FG;
                    WidthNumericField.Enabled = false;
                }
            }

            private static readonly Color TEXT_WIDTH_LABEL_ACTIVE_BG = Color.DarkBlue;
            private static readonly Color TEXT_WIDTH_LABEL_ACTIVE_FG = Color.White;
            private static readonly Color TEXT_WIDTH_LABEL_INACTIVE_BG = SystemColors.ControlDark;
            private static readonly Color TEXT_WIDTH_LABEL_INACTIVE_FG = SystemColors.Control;

        }

        protected TextColumnWidthControls[] textColumnWidthControls = null;

        private void initTextsTabLayoutGroupBox()
        {
            McCurdyUMD1 umd = (McCurdyUMD1)EditedModel;
            int textInfoLength = umd.TextInfo.Length;
            textColumnWidthControls = new TextColumnWidthControls[textInfoLength];
            string[] EXCLUDE_CLONE_PROPERTIES = new string[] { nameof(Control.Visible) };
            ColumnStyle baseColumnStyle = textColumnWidthNumericFieldsTable.ColumnStyles[1];
            for (int i = 0; i < textInfoLength; i++)
            {
                if (i > 0)
                {
                    textColumnWidthNumericFieldsTable.CloneColumn(1, -2, EXCLUDE_CLONE_PROPERTIES);
                    textColumnWidthLabelsTable.CloneColumn(0, -1, EXCLUDE_CLONE_PROPERTIES);
                }
                TextColumnWidthControls thisTextColumnWidthControls = new(this,
                    (NumericUpDown)textColumnWidthNumericFieldsTable.GetControlFromPosition(i + 1, 0),
                    (Label)textColumnWidthLabelsTable.GetControlFromPosition(i, 0));
                textColumnWidthControls[i] = thisTextColumnWidthControls;
                thisTextColumnWidthControls.WidthNumericField.ValueChanged += TextColumnWidthNumericField_ValueChanged;
            }
            foreach (TextControls textControl in textControls)
            {
                textControl.UsedCheckBox.CheckedChanged += UsedCheckBox_CheckedChanged;
                textControl.AlignmentDropDown.SelectedValueChanged += AlignmentDropDown_SelectedValueChanged;
            }
        }

        private void UsedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox typedSender = (CheckBox)sender;
            UmdText text = ((TextControls)typedSender.Tag).Text;
            if (text == null)
                return;
            int textIndex = text.IndexAtOwner;
            bool textUsed = typedSender.Checked;
            textColumnWidthControls[textIndex].SetUsed(textUsed);
            recalculateColumnWidthsByUserInput();
        }

        private void AlignmentDropDown_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox typedSender = (ComboBox)sender;
            UmdText text = ((TextControls)typedSender.Tag).Text;
            if (text == null)
                return;
            int textIndex = text.IndexAtOwner;
            textColumnWidthControls[textIndex].WidthLabel.TextAlign = (UmdTextAlignment)typedSender.SelectedValue switch
            {
                UmdTextAlignment.Left => ContentAlignment.MiddleLeft,
                UmdTextAlignment.Center => ContentAlignment.MiddleCenter,
                UmdTextAlignment.Right => ContentAlignment.MiddleRight,
                _ => ContentAlignment.MiddleCenter
            };
        }

        private bool updatingWidthNumericFieldsFromCode = false;

        private void TextColumnWidthNumericField_ValueChanged(object sender, EventArgs e)
        {
            if (!updatingWidthNumericFieldsFromCode)
                recalculateColumnWidthsByUserInput(((TextColumnWidthControls)((NumericUpDown)sender).Tag).Text.IndexAtOwner);
        }

        private void recalculateColumnWidthsByUserInput(int? changeSource = null)
        {
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            int textInfoLength = mcCurdyUmd1.TextInfo.Length;
            int[] currentWidthInputs = new int[textInfoLength];
            bool[] usedStates = new bool[textInfoLength];
            for (int i = 0; i < textInfoLength; i++)
            {
                currentWidthInputs[i] = (int)textColumnWidthControls[i].WidthNumericField.Value;
                usedStates[i] = textControls[i].UsedCheckBox.Checked;
            }
            int[] newWidths = mcCurdyUmd1.RealculateColumnWidths(currentWidthInputs, usedStates, changeSource);
            updatingWidthNumericFieldsFromCode = true;
            for (int i = 0; i < textInfoLength; i++)
                textColumnWidthControls[i].WidthNumericField.Value = newWidths[i];
            updatingWidthNumericFieldsFromCode = false;
            resizeTextColumnWidthLabelsTable();
        }

        private void resizeTextColumnWidthLabelsTable()
        {
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            int textInfoLength = mcCurdyUmd1.TextInfo.Length;
            for (int i = 0; i < textInfoLength; i++)
                textColumnWidthLabelsTable.ColumnStyles[i].Width = (float)textColumnWidthControls[i].WidthNumericField.Value * 100 / mcCurdyUmd1.TotalColumnWidth;
        }

    }

}
