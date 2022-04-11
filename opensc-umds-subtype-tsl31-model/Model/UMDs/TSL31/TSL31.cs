using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.TSL31
{
    [TypeLabel("TSL 3.1")]
    [TypeCode("tsl31")]
    public class TSL31 : UMD
    {

        #region Property: Port
        public event PropertyChangedTwoValuesDelegate<TSL31, SerialPort> PortChanged;

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
        public event PropertyChangedTwoValuesDelegate<TSL31, int> AddressChanged;

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
           new("Text", false, true, true, UmdTextAlignment.Center)
        };

        public override UmdTallyInfo[] TallyInfo => new UmdTallyInfo[]
        {
            new("Red", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Red),
            new("Green", UmdTallyInfo.ColorSettingMode.LocalChangeable, Color.Green)
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

        private void calculateTextFields()
        {
            int substrLength = 16;
            if (DisplayableCompactText.Length < 16)
                substrLength = DisplayableCompactText.Length;
            string textToHardwareTemp = DisplayableCompactText.Substring(0, substrLength);
            int textToHardwareTempLength = textToHardwareTemp.Length;
            textToHardware = (UseFullStaticText ? AlignmentWithFullStaticText : Texts[0].Alignment) switch
            {
                UmdTextAlignment.Left => textToHardwareTemp.PadRight(16),
                UmdTextAlignment.Center => textToHardwareTemp.PadLeft(textToHardwareTempLength / 2 + 8).PadRight(16),
                UmdTextAlignment.Right => textToHardwareTemp.PadLeft(16),
                _ => textToHardwareTemp.PadRight(16)
            };
            textBytesToHardware = Encoding.ASCII.GetBytes(textToHardware);
            DisplayableRawText = textToHardware;
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
