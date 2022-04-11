using BMD.Switcher;
using OpenSC.Model.General;
using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;
using OpenSC.Model.Persistence;
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
    public class BmdAtem : Umd
    {

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            restoreInput();
        }

        #region Property: Input
        public event PropertyChangedTwoValuesDelegate<BmdAtem, MixerInput> InputChanged;

        private MixerInput input;

        public MixerInput Input
        {
            get => input;
            set => this.setProperty(ref input, value, InputChanged, null, (ov, nv) => updateInputsSource());
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
            updateTextsToHardware();
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

        #region Sending data to hardware
        protected override void updateTextsToHardware() => updateTotalToHardware();

        protected override void updateTalliesToHardware() { }

        protected override void updateTotalToHardware()
        {
            calculateTextFields();
            sendData();
        }

        private const int LENGTH_SHORT = 4;
        private const int LENGTH_LONG = 16;

        protected override void calculateDisplayableCompactText()
        {
            base.calculateDisplayableCompactText();
            string shortText = "-";
            if (Texts[0].Used)
            {
                shortText = UseFullStaticText ? FullStaticText : Texts[0].CurrentValue;
                if (shortText.Length > LENGTH_SHORT)
                    shortText.Substring(0, LENGTH_SHORT);
            }
            string longText = "-";
            if (Texts[0].Used)
            {
                longText = UseFullStaticText ? FullStaticText : Texts[1].CurrentValue;
                if (longText.Length > LENGTH_LONG)
                    longText.Substring(0, LENGTH_LONG);
            }
            DisplayableCompactText = $"{shortText} | {longText}";
        }

        private string shortTextToHardware = "";
        private string longTextToHardware = "";

        private void calculateTextFields()
        {
            shortTextToHardware = UseFullStaticText ? FullStaticText : Texts[0].CurrentValue;
            if (shortTextToHardware.Length > LENGTH_SHORT)
                shortTextToHardware.Substring(0, LENGTH_SHORT);
            longTextToHardware = UseFullStaticText ? FullStaticText : Texts[1].CurrentValue;
            if (longTextToHardware.Length > LENGTH_LONG)
                longTextToHardware.Substring(0, LENGTH_LONG);
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
        }

        private void sendData()
        {
            if (Texts[0].Used)
                inputsSource?.UpdateShortName(shortTextToHardware);
            if (Texts[1].Used)
                inputsSource?.UpdateLongName(longTextToHardware);
        }
        #endregion

    }

}
