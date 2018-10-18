using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.McCurdy
{
    class McCurdyUMD1 : UMD
    {

        public override IUMDType Type => new McCurdyUMD1Type();

        [PersistAs("port")]
        private McCurdyPort port;

        [TempForeignKey("umd_ports", nameof(port))]
        private int _portId;

        public McCurdyPort Port
        {
            get { return port; }
            set { port = value; }
        }

        [PersistAs("address")]
        private int address;

        public int Address
        {
            get { return address; }
            set
            {
                if (value <= 0 || value > 255)
                    throw new ArgumentOutOfRangeException();
                address = value;
            }
        }

        public override Color[] TallyColors
        {
            get { return new Color[] { Color.Red, Color.Green }; }
        }

        public override event UmdTallyChanging TallyChanging;
        public override event UmdTallyChanged TallyChanged;
        public override event ParameterlessChangeNotifierDelegate TallyChangingPCN;
        public override event ParameterlessChangeNotifierDelegate TallyChangedPCN;

        private bool[] tallies = new bool[McCurdyUMD1Type.TALLY_COUNT] { };

        public override bool[] TallyStates
        {
            get { return tallies; }
        }

        public override void SetTally(int index, bool state)
        {

            if (index >= McCurdyUMD1Type.TALLY_COUNT)
                throw new ArgumentOutOfRangeException();

            bool oldState = tallies[index];
            if (oldState != state)
            {
                TallyChanging?.Invoke(this, index, oldState, state);
                TallyChangingPCN?.Invoke();
                tallies[index] = state;
                update();
                TallyChanged?.Invoke(this, index, oldState, state);
                TallyChangedPCN?.Invoke();
            }
            
        }

        protected override void update()
        {
            if (port == null)
                return;
            var d = new Datagram()
            {
                Text = currentText,
                ValidUntil = DateTime.Now + TimeSpan.FromSeconds(5),
                Tallies = tallies
            };
            port.SendData(address, d);
        }

        [PersistAs("column_count")]
        private ColumnCount columnCount = ColumnCount.Three;

        public ColumnCount ColumnCount
        {
            get { return ColumnCount; }
            set
            {
                columnCount = value;
                update();
            }
        }

        [PersistAs("column_widths")]
        private int[] columnWidths = new int[] { 70, 40 };

        public int[] ColumnWidths {
            get { return columnWidths; }
            set
            {
                columnWidths = value;
                update();
            }
        }

        [PersistAs("text_alignment")]
        private TextAlignment[] textAlignment = new TextAlignment[] {
            McCurdy.TextAlignment.Left,
            McCurdy.TextAlignment.Center,
            McCurdy.TextAlignment.Right
        };

        public TextAlignment[] TextAlignment
        {
            get { return textAlignment; }
            set
            {
                textAlignment = value;
                update();
            }
        }

        [PersistAs("use_separators")]
        private bool useSeparators = true;

        public bool UseSeparators
        {
            get { return useSeparators; }
            set
            {
                useSeparators = value;
                update();
            }
        }

        private static readonly int[] CHAR_WIDTHS = new int[]{ 4, 1, 3, 5, 5, 5, 5, 2, 3, 3, 5, 5, 2, 3, 2, 5, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 4, 5, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 5, 3, 5, 5, 2, 5, 5, 4, 5, 5, 4, 5, 5, 3, 4, 4, 3, 5, 5, 5, 5, 5, 4, 5, 4, 5, 5, 5, 5, 5, 5, 5, 1, 5 };
        private const int CHAR_WIDTHS_START = 32;
        private const int CHAR_SEPARATOR_WIDTH = 1;

        private int getWidthOfText(string text)
        {
            int width = 0;
            for(int i = 0; i <text.Length; i++)
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
        private static readonly int SEPARATOR_WIDTH = CHAR_WIDTHS[SEPARATOR_CHAR - CHAR_WIDTHS_START] + CHAR_SEPARATOR_WIDTH + 2 * SPACE_WIDTH;

        private string alignAndTrimText(string text, int columns, TextAlignment alignment)
        {

            int freeColumns;
            while ((freeColumns = columns - getWidthOfText(text)) < 0)
                text = text.Remove(text.Length - 1);

            int spacesLeft, spacesRight;
            switch (alignment)
            {
                case McCurdy.TextAlignment.Left:
                    spacesRight = freeColumns / SPACE_WIDTH;
                    return string.Format("{0}{1}", text, new string(' ', spacesRight));
                case McCurdy.TextAlignment.Center:
                    spacesLeft = (freeColumns / 2) / SPACE_WIDTH;
                    if (spacesLeft < 0)
                        spacesLeft = 0;
                    spacesRight = (freeColumns - (spacesLeft * SPACE_WIDTH)) / SPACE_WIDTH;
                    if (spacesRight < 0)
                        spacesRight = 0;
                    return string.Format("{1}{0}{2}", text, new string(' ', spacesLeft), new string(' ', spacesRight));
                case McCurdy.TextAlignment.Right:
                    spacesLeft = freeColumns / SPACE_WIDTH;
                    return string.Format("{1}{0}", text, new string(' ', spacesLeft));
            }

            return text;

        }

        private const int DISPLAY_COLUMNS = 160;

        private string getFullText()
        {

            int[] width = new int[3];
            int count = 0;

            switch (columnCount)
            {
                case ColumnCount.One:
                    count = 1;
                    width[0] = DISPLAY_COLUMNS;
                    break;
                case ColumnCount.Two:
                    count = 2;
                    width[0] = columnWidths[0] - (useSeparators ? SEPARATOR_WIDTH : 0);
                    width[1] = DISPLAY_COLUMNS - columnWidths[0];
                    break;
                case ColumnCount.Three:
                    count = 3;
                    width[0] = columnWidths[0] - (useSeparators ? SEPARATOR_WIDTH : 0);
                    width[1] = DISPLAY_COLUMNS - columnWidths[0] - columnWidths[1];
                    width[2] = columnWidths[1] - (useSeparators ? SEPARATOR_WIDTH: 0);
                    break;
            }

            string fullText = "";
            for (int i = 0; i < count; i++)
            {
                fullText += alignAndTrimText("" /* TODO */, width[i], textAlignment[i]);
                if (useSeparators && (i < count - 1))
                    fullText += " " + SEPARATOR_CHAR + " ";
            }
            return fullText;

        }

    }
}
