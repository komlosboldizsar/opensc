using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.Helpers;
using OpenSC.Model.UMDs;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{
    public partial class UmdEditorFormBase : ModelEditorFormBase
    {

        public UmdEditorFormBase() : base() => InitializeComponent();

        public UmdEditorFormBase(Umd umd) : base(umd)
        {
            InitializeComponent();
            initStatusMonitor();
            initTextsTab();
            initTalliesTab();
            initFullStaticTextTab();
        }

        protected override void loadData()
        {
            base.loadData();
            Umd umd = (Umd)EditedModel;
            if (umd == null)
                return;
            // Status monitor
            statusMonitorCompactTextTextBox.Text = umd.DisplayableCompactText;
            statusMonitorRawTextTextBox.Text = formatDisplayableRawText(umd.DisplayableRawText);
            umd.DisplayableCompactTextChanged += displayableCompactTextChanged;
            umd.DisplayableRawTextChanged += displayableRawTextChanged;
            for (int i = 0; i < umd.TallyInfo.Length; i++)
            {
                updateStatusMonitorTallyLabel(umd.Tallies[i]);
                umd.Tallies[i].CurrentStateChanged += tallyStateChanged;
            }
            // Tab: Texts
            for (int i = 0; i < umd.TextInfo.Length; i++)
            {
                textControls[i].AssignInfo(umd.TextInfo[i]);
                textControls[i].Load(umd.Texts[i]);
            }
            // Tab: Tallies
            for (int i = 0; i < umd.TallyInfo.Length; i++)
            {
                tallyControls[i].AssignInfo(umd.TallyInfo[i]);
                tallyControls[i].Load(umd.Tallies[i]);
            }
            // Tab: Full static text
            staticTextTextBox.Text = umd.FullStaticText;
            useStaticTextCheckBox.Checked = umd.UseFullStaticText;
            fullStaticAlignmentDropDown.SelectByValue(umd.AlignmentWithFullStaticText);
        }

        protected override void validateFields()
        {
            base.validateFields();
            Umd umd = (Umd)EditedModel;
            if (umd == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Umd umd = (Umd)EditedModel;
            if (umd == null)
                return;
            // Tab: Texts
            for (int i = 0; i < umd.TextInfo.Length; i++)
                textControls[i].Write();
            // Tab: Tallies
            for (int i = 0; i < umd.TallyInfo.Length; i++)
                tallyControls[i].Write();
            // Tab: Full static text
            umd.FullStaticText = staticTextTextBox.Text;
            umd.UseFullStaticText = useStaticTextCheckBox.Checked;
            umd.AlignmentWithFullStaticText = (UmdTextAlignment)fullStaticAlignmentDropDown.SelectedValue;
        }

        #region Status monitor
        protected Label[] statusMonitorTallyLabels = null;

        private void initStatusMonitor()
        {
            Umd umd = (Umd)EditedModel;
            int tallyCount = umd.TallyInfo.Length;
            statusMonitorTalliesPanel.Controls.Remove((tallyCount == 0) ? statusMonitorExampleTallyLabel : statusMonitorNoTalliesLabel);
            statusMonitorTallyLabels = new Label[tallyCount];
            for (int i = 0; i < tallyCount; i++)
            {
                Label thisStatusMonitorTallyLabel = statusMonitorExampleTallyLabel;
                if (i > 0)
                {
                    thisStatusMonitorTallyLabel = statusMonitorExampleTallyLabel.CloneT(new string[] { nameof(Control.Visible) });
                    statusMonitorTalliesPanel.Controls.Add(thisStatusMonitorTallyLabel);
                }
                statusMonitorTallyLabels[i] = thisStatusMonitorTallyLabel;
                thisStatusMonitorTallyLabel.Text = (i + 1).ToString();
                tallyNameToolTip.SetToolTip(thisStatusMonitorTallyLabel, umd.TallyInfo[i].Name);
            }
        }

        private string formatDisplayableRawText(string rawText) => $"[{rawText}]";

        private void displayableCompactTextChanged(Umd item, string oldValue, string newValue)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => displayableCompactTextChanged(item, oldValue, newValue)));
                return;
            }
            statusMonitorCompactTextTextBox.Text = newValue;
        }

        private void displayableRawTextChanged(Umd item, string oldValue, string newValue)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => displayableRawTextChanged(item, oldValue, newValue)));
                return;
            }
            statusMonitorRawTextTextBox.Text = formatDisplayableRawText(newValue);
        }

        private void tallyStateChanged(UmdTally item, bool oldValue, bool newValue)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => tallyStateChanged(item, oldValue, newValue)));
                return;
            }
            updateStatusMonitorTallyLabel(item);
        }

        private void updateStatusMonitorTallyLabel(UmdTally tally)
        {
            Label thisStatusMonitorTallyLabel = statusMonitorTallyLabels[tally.IndexAtOwner];
            if (tally.CurrentState)
            {
                Color tallyColor = tally.Color;
                thisStatusMonitorTallyLabel.BackColor = tallyColor;
                thisStatusMonitorTallyLabel.ForeColor = (tallyColor.GetBrightness() > 0.5f) ? Color.Black : Color.White;
                thisStatusMonitorTallyLabel.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                thisStatusMonitorTallyLabel.BackColor = SystemColors.ControlDark;
                thisStatusMonitorTallyLabel.ForeColor = SystemColors.Control;
                thisStatusMonitorTallyLabel.BorderStyle = BorderStyle.None;
            }
        }
        #endregion

        #region TabPage: Texts
        protected class TextControls
        {

            private UmdEditorFormBase parentForm;
            public readonly Label NameLabel;
            public readonly CheckBox UsedCheckBox;
            public readonly ComboBox DynamicSourceDropDown;
            public readonly CheckBox UseStaticSourceCheckBox;
            public readonly TextBox StaticSourceTextBox;
            public readonly ComboBox AlignmentDropDown;

            public TextControls(UmdEditorFormBase parentForm, Label nameLabel, CheckBox usedCheckBox,ComboBox dynamicSourceDropDown, CheckBox useStaticSourceCheckBox, TextBox staticSourceTextBox, ComboBox alignmentDropDown)
            {
                this.parentForm = parentForm;
                NameLabel = nameLabel;
                UsedCheckBox = usedCheckBox;
                DynamicSourceDropDown = dynamicSourceDropDown;
                UseStaticSourceCheckBox = useStaticSourceCheckBox;
                StaticSourceTextBox = staticSourceTextBox;
                AlignmentDropDown = alignmentDropDown;
                NameLabel.Tag = this;
                UsedCheckBox.Tag = this;
                DynamicSourceDropDown.Tag = this;
                UseStaticSourceCheckBox.Tag = this;
                StaticSourceTextBox.Tag = this;
                AlignmentDropDown.Tag = this;
            }

            public UmdTextInfo TextInfo { get; private set; }
            public UmdText Text { get; private set; }

            public void AssignInfo(UmdTextInfo textInfo)
            {
                TextInfo = textInfo;
                NameLabel.Text = TextInfo.Name;
                if (!TextInfo.Switchable)
                {
                    UsedCheckBox.Checked = true;
                    UsedCheckBox.Enabled = false;
                }
                DynamicSourceDropDown.GetAdapterFromFactoryAsDataSource(dynamicSourceDropDownAdapterFactory);
                DynamicSourceDropDown.ReceiveSystemObjectDrop().FilterByType<DynamicText>();
                AlignmentDropDown.GetAdapterFromFactoryAsDataSource(alignmentDropDownAdapterFactory);
                AlignmentDropDown.SelectByValue(TextInfo.DefaultAlignment);
                if (!TextInfo.Alignable)
                    AlignmentDropDown.Enabled = false;
            }

            public void Load(UmdText text)
            {
                Text = text;
                UsedCheckBox.Checked = text.Used;
                DynamicSourceDropDown.SelectByValue(text.Source);
                UseStaticSourceCheckBox.Checked = text.UseStaticValue;
                StaticSourceTextBox.Text = text.StaticValue;
                AlignmentDropDown.SelectByValue(text.Alignment);
            }

            public void Write()
            {
                if (Text == null)
                    return;
                Text.Used = UsedCheckBox.Checked;
                Text.Source = DynamicSourceDropDown.SelectedValue as DynamicText;
                Text.UseStaticValue = UseStaticSourceCheckBox.Checked;
                Text.StaticValue = StaticSourceTextBox.Text;
                Text.Alignment = (UmdTextAlignment)AlignmentDropDown.SelectedValue;
            }

        };

        protected TextControls[] textControls = null;

        private void initTextsTab()
        {
            Umd umd = (Umd)EditedModel;
            int textCount = umd.TextInfo.Length;
            textsTabPage.Controls.Remove((textCount == 0) ? textsSourceAndAlignmentGroupBox : textsTabPageNoTextLabel);
            textControls = new TextControls[textCount];
            int textsSourceAndAlignmentTableRowsPerText = textsSourceAndAlignmentTable.RowCount;
            TableLayoutHelpers.RowCloner[] textsSourceAndAlignmentTableRowCloners = new TableLayoutHelpers.RowCloner[textsSourceAndAlignmentTableRowsPerText];
            if (textCount > 1)
                for (int r = 0; r < textsSourceAndAlignmentTableRowsPerText; r++)
                    textsSourceAndAlignmentTableRowCloners[r] = new(textsSourceAndAlignmentTable, r);
            for (int i = 0; i < textCount; i++)
            {
                if (i > 0)
                    for (int r = 0; r < textsSourceAndAlignmentTableRowsPerText; r++)
                        textsSourceAndAlignmentTableRowCloners[r].DoCloning(TableLayoutHelpers.RowCloner.DESTINATION_INDEX_LAST, new string[] { nameof(Visible) });
                int rowBase = i * textsSourceAndAlignmentTableRowsPerText;
                TextControls thisTextControls = new(this,
                    (Label)textsSourceAndAlignmentTable.GetControlFromPosition(0, rowBase),
                    (CheckBox)textsSourceAndAlignmentTable.GetControlFromPosition(2, rowBase),
                    (ComboBox)textsSourceAndAlignmentTable.GetControlFromPosition(3, rowBase + 1),
                    (CheckBox)textsSourceAndAlignmentTable.GetControlFromPosition(2, rowBase + 2),
                    (TextBox)textsSourceAndAlignmentTable.GetControlFromPosition(3, rowBase + 2),
                    (ComboBox)textsSourceAndAlignmentTable.GetControlFromPosition(3, rowBase + 3));
                textControls[i] = thisTextControls;
            }
        }

        private static ComboBoxAdapterFactory<DynamicText> dynamicSourceDropDownAdapterFactory = new(DynamicTextDatabase.Instance, null, true, "(not set)");
        private static Dictionary<UmdTextAlignment, string> alignmentDropDownTranslations = new()
        {
            { UmdTextAlignment.Left, "left" },
            { UmdTextAlignment.Center, "center" },
            { UmdTextAlignment.Right, "right" },
        };
        private static EnumComboBoxAdapterFactory<UmdTextAlignment> alignmentDropDownAdapterFactory = new(alignmentDropDownTranslations);
        #endregion

        #region TabPage: Tallies
        protected class TallyControls
        {

            private UmdEditorFormBase parentForm;
            public readonly Label NameLabel;
            public readonly ComboBox SourceDropDown;
            public readonly Button ColorButton;

            public TallyControls(UmdEditorFormBase parentForm, Label nameLabel, ComboBox sourceDropDown, Button colorButton)
            {
                this.parentForm = parentForm;
                NameLabel = nameLabel;
                SourceDropDown = sourceDropDown;
                ColorButton = colorButton;
                NameLabel.Tag = this;
                SourceDropDown.Tag = this;
                ColorButton.Tag = this;
            }

            public UmdTallyInfo TallyInfo { get; private set; }
            public UmdTally Tally { get; private set; }

            public void AssignInfo(UmdTallyInfo tallyInfo)
            {
                TallyInfo = tallyInfo;
                NameLabel.Text = TallyInfo.Name;
                SourceDropDown.CreateAdapterAsDataSource(BooleanRegister.Instance, BooleanRegister.Instance.ToStringMethod, true, "(not set)");
                SourceDropDown.ReceiveSystemObjectDrop().FilterByType<IBoolean>();
                setTallyColorButtonColor(TallyInfo.DefaultColor);
                switch (TallyInfo.ColorMode)
                {
                    case UmdTallyInfo.ColorSettingMode.Fix:
                        ColorButton.Text = "";
                        parentForm.tallyColorButtonToolTip.SetToolTip(ColorButton, "The color of this tally indicator is fixed, it cannot be set.");
                        break;
                    case UmdTallyInfo.ColorSettingMode.LocalChangeable:
                        ColorButton.Click += tallyColorButtonClick;
                        parentForm.tallyColorButtonToolTip.SetToolTip(ColorButton, "The color of this tally indicator can be set, but only locally on the hardware. Changing here won't update this setting on the hardware.");
                        break;
                    case UmdTallyInfo.ColorSettingMode.RemoteChangeable:
                        ColorButton.Click += tallyColorButtonClick;
                        parentForm.tallyColorButtonToolTip.SetToolTip(ColorButton, "The color of this tally indicator displayed by the hardware can be set remotely. If you change it here, it will also update this setting on the hardware.");
                        break;
                }
            }

            public void Load(UmdTally tally)
            {
                Tally = tally;
                SourceDropDown.SelectByValue(Tally.Source);
                setTallyColorButtonColor(Tally.Color);
            }

            public void Write()
            {
                if (Tally == null)
                    return;
                Tally.Source = SourceDropDown.SelectedValue as IBoolean;
                Tally.Color = ColorButton.BackColor;
            }


            private void tallyColorButtonClick(object sender, EventArgs e)
            {
                ColorDialog colorDialog = new ColorDialog();
                colorDialog.AnyColor = true;
                colorDialog.FullOpen = true;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                    setTallyColorButtonColor(colorDialog.Color);
            }

            private void setTallyColorButtonColor(Color color)
            {
                ColorButton.BackColor = color;
                ColorButton.ForeColor = (color.GetBrightness() > 0.5f) ? Color.Black : Color.White;
            }

        };

        protected TallyControls[] tallyControls = null;

        private void initTalliesTab()
        {
            Umd umd = (Umd)EditedModel;
            int tallyCount = umd.TallyInfo.Length;
            talliesTabPage.Controls.Remove((tallyCount == 0) ? talliesSourceAndColorGroupBox : talliesTabPageNoTallyLabel);
            tallyControls = new TallyControls[tallyCount];
            int talliesSourceAndColorTableRowsPerText = talliesSourceAndColorTable.RowCount;
            TableLayoutHelpers.RowCloner[] talliesSourceAndColorTableRowCloners = new TableLayoutHelpers.RowCloner[talliesSourceAndColorTableRowsPerText];
            if (tallyCount > 1)
                for (int r = 0; r < talliesSourceAndColorTableRowsPerText; r++)
                    talliesSourceAndColorTableRowCloners[r] = new(talliesSourceAndColorTable, r);
            for (int i = 0; i < tallyCount; i++)
            {
                UmdTallyInfo thisTallyInfo = umd.TallyInfo[i];
                if (i > 0)
                    for (int r = 0; r < talliesSourceAndColorTableRowsPerText; r++)
                        talliesSourceAndColorTableRowCloners[r].DoCloning(TableLayoutHelpers.RowCloner.DESTINATION_INDEX_LAST, new string[] { nameof(Visible) });
                int rowBase = i * talliesSourceAndColorTableRowsPerText;
                TallyControls thisTallyControls = new(this,
                    (Label)talliesSourceAndColorTable.GetControlFromPosition(0, rowBase),
                    (ComboBox)talliesSourceAndColorTable.GetControlFromPosition(1, rowBase),
                    (Button)talliesSourceAndColorTable.GetControlFromPosition(2, rowBase));
                tallyControls[i] = thisTallyControls;
            }
            talliesTabInitialized();
        }

        protected virtual void talliesTabInitialized() { }
        #endregion

        #region TabPage: Full static text
        private void initFullStaticTextTab()
        {
            fullStaticAlignmentDropDown.GetAdapterFromFactoryAsDataSource(alignmentDropDownAdapterFactory);
            fullStaticAlignmentDropDown.Enabled = ((Umd)EditedModel).AlignableFullStaticText;
        }
        #endregion

    }

}
