using OpenSC.Model.Persistence;
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
        private int address = 1;

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

        protected override void tallyChanged(int index, bool state)
        {
            update();
        }

        protected override void update()
        {
            if (port == null)
                return;
            var d = new Datagram()
            {
                Text = getTextToSend(),
                ValidUntil = DateTime.Now + TimeSpan.FromSeconds(5),
                Tallies = TallyStates
            };
            port.SendData(address, d);
        }

        protected virtual string getTextToSend()
        {
            return currentText.Replace('1', (char)0x7E);
        }

        private void updateCurrentText()
        {
            CurrentText = getFullDynamicText();
        }

        [PersistAs("dynamic_text_sources")]
        private DynamicText[] dynamicTextSources = new DynamicText[] { null, null, null };

        [TempForeignKey("dynamictexts", nameof(dynamicTextSources))]
        private int[] _dynamicTextSources = new int[] { 0, 0, 0 };

        public void SetDynamicTextSource(int columnIndex, DynamicText dynamicTextSource)
        {

            if ((columnIndex < 0) || (columnIndex > 2))
                throw new ArgumentOutOfRangeException();

            if (dynamicTextSources[columnIndex] == dynamicTextSource)
                return;

            int subscribersLeft = 0;
            for (int i = 0; i < 3; i++)
                if ((dynamicTextSources[columnIndex] == dynamicTextSource) && (i != columnIndex))
                    subscribersLeft++;

            if ((dynamicTextSource != null) && (subscribersLeft == 0))
                dynamicTextSource.CurrentTextChanged -= dynamicTextChangedHandler;

            dynamicTextSources[columnIndex] = dynamicTextSource;

            if (dynamicTextSource != null)
            {
                if(subscribersLeft == 0)
                    dynamicTextSource.CurrentTextChanged += dynamicTextChangedHandler;
                dynamicTexts[columnIndex] = dynamicTextSource.CurrentText;
            }
            else
            {
                dynamicTexts[columnIndex] = "";
            }

            updateCurrentText();

        }

        public DynamicText GetDynamicTextSource(int columnIndex)
        {
            if ((columnIndex < 0) || (columnIndex > 2))
                throw new ArgumentOutOfRangeException();
            return dynamicTextSources[columnIndex];
        }

        private void dynamicTextChangedHandler(DynamicText text, string oldText, string newText)
        {
            updateDynamicTexts(text, newText);
        }

        private string[] dynamicTexts = new string[] { "", "", "" };

        private void updateDynamicTexts(DynamicText textSource, string newText)
        {
            for (int i = 0; i < 3; i++)
                if (dynamicTextSources[i] == textSource)
                    dynamicTexts[i] = newText;
            updateCurrentText();
        }

        [PersistAs("column_count")]
        private ColumnCount columnCount = ColumnCount.One;

        public ColumnCount ColumnCount
        {
            get { return columnCount; }
            set
            {
                columnCount = value;
                updateCurrentText();
            }
        }

        public virtual int TotalWidth
        {
            get => 160;
        }

        [PersistAs("column_widths")]
        private int[] columnWidths = new int[] { 40, 40 };

        public int[] ColumnWidths {
            get { return columnWidths; }
            set
            {
                columnWidths = value;
                updateCurrentText();
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
                updateCurrentText();
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

        private static readonly int[] CHAR_WIDTHS = new int[]{ 4, 1, 3, 5, 5, 5, 5, 2, 3, 3, 5, 5, 2, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 4, 5, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 5, 3, 5, 5, 2, 5, 5, 4, 5, 5, 4, 5, 5, 3, 4, 4, 3, 5, 5, 5, 5, 5, 4, 5, 4, 5, 5, 5, 5, 5, 5, 5, 1, 5 };
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
        private static readonly int SEPARATOR_WIDTH = CHAR_WIDTHS[SEPARATOR_CHAR - CHAR_WIDTHS_START] + 2 * CHAR_SEPARATOR_WIDTH + 2 * SPACE_WIDTH;

        private string alignAndTrimText(string text, int columns, TextAlignment alignment)
        {

            int freeColumns;
            while (((freeColumns = columns - getWidthOfText(text)) < 0) && (text.Length > 0))
                text = text.Remove(text.Length - 1);
            if (freeColumns < 0)
                freeColumns = 0;

            int spacesLeft, spacesRight;
            switch (alignment)
            {
                case McCurdy.TextAlignment.Left:
                    spacesRight = freeColumns / SPACE_WIDTH;
                    return string.Format("{0}{1}", text, new string(' ', spacesRight));
                case McCurdy.TextAlignment.Center:
                    spacesRight = (freeColumns / 2) / SPACE_WIDTH;
                    if (spacesRight < 0)
                        spacesRight = 0;
                    spacesLeft = (freeColumns - (spacesRight * SPACE_WIDTH)) / SPACE_WIDTH;
                    if (spacesLeft < 0)
                        spacesRight = 0;
                    return string.Format("{1}{0}{2}", text, new string(' ', spacesLeft), new string(' ', spacesRight));
                case McCurdy.TextAlignment.Right:
                    spacesLeft = freeColumns / SPACE_WIDTH;
                    return string.Format("{1}{0}", text, new string(' ', spacesLeft));
            }

            return text;

        }

        private string getFullDynamicText()
        {

            int[] width = new int[3];
            int[] order = new int[] { 0, 1, 2 };
            int count = 0;

            switch (columnCount)
            {
                case ColumnCount.One:
                    count = 1;
                    width[0] = TotalWidth;
                    break;
                case ColumnCount.Two:
                    count = 2;
                    width[0] = columnWidths[0];
                    width[1] = TotalWidth - columnWidths[0] - (useSeparators ? SEPARATOR_WIDTH : 0);
                    break;
                case ColumnCount.Three:
                    count = 3;
                    width[0] = columnWidths[0];
                    width[1] = columnWidths[1];
                    width[2] = TotalWidth - columnWidths[0] - columnWidths[1] - (useSeparators ? 2 * SEPARATOR_WIDTH : 0);
                    order = new int[] { 0, 2, 1 };
                    break;
            }

            int[] widthTmp = width;
            int totalRealWidth = 0;
            string[] textParts = new string[3];
            for (int i = 0; i < count; i++)
            {
                int ci = order[i];
                string textPart = alignAndTrimText(dynamicTexts[ci], widthTmp[ci], textAlignment[ci]);
                textParts[ci] = textPart;
                int realWidth = getWidthOfText(textPart);
                totalRealWidth += realWidth;
                if (i < count - 1)
                    widthTmp[count/2] += width[ci] - realWidth;
            }

            string fullText = "";
            for (int i = 0; i < count; i++)
            {
                fullText += textParts[i];
                if (useSeparators && (i < count - 1))
                    fullText += " " + SEPARATOR_CHAR + " ";
            }

            int totalRealWidthWithSeparators = getWidthOfText(fullText);
            int plusSpaces = (TotalWidth - totalRealWidthWithSeparators) / SPACE_WIDTH;

            switch (textAlignment[count / 2])
            {
                case McCurdy.TextAlignment.Left:
                    textParts[count / 2] += new string(' ', plusSpaces);
                    break;
                case McCurdy.TextAlignment.Center:
                    textParts[count / 2] =
                        new string(' ', (plusSpaces - (plusSpaces / 2)))
                        + textParts[count / 2] 
                        + new string(' ', (plusSpaces/2));
                    break;
                case McCurdy.TextAlignment.Right:
                    textParts[count / 2] =
                        new string(' ', plusSpaces)
                        + textParts[count / 2];
                    break;
            }

            fullText = "";
            for(int i = 0; i < count; i++)
            {
                fullText += textParts[i];
                if (useSeparators && (i < count - 1))
                    fullText += " " + SEPARATOR_CHAR + " ";
            }

            return fullText;

        }

        public override void Restored()
        {
            base.Restored();
            DynamicText[] restoredDynamicTextSources = dynamicTextSources;
            dynamicTextSources = new DynamicText[] { null, null, null };
            for (int i = 0; i < 3; i++)
                SetDynamicTextSource(i, restoredDynamicTextSources[i]);
        }

    }
}
