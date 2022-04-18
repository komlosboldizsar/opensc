using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.McCurdy
{
    [TypeLabel("McCurdy UMD-1")]
    [TypeCode("mccurdy")]
    public class McCurdyUMD1 : Umd
    {

        #region Property: Port
        public event PropertyChangedTwoValuesDelegate<McCurdyUMD1, SerialPort> PortChanged;

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
        public event PropertyChangedTwoValuesDelegate<McCurdyUMD1, int> AddressChanged;

        [PersistAs("address")]
        private int address = 1;

        public int Address
        {
            get => address;
            set => this.setProperty(ref address, value, AddressChanged, validator: ValidateAddress);
        }

        public void ValidateAddress(int address)
        {
            if ((address <= 0) || (address > 255))
                throw new ArgumentOutOfRangeException();
        }
        #endregion

        #region Property: ColumnWidths
        [PersistAs("column_widths")]
        private int[] columnWidths = new int[] { 40, 40 };

        public int[] ColumnWidths
        {
            get { return columnWidths; }
            set
            {
                columnWidths = value;
                //updateCurrentText();
            }
        }
        #endregion

        #region Info
        public override UmdTextInfo[] TextInfo => new UmdTextInfo[]
        {
           new("Text 1", true, true, true, UmdTextAlignment.Center),
           new("Text 2", true, false, true, UmdTextAlignment.Center),
           new("Text 3", true, false, true, UmdTextAlignment.Center),
        };

        public override UmdTallyInfo[] TallyInfo => new UmdTallyInfo[] { };

        public override bool AlignableFullStaticText => true;

        public virtual int TotalWidth => 160;
        #endregion

        #region Calculating and sending data to hardware
        protected override void calculateTextFields()
        {
            // TODO
        }

        protected string textToHardware = "";

        protected override void calculateTallyFields() { }

        protected override void sendTextsToHardware() => sendData();
        protected override void sendTalliesToHardware() => sendData();
        protected override void sendEverythingToHardware() => sendData();

        private void sendData()
        {
            if (port == null)
                return;
            byte[] bytesToSend = Encoding.ASCII.GetBytes(getCommandTextToSend());
            DateTime packetValidUntil = DateTime.Now + TimeSpan.FromSeconds(5);
            port.SendData(bytesToSend, packetValidUntil);
        }

        protected virtual string getCommandTextToSend()
        {
            string replaced = textToHardware.Replace('1', (char)0x7E);
            replaced = replaced.Replace("%", "%%");
            return string.Format("%{0}D{1}%Z", address, replaced);
        }
        #endregion

        #region Alignment, text calculation
        private static readonly int[] CHAR_WIDTHS = new int[] {
            4, 1, 3, 5, 5, 5, 5, 2, 3,
            3, 5, 5, 2, 3, 2, 5, 5, 5,
            5, 5, 5, 5, 5, 5, 5, 5, 2,
            2, 4, 5, 4, 5, 5, 5, 5, 5,
            5, 5, 5, 5, 5, 3, 5, 5, 5,
            5, 5, 5, 5, 5, 5, 5, 5, 5,
            5, 5, 5, 5, 5, 3, 5, 3, 5,
            5, 2, 5, 5, 4, 5, 5, 4, 5,
            5, 3, 4, 4, 3, 5, 5, 5, 5,
            5, 4, 5, 4, 5, 5, 5, 5, 5,
            5, 5, 1, 5
        };
        private const int CHAR_WIDTHS_START = 32;
        private const int CHAR_SEPARATOR_WIDTH = 1;

        private int getWidthOfText(string text)
        {
            int width = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char chr = text[i];
                width += CHAR_WIDTHS[chr - CHAR_WIDTHS_START];
                if (chr != ' ')
                    width += CHAR_SEPARATOR_WIDTH;
            }
            return width;
        }

        private static readonly int SPACE_WIDTH = CHAR_WIDTHS[' ' - CHAR_WIDTHS_START];
        private const char SEPARATOR_CHAR = '|';
        private static readonly int SEPARATOR_WIDTH = CHAR_WIDTHS[SEPARATOR_CHAR - CHAR_WIDTHS_START] + 2 * CHAR_SEPARATOR_WIDTH + 2 * SPACE_WIDTH;

        #endregion

    }

}
