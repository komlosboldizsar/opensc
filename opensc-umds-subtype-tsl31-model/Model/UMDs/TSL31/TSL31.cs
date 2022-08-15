using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Tsl31
{
    [TypeLabel("TSL 3.1")]
    [TypeCode("tsl31")]
    public partial class Tsl31 : Umd
    {

        #region Property: Port
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(UpdateEverything))]
        [PersistAs("port")]
        private SerialPort port;
        #endregion

        #region Property: Address
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(UpdateEverything))]
        [AutoProperty.Validator(nameof(ValidateAddress))]
        [PersistAs("address")]
        private int address = 1;

        public void ValidateAddress(int address)
        {
            if ((address < 0) || (address > 126))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Properties: Tally1Overrides2, Tally3Overrides4
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(UpdateTallies))]
        [PersistAs("tally_12_override_mode")]
        private TallyOverrideMode tally12OverrideMode = TallyOverrideMode.NoOverride;

        [AutoProperty]
        [AutoProperty.AfterChange(nameof(UpdateTallies))]
        [PersistAs("tally_34_override_mode")]
        private TallyOverrideMode tally34OverrideMode = TallyOverrideMode.NoOverride;
        #endregion

        #region Info
        public override UmdTextInfo[] TextInfo => new UmdTextInfo[]
        {
           new("Main/left", false, true, true, UmdTextAlignment.Center),
           new("Right", true, false, true, UmdTextAlignment.Center)
        };

        public override UmdTallyInfo[] TallyInfo => new UmdTallyInfo[]
        {
            new("Main/left red", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Red),
            new("Main/left green", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Green),
            new("Right red", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Red),
            new("Right green", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Green)
        };

        public override bool AlignableFullStaticText => false;
        #endregion

        #region Sending data to hardware
        private const int TEXT_SINGLE_MAX_LENGTH = 16;
        private const int TEXT_DUAL_MAX_LENGTH = 8;

        protected override void calculateTextFields()
        {
            if (UseFullStaticText)
            {
                string textToDisplay = FullStaticText;
                if (textToDisplay.Length > TEXT_SINGLE_MAX_LENGTH)
                    textToDisplay = textToDisplay.Substring(0, TEXT_SINGLE_MAX_LENGTH);
                DisplayableCompactText = textToDisplay;
                textBytesToHardware = Encoding.ASCII.GetBytes(textToDisplay);
                DisplayableRawText = textToDisplay;
            }
            else
            {
                int textMaxLength = Texts[1].Used ? TEXT_DUAL_MAX_LENGTH : TEXT_SINGLE_MAX_LENGTH;
                string[] textsToDisplay = getDynamicTextSources();
                for (int i = 0; i < textsToDisplay.Length; i++)
                    if (textsToDisplay[i].Length > textMaxLength)
                        textsToDisplay[i] = textsToDisplay[i].Substring(0, textMaxLength);
                DisplayableCompactText = string.Join(" | ", textsToDisplay);
                string[] textsToDisplayAligned = new string[textsToDisplay.Length];
                for (int i = 0; i < textsToDisplay.Length; i++)
                    textsToDisplayAligned[i] = alignText(textsToDisplay[i], textMaxLength, Texts[i].Alignment);
                string textToHardware = string.Join("", textsToDisplayAligned);
                textBytesToHardware = Encoding.ASCII.GetBytes(textToHardware);
                DisplayableRawText = textToHardware;
            }
        }

        protected byte[] textBytesToHardware;

        private string[] getDynamicTextSources()
        {
            bool dualMode = Texts[1].Used;
            int arraySize = dualMode ? 2 : 1;
            string[] textsToDisplay = new string[arraySize];
            for (int i = 0; i < arraySize; i++)
               textsToDisplay[i] = Texts[i].CurrentValue;
            if (dualMode)
                for (int i = 0; i < 2; i++)
                    if (textsToDisplay[i].Length > TEXT_DUAL_MAX_LENGTH)
                        textsToDisplay[i] = textsToDisplay[i].Substring(0, TEXT_DUAL_MAX_LENGTH);
            else
                if (textsToDisplay[0].Length > TEXT_SINGLE_MAX_LENGTH)
                    textsToDisplay[0] = textsToDisplay[0].Substring(0, TEXT_SINGLE_MAX_LENGTH);
            return textsToDisplay;
        }

        private string alignText(string text, int maxLength, UmdTextAlignment alignment)
        {
            if (text == null)
                return "".PadRight(maxLength);
            return alignment switch
            {
                UmdTextAlignment.Left => text.PadRight(maxLength),
                UmdTextAlignment.Center => text.PadLeft((text.Length + maxLength) / 2).PadRight(maxLength),
                UmdTextAlignment.Right => text.PadLeft(maxLength),
                _ => text.PadRight(maxLength)
            };
        }

        private byte tallyByteToHardware = 0x00;

        protected override void calculateTallyFields()
        {
            tallyByteToHardware = 0;
            for (int i = 0, t = 1; i < TallyInfo.Length; i++, t *= 2)
                if (Tallies[i].CurrentState)
                    tallyByteToHardware += (byte)t;
            if (Tallies[0].CurrentState && Tallies[1].CurrentState)
                tallyByteToHardware &= tally12OverrideMode switch
                {
                    TallyOverrideMode.AOverridesB => (0xFF ^ 0x02),
                    TallyOverrideMode.BOverridesA => (0xFF ^ 0x01),
                    _ => 0xFF
                };
            if (Tallies[2].CurrentState && Tallies[3].CurrentState)
                tallyByteToHardware &= tally34OverrideMode switch
                {
                    TallyOverrideMode.AOverridesB => (0xFF ^ 0x08),
                    TallyOverrideMode.BOverridesA => (0xFF ^ 0x04),
                    _ => 0xFF
                };
        }

        protected override void sendTextsToHardware() => sendData();
        protected override void sendTalliesToHardware() => sendData();
        protected override void sendEverythingToHardware() => sendData();

        private void sendData()
        {
            if (port == null)
                return;
            DateTime packetValidUntil = DateTime.Now + TimeSpan.FromSeconds(5);
            port.SendData(getBytesToSend(), packetValidUntil);
        }

        protected virtual byte[] getBytesToSend()
        {
            byte[] bytes = new byte[18];
            bytes[0] = (byte)(Address + 0x80);
            bytes[1] = tallyByteToHardware;
            int bytesToCopy = textBytesToHardware.Length;
            for (int i = 2; i < 18; i++)
                bytes[i] = (byte)' ';
            if (bytesToCopy > 16)
                bytesToCopy = 16;
            for (int i = 0; i < bytesToCopy; i++)
                bytes[i + 2] = textBytesToHardware[i];
            return bytes;
        }
        #endregion

    }

}
