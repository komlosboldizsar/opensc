using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Tsl50
{
    [TypeLabel("TSL 5.0")]
    [TypeCode("tsl50")]
    public partial class Tsl50Display : Umd
    {

        #region Property: Screen
        [AutoProperty]
        [PersistAs("screen")]
        private Tsl50Screen screen;
        #endregion

        #region Property: Index
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateIndex))]
        [PersistAs("index")]
        private int index = 1;

        public void ValidateIndex(int index)
        {
            if ((index < 0) || (index > 65534))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Info
        public override UmdTextInfo[] TextInfo => new UmdTextInfo[]
        {
           new("Text", false, true, false, UmdTextAlignment.Left)
        };

        public override UmdTallyInfo[] TallyInfo => new UmdTallyInfo[]
        {
            new("Left tally 1 (red)", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Red),
            new("Left tally 2 (green)", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Green),
            new("Text tally 1 (red)", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Red),
            new("Text tally 2 (green)", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Green),
            new("Right tally 1 (red)", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Red),
            new("Right tally 2 (green)", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Green)
        };
        #endregion

        #region Sending data to hardware
        protected override void calculateTextFields()
        {
            string text = UseFullStaticText ? FullStaticText : Texts[0].CurrentValue;
            textBytesToHardware = Encoding.ASCII.GetBytes(text);
            DisplayableCompactText = text;
            DisplayableRawText = text;
        }

        protected byte[] textBytesToHardware = Array.Empty<byte>();

        protected override void calculateTallyFields()
        {
            tallyByteToHardware = 0;
            for (int i = 0, t = 32; i < TallyInfo.Length; i++, t /= 2)
                if (Tallies[i].CurrentState)
                    tallyByteToHardware += (byte)t;
        }

        private byte tallyByteToHardware;

        protected override void sendTextsToHardware() => sendData(true);
        protected override void sendTalliesToHardware() => sendData(false);
        protected override void sendEverythingToHardware() => sendData(true);

        protected virtual byte[] getAllBytesToSend(bool sendText = true)
        {
            int byteCount = 4; // INDEX + CONTROL
            if (sendText)
                byteCount += 2 + textBytesToHardware.Length; // LENGTH + TEXT
            byte[] bytes = new byte[byteCount]; // LITTLE ENDIAN!
            bytes[0] = (byte)(index & 0xFF); // DISPLAY INDEX LSB
            bytes[1] = (byte)((index >> 8) & 0xFF); // DISPLAY INDEX MSB
            bytes[2] = tallyByteToHardware; // CONTROL LSB (tallies, brightness)
            bytes[3] = 0; // CONTROL MSB (control bit, reserved)
            if (sendText)
            {
                bytes[4] = (byte)(textBytesToHardware.Length & 0xFF); // LENGTH MSB
                bytes[5] = (byte)((textBytesToHardware.Length >> 8) & 0xFF); // LENGTH LSB
                textBytesToHardware.CopyTo(bytes, 6);
            }
            return bytes;
        }

        private void sendData(bool sendText) => screen?.SendDisplayData(getAllBytesToSend(sendText));
        #endregion

    }

}
