using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers;
using OpenSC.Model.UMDs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{
    internal class CustomDataGridViewUmdListTallyMonitorColumnDescriptor : CustomDataGridViewCustomColumnTypeDescriptor
    {

        public static readonly CustomDataGridViewUmdListTallyMonitorColumnDescriptor Default = new();

        public const int DEFAULT_BOX_PADDING = 4;
        public const int DEFAULT_TEXT_PADDING = 0;

        public int BoxPadding { get; init; } = DEFAULT_BOX_PADDING;
        public int TextPadding { get; init; } = DEFAULT_TEXT_PADDING;

        public override DataGridViewColumn CreateColumn() => new Column();
        public override DataGridViewCell CreateCell() => new Cell(BoxPadding, TextPadding);

        public override void Initialize(DataGridViewCell cell) => ((Cell)cell).Initialize();
        public override void AfterAddToRow(DataGridViewCell cell)
        {
            cell.ReadOnly = true;
        }

        public class Column : DataGridViewTextBoxColumn { }

        public class Cell : DataGridViewTextBoxCell
        {

            public Cell(int drawPadding, int textPadding)
            {
                this.boxPadding = drawPadding;
                this.textPadding = textPadding;
            }

            private Umd umd;

            public void Initialize()
            {
                Value = "";
                umd = (OwningRow as CustomDataGridViewRow<Umd>)?.Item;
                subscribe();
                update();
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (disposing)
                    unsubscribe();
            }

            private void subscribe()
            {
                if (umd == null)
                    return;
                foreach (UmdTally tally in umd.Tallies)
                    tally.CurrentStateChanged += tallyStateChangedHandler;
            }

            private void unsubscribe()
            {
                if (umd == null)
                    return;
                foreach (UmdTally tally in umd.Tallies)
                    tally.CurrentStateChanged -= tallyStateChangedHandler;
            }

            private void update() => DataGridView?.InvalidateCell(this);

            private void tallyStateChangedHandler(UmdTally item, bool oldValue, bool newValue) => update();

            protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                if ((paintParts & (DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground)) != 0)
                {
                    Rectangle borderWidths = BorderWidths(advancedBorderStyle);
                    int drawWH = cellBounds.Height - 2 * boxPadding - borderWidths.Height;
                    int textWH = drawWH - 2 * textPadding;
                    Rectangle boxRectangle = new Rectangle(cellBounds.X + boxPadding, cellBounds.Y + boxPadding, drawWH, drawWH);
                    Rectangle textRectangle = new Rectangle(cellBounds.X + boxPadding + textPadding, cellBounds.Y + boxPadding + textPadding, textWH, textWH);
                    int stepX = drawWH + boxPadding;
                    int cellContentRightX = cellBounds.X + cellBounds.Width - borderWidths.Width;
                    Font font = FontStore.getFontForDesiredHeight(cellStyle.Font.FontFamily, cellStyle.Font.Style, textWH);
                    Size ellipsisSize = TextRenderer.MeasureText(STR_ELLIPSIS, font);
                    int thisWithPaddingRightX = boxRectangle.X + stepX;
                    int nextWithPaddingRightX = thisWithPaddingRightX + stepX;
                    int ellipsisWithPaddingRightX = textRectangle.X + stepX + ellipsisSize.Width - textPadding;
                    int tallyCount = umd.Tallies.Count;
                    int tallyIndex = 0;
                    foreach (UmdTally tally in umd.Tallies)
                    {
                        bool last = (tallyIndex == (tallyCount - 1));
                        bool thisFits = (thisWithPaddingRightX < cellContentRightX);
                        bool nextFits = (nextWithPaddingRightX < cellContentRightX);
                        bool nextEllipsisFits = (ellipsisWithPaddingRightX < cellContentRightX);
                        if (!thisFits || (!last && !nextFits && !nextEllipsisFits))
                        {
                            graphics.DrawString(STR_ELLIPSIS, font, Brushes.Black, textRectangle.X - textPadding, textRectangle.Y);
                            break;
                        }
                        else
                        {
                            Brush fillBrush = SolidBrushStore.Get(tally.CurrentState ? tally.Color : UmdGuiConstants.TALLY_MONITOR_INACTIVE_BG);
                            Brush textBrush = tally.CurrentState ? tally.Color.GetTextBrushForBackground() : SolidBrushStore.Get(UmdGuiConstants.TALLY_MONITOR_INACTIVE_FG);
                            graphics.FillRectangle(fillBrush, boxRectangle);
                            graphics.DrawRectangle(Pens.LightGray, boxRectangle);
                            graphics.DrawString(tally.IndexAtOwner.ToString(), font, textBrush, textRectangle, TEXT_FORMAT);
                            boxRectangle.X += stepX;
                            textRectangle.X += stepX;
                            thisWithPaddingRightX += stepX;
                            nextWithPaddingRightX += stepX;
                            ellipsisWithPaddingRightX += stepX;
                        }
                        tallyIndex++;
                    }
                }
            }

            private int boxPadding;
            private int textPadding;

            private static readonly StringFormat TEXT_FORMAT = new StringFormat() { Alignment = StringAlignment.Center };
            private const string STR_ELLIPSIS = "...";

        }

    }
}
