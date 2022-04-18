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

        #region Instantiation, restoration, removation
        public McCurdyUMD1()
        {
            columnWidths[0] = TotalColumnWidth;
        }
        #endregion

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
        private int[] columnWidths = new int[] { 0, 0, 0 };

        public int[] ColumnWidths
        {
            get => columnWidths;
            set
            {
                columnWidths = value;
                UpdateTexts();
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

        public virtual int TotalColumnWidth => 160;
        #endregion

        #region Calculating and sending data to hardware
        protected override void calculateTextFields()
        {
            List<TextPiece> rawTextPieces = new();
            List<string> compactTextPieces = new();
            // Align texts
            if (UseFullStaticText)
            {
                alignTextForRaw(FullStaticText, AlignmentWithFullStaticText, TotalColumnWidth, rawTextPieces);
                compactTextPieces.Add(FullStaticText);
            }
            else
            {
                for (int i = 0; i < TextInfo.Length; i++)
                {
                    if (Texts[i].Used)
                    {
                        alignTextForRaw(Texts[i].CurrentValue, Texts[i].Alignment, columnWidths[i], rawTextPieces);
                        compactTextPieces.Add(Texts[i].CurrentValue);
                    }
                    else
                    {
                        rawTextPieces.Add(new(columnWidths[i]));
                    }
                }
            }
            // Join spaces
            List<TextPiece> rawTextPiecesSpacesJoined = new();
            int sumSpacesWidth = 0;
            foreach (TextPiece piece in rawTextPieces)
            {
                if (piece.text == null)
                {
                    sumSpacesWidth += piece.spacesWidth;
                }
                else
                {
                    if (sumSpacesWidth > 0)
                    {
                        rawTextPiecesSpacesJoined.Add(new(sumSpacesWidth));
                        sumSpacesWidth = 0;
                    }
                    rawTextPiecesSpacesJoined.Add(piece);
                }
            }
            if (sumSpacesWidth > 0)
                rawTextPiecesSpacesJoined.Add(new(sumSpacesWidth));
            // Play with spaces...
            int spareSpaces = 0;
            StringBuilder joinedTextBuilder = new();
            foreach (TextPiece piece in rawTextPiecesSpacesJoined)
            {
                if (piece.text == null)
                {
                    int spacesFloorDiff = piece.spacesWidth % SPACE_WIDTH;
                    if (spacesFloorDiff != 0) {
                        int spacesCeilDiff = SPACE_WIDTH - spacesFloorDiff;
                        if (spacesCeilDiff <= spareSpaces)
                        {
                            spareSpaces -= spacesCeilDiff;
                            piece.spacesWidth += spacesCeilDiff;
                        }
                        else
                        {
                            spareSpaces += spacesFloorDiff;
                            piece.spacesWidth -= spacesFloorDiff;
                        }
                    }
                    joinedTextBuilder.Append("".PadRight(piece.spacesWidth / SPACE_WIDTH));
                }
                else
                {
                    joinedTextBuilder.Append(piece.text);
                }
            }
            // Store
            string builtText = joinedTextBuilder.ToString();
            textToHardware = builtText.Replace('1', (char)0x7E).Replace("%", "%%");
            DisplayableRawText = builtText;
            DisplayableCompactText = string.Join(" | ", compactTextPieces);
        }

        private void alignTextForRaw(string text, UmdTextAlignment alignment, int columnWidth, List<TextPiece> rawPieces)
        {
            string trimmedText = trimTextToFit(text, columnWidth, out int trimmedWidth);
            int leftoverWidth = columnWidth - trimmedWidth;
            switch (alignment)
            {
                case UmdTextAlignment.Left:
                    rawPieces.Add(new(trimmedText, trimmedWidth));
                    rawPieces.Add(new(leftoverWidth));
                    break;
                case UmdTextAlignment.Center:
                    int leftSpacesWidth = leftoverWidth / 2;
                    rawPieces.Add(new(leftSpacesWidth));
                    rawPieces.Add(new(trimmedText, trimmedWidth));
                    rawPieces.Add(new(leftoverWidth - leftSpacesWidth));
                    break;
                case UmdTextAlignment.Right:
                    rawPieces.Add(new(leftoverWidth));
                    rawPieces.Add(new(trimmedText, trimmedWidth));
                    break;
            }
        }

        private class TextPiece
        {

            public string text;
            public int textWidth;
            public int spacesWidth;

            public TextPiece(string text, int textWidth)
            {
                this.text = text;
                this.textWidth = textWidth;
            }

            public TextPiece(int spacesWidth)
            {
                this.spacesWidth = spacesWidth;
            }

        }

        private static readonly int[] CHAR_WIDTHS = new int[] {
            4, 2, 4, 6, 6, 6, 6, 3,
            4, 4, 6, 6, 3, 4, 3, 6,
            6, 6, 6, 6, 6, 6, 6, 6, // "1" will be replaced with 0x7F in calculateTextFields(), so calculate with 6 columns width
            6, 6, 3, 3, 5, 6, 5, 6,
            6, 6, 6, 6, 6, 6, 6, 6,
            6, 4, 6, 6, 6, 6, 6, 6,
            6, 6, 6, 6, 6, 6, 6, 6,
            6, 6, 6, 4, 6, 4, 6, 6,
            3, 6, 6, 5, 6, 6, 5, 6,
            6, 4, 5, 5, 4, 6, 6, 6,
            6, 6, 5, 6, 5, 6, 6, 6,
            6, 6, 6, 6, 2, 6, 6, 6
        };
        private const int CHAR_WIDTHS_START = 0x20;

        private static readonly int SPACE_WIDTH = CHAR_WIDTHS[' ' - CHAR_WIDTHS_START];

        private string trimTextToFit(string original, int maxWidth, out int newWidth)
        {
            int trimToChr = 0;
            newWidth = 0;
            for (; trimToChr < original.Length; trimToChr++)
            {
                char chr = original[trimToChr];
                newWidth += CHAR_WIDTHS[chr - CHAR_WIDTHS_START];
                if (newWidth > maxWidth)
                    break;
            }
            return original.Substring(0, trimToChr);
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

        protected virtual string getCommandTextToSend() => string.Format("%{0}D{1}%Z", address, textToHardware);
        #endregion

    }

}
