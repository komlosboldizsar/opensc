using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.Tsl31
{
    [TypeLabel("TSL 3.1")]
    [TypeCode("tsl31")]
    public class Tsl31 : Umd
    {

        #region Property: Port
        public event PropertyChangedTwoValuesDelegate<Tsl31, SerialPort> PortChanged;

        [PersistAs("port")]
        private SerialPort port;

#pragma warning disable CS0169
        [TempForeignKey(nameof(port))]
        private string _portId;
#pragma warning restore CS0169

        public SerialPort Port
        {
            get => port;
            set => this.setProperty(ref port, value, PortChanged);
        }
        #endregion

        #region Property: Address
        public event PropertyChangedTwoValuesDelegate<Tsl31, int> AddressChanged;

        [PersistAs("address")]
        private int address = 1;

        public int Address
        {
            get => address;
            set => this.setProperty(ref address, value, AddressChanged, validator: ValidateAddress);
        }

        public void ValidateAddress(int address)
        {
            if ((address < 0) || (address > 126))
                throw new ArgumentOutOfRangeException();
        }
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

        public override bool AlignableFullStaticText => true;
        #endregion

        #region Sending data to hardware
        protected override void updateTextsToHardware()
        {
            calculateTextFields();
            updateTotalToHardware();
        }

        protected override void updateTalliesToHardware()
        {
            calculateTallyFields();
            sendData();
        }

        protected override void updateTotalToHardware()
        {
            calculateTextFields();
            calculateTallyFields();
            sendData();
        }

        private string textToHardware;
        protected byte[] textBytesToHardware;

        private const int TEXT_SINGLE_MAX_LENGTH = 16;
        private const int TEXT_DUAL_MAX_LENGTH = 8;

        private string[] getTextsToDisplay()
        {
            bool dualMode = Texts[1].Used;
            int arraySize = dualMode ? 2 : 1;
            string[] textsToDisplay = new string[arraySize];
            for (int i = 0; i < arraySize; i++)
               textsToDisplay[i] = Texts[i].CurrentValue;
            if (UseFullStaticText)
            {
                if (dualMode)
                {
                    string[] fullStaticPieces = FullStaticText.Split('|');
                    if (fullStaticPieces.Length > 1)
                        for (int i = 0; i < 2; i++)
                            textsToDisplay[i] = fullStaticPieces[i];
                }
                else
                {
                    textsToDisplay[0] = FullStaticText;
                }
            }
            if (dualMode)
                for (int i = 0; i < 2; i++)
                    if (textsToDisplay[i].Length > TEXT_DUAL_MAX_LENGTH)
                        textsToDisplay[i] = textsToDisplay[i].Substring(0, TEXT_DUAL_MAX_LENGTH);
            else
                if (textsToDisplay[0].Length > TEXT_SINGLE_MAX_LENGTH)
                    textsToDisplay[0] = textsToDisplay[0].Substring(0, TEXT_SINGLE_MAX_LENGTH);
            return textsToDisplay;
        }

        protected override void calculateDisplayableCompactText()
            => DisplayableCompactText = string.Join(" | ", getTextsToDisplay());

        private void calculateTextFields()
        {
            string[] textsToDisplay = getTextsToDisplay();
            string[] textsToDisplayAligned = new string[textsToDisplay.Length];
            for (int i = 0; i < textsToDisplay.Length; i++)
                textsToDisplayAligned[i] = alignText(textsToDisplay[i], Texts[1].Used ? TEXT_DUAL_MAX_LENGTH : TEXT_SINGLE_MAX_LENGTH, Texts[i].Alignment);
            textBytesToHardware = Encoding.ASCII.GetBytes(string.Join("", textsToDisplayAligned));
            DisplayableRawText = textToHardware;
        }

        private string alignText(string text, int maxLength, UmdTextAlignment alignment)
        {
            return alignment switch
            {
                UmdTextAlignment.Left => text.PadRight(maxLength),
                UmdTextAlignment.Center => text.PadLeft((text.Length + maxLength) / 2).PadRight(maxLength),
                UmdTextAlignment.Right => text.PadLeft(maxLength),
                _ => text.PadRight(maxLength)
            };
        }

        private byte tallyByteToHardware = 0x00;

        private void calculateTallyFields()
        {
            tallyByteToHardware = 0;
            for (int i = 0, t = 1; i < TallyInfo.Length; i++, t *= 2)
                if (Tallies[i].CurrentState)
                    tallyByteToHardware += (byte)t;
        }

        protected virtual byte[] getBytesToSend()
        {
            byte[] bytes = new byte[18];
            bytes[0] = (byte)(Address + 0x80);
            bytes[1] = tallyByteToHardware;
            textBytesToHardware.CopyTo(bytes, 2);
            return bytes;
        }

        private void sendData()
        {
            if (port == null)
                return;
            DateTime packetValidUntil = DateTime.Now + TimeSpan.FromSeconds(5);
            port.SendData(getBytesToSend(), packetValidUntil);
        }
        #endregion

    }

}
