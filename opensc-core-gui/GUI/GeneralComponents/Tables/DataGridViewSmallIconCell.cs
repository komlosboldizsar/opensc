using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class DataGridViewSmallIconCell: DataGridViewTextBoxCell
    {

        public bool IconShown { get; set; }

        public Color IconColor { get; set; }

        public IconTypes IconType { get; set; }

        public Padding IconPadding { get; set; }
        public override object Clone()
        {
            DataGridViewSmallIconCell cell = (DataGridViewSmallIconCell)base.Clone();
            cell.IconShown = this.IconShown;
            cell.IconColor = this.IconColor;
            cell.IconType = this.IconType;
            cell.IconPadding = this.IconPadding;
            return cell;
        }

        private const float CELL_OWN_PADDING = 5;

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            if (!IconShown)
                return;

            float canvasWidth = cellBounds.Width - 2 * CELL_OWN_PADDING - IconPadding.Left - IconPadding.Right;
            float canvasHeight = cellBounds.Height - 2 * CELL_OWN_PADDING - IconPadding.Top - IconPadding.Bottom;

            float drawSize = (canvasWidth > canvasHeight) ? canvasHeight : canvasWidth;

            float x = cellBounds.X + (cellBounds.Width / 2.0F) - (drawSize / 2.0F);
            float y = cellBounds.Y + (cellBounds.Height / 2.0F) - (drawSize / 2.0F);

            Brush coloredBrush = new SolidBrush(IconColor);
            switch (IconType)
            {
                case IconTypes.Circle:
                    graphics.FillEllipse(coloredBrush, x, y, drawSize, drawSize);
                    break;
                case IconTypes.Square:
                    graphics.FillRectangle(coloredBrush, x, y, drawSize, drawSize);
                    break;
            }

        }

        public enum IconTypes
        {
            Circle,
            Square
        }

    }
}
