using BMD.Switcher;
using OpenSC.Model.General;
using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.BmdAtem
{

    [TypeLabel("BMD ATEM")]
    [TypeCode("bmdatem")]
    public partial class BmdAtem : Umd
    {

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            restoreInput();
        }

        #region Property: Mixer
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_mixer_beforeChange))]
        [AutoProperty.AfterChange(nameof(_mixer_afterChange))]
        private Mixer mixer;

        private void _mixer_beforeChange(Mixer oldValue, Mixer newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                oldValue.StateChanged -= Mixer_StateChanged;
        }

        private void _mixer_afterChange(Mixer oldValue, Mixer newValue)
        {
            if (newValue != null)
                newValue.StateChanged += Mixer_StateChanged;
        }

        private void Mixer_StateChanged(Mixer item, MixerState oldValue, MixerState newValue) => updateInputsSource();
        #endregion

        #region Property: Input
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_input_afterChange))]
        private MixerInput input;

        private void _input_afterChange(MixerInput oldValue, MixerInput newValue)
        {
            Mixer = newValue?.Mixer;
            updateInputsSource();
        }

        [PersistAs("input")]
        private string _inputId
        {
            get
            {
                if ((input == null) || (input.Mixer == null))
                    return null;
                return $"mixer.{input.Mixer.ID}.input.{input.Index}";
            }
            set => __inputId = value;
        }

        private string __inputId;

        private void restoreInput()
        {
            // Will be easier later when MixerInput implements ISystemObject
            if (__inputId == null)
            {
                Input = null;
                return;
            }
            string[] inputIdParts = __inputId.Split('.');
            if (inputIdParts.Length != 4)
            {
                Input = null;
                return;
            }
            if (!int.TryParse(inputIdParts[1], out int mixerId))
            {
                Input = null;
                return;
            }
            if (!int.TryParse(inputIdParts[3], out int inputIndex))
            {
                Input = null;
                return;
            }
            Mixer mixer = MixerDatabase.Instance.GetTById(mixerId);
            Input = mixer.Inputs.FirstOrDefault(i => i.Index == inputIndex);
        }
        #endregion

        #region Inputs source
        private Source inputsSource;

        private void updateInputsSource()
        {
            if (input != null)
            {
                BmdMixer bmdMixer = input.Mixer as BmdMixer;
                inputsSource = bmdMixer.ApiSwitcher?.GetSource(input.Index);
            }
            else
            {
                inputsSource = null;
            }
            UpdateTexts();
        }
        #endregion

        #region Info
        public override UmdTextInfo[] TextInfo => new UmdTextInfo[]
        {
           new("Short", true, true, false, UmdTextAlignment.Left),
           new("Long", true, true, false, UmdTextAlignment.Center),
        };

        public override UmdTallyInfo[] TallyInfo => Array.Empty<UmdTallyInfo>();

        public override bool AlignableFullStaticText => false;
        #endregion

        #region Calculating and sending data to hardware
        private const int LENGTH_SHORT = 4;
        private const int LENGTH_LONG = 16;

        protected override void calculateTextFields()
        {
            // To hardware
            shortTextToHardware = Texts[0].CurrentValue;
            longTextToHardware = Texts[1].CurrentValue;
            if (UseFullStaticText)
            {
                string[] fullStaticPieces = FullStaticText.Split('|');
                if (fullStaticPieces.Length > 1)
                {
                    shortTextToHardware = fullStaticPieces[0];
                    longTextToHardware = fullStaticPieces[1];
                }
                else
                {
                    shortTextToHardware = FullStaticText;
                    longTextToHardware = FullStaticText;
                }
            }
            if (shortTextToHardware.Length > LENGTH_SHORT)
                shortTextToHardware = shortTextToHardware.Substring(0, LENGTH_SHORT);
            if (longTextToHardware.Length > LENGTH_LONG)
                longTextToHardware = longTextToHardware.Substring(0, LENGTH_LONG);
            // Raw
            string _displayableRawText = "";
            if (Texts[0].Used)
                _displayableRawText += $"Short: [{shortTextToHardware}]";
            if (Texts[1].Used)
            {
                if (_displayableRawText.Length > 0)
                    _displayableRawText += " ";
                _displayableRawText += $"Long: [{longTextToHardware}]";
            }
            DisplayableRawText = _displayableRawText;
            // Compact
            string shortTextCompact = Texts[0].Used ? shortTextToHardware : "-";
            string longTextCompact = Texts[1].Used ? longTextToHardware : "-";
            DisplayableCompactText = $"{shortTextCompact} | {longTextCompact}";
        }

        private string shortTextToHardware = "";
        private string longTextToHardware = "";

        protected override void calculateTallyFields() { }

        protected override void sendTextsToHardware() => sendData();
        protected override void sendTalliesToHardware() => sendData();
        protected override void sendEverythingToHardware() => sendData();

        private void sendData()
        {
            if ((!UseFullStaticText && Texts[0].Used) || (UseFullStaticText && shortTextToHardware.Length > 0))
                inputsSource?.UpdateShortName(shortTextToHardware);
            if ((!UseFullStaticText && Texts[1].Used) || (UseFullStaticText && longTextToHardware.Length > 0))
                inputsSource?.UpdateLongName(longTextToHardware);
        }
        #endregion

    }

}
