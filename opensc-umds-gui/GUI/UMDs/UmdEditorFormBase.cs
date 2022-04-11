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

        public UmdEditorFormBase(UMD umd) : base(umd)
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
            UMD umd = (UMD)EditedModel;
            if (umd == null)
                return;
            // Status monitor
            statusMonitorCompactTextTextBox.Text = umd.DisplayableRawText;
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
                UmdText thisText = umd.Texts[i];
                TextControls thisTextControls = textControls[i];
                thisTextControls.UseStaticSourceCheckBox.Checked = thisText.Used;
                thisTextControls.DynamicSourceDropDown.SelectByValue(thisText.Source);
                thisTextControls.UseStaticSourceCheckBox.Checked = thisText.UseStaticValue;
                thisTextControls.StaticSourceTextBox.Text = thisText.StaticValue;
                thisTextControls.AlignmentDropDown.SelectByValue(thisText.Alignment);
            }
            // Tab: Tallies
            for (int i = 0; i < umd.TallyInfo.Length; i++)
            {
                UmdTally thisTally = umd.Tallies[i];
                TallyControls thisTallyControls = tallyControls[i];
                thisTallyControls.SourceDropDown.SelectByValue(thisTally.Source);
                setTallyColorButtonColor(thisTallyControls.ColorButton, thisTally.Color);
            }
            // Tab: Full static text
            staticTextTextBox.Text = umd.FullStaticText;
            useStaticTextCheckBox.Checked = umd.UseFullStaticText;
            fullStaticAlignmentDropDown.SelectByValue(umd.AlignmentWithFullStaticText);
        }

        protected override void validateFields()
        {
            base.validateFields();
            UMD umd = (UMD)EditedModel;
            if (umd == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            UMD umd = (UMD)EditedModel;
            if (umd == null)
                return;
            // Tab: Texts
            for (int i = 0; i < umd.TextInfo.Length; i++)
            {
                UmdText thisText = umd.Texts[i];
                TextControls thisTextControls = textControls[i];
                thisText.Used = thisTextControls.UseStaticSourceCheckBox.Checked;
                thisText.Source = thisTextControls.DynamicSourceDropDown.SelectedValue as DynamicText;
                thisText.UseStaticValue = thisTextControls.UseStaticSourceCheckBox.Checked;
                thisText.StaticValue = thisTextControls.StaticSourceTextBox.Text;
                thisText.Alignment = (UmdTextAlignment)thisTextControls.AlignmentDropDown.SelectedValue;
            }
            // Tab: Tallies
            for (int i = 0; i < umd.TallyInfo.Length; i++)
            {
                UmdTally thisTally = umd.Tallies[i];
                TallyControls thisTallyControls = tallyControls[i];
                thisTally.Source = thisTallyControls.SourceDropDown.SelectedValue as IBoolean;
                thisTally.Color = thisTallyControls.ColorButton.BackColor;
            }
            // Tab: Full static text
            umd.FullStaticText = staticTextTextBox.Text;
            umd.UseFullStaticText = useStaticTextCheckBox.Checked;
            umd.AlignmentWithFullStaticText = (UmdTextAlignment)fullStaticAlignmentDropDown.SelectedValue;
        }

        #region Status monitor
        private Label[] statusMonitorTallyLabels = null;

        private void initStatusMonitor()
        {
            UMD umd = (UMD)EditedModel;
            int tallyCount = umd.TallyInfo.Length;
            statusMonitorTalliesPanel.Controls.Remove((tallyCount == 0) ? statusMonitorExampleTallyLabel : statusMonitorNoTalliesLabel);
            statusMonitorTallyLabels = new Label[tallyCount];
            for (int i = 0; i < tallyCount; i++)
            {
                Label thisStatusMonitorTallyLabel = statusMonitorExampleTallyLabel;
                if (i > 0)
                {
                    thisStatusMonitorTallyLabel = statusMonitorExampleTallyLabel.CloneT();
                    statusMonitorTalliesPanel.Controls.Add(thisStatusMonitorTallyLabel);
                }
                statusMonitorTallyLabels[i] = thisStatusMonitorTallyLabel;
                thisStatusMonitorTallyLabel.Text = (i + 1).ToString();
                tallyNameToolTip.SetToolTip(thisStatusMonitorTallyLabel, umd.TallyInfo[i].Name);
            }
        }

        private string formatDisplayableRawText(string rawText) => $"[{rawText}]";

        private void displayableCompactTextChanged(UMD item, string oldValue, string newValue)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => displayableCompactTextChanged(item, oldValue, newValue)));
                return;
            }
            statusMonitorCompactTextTextBox.Text = newValue;
        }

        private void displayableRawTextChanged(UMD item, string oldValue, string newValue)
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
        private class TextControls
        {
            public Label NameLabel;
            public CheckBox UsedCheckBox;
            public ComboBox DynamicSourceDropDown;
            public CheckBox UseStaticSourceCheckBox;
            public TextBox StaticSourceTextBox;
            public ComboBox AlignmentDropDown;
        };

        private TextControls[] textControls = null;

        private void initTextsTab()
        {
            UMD umd = (UMD)EditedModel;
            int textCount = umd.TextInfo.Length;
            textsTabPage.Controls.Remove((textCount == 0) ? textsSourceAndAlignmentGroupBox : textsTabPageNoTextLabel);
            textControls = new TextControls[textCount];
            for (int i = 0; i < textCount; i++)
            {
                if (i > 0)
                    for (int r = 0; r < 4; r++)
                        textsSourceAndAlignmentTable.CloneRow(r, TableLayoutHelpers.ROW_INDEX_LAST, new string[] { nameof(Visible) });
                int rowBase = i * 4;
                textControls[i] = new()
                {
                    NameLabel = (Label)textsSourceAndAlignmentTable.GetControlFromPosition(0, rowBase),
                    UsedCheckBox = (CheckBox)textsSourceAndAlignmentTable.GetControlFromPosition(2, rowBase),
                    DynamicSourceDropDown = (ComboBox)textsSourceAndAlignmentTable.GetControlFromPosition(3, rowBase + 1),
                    UseStaticSourceCheckBox = (CheckBox)textsSourceAndAlignmentTable.GetControlFromPosition(2, rowBase + 2),
                    StaticSourceTextBox = (TextBox)textsSourceAndAlignmentTable.GetControlFromPosition(3, rowBase + 2),
                    AlignmentDropDown = (ComboBox)textsSourceAndAlignmentTable.GetControlFromPosition(3, rowBase + 3)
                };
            }
            for (int i = 0; i < textCount; i++)
            {
                UmdTextInfo thisTextInfo = umd.TextInfo[i];
                TextControls thisTextControls = textControls[i];
                thisTextControls.NameLabel.Text = thisTextInfo.Name;
                if (!thisTextInfo.Switchable)
                {
                    thisTextControls.UsedCheckBox.Checked = true;
                    thisTextControls.UsedCheckBox.Enabled = false;
                }
                thisTextControls.DynamicSourceDropDown.GetAdapterFromFactoryAsDataSource(dynamicSourceDropDownAdapterFactory);
                thisTextControls.DynamicSourceDropDown.ReceiveSystemObjectDrop().FilterByType<DynamicText>();
                thisTextControls.AlignmentDropDown.GetAdapterFromFactoryAsDataSource(alignmentDropDownAdapterFactory);
                thisTextControls.AlignmentDropDown.SelectByValue(thisTextInfo.DefaultAlignment);
                if (!thisTextInfo.Alignable)
                    thisTextControls.AlignmentDropDown.Enabled = false;
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
        private class TallyControls
        {
            public Label NameLabel;
            public ComboBox SourceDropDown;
            public Button ColorButton;
        };

        private TallyControls[] tallyControls = null;

        private void initTalliesTab()
        {
            UMD umd = (UMD)EditedModel;
            int tallyCount = umd.TallyInfo.Length;
            talliesTabPage.Controls.Remove((tallyCount == 0) ? talliesSourceAndColorGroupBox : talliesTabPageNoTallyLabel);
            tallyControls = new TallyControls[tallyCount];
            for (int i = 0; i < tallyCount; i++)
            {
                if (i > 0)
                    talliesSourceAndColorTable.CloneRow(0, TableLayoutHelpers.ROW_INDEX_LAST, new string[] { nameof(Visible) });
                tallyControls[i] = new()
                {
                    NameLabel = (Label)talliesSourceAndColorTable.GetControlFromPosition(0, i),
                    SourceDropDown = (ComboBox)talliesSourceAndColorTable.GetControlFromPosition(1, i),
                    ColorButton = (Button)talliesSourceAndColorTable.GetControlFromPosition(2, i)
                };
            }
            for (int i = 0; i < tallyCount; i++)
            {
                UmdTallyInfo thisTallyInfo = umd.TallyInfo[i];
                TallyControls thisTallyControls = tallyControls[i];
                thisTallyControls.NameLabel.Text = thisTallyInfo.Name;
                thisTallyControls.SourceDropDown.CreateAdapterAsDataSource(BooleanRegister.Instance, BooleanRegister.Instance.ToStringMethod, true, "(not set)");
                thisTallyControls.SourceDropDown.ReceiveSystemObjectDrop().FilterByType<IBoolean>();
                Button thisTallyColorButton = thisTallyControls.ColorButton; 
                setTallyColorButtonColor(thisTallyColorButton, thisTallyInfo.DefaultColor);
                switch (thisTallyInfo.ColorMode)
                {
                    case UmdTallyInfo.ColorSettingMode.Fix:
                        thisTallyColorButton.Text = "";
                        tallyColorButtonToolTip.SetToolTip(thisTallyColorButton, "The color of this tally indicator is fixed, it cannot be set.");
                        break;
                    case UmdTallyInfo.ColorSettingMode.LocalChangeable:
                        thisTallyColorButton.Click += tallyColorButtonClick;
                        tallyColorButtonToolTip.SetToolTip(thisTallyColorButton, "The color of this tally indicator can be set, but only locally on the hardware. Changing here won't update this setting on the hardware.");
                        break;
                    case UmdTallyInfo.ColorSettingMode.RemoteChangeable:
                        thisTallyColorButton.Click += tallyColorButtonClick;
                        tallyColorButtonToolTip.SetToolTip(thisTallyColorButton, "The color of this tally indicator displayed by the hardware can be set remotely. If you change it here, it will also update this setting on the hardware.");
                        break;
                }
            }
        }

        private void tallyColorButtonClick(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AnyColor = true;
            colorDialog.FullOpen = true;
            if (colorDialog.ShowDialog() == DialogResult.OK)
                setTallyColorButtonColor((Button)sender, colorDialog.Color);
        }

        private void setTallyColorButtonColor(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = (color.GetBrightness() > 0.5f) ? Color.White : Color.Black;
        }
        #endregion

        #region TabPage: Full static text
        private void initFullStaticTextTab()
        {
            fullStaticAlignmentDropDown.GetAdapterFromFactoryAsDataSource(alignmentDropDownAdapterFactory);
            fullStaticAlignmentDropDown.Enabled = ((UMD)EditedModel).AlignableFullStaticText;
        }
        #endregion

    }

}
